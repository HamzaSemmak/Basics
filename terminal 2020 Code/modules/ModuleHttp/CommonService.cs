using log4net;
using sorec_gamma.modules.Config;
using System;
using System.Net;
using System.Text;

namespace sorec_gamma.modules.ModuleHttp
{
    public class CommonService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommonService));
        public static string SendRequest(string path, string data, string mac)
        {
            string resultContent = "";
            try
            {
                using (var client = new WebClient())
                {
                    string uri = ConfigUtils.ConfigData.GetAddress() + "/sorecws/api/" + path;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";

                    string dataJson = "{\"tlvData\" : \"" + data + "\", \"mac\" : \"" + mac + "\"}";
                    var dataJsonBytes = Encoding.UTF8.GetBytes(dataJson);
                    Logger.Info(string.Format("Server Request {0}: {1}", uri, dataJson));

                    byte[] result = client.UploadData(uri, "POST", dataJsonBytes);
                    resultContent = Encoding.UTF8.GetString(result, 0, result.Length);
                    
                    Logger.Info(string.Format("Server Response {0}: {1}", uri, resultContent));
                    return resultContent;
                }

                /*using (var client = new HttpClient())
                {
                    string uri = ConfigUtils.ConfigData.GetAddress() + "/sorecws/api/" + path;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"tlvData\" : \"");
                    sb.Append(data);
                    sb.Append("\", \"mac\" : \"");
                    sb.Append(mac);
                    sb.Append("\"}");

                    ApplicationContext.Logger.Info(string.Format("Server Request {0}: {1}", uri, sb.ToString()));
                    resultContent = client.UploadString(uri, sb.ToString());
                    ApplicationContext.Logger.Info(string.Format("Server Response {0}: {1}", uri, resultContent));
                    return resultContent;
                }*/
            }
            catch (Exception ex)
            {
                Logger.Error("Exception: " + ex.Message + ex.StackTrace);
                return resultContent;
            }
        }
    }
}
