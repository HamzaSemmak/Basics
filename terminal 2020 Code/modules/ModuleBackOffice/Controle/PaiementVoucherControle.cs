using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleBackOffice.Service;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using System;

namespace sorec_gamma.modules.ModuleBackOffice.Controle
{
    class PaiementVoucherControle
    {
        public string PayerVoucher( Voucher voucher, bool isManuel = true, bool isMachine = false)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler SorecDataVoucherTlvHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_PAIEMENT_VOUCHER);
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_IDENTIFIANTCONC, voucher.IdVoucher);
            if (voucher.DateSession != null)
            {
                SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_DATESESSION, voucher.DateSession.ToString("yyyy-MM-dd"));
            }
            else
            {
                ApplicationContext.Logger.Info("PaiementVoucherControle: PayerVoucher => : DateSession is null");
            }
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_IDSERVEUR, voucher.IdServeur);
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_TYPEPAIEMENT, isMachine ? "MACHINE" : "CLIENT");
            if (isManuel)
                SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_MODEPAIEMENT, "MANUEL");
            else
                SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_MODEPAIEMENT, "AUTOMATIQUE");
            SorecDataVoucherTlvHandler.addASCII(TLVTags.SOREC_DATA_VOUCHER_CVNV, voucher.CVNT);

            appTagsHandler.add(TLVTags.SOREC_DATA_VOUCHER, SorecDataVoucherTlvHandler);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());
            TLVHandler pari_annul_paiem_handler = isMachine ? null : TlvUtlis.Annul_paiement_sys_controle();
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
                data = PaiementVoucherService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("Exception : " + e.StackTrace);
            }

            String myTlv = TLVHandler.getTLVChamps(data);
            
            return myTlv;
        }
    }
}
