using sorec_gamma.modules.ModuleCote_rapport.services;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModuleCote_rapport.controls
{
    class AlimentationCompteControle
    {
        public string alimenterCompte(string cin, string nom, string prenom, string montantDepot)
        {

            TLVHandler appTagsHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_ALIMENTATION_COMPTE);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_CIN, cin);
            //appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_NOM, nom);
           // appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_PRENOM, prenom);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_MONTANT_ALIMENTATION, montantDepot);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());

            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = AlimentationCompte.SendRequest(tlvData, macString);
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
