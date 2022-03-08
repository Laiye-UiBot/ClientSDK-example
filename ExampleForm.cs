using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laiye.RPA;

namespace ClientSDK_example
{
    public partial class ExampleForm : Form
    {
        private ClientSDK m_ClientSDK = new ClientSDK();

        public ExampleForm()
        {
            InitializeComponent();
        }

        private void ExampleForm_Load(object sender, EventArgs e)
        {
            m_ClientSDK.Open("exampleform");
        }
    }
}
