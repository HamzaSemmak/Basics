using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModuleBackOffice.Service
{
    class DemandeVoucherService
    {
        private static string _path = "terminal/DemandeVoucher";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
