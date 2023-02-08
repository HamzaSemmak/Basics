using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.ModulePari.Services;
using sorec_gamma.modules.TLV;
using System;

namespace sorec_gamma.modules.ModulePari.Controls
{
    class GagnantContole
    {
        public string PayerGrosGain(Gagnant gagnant, string TypePaiement,bool isManuel)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler SorecDataGagnatTlvHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_INFORMATION_GAGNANT);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_IDENTIFIANTCONC, gagnant.IdConc);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_CIN, gagnant.CIN);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_NOM, gagnant.Nom);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_PRENOM, gagnant.Prenom);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_TELEPHONE, gagnant.NumTele);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_COMMENTAIRE, gagnant.Commentaires);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_DATEREUNION, gagnant.DateReunion.ToString("yyyy-MM-dd"));
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_IDSERVEUR, gagnant.IdServeur);
            SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_GAGNANT_TYPEPAIEMENT, TypePaiement);
            if(isManuel)
                SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_MODE_PAIEMENT, "MANUEL");
            else
                SorecDataGagnatTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_MODE_PAIEMENT, "AUTOMATIQUE");
            appTagsHandler.add(TLVTags.SOREC_DATA_GAGNANT, SorecDataGagnatTlvHandler);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());

            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = GagnatService.SendRequest(tlvData, macString);
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
