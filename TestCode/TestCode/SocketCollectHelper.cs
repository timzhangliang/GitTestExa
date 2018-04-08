//add by fuqp 2017-07-21 Socket监听发送类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace DBUtility
{
    public class SocketCollectHelper
    {
        public enum SocketStyle    //定义socket类型
        {
            server_page = 1,
            client_page = 2
        }
        private Socket f_socket;
        private SocketStyle f_style;
        public CallBackHelper.SocketReciveCallBack f_Callback;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public SocketCollectHelper(string ip, int port, SocketStyle type)
        {
            f_style = type;
            IPAddress ipAddress = IPAddress.Parse(ip);
            f_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            f_socket.Bind(ipEndPoint);  //绑定IP地址：端口  
            f_socket.Listen(10);    //设定最多10个排队连接请求  
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.IsBackground = true;
            myThread.Start();
        }
        public SocketCollectHelper()
        {
        }

        private bool isDisposed = false;
        private bool threaddown = false;
        ~SocketCollectHelper()
        {
            FreeSocket(false);
        }
        public void FreeSocket()
        {
            FreeSocket(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void FreeSocket(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    //释放托管资源   
                }
                //关闭Socket之前，首选需要把双方的Socket Shutdown掉
                if (f_socket != null)
                {
                    if (f_socket.Connected)
                    {
                        f_socket.Shutdown(SocketShutdown.Both);
                        //Shutdown掉Socket后主线程停止10ms，保证Socket的Shutdown完成
                        System.Threading.Thread.Sleep(10);
                        //关闭客户端Socket,清理资源                          
                    }
                    threaddown = true;
                    System.Threading.Thread.Sleep(100);
                    f_socket.Close();
                    f_socket.Dispose();
                }
            }
            isDisposed = true;
        }
        private void ListenClientConnect()
        {
            while (true)
            {
                try
                {
                    if (threaddown) return;
                    Socket clientSocket = f_socket.Accept();
                    Thread receiveThread = new Thread(ReceiveMessage);
                    receiveThread.IsBackground = true;
                    receiveThread.Start(clientSocket);
                }
                catch { }
            }
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                if (threaddown) return;
                try
                {
                    //通过clientSocket接收数据 
                    byte[] result = new byte[1024];
                    int receiveNumber = myClientSocket.Receive(result);
                    if (f_style == SocketStyle.server_page)
                        f_Callback(Encoding.ASCII.GetString(result, 0, receiveNumber));
                }
                catch
                {
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ClientSend(string ip, int port, string data)
        {
            if (!PingHelper.PingFun(ip)) return false;
            IPAddress ipAddress = IPAddress.Parse(ip);
            try
            {
                FreeSocket();
                f_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                f_socket.SendTimeout = 2000;
                f_socket.Connect(new IPEndPoint(ipAddress, port)); //配置服务器IP与端口  
            }
            catch
            {
                return false;
            }
            Thread.Sleep(200);
            int result = f_socket.Send(Encoding.UTF8.GetBytes(data));
            return result >= 1;
        }
    }
}
