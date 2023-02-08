using sorec_gamma.modules.ModuleCote_rapport.services;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModuleCote_rapport.controls
{
    class CreationCompteControle
    {
        public string createCompte(string cin, string nom, string prenom, string NumTele, string dateNaissance)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_CREATION_COMPTE);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_CIN, cin);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_NOM, nom);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_PRENOM, prenom);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_TELEPHONE, NumTele);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_DATE_NAISSANCE, dateNaissance);
            //appTagsHandler.addASCII(TLVTags.SOREC_DATA_COMPTE_EMAIL, "");
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());

            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = CreationCompteService.SendRequest(tlvData, macString);
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
