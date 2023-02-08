using System;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;

namespace sorec_gamma.modules.ModuleAuthentification
{
    class AuthentificationControl
    {
        public static string Login(string login, string mdp)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_AUTHENTICATION);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_LOGIN, login);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_PASSWORD, mdp);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, "1");
            appTagsHandler.addASCII(TLVTags.SOREC_CODE_PANNE_LIST, "1,2,3");
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_ATTRIBUTAIRE, "0");
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_LOGICIEL, ApplicationContext.SOREC_DATA_VERSION_LOG);
            appTagsHandler.addASCII(TLVTags.SOREC_TYPE_TERMINAL, "T2020");
            TLVHandler pari_annul_paiem_handler = TlvUtlis.Annul_paiement_sys_controle();
            if (pari_annul_paiem_handler != null)
            {
                appTagsHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME, pari_annul_paiem_handler);
            }
            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String result = "";
            try {
               result = AuthentificationService.SendRequest(tlvData, macString);
            } catch(Exception e){
                ApplicationContext.Logger.Error("Authentification Exception " + e.Message);
            }
            return result;
        }
        
        public static void responseAuthentificationControle(string TLV)
        {
            ApplicationContext.SOREC_DATA_ATTRIBUTAIRE = new Attributaire();
            TLVHandler tLVHandler = new TLVHandler(TLV);
            TLVTags communicationsTag = tLVHandler.getTLV(TLVTags.SOREC_DATA_COMMUNICATIONS);
            TLVTags responseTag = tLVHandler.getTLV(TLVTags.SOREC_DATA_RESPONSE);
            TLVHandler responseTagHandler = new TLVHandler(Utils.bytesToHex(responseTag.value));
            TLVTags attributaireTag = responseTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE);
            TLVTags NumPdvTag = responseTagHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
            TLVTags PosTeminalTag = responseTagHandler.getTLV(TLVTags.SOREC_DATA_TERMINAL_POSITION);
            TLVTags authJeuTag = responseTagHandler.getTLV(TLVTags.SOREC_AUTORISATION_TERMINAL_JEU);
            TLVTags authGainTag = responseTagHandler.getTLV(TLVTags.SOREC_AUTORISATION_TERMINAL_GAIN);
            TlvUtlis.AttributaireControle(attributaireTag);
            TlvUtlis.SetCorpsCommunication(communicationsTag);
            TlvUtlis.NumPosPdvControle(NumPdvTag,PosTeminalTag);

            if (authJeuTag != null)
            {
                ApplicationContext.IsAutJeu = Utils.bytesToHex(authJeuTag.value) == "01";
            }
            if (authGainTag != null)
            {
                ApplicationContext.IsAutGain = Utils.bytesToHex(authJeuTag.value) == "01";
            }
        }
    }                                                                             
}
