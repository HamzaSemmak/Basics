using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleBackOffice.Service;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModuleBackOffice.Controle
{
    class SoldePrePayeControle
    {
        public string requestSoldePrepaye()
        {

            TLVHandler appTagsHandler = new TLVHandler();

            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_SOLDE_PREPAYE);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());


            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = SoldePrePayeService.SendRequest(tlvData, macString);
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
