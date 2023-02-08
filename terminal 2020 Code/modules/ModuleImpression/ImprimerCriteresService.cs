using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.UTILS;
using System;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using sorec_gamma.modules.Config;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerCriteresService
    {
        private static string imageFilePath = @"Images\ticketModels\model3200.bmp";
        private static string lastCriteresTicket = @"Images\tickets\LastCRITERES.bmp";
        public static bool ImprimerCriteres(Criteres criteres)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            int SPACE = 15;
            int marginLeft = 18;
            int width = 640;
            bool isPrinted = false;
            using (Image image = Image.FromFile(imageFilePath))
            using (Bitmap bitmap = new Bitmap(image))
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
                using (SolidBrush sb = new SolidBrush(Color.Black))
                using (Font fTitle1 = new Font("Lucida Console", 27, FontStyle.Bold))
                using (Font fTitle2 = new Font("Lucida Console", 25, FontStyle.Bold))
                using (Font fTitle4 = new Font("Lucida Console", 20, FontStyle.Regular))
                using (Font fTitle3 = new Font("Lucida Console", 20, FontStyle.Regular))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Rectangle rectTitle = new Rectangle(0, SPACE, width, 40);
                    g.DrawString("CRITERES", fTitle1, sb, rectTitle, stringFormatCenter);
                    SPACE += 65;
                    Rectangle rectDate = new Rectangle(0, SPACE, width, 35);
                    string date = DateTime.Now.ToString("dd/MM/yyy H:m:ss-" 
                        + ConfigUtils.ConfigData.Num_pdv + "." 
                        + ConfigUtils.ConfigData.Pos_terminal 
                        + "-" + ApplicationContext.SOREC_DATA_VERSION_LOG
                        + ApplicationContext.SOREC_DATA_ENV);
                    g.DrawString(date, fTitle4, sb, rectDate, stringFormatCenter);
                    SPACE += 45;
                    Rectangle rectHippo = new Rectangle(0, SPACE, width, 40);
                    g.DrawString(criteres.Hipodrome.ToUpper(), fTitle1, sb, rectHippo, stringFormatCenter);
                    SPACE += 45;
                    Rectangle rectoDateReunion = new Rectangle(0, SPACE, width, 40);
                    g.DrawString(criteres.DateReunion.ToString("dd/MM/yy").ToUpper(), fTitle2, sb, rectoDateReunion, stringFormatCenter);
                    SPACE += 45;
                    Rectangle rectoReunionCourse = new Rectangle(0, SPACE, width, 40);
                    g.DrawString("Réunion " + criteres.NumReunion.ToString() + " - Course " + criteres.Course.NumCoursePmu.ToString(), fTitle2, sb, rectoReunionCourse, stringFormatCenter);
                    SPACE += 55;
                    Rectangle partants = new Rectangle(marginLeft, SPACE, width, 40);
                    g.DrawString(criteres.Course.ListeHorses.Count+" DECLARES PARTANTS", fTitle3, sb, partants, stringFormatNear);
                    
                    List<string> nbrNPartsList = criteres.Course.ListeNonPartant
                            .Select(nPar => nPar.NumPartant.ToString())
                            .ToList();
                    SPACE += 35;
                    if (nbrNPartsList.Count == 0)
                    {
                        Rectangle rectoNonPartant = new Rectangle(marginLeft, SPACE, width, 40);
                        g.DrawString("TOUS PARTANTS", fTitle3, sb, rectoNonPartant, stringFormatNear);
                    }
                    else
                    {
                        Rectangle rectoNonPartant = new Rectangle(marginLeft, SPACE, width, 40);
                        g.DrawString(nbrNPartsList.Count + " NON PARTANT", fTitle3, sb, rectoNonPartant, stringFormatNear);
                        Rectangle rectoListNonPartant = new Rectangle(width / 2 - 20, SPACE, width / 2 + 30, 80);
                        string nParts = nbrNPartsList.Aggregate((i, j) => i + " " + j);
                        g.DrawString(nParts, fTitle3, sb, rectoListNonPartant, stringFormatNear);
                        SPACE += nParts.Length <= 20 ? 0 : 45;
                    }
                    SPACE += 35;

                    Rectangle rectoBaseChamps = new Rectangle(marginLeft, SPACE, width, 40);
                    g.DrawString("BASE CHAMPS", fTitle3, sb, rectoBaseChamps, stringFormatNear);
                    Rectangle rectoNumberBaseChamps = new Rectangle(width / 2 - 20, SPACE, width, 40);
                    g.DrawString(criteres.Course.ListeHorses.Count - nbrNPartsList.Count + " CHEVAUX", fTitle3, sb, rectoNumberBaseChamps, stringFormatNear);
                    List<IGrouping<string, Horse>> ecuries = getListPartantParEcurie(criteres.Course);
                    if (ecuries.Count > 0)
                    {
                        SPACE += 55;
                        foreach (IGrouping<string, Horse> ecurie in ecuries)
                        {
                            Rectangle rectEcurie = new Rectangle(marginLeft, SPACE, width, 40);
                            string[] parts = ecurie.Select(p => p.NumPartant.ToString()).ToArray();
                            g.DrawString(string.Format("ECURIE {0} {1}", ecurie.Key, string.Join(" ", parts)), fTitle3, sb, rectEcurie, stringFormatNear);
                            SPACE += 30;
                        }
                    }
                    SPACE += 55;
                    criteres.Course.ListeHorses.Sort(delegate (Horse x, Horse y)
                    {
                        return x.NumPartant.CompareTo(y.NumPartant);
                    });
                    foreach (Horse partant in criteres.Course.ListeHorses)
                    {
                        int x = partant.NumPartant < 10 ? 43 : 26;
                        Rectangle rectPartant = new Rectangle(x, SPACE, width, 40);
                        g.DrawString(partant.NumPartant + "   " + partant.NomPartant, fTitle3, sb, rectPartant, stringFormatNear);
                        SPACE += 35;
                    }
                    SPACE += 55;
                    Rectangle rectPARIS = new Rectangle(marginLeft, SPACE, width, 40);
                    g.DrawString("PARIS", fTitle3, sb, rectPARIS, stringFormatNear);
                    Rectangle rectENJEUX = new Rectangle(0, SPACE, width, 40);
                    g.DrawString("ENJEUX", fTitle3, sb, rectENJEUX, stringFormatFar);
                    criteres.Course.ListeProduit.Sort(delegate (Produit x, Produit y)
                    {
                        return x.CiriteresOrdre.CompareTo(y.CiriteresOrdre);
                    });
                    foreach (Produit p in criteres.Course.ListeProduit)
                    {
                        SPACE += 35;
                        Rectangle rectProduct = new Rectangle(43, SPACE, width, 40);
                        g.DrawString(getProductName(p.CodeProduit), fTitle3, sb, rectProduct, stringFormatNear);
                        if (p.Ordre)
                        {
                           Rectangle rectProductOrdre = new Rectangle(43, SPACE, width, 40);
                           g.DrawString("ORDRE", fTitle3, sb, rectProductOrdre, stringFormatCenter);
                        }
                        Rectangle rectENJEUProduct = new Rectangle(0, SPACE, width, 40);
                        g.DrawString(p.EnjeuMin.ToString()+"DH", fTitle3, sb, rectENJEUProduct, stringFormatFar);
                    }
                    SPACE += 70;
                    using (Image imgLogo = Image.FromFile(@"Images\ticketModels\Logo.bmp"))
                    using (Image logo = resizeImage(imgLogo, new Size(500, 200)))
                    {
                        Rectangle rectangle = new Rectangle(150, SPACE, width, 200);
                        g.DrawImage(logo, rectangle);
                    }
                    SPACE += 300;
                    Bitmap finalBitmap = null;
                    try
                    {
                        if (SPACE < 3072)
                        {
                            Rectangle recRes = new Rectangle(0, 0, width, SPACE);
                            finalBitmap = CropImage(bitmap, recRes);
                        }
                        else
                        {
                            finalBitmap = bitmap;
                        }
                        using (Bitmap bmpNew = GraphicUtils.ConvertBitmapTo1Bpp(finalBitmap))
                        {
                            if (ApplicationContext.develop)
                            {
                                bmpNew.Save(lastCriteresTicket);
                            }
                            else
                            {
                                Thread printThread = new Thread(() =>
                                {
                                    isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                                });
                                printThread.Name = "PRINT_CRITERES";
                                printThread.Start();
                                printThread.Join();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ApplicationContext.Logger.Error("Impression: Exception Stack Trace => " + ex.StackTrace);
                    }
                    finally
                    {
                        ApplicationContext.Logger.Info("End impression ...");

                    }
                }
            }
            return isPrinted;
        }
        private static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }
        private static string getProductName(string productCode)
        {
            switch (productCode)
            {
                case "GAG": return "GAGNANT";
                case "PLA": return "PLACE"; 
                case "JUG": return "JUMELE G"; 
                case "JUP": return "JUMELE P"; 
                case "TRO": return "TRIO"; 
                case "TRC": return "TIERCE";
                case "QUU": return "QUARTE"; 
                case "QAP": return "QUARTE+"; 
                case "QIP": return "QUINTE+"; 
                case "JUO": return "JUMELE O"; 
                case "ML4": return "MULTI 4"; 
                case "ML5": return "MULTI 5"; 
                case "ML6": return "MULTI 6";
                case "ML7": return "MULTI 7";
                default: return "UNDEFINED";
            }
        }
        public static List<IGrouping<string, Horse>> getListPartantParEcurie(Course c)
        {
            return c.ListeHorses
                .Where(e => e.EcuriePart != null && !e.EcuriePart.Equals(""))
                .GroupBy(p => p.EcuriePart)
                .ToList();
        }
    }
}
