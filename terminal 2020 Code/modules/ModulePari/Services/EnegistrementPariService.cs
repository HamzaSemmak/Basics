using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModulePari.Services
{
    class EnegistrementPariService
    {
        private static string _path = "terminal/EnregistrementPari";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
