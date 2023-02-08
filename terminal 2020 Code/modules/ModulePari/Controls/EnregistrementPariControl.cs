using System;
using sorec_gamma.modules.ModulePari.Services;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;

namespace sorec_gamma.modules.ModulePari.Controls
{
    class EnregistrementPariControl
    {
        public string EnregistrerPari(Ticket ticket)
        {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler pariDetailsTlvHandler = new TLVHandler();
            TLVHandler formulationsTlvHandler = new TLVHandler();

            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_ENREGISTREMENT_PARI);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString() );
            pariDetailsTlvHandler.add(TLVTags.SOREC_DATA_PARI_IDCANAL, Int32.Parse("123"));
            pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_TOTAL_TICKET, ticket.PrixTotalTicket.ToString());
            pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_DATEREUNION, ticket.DateReunion.ToString("yyyy-MM-dd"));
            pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_NUMREUNION, ticket.NumReunion.ToString());
            pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_NUMCOURSE, ticket.NumCourse.ToString());
            
            int num_Form = 0;
            foreach (Formulation f in ticket.ListeFormulation) {
                  num_Form++;
                  TLVHandler formulationDetailsTlvHandler = new TLVHandler();
                  formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_CODEPRODUIT, f.Produit.CodeProduit);
                  f.Designation = f.Designation.Replace("R","SE");
                  f.Designation = f.Designation.Replace("TOTAL", "");
                  formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_DESIGNATION, f.Designation);
                  f.Designation = f.Designation.Replace("SE", "R");
                 if (f.FormComplete)
                 {
                    formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_FORMCOMPLETE, "true");
                }
                else 
                {
                    formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_FORMCOMPLETE, "false");

                }
                
                bool checkChampTotal = f.Designation.Contains("R");
                if (checkChampTotal)
                {
                    formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_CHAMP_X, "false");
                }
                else
                {
                    formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_CHAMP_X, "true");
                }
                
                formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_MISECOMB, f.MiseCombinaison.ToString());
                formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_MISETOTALE, f.MiseTotal.ToString());
                formulationDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_FORMULATION_NUMFORM, num_Form.ToString());
                formulationsTlvHandler.add(TLVTags.SOREC_DATA_PARI_FORMULATION, formulationDetailsTlvHandler);   
             }
             pariDetailsTlvHandler.add(TLVTags.SOREC_DATA_PARI_FORMULATIONS, formulationsTlvHandler);
             pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_NUMTICKETANNUL, ticket.NumCourse.ToString());
             pariDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_PARI_TYPECANAL, "TERMINAL");
             pariDetailsTlvHandler.add(TLVTags.SOREC_DATA_PARI_NUMBER_PARTANT_T, ticket.NumberPartans); // get nombre partants
             appTagsHandler.add(TLVTags.SOREC_DATA_PARI, pariDetailsTlvHandler);
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
             try
             {
                 result = EnegistrementPariService.SendRequest(tlvData, macString);
             }
             catch (Exception e) {
                 ApplicationContext.Logger.Error("Exception Enregistrement Pari send request : " + e.Message);
             }
             return result;
        }
    }
}
