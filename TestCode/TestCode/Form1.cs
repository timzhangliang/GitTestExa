using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBUtility;

namespace TestCode
{
    public partial class Form1 : Form
    {
        private SocketCollectHelper g_Socket_server_Page;
        public Form1()
        {
            InitializeComponent();
            dd += fun;
            g_Socket_server_Page = new SocketCollectHelper("192.168.40.169", 10011, SocketCollectHelper.SocketStyle.server_page);
            g_Socket_server_Page.f_Callback += new CallBackHelper.SocketReciveCallBack(SocketPageBack_OnEvent);
        }

        public delegate void dele(string str);

        public dele dd;
        public void fun(string str)
        {
            textBox1.Text = str;
        }

        private void SocketPageBack_OnEvent(string data) //回调委托事件
        {

            textBox1.Invoke(dd,data);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            SocketCollectHelper g_Socket_client_Page = new SocketCollectHelper();
            g_Socket_client_Page.ClientSend("192.168.40.169", 10023, textBox1.Text);

        }
    }
}
