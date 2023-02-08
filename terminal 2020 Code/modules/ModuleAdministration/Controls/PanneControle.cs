using sorec_gamma.modules.ModuleAdministration.Service;
using sorec_gamma.modules.TLV;
using System;
using System.Threading;

namespace sorec_gamma.modules.ModuleAdministration.Controls
{
    public class PanneControle
    {
        public static void sendRequest(Panne panne)
        {
            Thread panneReqThread = new Thread(() =>
            {
                TLVHandler appTagsHandler = new TLVHandler();
                TLVHandler tLVdataTerminalHandler = new TLVHandler();
                TLVHandler tLVPanneContentHandler = new TLVHandler();
                TLVHandler tLVPanneHandler = new TLVHandler();

                appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_RECEPTION_PANNES);
                tLVdataTerminalHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
                appTagsHandler.add(TLVTags.SOREC_DATA_TERMINAL, tLVdataTerminalHandler);
                appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_VERSION_LOG);
                tLVPanneContentHandler.addASCII(TLVTags.SOREC_CODE_PANNE_CODE, panne.Code);
                tLVPanneContentHandler.addASCII(TLVTags.SOREC_CODE_PANNE_TYPE, panne.Category.ToString());
                tLVPanneContentHandler.addASCII(TLVTags.SOREC_CODE_PANNE_DESCRIPTION, panne.Desc);
                tLVPanneHandler.add(TLVTags.SOREC_CODE_PANNE, tLVPanneContentHandler);
                appTagsHandler.add(TLVTags.SOREC_CODE_PANNE_LIST, tLVPanneHandler);

                string tlvData = appTagsHandler.toString();
                string tlvDataHash256 = Utils.getSha256(tlvData);
                byte[] macBytes = Utils.macSign(tlvDataHash256);
                string macString = Utils.bytesToHex(macBytes);
                string data = "";
                try
                {
                    data = PanneService.SendRequest(tlvData, macString);
                }
                catch (Exception e)
                {
                    ApplicationContext.Logger.Error("PanneControle Exception : " + e.Message + e.StackTrace);
                }

            });
            panneReqThread.Priority = ThreadPriority.Lowest;
            panneReqThread.Name = "PANNE_REQUEST";
            panneReqThread.Start();
        }
    }
}
