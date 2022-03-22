//这是流程机器人（UiBot Worker）SDK的例子
//这个SDKSDK包括了同步和异步两套接口
//同步接口更简单，异步接口更强大
//下面这行如果注释掉，代表后续示例是使用同步版本的接口；如果没有注释掉，则使用异步版本的接口
//#define ASYNC_INTERFACE

using System;
using System.Collections.Generic;
using System.Windows.Forms;

//本项目已包含了对Laiye.RPA.ClientSDK的引用
//如果是其他项目，请在“引用”里面增加对Laiye.RPA.ClientSDK的引用
using Laiye.RPA;

namespace ClientSDK_example
{
    public partial class ExampleForm : Form
    {   
        //在使用SDK之前，需要创建一个Laiye.RPA.ClientSDK对象
        private ClientSDK m_ClientSDK = new ClientSDK();

        //这里指定流程机器人（UiBot Worker）的安装路径，可以为null。代表当前运行的路径
        private string m_WorkPath = null;

        public ExampleForm()
        {
            InitializeComponent();
        }

        //在用完SDK之后，建议调用Dispose接口，将其关闭
        private void ExampleForm_Closing(object sender, FormClosingEventArgs e)
        {
            m_ClientSDK.Dispose();
        }

#if ASYNC_INTERFACE
        //下面是使用异步接口的例子，如果您了解C#里面的async/await机制，可以使用异步接口。如果不了解，请使用下面的同步接口
        //在下面的代码中，注意ExampleForm_Load函数是异步的。也就是说，窗口会先显示出来，然后SDK再异步地往里面加载流程的列表
        private async void ExampleForm_Load(object sender, EventArgs e)
        {
            //第一步：指定流程机器人（UiBot Worker）的安装路径，并打开SDK。注意以Async结尾的方法是异步的
            //在这一步中，SDK会在后台启动流程机器人（UiBot Worker），并与其建立连接
            //这里可能会出现错误，当出现错误时，会抛出类型为ClientSDKException的异常。这个例子中没有异常处理，实际场景中建议进行异常处理
            await m_ClientSDK.OpenAsync(m_WorkPath);

            //第二步：获得流程机器人（UiBot Worker）中已有的流程列表。注意以Async结尾的方法是异步的
            List<FlowListItem> itemList = await m_ClientSDK.GetFlowListAsync();

            //把得到的流程列表在例子程序的界面里显示出来
            foreach (FlowListItem item in itemList)
                FlowList.Items.Add(item);
        }

#else
        //下面是使用同步接口的例子，如果您是初学者，不了解C#里面的async/await机制，建议使用同步接口
        //使用同步接口时，下面所有步骤都做完了以后，ExampleForm_Load函数才会返回，界面才会显示出来
        //所以在启动的时候，会稍微卡两三秒钟，然后界面才出现。当界面出现以后，里面已经加载好了所有的流程。
        private void ExampleForm_Load(object sender, EventArgs e)
        {
            //第一步：指定流程机器人（UiBot Worker）的安装路径，并打开SDK。
            //在这一步中，SDK会在后台启动流程机器人（UiBot Worker），并与其建立连接
            //这里可能会出现错误，当出现错误时，会抛出类型为ClientSDKException的异常。这个例子中没有异常处理，实际场景中建议进行异常处理
            m_ClientSDK.Open(m_WorkPath);

            //第二步：获得流程机器人（UiBot Worker）中已有的流程列表。
            List<FlowListItem> itemList = m_ClientSDK.GetFlowList();

            //把得到的流程列表在例子程序的界面里显示出来
            foreach (FlowListItem item in itemList)
                FlowList.Items.Add(item);
        }
#endif
        
        private void Execute_Click(object sender, EventArgs e)
        {
            //当点击了“运行”按钮的时候，执行这里，运行选中的流程。所以需要先判断是否选中了某个流程
            if(FlowList.SelectedItem == null)
            {
                MessageBox.Show("尚未选中要运行的流程");
                return;
            }
            //调用Execute函数来运行指定的流程
            //这个函数会马上返回，而不会等到流程运行完毕才返回
            //第二个参数是一个回调函数，在流程运行过程中，SDK会调用这个函数，通知运行的状态
            m_ClientSDK.Execute((FlowListItem)FlowList.SelectedItem, OnExecuteMessage);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            //当点击了“停止”按钮的时候，执行这里，停止正在运行的流程。所以需要先判断是否有流程在运行
            if(!m_ClientSDK.IsExecuting())
            {
                MessageBox.Show("当前没有流程在运行");
                return;
            }
            //调用Stop函数来停止指定的流程
            //这个函数会给流程发出一个“停止请求”，并马上返回
            //但流程未必会马上响应这个请求
            m_ClientSDK.Stop();
        }

        private void OnExecuteMessage(string message)
        {   
            //这是个回调函数。当流程在运行的时候，SDK会自动调用这个函数。并用参数message告知当前的运行状态
            
            //在调试的时候显示message（运行状态），可以省略这一行
            System.Diagnostics.Debug.WriteLine(message);

            //根据运行状态作出选择
            switch (message)
            {
                case "readyStart":      //这个状态表示SDK接到了“运行”的请求，准备运行
                case "readyStop":       //这个状态表示SDK接到了“停止”的请求，准备停止
                    break;
                case "running":
                    //这个状态表示流程已经开始运行了
                    //在这个例子中，我们会把“运行”按钮置为不可用，把“停止”按钮置为可用。您可以根据实际需求做其他操作
                    ExecuteButton.Enabled = false;
                    StopButton.Enabled = true;
                    break;
                case "completed":   //这个状态表示流程正常运行完成了
                case "failed":      //这个状态表示流程运行中发生了错误
                case "aborted":     //这个状态表示流程运行中，因为某些严重错误而放弃了
                case "timeout":     //这个状态表示流程运行中，因为超时而结束了
                case "stop":        //这个状态表示流程运行中，响应了用户发出的“停止请求”，并停止了
                    //上述状态都表示流程已经结束运行了
                    //在这个例子中，我们会把“运行”按钮置为可用，把“停止”按钮置为不可用。您可以根据实际需求做其他操作
                    ExecuteButton.Enabled = true;
                    StopButton.Enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
