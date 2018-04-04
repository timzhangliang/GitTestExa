using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "求和方法")]
        public int Add(string a, string b)
        {
            int sum = 0;
            sum = Convert.ToInt32(a) + Convert.ToInt32(b);
            return sum;
        }

        public int Multi(string a, string b)
        {
            int sum = 0;
            sum = Convert.ToInt32(a)*Convert.ToInt32(b);
            return sum;
        }
    }
}
