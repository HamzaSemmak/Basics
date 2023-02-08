using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModuleCote_rapport.services
{
    class CreationCompteService
    {
        private static string _path = "compte/CreationCompte";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
