using sorec_gamma.modules.ModuleBackOffice.Service;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using System;

namespace sorec_gamma.modules.ModuleBackOffice.Controle
{
    class DemandeVoucherControle
    {
        public string RequestVoucher(decimal montant)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler SorecDataVoucherTlvHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_DEMANDE_VOUCHER);
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_IDENTIFIANTCAN, "123");
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_MONTANT, montant.ToString().Replace(",","."));
            appTagsHandler.add(TLVTags.SOREC_DATA_VOUCHER,SorecDataVoucherTlvHandler);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());
            TLVHandler pari_annul_paiem_handler = TlvUtlis.Annul_paiement_sys_controle();
            if (pari_annul_paiem_handler != null)
            {
                appTagsHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME, pari_annul_paiem_handler);

            }
            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = DemandeVoucherService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("Exception : " + e.StackTrace);
            }

            return data;
        }
    }
}
