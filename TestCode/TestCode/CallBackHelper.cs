//add by fuqp 2017-07-21 委托回调定义类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//using Model;

namespace DBUtility
{
    public class CallBackHelper
    {
        public const int WM_User = 0x0400;//用户自定义消息的开始数值
        public const int WM_PageEdit = WM_User + 1;

        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        //public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        /// <summary>
        /// socket接收回调定义
        /// </summary>
        /// <param name="data"></param>
        public delegate void SocketReciveCallBack(string data);

        /// <summary>
        /// 采集线程日志回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      //  public delegate void ViewEventHandlerEqmData(object sender, LogRecordEqmData e, MBF_Plc plc);
       // public delegate void SetDataSourceCallBackEqmData(LogRecordEqmData e, MBF_Plc plc);
    }

    public class LogRecord
    {
        public DateTime F_CollectTime;
        public int? F_CollectTimes;
        public int? F_ReadTimes;
        public int? F_ErrorTimes;
    }
    public class LogRecordEqmData: LogRecord
    {
        public int? F_CollectByself { get; set; }
        public int? F_CollectOnline { get; set; }
        public string F_CollectWaring { get; set; }
        public string F_CollectLog { get; set; }
    }

    public class CboItemEntity
    {
        private object _text = 0;
        private object _Value = "";
        /// <summary>
        /// 显示值
        /// </summary>
        public object Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        /// <summary>
        /// 对象值
        /// </summary>
        public object Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        public override string ToString()
        {
            return this.Text.ToString();
        }
    }
}
