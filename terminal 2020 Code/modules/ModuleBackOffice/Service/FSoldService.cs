using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModuleBackOffice.Service
{
    class FSoldService
    {
        private static string _path = "terminal/DemandeFsold";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
