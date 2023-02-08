using sorec_gamma.modules.ModuleHttp;

/**
 * Created by yelkarkouri
 */
namespace sorec_gamma.modules.ModuleCote_rapport.services
{
    class RapportsServices
    {
        private static string _path = "terminal/DemandeRapport";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
