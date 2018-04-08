//add by fuqp 2017-07-21 ping通ip类  测试ip地址是否连通
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace DBUtility
{
    public class PingHelper
    {
        /// <summary>
        /// 测试ping通 
        /// </summary>
        /// <param name="ip"></param>ip地址 
        /// <param name="num"></param>ping次数
        /// <returns></returns>
        public static bool PingFun(string ip, Int32 num = 4)
        {
            for (int i = 0; i < num; i++)   //ping4次 连续ping不通就不通了
            {
                if (PingConnect(ip))
                    return true;
            }
            return false;
        }

        public static bool PingConnect(string ip)
        {
            try
            {
                if (ip == "") return false;
                if (ip == "127.0.0.1") return true;
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "pingip";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 200;
                //如果网络连接成功，PING就应该有返回；否则，网络连接有问题
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool PingConnectString(string constr)
        {
            try
            {
                SqlConnectionStringBuilder con = new SqlConnectionStringBuilder(constr);
                string ip = con.DataSource;
                if (ip == "") return false;
                if (ip == "127.0.0.1" || ip.ToLower() == "localhost") return true;
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "pingip";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 200;
                //测试网络连接：目标计算机为192.168.1.1(可以换成你所需要的目标地址）
                //如果网络连接成功，PING就应该有返回；否则，网络连接有问题
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
        public static string GetLocalIP()
        {
            NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            int len = interfaces.Length;

            for (int i = 0; i < len; i++)
            {
                NetworkInterface ni = interfaces[i];
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (ni.Name == "本地连接")
                    {
                        IPInterfaceProperties property = ni.GetIPProperties();
                        foreach (UnicastIPAddressInformation ip in
                            property.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ip.Address.ToString().Contains("192.168.40"))
                            {
                                string localIP = ip.Address.ToString();
                                return localIP;
                            }
                        }
                    }
                }

            }
            return "";
        }
    }
}
