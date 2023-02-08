using sorec_gamma.modules.ModuleMAJ.Service;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModuleMAJ.Controls
{
    class MAJControle
    {
        public string sendRequest(string statutMAJ)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler tLVdataTerminalHandler = new TLVHandler();
            TLVHandler tLVMAJContentHandler = new TLVHandler();

            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_MAJ_LOGICIEL);
            tLVdataTerminalHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.add(TLVTags.SOREC_DATA_TERMINAL, tLVdataTerminalHandler);

            string offreVersion = ApplicationContext.SOREC_DATA_OFFRE != null && ApplicationContext.SOREC_DATA_OFFRE.NumVersion != 0 ?
                ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString() : "1";

            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, offreVersion);

            tLVMAJContentHandler.addASCII(TLVTags.SOREC_MAJ_LOGICIELLE_STATUT, statutMAJ);
            appTagsHandler.add(TLVTags.SOREC_MAJ_LOGICIELLE, tLVMAJContentHandler);

            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = MAJService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("MAJControle Exception : " + e.Message);
            }

            String myTlv = TLVHandler.getTLVChamps(data);

            return myTlv;
        }
    }
}
