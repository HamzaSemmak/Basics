using sorec_gamma.modules.ModuleBackOffice.Service;
using sorec_gamma.modules.TLV;
using System;
using System.Globalization;

namespace sorec_gamma.modules.ModuleBackOffice.Controle
{
    class EtatDeCaisseControle
    {
        public string dateCaisse = "";
        public string typeCaisse = "";
        public int caisse_Paris_Nombre = 0;
        public decimal caisse_Paris_Cummul = 0;
        public int caisse_Annulation_Nombre = 0;
        public decimal caisse_Annulation_Cummul = 0;
        public int caisse_Paiement_Nombre = 0;
        public decimal caisse_Paiement_Cummul = 0;
        public int caisse_Voucher_Nombre = 0;
        public decimal caisse_Voucher_Cummul = 0;
        public int caisse_depot_Nombre = 0;
        public decimal caisse_depot_Cummul = 0;
        public int caisse_AnnulSys_Nombre = 0;
        public decimal caisse_AnnulSys_Cummul = 0;
        public int caisse_AnnulMachine_Nombre = 0;
        public decimal caisse_AnnulMachine_Cummul = 0;         
        public int caisse_paiements_Nombre = 0;
        public decimal caisse_paiements_Cummul = 0;
        public int nombre_guichet = 0;
        
        public string requestEtatCaisse(string typeEtat,string date)
        {

            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler EtatDeCaiiseHandler = new TLVHandler();

            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_ETAT_CAISSE);
            EtatDeCaiiseHandler.addASCII(TLVTags.SOREC_DATA_ETAT_CAISSE_DATE, date);
            EtatDeCaiiseHandler.addASCII(TLVTags.SOREC_DATA_ETAT_CAISSE_TYPE, typeEtat);
            appTagsHandler.add(TLVTags.SOREC_DATA_ETAT_CAISSE, EtatDeCaiiseHandler);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, ApplicationContext.SOREC_DATA_OFFRE.NumVersion.ToString());


            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String data = "";
            try
            {
                data = EtatDeCaisseService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("EtatDeCaisseControle Exception : " + e.Message);
            }

            String myTlv = TLVHandler.getTLVChamps(data);

            return myTlv;
        }
        public void getEtatDeCaisse(string result)
        {
            TLVHandler appTagsHandler = new TLVHandler(result);
            TLVTags dataCaisseTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE) ;
            TLVHandler dataCaisseTagHandler = new TLVHandler(Utils.bytesToHex(dataCaisseTag.value));
            this.dateCaisse = Utils.HexToASCII(Utils.bytesToHex(dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_DATE).value)) ;
            this.typeCaisse = Utils.HexToASCII(Utils.bytesToHex(dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_TYPE).value));
            
            TLVTags Paris_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PARIS);
            TLVTags Annulation_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS);
            TLVTags Paiement_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS_PAIEMENT);
            TLVTags Voucher_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS);
            TLVTags Depot_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_DEPOTS_COMPTES);
            TLVTags Annul_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_SYSTEME) ;
            TLVTags AnnulMachine_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_MACHINE);
            TLVTags paiements_TAGS = dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PAIEMENTS) ;

            TLVHandler Paris_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Paris_TAGS.value));
            TLVHandler Annulation_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Annulation_TAGS.value));
            TLVHandler Paiement_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Paiement_TAGS.value));
            TLVHandler Voucher_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Voucher_TAGS.value));
            TLVHandler depot_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Depot_TAGS.value));
            TLVHandler annul_TAGS_Handler = new TLVHandler(Utils.bytesToHex(Annul_TAGS.value));
            TLVHandler paiements_TAGS_Handler = new TLVHandler(Utils.bytesToHex(paiements_TAGS.value));
            TLVHandler annul_Machine_TAGS_Handler = new TLVHandler(Utils.bytesToHex(AnnulMachine_TAGS.value));
            switch (this.typeCaisse)
            {
                case "PDD":
                    this.nombre_guichet = 0;
                    break;
                case "PDV":
                    this.nombre_guichet = Int32.Parse(Utils.bytesToHex(dataCaisseTagHandler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_NOMBRE_GUICHETS).value));
                    break;
            }
            try
            {

                this.caisse_Paris_Nombre = Int32.Parse(Utils.bytesToHex(Paris_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PARIS_NOMBRE).value));
                this.caisse_Paris_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(Paris_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PARIS_CUMUL).value)), CultureInfo.InvariantCulture);

                this.caisse_Annulation_Nombre = Int32.Parse(Utils.bytesToHex(Annulation_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_NOMBRE).value));
                this.caisse_Annulation_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(Annulation_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_CUMUL).value)), CultureInfo.InvariantCulture);

                this.caisse_Paiement_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(Paiement_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS_PAIEMENT_CUMUL).value)), CultureInfo.InvariantCulture);
                this.caisse_Paiement_Nombre = Int32.Parse(Utils.bytesToHex(Paiement_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS_PAIEMENT_NOMBRE).value));

                this.caisse_Voucher_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(Voucher_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS_CUMUL).value)), CultureInfo.InvariantCulture);
                this.caisse_Voucher_Nombre = Int32.Parse(Utils.bytesToHex(Voucher_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_VOUCHERS_NOMBRE).value));

                this.caisse_depot_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(depot_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_DEPOTS_COMPTES_CUMUL).value)), CultureInfo.InvariantCulture);
                this.caisse_depot_Nombre = Int32.Parse(Utils.bytesToHex(depot_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_DEPOTS_COMPTES_NOMBRE).value));

                this.caisse_AnnulSys_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(annul_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_SYSTEME_CUMUL).value)), CultureInfo.InvariantCulture);
                this.caisse_AnnulSys_Nombre = Int32.Parse(Utils.bytesToHex(annul_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_SYSTEME_NOMBRE).value));

                this.caisse_paiements_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(paiements_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PAIEMENTS_CUMUL).value)), CultureInfo.InvariantCulture);
                this.caisse_paiements_Nombre = Int32.Parse(Utils.bytesToHex(paiements_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_PAIEMENTS_NOMBRE).value));

                caisse_AnnulMachine_Cummul = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(annul_Machine_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_MACHINE_CUMUL).value)), CultureInfo.InvariantCulture);
                caisse_AnnulMachine_Nombre = Int32.Parse(Utils.bytesToHex(annul_Machine_TAGS_Handler.getTLV(TLVTags.SOREC_DATA_ETAT_CAISSE_ANNULATIONS_MACHINE_NOMBRE).value));

            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("Etat Caisse Exception : " + ex.Message);
            }
        }
    }
}
