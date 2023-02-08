using System;
using System.Net;

namespace sorec_gamma.modules.UTILS
{
    public class HttpClient : WebClient
    {
        private static object _lock = new object();
        private static HttpClient Instance;
       
        public static HttpClient GetInstance()
        {
            lock (_lock)
            {
                if (Instance == null)
                    Instance = new HttpClient();
                return Instance;
            }
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest wr = base.GetWebRequest(uri);
            wr.Timeout = 30 * 1000;
            return wr;
        }
    }
}
