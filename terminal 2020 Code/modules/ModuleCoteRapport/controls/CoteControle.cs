using sorec_gamma.modules.ModuleCoteRapport;
using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.TLV;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace sorec_gamma.modules.ModuleCote_rapport.controls
{
     public class CoteControle
    {
        public ConcurrentBag<ReunionCote> ProcessCotes(TLVTags coteDataTag)
        {
            ConcurrentBag<ReunionCote> reunionCotes = new ConcurrentBag<ReunionCote>();
            if (coteDataTag != null)
            {
                try
                {
                    string TagTLV = Utils.bytesToHex(coteDataTag.value);
                    if (TagTLV != null)
                    {
                        TLVHandler tLVHandler = new TLVHandler(TagTLV);
                        TLVTags sorec_data_event = tLVHandler.getTLV(TLVTags.SOREC_DATA_EVENEMENT);
                        if (sorec_data_event != null)
                        {
                            TLVHandler eventTagHandler = new TLVHandler(Utils.bytesToHex(sorec_data_event.value));
                            TLVTags hippo = eventTagHandler.getTLV(TLVTags.SOREC_DATA_TICKET_HIPPODROME);

                            TLVTags listeReunionsTag = eventTagHandler.getTLV(TLVTags.SOREC_DATA_COTES_REUNIONS);
                            if (listeReunionsTag != null && listeReunionsTag.value != null)
                            {
                                TLVHandler tLVHandlerReunionsTag = new TLVHandler(Utils.bytesToHex(listeReunionsTag.value));

                                List<TLVTags> reunionTag = tLVHandlerReunionsTag.getTLVList(TLVTags.SOREC_DATA_COTES_REUNION);

                                foreach (TLVTags tLVTags in reunionTag)
                                {
                                    if (tLVTags == null || tLVTags.value == null)
                                        continue;
                                    TLVHandler tLVHandlerReunionTag = new TLVHandler(Utils.bytesToHex(tLVTags.value));
                                    TLVTags NumReunionTag = tLVHandlerReunionTag.getTLV(TLVTags.SOREC_DATA_EVENEMENT_NUMREUNION);
                                    TLVTags DateReunionTag = tLVHandlerReunionTag.getTLV(TLVTags.SOREC_DATA_EVENEMENT_DATEREUNION);
                                    DateTime dateR = DateReunionTag == null ? DateTime.MinValue : Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(DateReunionTag.value)));
                                    string codeHippo = hippo != null ? Utils.HexToASCII(Utils.bytesToHex(hippo.value)) : "";
                                    int numR = NumReunionTag == null ? 0 : Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(NumReunionTag.value)));

                                    ReunionCote reunionCote = new ReunionCote(numR.ToString(), codeHippo, dateR);

                                    TLVTags listeCoursesTag = tLVHandlerReunionTag.getTLV(TLVTags.SOREC_DATA_COTES_COURSES);
                                    if (listeCoursesTag == null || listeCoursesTag.value == null)
                                        continue;
                                    TLVHandler listeCoursesTagHandler = new TLVHandler(Utils.bytesToHex(listeCoursesTag.value));
                                    List<TLVTags> courseTag = listeCoursesTagHandler.getTLVList(TLVTags.SOREC_DATA_COTES_COURSE);
                                    foreach (TLVTags tLVTags2 in courseTag)
                                    {
                                        if (tLVTags2 == null || tLVTags2.value == null)
                                            continue;
                                        TLVHandler tLVHandlerCourseTag = new TLVHandler(Utils.bytesToHex(tLVTags2.value));
                                        TLVTags NumCourseTag = tLVHandlerCourseTag.getTLV(TLVTags.SOREC_DATA_EVENEMENT_NUMCOURSE);
                                        int numC = NumCourseTag == null ? 0 : Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(NumCourseTag.value)));
                                        CourseCote courseCote = new CourseCote(numR.ToString(), numC.ToString());

                                        TLVTags listeProduitsTag = tLVHandlerCourseTag.getTLV(TLVTags.SOREC_DATA_EVENEMENT_PRODUITS);
                                        if (listeProduitsTag == null || listeProduitsTag.value == null)
                                            continue;
                                        TLVHandler listeProduitsTagHandler = new TLVHandler(Utils.bytesToHex(listeProduitsTag.value));
                                        List<TLVTags> produitTag = listeProduitsTagHandler.getTLVList(TLVTags.SOREC_DATA_PRODUIT);

                                        foreach (TLVTags tLVTags3 in produitTag)
                                        {
                                            if (tLVTags3 == null || tLVTags3.value == null)
                                                continue;
                                            TLVHandler tLVHandlerProduitTag = new TLVHandler(Utils.bytesToHex(tLVTags3.value));
                                            TLVTags coteEvenDateTag = tLVHandlerProduitTag.getTLV(TLVTags.SOREC_DATA_COTES_EVENEMENT_TIMESTAMP);
                                            TLVTags totalMisesTag = tLVHandlerProduitTag.getTLV(TLVTags.SOREC_DATA_PRODUIT_TOTALMISES);
                                            TLVTags CodeProduitTag = tLVHandlerProduitTag.getTLV(TLVTags.SOREC_DATA_PRODUIT_CODE);

                                            string prodCode = Utils.HexToASCII(Utils.bytesToHex(CodeProduitTag.value));
                                            string totMises = Utils.HexToASCII(Utils.bytesToHex(totalMisesTag.value));
                                            DateTime dateEvenementCote = coteEvenDateTag == null ? DateTime.MinValue : Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(coteEvenDateTag.value)));

                                            ProduitCote prodCote = new ProduitCote(prodCode, dateEvenementCote, totMises);

                                            TLVTags listeCombinaisonsTag = tLVHandlerProduitTag.getTLV(TLVTags.SOREC_DATA_PRODUIT_COMBINAISONS);
                                            if (listeCombinaisonsTag == null || listeCombinaisonsTag.value == null)
                                                continue;
                                            TLVHandler tLVHandlerCombinaisonsTag = new TLVHandler(Utils.bytesToHex(listeCombinaisonsTag.value));
                                            List<TLVTags> CombinaisonTag = tLVHandlerCombinaisonsTag.getTLVList(TLVTags.SOREC_DATA_COMBINAISON);

                                            foreach (TLVTags tLVTags4 in CombinaisonTag)
                                            {
                                                if (tLVTags4 == null || tLVTags4.value == null)
                                                    continue;
                                                TLVHandler tLVHandlerCombinaisonTag = new TLVHandler(Utils.bytesToHex(tLVTags4.value));
                                                TLVTags CombinaisoncombTag = tLVHandlerCombinaisonTag.getTLV(TLVTags.SOREC_DATA_COMBINAISON_COMBINAISON);
                                                TLVTags combcoteTag = tLVHandlerCombinaisonTag.getTLV(TLVTags.SOREC_DATA_COMBINAISON_COTE);
                                                TLVTags combMiseTag = tLVHandlerCombinaisonTag.getTLV(TLVTags.SOREC_DATA_COMBINAISON_TOTALMISES);

                                                string combParts = Utils.HexToASCII(Utils.bytesToHex(CombinaisoncombTag.value));
                                                string combCote = Utils.HexToASCII(Utils.bytesToHex(combcoteTag.value));
                                                string combTotMise = Utils.HexToASCII(Utils.bytesToHex(combMiseTag.value));
                                                CombinaisonCote combinCote = new CombinaisonCote(combParts, combCote, combTotMise, prodCode);
                                                prodCote.AddOrUpdateCombCotes(combinCote);
                                            }
                                            courseCote.AddOrUpdateProduitCotes(prodCote);
                                        }
                                        reunionCote.AddOrUpdateCourseCotes(courseCote);
                                    }
                                    reunionCotes.Add(reunionCote);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ApplicationContext.Logger.Error(string.Format("CoteControle Exception {0}, {1}", ex.Message, ex.StackTrace));
                }
            }
            return reunionCotes;
        }
    }
}
