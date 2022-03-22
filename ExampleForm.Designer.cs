namespace ClientSDK_example
{
    partial class ExampleForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.FlowList = new System.Windows.Forms.ListBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(165, 338);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(143, 47);
            this.ExecuteButton.TabIndex = 0;
            this.ExecuteButton.Text = "运行";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.Execute_Click);
            // 
            // FlowList
            // 
            this.FlowList.FormattingEnabled = true;
            this.FlowList.ItemHeight = 14;
            this.FlowList.Location = new System.Drawing.Point(14, 14);
            this.FlowList.Name = "FlowList";
            this.FlowList.Size = new System.Drawing.Size(652, 298);
            this.FlowList.TabIndex = 1;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(384, 338);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(143, 47);
            this.StopButton.TabIndex = 0;
            this.StopButton.Text = "停止";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.Stop_Click);
            // 
            // ExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 413);
            this.Controls.Add(this.FlowList);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.ExecuteButton);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ExampleForm";
            this.Text = "示例程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExampleForm_Closing);
            this.Load += new System.EventHandler(this.ExampleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.ListBox FlowList;
        private System.Windows.Forms.Button StopButton;
    }
}

