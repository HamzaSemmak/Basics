using sorec_gamma.modules.Config;
using sorec_gamma.modules.UTILS;
using System;
using System.Drawing;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerFSoldService
    {
        private static string logoFilePath = @"Images\ticketModels\Logo.bmp";
        private static string modelFilePath = @"Images\ticketModels\model3200.bmp";
        private static string lastFSoldTicketPath = @"Images\tickets\LastFSold.bmp";

        public bool ImprimerFsold(string content)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            int SPACE = 10;
            int marginLeft = 18;
            int width = 640;
            bool isPrinted = false;
            Bitmap finalBitmap = null;
            try
            {
                using (Image modeleImage = Image.FromFile(modelFilePath))
                using (Bitmap bitmap = new Bitmap(modeleImage))
                using (Graphics graphic = Graphics.FromImage(bitmap))
                using (Font fTitle = new Font("Arial", 26f, FontStyle.Bold))
                using (Font fTitle1 = new Font("Arial", 24f, FontStyle.Bold))
                using (Font fBody = new Font("Lucida Console", 16, FontStyle.Bold))
                using (SolidBrush sb = new SolidBrush(Color.Black))
                using (StringFormat stringFormatCenter = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    Rectangle titleRect = new Rectangle(marginLeft, SPACE, width, 35);
                    graphic.DrawString("INFO", fTitle, sb, titleRect, stringFormatCenter);

                    SPACE += 50;
                    string header =
                        DateTime.Now.ToString("dd/MM/yyyy HH:m:ss") + "-" +
                        ConfigUtils.ConfigData.Num_pdv + "." +
                        ConfigUtils.ConfigData.Pos_terminal + " - " +
                        ApplicationContext.SOREC_DATA_VERSION_LOG
                        + ApplicationContext.SOREC_DATA_ENV;

                    Rectangle dateRect = new Rectangle(marginLeft, SPACE, width, 35);
                    graphic.DrawString(header, fTitle1, sb, dateRect, stringFormatCenter);

                    SPACE += 50;
                    string[] lines = content.Split('\n');
                    foreach (string line in lines)
                    {
                        Rectangle lineRect = new Rectangle(marginLeft, SPACE, width, 35);
                        graphic.DrawString(line, fBody, sb, lineRect);
                        SPACE += 35;
                    }
                    SPACE += 10;
                    using (Image imgLogo = Image.FromFile(logoFilePath))
                    using (Image resizedLogo = resizeImage(imgLogo, new Size(500, 200)))
                    {
                        Rectangle rectangle = new Rectangle(150, SPACE, width, 200);
                        graphic.DrawImage(resizedLogo, rectangle);
                    }
                    SPACE += 300;
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
                        if (ApplicationContext.develop) bmpNew.Save(lastFSoldTicketPath);
                        else 
                            isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("Imprimer FSold Exception : " + ex.Message);
            }
            finally
            {
                if (finalBitmap != null)
                    finalBitmap.Dispose();
            }
            return isPrinted || ApplicationContext.develop;
        }

        public Bitmap resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }
    }
}
