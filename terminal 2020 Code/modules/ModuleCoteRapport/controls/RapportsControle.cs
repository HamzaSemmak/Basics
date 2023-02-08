using sorec_gamma.modules.ModuleCote_rapport.model;
using sorec_gamma.modules.ModuleCote_rapport.services;
using sorec_gamma.modules.ModuleEcranClient;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using System;
using System.Collections.Generic;

namespace sorec_gamma.modules.ModuleCote_rapport.controls
{
    class RapportsControle
    {
        public List<RapportCoteModel> GetRapports(Reunion r, Course c)
        {
            ApplicationContext.Logger.Info(string.Format("Rapport Controle: Demande de rapport pour {0} \n {1}", r.ToString(), c.ToString()));
            TLVHandler appTagsHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_DEMANDE_RAPPORT);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_EVENEMENT_DATEREUNION, r.DateReunion.ToString("yyyy-MM-dd"));
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_EVENEMENT_NUMREUNION, r.NumReunion.ToString());
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_EVENEMENT_NUMCOURSE, c.NumCoursePmu.ToString());
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            TLVHandler pari_annul_paiem_handler = TlvUtlis.Annul_paiement_sys_controle();
            if (pari_annul_paiem_handler != null)
            {
                appTagsHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME, pari_annul_paiem_handler);
            }
            string tlvData = appTagsHandler.toString();
            string tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            List<RapportCoteModel> parsedRapportResults = new List<RapportCoteModel>();
            try
            {
                string data = RapportsServices.SendRequest(tlvData, macString);
                if (!string.IsNullOrEmpty(data))
                {
                    string myTlv = TLVHandler.getTLVChamps(data);
                    int codeReponse = TLVHandler.Response(data);
                    TLVHandler dataRapport = new TLVHandler(myTlv);
                    TLVTags sorec_data_rapports = dataRapport.getTLV(TLVTags.SOREC_DATA_RAPPORTS);

                    if (sorec_data_rapports != null)
                    {
                        TLVHandler sorec_data_rapportsHandler = new TLVHandler(Utils.bytesToHex(sorec_data_rapports.value));
                        TLVTags sorec_data_evenement_tag = sorec_data_rapportsHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORTS);//8447
                        TLVHandler sorec_data_evenementHandler = new TLVHandler(Utils.bytesToHex(sorec_data_evenement_tag.value));
                        List<TLVTags> listRapports2 = sorec_data_evenementHandler.getTLVList();

                        foreach (var rapport in listRapports2)
                        {
                            RapportCoteModel rapport1 = new RapportCoteModel();

                            rapport1.DateReunion = r.DateReunion;
                            rapport1.NumCourse = c.NumCoursePmu;
                            rapport1.NumReunion = r.NumReunion;
                            TLVHandler rapportProduitHandler2 = new TLVHandler(Utils.bytesToHex(rapport.value));
                            TLVTags produit = rapportProduitHandler2.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUITS); //8436
                            TLVHandler newHandler = new TLVHandler(Utils.bytesToHex(produit.value));
                            List<TLVTags> listProduits = newHandler.getTLVList(); // ALL 8437
                            List<RapportCoteItemModel> produits_Rapports = new List<RapportCoteItemModel>();
                            foreach (var products in listProduits)
                            {
                                TLVHandler eventRapportProduitHandler = new TLVHandler(Utils.bytesToHex(products.value));
                                RapportCoteItemModel pR = new RapportCoteItemModel();
                                TLVTags rapportProduitCode1 = eventRapportProduitHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_CODE); //8438
                                TLVTags rapportProduitMise1 = eventRapportProduitHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_MISES);//8439
                                TLVTags rapportProduitStatut1 = eventRapportProduitHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_STATUT);//8440
                                pR.CodeProduit = Utils.HexToASCII(Utils.bytesToHex(rapportProduitCode1.value));
                                pR.TotalMise = Utils.HexToASCII(Utils.bytesToHex(rapportProduitMise1.value));
                                pR.StatutRapport = Utils.HexToASCII(Utils.bytesToHex(rapportProduitStatut1.value));
                                List<TLVTags> rapportProduitCombinaisons1 = new List<TLVTags>();
                                TLVTags evenementrapportProduitrapport1 = eventRapportProduitHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_RAPPORTS);
                                if (evenementrapportProduitrapport1 != null)
                                {
                                    TLVHandler rapportProduitCombinaisonsrapportsHandler = new TLVHandler(Utils.bytesToHex(evenementrapportProduitrapport1.value));
                                    rapportProduitCombinaisons1 = rapportProduitCombinaisonsrapportsHandler.getTLVList(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_RAPPORT);
                                }
                                if (rapportProduitCombinaisons1.Count > 0)
                                {
                                    List<CombinaisonCoteRapportModel> combinaisonsListe1 = new List<CombinaisonCoteRapportModel>();
                                    foreach (var combinaisonTag in rapportProduitCombinaisons1)
                                    {
                                        CombinaisonCoteRapportModel combinaison = new CombinaisonCoteRapportModel();
                                        TLVHandler rapportProduitCombinaisonsrapportHandler = new TLVHandler(Utils.bytesToHex(combinaisonTag.value));

                                        TLVTags rapportcombTag = rapportProduitCombinaisonsrapportHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_RAPPORT_COMBINAISON);
                                        TLVTags rapport1DHTag = rapportProduitCombinaisonsrapportHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_RAPPORT_RAPPORT);
                                        TLVTags typerapportTag = rapportProduitCombinaisonsrapportHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_PRODUIT_RAPPORT_TYPE);
                                        TLVTags NumReunion = rapportProduitCombinaisonsrapportHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_REUNION);
                                        TLVTags CourseTag = rapportProduitCombinaisonsrapportHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT_RAPPORT_COURSE);
                                        combinaison.RapportCombinaison = Utils.HexToASCII(Utils.bytesToHex(rapportcombTag.value));
                                        combinaison.Rapport1DH = Utils.HexToASCII(Utils.bytesToHex(rapport1DHTag.value));
                                        combinaison.TypeRapport = Utils.HexToASCII(Utils.bytesToHex(typerapportTag.value));

                                        combinaisonsListe1.Add(combinaison);
                                    }
                                    pR.CombinaisonRapports = combinaisonsListe1;
                                }
                                produits_Rapports.Add(pR);
                            }
                            rapport1.RapportCoteItem_Dupliateds = produits_Rapports;
                            parsedRapportResults.Add(rapport1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                 CoteUtils.logger.addLog("RapportContole GetRapports Exception : " + e.Message, 2);
            }
            return parsedRapportResults;
        }
    }
}
