using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MOLPayTransactionAPI.Helper
{
   public class APIHelper
    {
        public static string Get(string url, string Xtoken = "")
        {
            string msg = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 30000;
            if (Xtoken != "")
                httpWebRequest.Headers.Add("X-access-token", Xtoken);
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    msg = result;
                }
            }
            catch (WebException wex)
            {
                msg = wex.ToString();
            }
            return msg;


        }
    }
}
