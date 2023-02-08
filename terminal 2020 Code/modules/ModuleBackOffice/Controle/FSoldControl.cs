using sorec_gamma.modules.ModuleBackOffice.Service;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModuleBackOffice.Controle
{
    class FSoldControl
    {
        public string requestFSold(string date)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler dataFsoldHandler = new TLVHandler();
            dataFsoldHandler.addASCII(TLVTags.SOREC_DATA_FSOLD_DATE,date);
            dataFsoldHandler.addASCII(TLVTags.SOREC_DATA_FSOLD_NUMERO_TICKETANNUL, "15");

            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_DEMANDE_FSOLD);
            appTagsHandler.add(TLVTags.SOREC_DATA_FSOLD, dataFsoldHandler);
            //appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP,ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);

            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE,ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());
            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);

            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = FSoldService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("Exception : " + e.StackTrace);
            }
            return data;
        }
       
    }
}
