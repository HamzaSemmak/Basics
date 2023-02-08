using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.UTILS;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerJournalService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ImprimerJournalService));

        private static string ModelFilePath = @"Images\ticketModels\model3200.bmp";
        private static string lastJournalTicket = @"Images\tickets\LastJournal.bmp";
        public static bool imprimerJournal(List<ConcurrentLigne> dataJournal)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            bool isPrinted = false;
            int SPACE = 10;
            int marginLeft = 27;
            int width = 640;
            using (Image image = Image.FromFile(ModelFilePath))
            using (Bitmap bitmap = new Bitmap(image))
            using (Graphics graphics = Graphics.FromImage(bitmap))
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
                using (Font title1 = new Font("Arial", 24f, FontStyle.Bold))
                using (Font title2 = new Font("Arial", 20f))
                using (Font title3 = new Font("Arial", 11f, FontStyle.Bold))
                using (SolidBrush sb = new SolidBrush(Color.Black))
                {
                    Rectangle rectTitle = new Rectangle(0, SPACE, width, 60);
                    graphics.DrawString("JOURNAL", title1, sb, rectTitle, stringFormatCenter);
                    SPACE += 80;
                    Rectangle dateRect = new Rectangle(marginLeft, SPACE, width, 35);
                    string date = DateTime.Now.ToString("dd/MM/yyy H:m:ss-"
                        + ConfigUtils.ConfigData.Num_pdv
                        + "." + ConfigUtils.ConfigData.Pos_terminal + "-"
                        + ApplicationContext.SOREC_DATA_VERSION_LOG
                        + ApplicationContext.SOREC_DATA_ENV);
                    graphics.DrawString(date, title2, sb, dateRect, stringFormatCenter);

                    SPACE += 50;
                    foreach (ConcurrentLigne s in dataJournal)
                    {
                        Rectangle val1Rect = new Rectangle(marginLeft, SPACE, 180, 33);
                        Rectangle val2Rect = new Rectangle(marginLeft + 180, SPACE, 330, 33);
                        Rectangle val3Rect = new Rectangle(marginLeft + 180 + 330, SPACE, 100, 33);

                        graphics.DrawString(s.Value1, title3, sb, val1Rect, stringFormatNear);
                        graphics.DrawString(s.Value2, title3, sb, val2Rect, stringFormatNear);
                        graphics.DrawString(s.Value3, title3, sb, val3Rect, stringFormatFar);
                        SPACE += 25;
                    }

                    SPACE += 40;
                    using (Image imgLogo = Image.FromFile(@"Images\ticketModels\Logo.bmp"))
                    using (Image logo = resizeImage(imgLogo, new Size(500, 200)))
                    {
                        Rectangle rectangle = new Rectangle(150, SPACE, width, 200);
                        graphics.DrawImage(logo, rectangle);
                    }

                    SPACE += 300;
                    Bitmap finalBmp = null;
                    try
                    {
                        if (SPACE < 3072)
                        {
                            Rectangle recRes = new Rectangle(0, 0, width, SPACE);
                            finalBmp = CropImage(bitmap, recRes);
                        }
                        else
                        {
                            finalBmp = bitmap;
                        }
                        using (Bitmap bmpNew = GraphicUtils.ConvertBitmapTo1Bpp(finalBmp))
                        {
                            if (ApplicationContext.develop)
                            {
                                bmpNew.Save(lastJournalTicket);
                            }
                            else
                            {
                                isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CaissePDDForm Exception : " + ex.Message + "\n" + ex.StackTrace);
                    }
                    finally
                    {
                        if (finalBmp != null)
                            finalBmp.Dispose();
                    }
                }
            }
            return isPrinted || ApplicationContext.develop;
        }

        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
    }
}
