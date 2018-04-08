using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCode.localhost;

namespace TestCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cn.com.webxml.www.WeatherWebService ws = new cn.com.webxml.www.WeatherWebService();
            //string[] r = ws.getWeatherbyCityName(this.textBox1.Text);
            //this.richTextBox1.Text = "";
            //if (r == null)
            //{
            //    this.richTextBox1.Text = "无" + this.textBox1.Text + "城市的天气信息";
            //    return;
            //}
            //foreach (string i in r)
            //{
            //    this.richTextBox1.Text += i;
            //}
            WebService1 a =new WebService1();
            int m = a.Multi(textBox1.Text, textBox2.Text);
            richTextBox1.Text = m.ToString();
        }
    }
}
