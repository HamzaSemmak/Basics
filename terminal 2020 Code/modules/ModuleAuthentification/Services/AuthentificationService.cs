using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.TLV
{
    class AuthentificationService
    {
        private static string _path = "terminal/Authentication";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
