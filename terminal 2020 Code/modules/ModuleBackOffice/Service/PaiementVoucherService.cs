using sorec_gamma.modules.ModuleHttp;

namespace sorec_gamma.modules.ModuleBackOffice.Service
{
    class PaiementVoucherService
    {
        private static string _path = "terminal/PaiementVoucher";

        public static string SendRequest(string data, string mac)
        {
            return CommonService.SendRequest(_path, data, mac);
        }
    }
}
