using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModuleCote_rapport.services
{
    class VerificationCompte
    {
        private static string _path = "compte/VerificationCompte";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
