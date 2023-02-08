using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleCote_rapport.model;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.UTILS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerRapportService
    {
        private static string lastRaportTicketPath = @"Images\tickets\LastRapport.bmp";
        private static string imageFilePath = @"Images\ticketModels\model3200.bmp";

        public List<string> getListNonPartants(Course c)
        {
            return c.ListeNonPartant
                .Select(nPart => nPart.NumPartant.ToString())
                .ToList();
        }

        private string getNFirstCaract(string value, int n)
        {
            if (value == null || value.Length <= n)
            {
                return value;
            }
            return value.Substring(0, n);
        }

        public List<IGrouping<string, Horse>> getListPartantParEcurie(Course c)
        {
            return c.ListeHorses
                .Where(e => e.EcuriePart != null && !e.EcuriePart.Equals(""))
                .GroupBy(p => p.EcuriePart)
                .ToList();

            /* SortedDictionary<string, string> mapEcuriesE = new SortedDictionary<string, string>();
            foreach (Partant p in c.ListePartant.)
            {
                if (p.Ecurie_Part != null && p.Ecurie_Part.Length > 0)
                    mapEcuries.Add(p.NumPartant.ToString(), p.Ecurie_Part);
            }

            Dictionary<string, List<string>> groupedEcuries =
                   mapEcuries.GroupBy(r => r.Value)
                  .ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());

            List<string> listEcuries = new List<string>();

            foreach (string ecCode in groupedEcuries.Keys)
            {
                List<string> ecurieValues;
                groupedEcuries.TryGetValue(ecCode, out ecurieValues);
                listEcuries.Add(string.Format("ECURIE {0} {1}", ecCode, string.Join(" ", ecurieValues.ToArray())));
            }

            return listEcuries;*/
        }

        public bool imprimerRapport(Reunion r, Course c, List<RapportCoteModel> rapport)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;

            int SPACE = 10;
            int marginLeft = 18;
            int width = 640;

            bool printed = false;
            using (Image image = Image.FromFile(imageFilePath))
            using (Bitmap bitmap = new Bitmap(image))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (StringFormat stringFormatFar = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                })
                using (StringFormat stringFormatNear = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                })
                using (StringFormat stringFormatCenter = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                using (Font fTitle1 = new Font("Lucida Console", 27, FontStyle.Bold))
                using (Font fTitle2 = new Font("Lucida Console", 24, FontStyle.Regular))
                using (Font fTitle3 = new Font("Lucida Console", 22, FontStyle.Bold))
                using (Font fTitle4 = new Font("Lucida Console", 20, FontStyle.Regular))
                using (SolidBrush sb = new SolidBrush(Color.Black))
                {
                    Rectangle rectTitle = new Rectangle(marginLeft, SPACE, width, 40);
                    g.DrawString("RAPPORTS", fTitle1, sb, rectTitle, stringFormatCenter);

                    SPACE += 65;
                    Rectangle rectDate = new Rectangle(0, SPACE, width, 35);
                    string date = DateTime.Now.ToString("dd/MM/yyy H:m:ss-"
                        + ConfigUtils.ConfigData.Num_pdv
                        + "." + ConfigUtils.ConfigData.Pos_terminal + "-"
                        + ApplicationContext.SOREC_DATA_VERSION_LOG
                        + ApplicationContext.SOREC_DATA_ENV);
                    g.DrawString(date, fTitle4, sb, rectDate, stringFormatCenter);

                    SPACE += 40;
                    Rectangle rectReun = new Rectangle(0, SPACE, width, 35);
                    g.DrawString("Réunion " + r.NumReunion + " - " + "Course " + c.NumCoursePmu, fTitle3, sb, rectReun, stringFormatCenter);

                    SPACE += 40;
                    Rectangle rectCodeHippo = new Rectangle(0, SPACE, width, 35);
                    g.DrawString(r.LibReunion.ToUpper(), fTitle3, sb, rectCodeHippo, stringFormatCenter);

                    SPACE += 40;
                    Rectangle rectDateReun = new Rectangle(0, SPACE, width, 35);
                    g.DrawString(r.DateReunion.ToString("dddd, dd MMMM yyyy").ToUpper(), fTitle3, sb, rectDateReun, stringFormatCenter);

                    SPACE += 40;
                    Rectangle rectPrix = new Rectangle(0, SPACE, width, 35);
                    g.DrawString("Rapports pour 1DH", fTitle3, sb, 200, SPACE);

                    SPACE += 70;
                    foreach (RapportCoteModel rr in rapport)
                    {
                        foreach (RapportCoteItemModel pR in rr.RapportCoteItem_Dupliateds)
                        {
                            string produit = "";
                            switch (pR.CodeProduit)
                            {
                                case "GAG": produit = "GAGNANT"; break;
                                case "PLA": produit = "PLACE"; break;
                                case "JUG": produit = "JUMELE G"; break;
                                case "JUP": produit = "JUMELE P"; break;
                                case "TRO": produit = "TRIO"; break;
                                case "TRC": produit = "TIERCE"; break;
                                case "QUU": produit = "QUARTE"; break;
                                case "QAP": produit = "QUARTE+"; break;
                                case "QIP": produit = "QUINTE+"; break;
                                case "JUO": produit = "JUMELE O"; break;
                                case "MLT": produit = "MULTI"; break;
                                default: produit = getNFirstCaract(pR.CodeProduit, 8); break;
                            }

                            Rectangle rectProdName = new Rectangle(marginLeft, SPACE, width, 40);
                            g.DrawString(produit, fTitle1, sb, rectProdName, stringFormatNear);
                            switch (pR.StatutRapport)
                            {
                                case "RAPPORT":
                                    SPACE += 40;
                                    List<CombinaisonCoteRapportModel> combinaisonRapport = pR.CombinaisonRapports;
                                    foreach (CombinaisonCoteRapportModel cR in combinaisonRapport)
                                    {
                                        string combinaison = cR.RapportCombinaison.Replace(" ", "-");
                                        string statut = "";
                                        string rapport1DH = cR.Rapport1DH;

                                        switch (cR.TypeRapport)
                                        {
                                            case "N":
                                                statut = "";
                                                break;
                                            case "O":
                                                statut = "Ordre";
                                                break;
                                            case "DO":
                                                statut = "Désordre";
                                                break;
                                            case "B":
                                                statut = "Bonus";
                                                break;
                                            case "B3":
                                                statut = "Bonus 3";
                                                break;
                                            case "B4":
                                                statut = "Bonus 4";
                                                break;
                                            case "ML4":
                                                statut = "Multi 4";
                                                break;
                                            case "ML5":
                                                statut = "Multi 5";
                                                break;
                                            case "ML6":
                                                statut = "Multi 6";
                                                break;
                                            case "ML7":
                                                statut = "Multi 7";
                                                break;
                                            default:
                                                statut = cR.TypeRapport;
                                                break;
                                        }

                                        if (!IsEmpty(statut))
                                        {
                                            Rectangle rectStatus = new Rectangle(0, SPACE, width, 76);
                                            g.DrawString(statut, fTitle4, sb, rectStatus, stringFormatCenter);
                                            SPACE += 25;
                                        }

                                        Rectangle rectComb = new Rectangle(marginLeft + 15, SPACE, width, 30);
                                        Rectangle rectRapp = new Rectangle(0, SPACE, width, 35);
                                        g.DrawString(combinaison, fTitle4, sb, rectComb, stringFormatNear);

                                        rectRapp = new Rectangle(0, SPACE, width, 30);
                                        g.DrawString(rapport1DH + "DH", fTitle3, sb, rectRapp, stringFormatFar);

                                        SPACE += 30;
                                    }
                                    break;

                                case "REMBOURSEMENT":
                                    Rectangle rectRemb = new Rectangle(0, SPACE, width, 40);
                                    g.DrawString("REMBOURSÉ", fTitle3, sb, rectRemb, stringFormatFar);
                                    SPACE += 30;
                                    break;
                            }
                            Rectangle rectLigne = new Rectangle(marginLeft, SPACE, width, 30);
                            g.DrawString("---------------------------------------------------------------------------------------", fTitle4, sb, rectLigne, stringFormatNear);
                            SPACE += 40;
                        }
                    }

                    List<IGrouping<string, Horse>> ecuries = getListPartantParEcurie(c);
                    if (ecuries.Count < 1)
                    {
                        Rectangle rectEcurie = new Rectangle(marginLeft, SPACE, width, 40);
                        g.DrawString("PAS D'ECURIE", fTitle3, sb, rectEcurie, stringFormatNear);
                        SPACE += 30;
                    }
                    else
                    {
                        foreach (IGrouping<string, Horse> ecurie in ecuries)
                        {
                            Rectangle rectEcurie = new Rectangle(marginLeft, SPACE, width, 40);
                            string[] parts = ecurie.Select(p => p.NumPartant.ToString()).ToArray();
                            g.DrawString(string.Format("ECURIE {0} {1}", ecurie.Key, string.Join(" ", parts)), fTitle3, sb, rectEcurie, stringFormatNear);
                            SPACE += 30;
                        }
                    }

                    Rectangle rectLigne2 = new Rectangle(marginLeft, SPACE, width, 30);
                    g.DrawString("---------------------------------------------------------------------------------------", fTitle4, sb, rectLigne2, stringFormatNear);

                    SPACE += 40;

                    List<string> nonPartants = getListNonPartants(c);
                    var ListNonPartants = string.Join(" ", getListNonPartants(c).ToArray());
                    Rectangle rectNbrPartants = new Rectangle(marginLeft, SPACE, width, 40);
                    g.DrawString(nonPartants.Count == 0 ? "PAS DE NON PARTANTS" : "NON PARTANTS: " + ListNonPartants, fTitle3, sb, rectNbrPartants, stringFormatNear);
                }
                SPACE += 70;
                using (Image imgLogo = Image.FromFile(@"Images\ticketModels\Logo.bmp"))
                using (Image logo = resizeImage(imgLogo, new Size(500, 200)))
                {
                    Rectangle rectangle = new Rectangle(150, SPACE, width, 200);
                    g.DrawImage(logo, rectangle);
                }
                SPACE += 300;
                Bitmap croppedBmp = null;
                Bitmap bmpNew = null;
                try
                {
                    if (SPACE < 3072)
                    {
                        Rectangle recRes = new Rectangle(0, 0, width, SPACE);
                        croppedBmp = CropImage(bitmap, recRes);
                    }
                    else
                    {
                        croppedBmp = bitmap;
                    }
                    int w = croppedBmp.Width;
                    int h = croppedBmp.Height;
                    bmpNew = GraphicUtils.ConvertBitmapTo1Bpp(croppedBmp);
                    if (ApplicationContext.develop)
                    {
                        bmpNew.Save(lastRaportTicketPath);
                    }
                    else
                    {
                        Thread printThread = new Thread(() =>
                        {
                            printed = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                        });
                        printThread.Name = "PRINT_RAPPORT";
                        printThread.Start();
                        printThread.Join();
                    }
                }
                catch (Exception ex)
                {
                    ApplicationContext.Logger.Error("Impression: Exception Stack Trace => " + ex.StackTrace);
                }
                finally
                {
                    if (bmpNew != null)
                    {
                        bmpNew.Dispose();
                    }
                    if (croppedBmp != null)
                    {
                        croppedBmp.Dispose();
                    }
                }
            }
            return printed || ApplicationContext.develop;
        }

        private static bool IsEmpty(string value)
        {
            return value == null || value == "";
        }
        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

    }
}
