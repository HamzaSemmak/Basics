using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
/**
 * Created by yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.UTILS
{
    public class GraphicUtils
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public static Bitmap GetEcranClientBitmap(EcranClientModel model)
        {
            int SPACE = 100;
            int marginLeft = 5;
            int width = 640;

            SolidBrush sb = new SolidBrush(Color.Black);

            StringFormat stringFormatNear = new StringFormat();
            stringFormatNear.Alignment = StringAlignment.Near;
            stringFormatNear.LineAlignment = StringAlignment.Near;

            StringFormat stringFormatCenter = new StringFormat();
            stringFormatCenter.Alignment = StringAlignment.Center;
            stringFormatCenter.LineAlignment = StringAlignment.Center;

            StringFormat stringFormatFar = new StringFormat();
            stringFormatFar.Alignment = StringAlignment.Far;
            stringFormatFar.LineAlignment = StringAlignment.Far;
            Bitmap bmp = null;
            using (Bitmap bitmap = new Bitmap(@"Images\ticketModels\ecranClientModelNotIndx.bmp"))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (Font fSimple = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Point))
            using (Font fTitle = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point))
            using (Font fPrice = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Point))
            {
                //if (model.IsSimpleMsg)
                if(model.MessageType == MessageType.Simple)
                {
                    SPACE += 110;
                    Rectangle rect1 = new Rectangle(marginLeft, SPACE, width, 200);
                    g.DrawString(model.SimpleMsg, fSimple, sb, rect1, stringFormatCenter);
                }
                else if(model.MessageType == MessageType.ClientDetail)
                {
                    SPACE += 65;
                    Rectangle rectDistributionCnt = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.DistributionCount + " DISTRIBUTION", fSimple, sb, rectDistributionCnt, stringFormatNear);
                    Rectangle rectDistribution = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.Distribution + "DH", fSimple, sb, rectDistribution, stringFormatFar);
                    SPACE += 105; 
                    Rectangle rectAnnulationCnt = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.AnnulationCount + " ANNULATION", fSimple, sb, rectAnnulationCnt, stringFormatNear);
                    Rectangle rectAnnulation = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.Annulation + "DH", fSimple, sb, rectAnnulation, stringFormatFar);
                    SPACE += 105;
                    Rectangle rectPaiementCnt = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.PaiementCount + " PAIEMENT", fSimple, sb, rectPaiementCnt, stringFormatNear);
                    Rectangle rectPaiement = new Rectangle(marginLeft, SPACE, width, 70);
                    g.DrawString(model.Paiement + "DH", fSimple, sb, rectPaiement, stringFormatFar);
                    SPACE += 160;
                    decimal rest = model.Distribution - model.Paiement - model.Annulation;
                    if (rest >= 0)
                    {
                        Rectangle rectRest = new Rectangle(marginLeft, SPACE, width, 70);
                        g.DrawString("VOUS DEVEZ", fSimple, sb, rectRest, stringFormatNear);
                        Rectangle rectRestValue = new Rectangle(marginLeft, SPACE, width, 70);
                        g.DrawString(rest+"DH", fSimple, sb, rectRest, stringFormatFar);
                    }
                    else
                    {
                        Rectangle rectRest = new Rectangle(marginLeft, SPACE, width, 70);
                        g.DrawString("VOUS RECEVEZ", fSimple, sb, rectRest, stringFormatNear);
                        Rectangle rectRestValue = new Rectangle(marginLeft, SPACE, width, 70);
                        g.DrawString(Math.Abs(rest) + "DH", fSimple, sb, rectRest, stringFormatFar);
                    }
                }
                else
                {
                    Rectangle rectDate = new Rectangle(marginLeft, SPACE, width, 50);
                    g.DrawString(model.Hippo, fTitle, sb, rectDate, stringFormatCenter);

                    SPACE += 65;
                    Rectangle rectReunion = new Rectangle(marginLeft, SPACE, width, 50);
                    g.DrawString(model.Date.ToUpper(), fTitle, sb, rectReunion, stringFormatCenter);

                    SPACE += 65;
                    Rectangle rectCourse = new Rectangle(marginLeft, SPACE, width, 50);
                    g.DrawString(string.Format("REUNION {0} - COURSE {1}", model.Reunion, model.Course), fTitle, sb, rectCourse, stringFormatCenter);

                    SPACE += 65;
                    Rectangle rectParis = new Rectangle(marginLeft, SPACE, width, 50);
                    g.DrawString(model.Paris, fTitle, sb, rectParis, stringFormatCenter);

                    //SPACE += 65;
                    //Rectangle rectFormulation = new Rectangle(marginLeft, SPACE, width, 80);
                    //g.DrawString(model.Formulation, fTitle, sb, rectFormulation, stringFormatCenter);

                    SPACE += 175;
                    Rectangle rectPrice = new Rectangle(marginLeft, SPACE, width, 50);
                    g.DrawString(model.Price, fPrice, sb, rectPrice, stringFormatCenter);
                }
                bmp = ConvertBitmapTo1Bpp(bitmap);
            }
            // ----------------------
            return bmp;
        }

        private static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }
            return result;
        }

        public static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }

        public static Bitmap ConvertBitmapTo1BppOld(Bitmap bitmap)
        {
            int w = bitmap.Width;
            int h = bitmap.Height;
            Bitmap bmpNew = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData data = bmpNew.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            byte[] scan = new byte[(w + 7) / 8];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x % 8 == 0) scan[x / 8] = 0;
                    Color color = bitmap.GetPixel(x, y);
                    if (color.GetBrightness() >= 0.5) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }
                Marshal.Copy(scan, 0, (IntPtr)((long)data.Scan0 + data.Stride * y), scan.Length);
            }

            bmpNew.UnlockBits(data);
            return bmpNew;
        }

        public static Bitmap ConvertBitmapTo1Bpp(Bitmap processingBitmap)
        {
            int Width = processingBitmap.Width;
            int Height = processingBitmap.Height;

            // get total locked pixels count
            int PixelCount = Width * Height;

            // Create rectangle to lock
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            // get source bitmap pixel format size
            int Depth = Bitmap.GetPixelFormatSize(processingBitmap.PixelFormat);
            BitmapData bitmapData = processingBitmap.LockBits(rect, ImageLockMode.ReadWrite, processingBitmap.PixelFormat);

            Bitmap bmpNew = new Bitmap(Width, Height, PixelFormat.Format1bppIndexed);
            BitmapData bmpNewData = bmpNew.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);

            int step = Depth / 8;
            byte[] Pixels = new byte[PixelCount * step];
            IntPtr Iptr = bitmapData.Scan0;

            // Copy data from pointer to array
            Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            processingBitmap.UnlockBits(bitmapData);
            byte[] scan = new byte[(Width + 7) / 8];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int i = ((y * Width) + x) * 4;
                    if (x % 8 == 0) scan[x / 8] = 0;
                    byte b = Pixels[i];
                    byte g = Pixels[i + 1];
                    byte r = Pixels[i + 2];
                    byte a = Pixels[i + 3]; // a
                    Color clr = Color.FromArgb(a, r, g, b);
                    if (clr.GetBrightness() >= 0.5) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }
                Marshal.Copy(scan, 0, (IntPtr)((long)bmpNewData.Scan0 + bmpNewData.Stride * y), scan.Length);
            }
            bmpNew.UnlockBits(bmpNewData);
            return bmpNew;
        }

        public static void UpdateBackgroundImage(Control ctrl, Image newImage)
        {
            try
            {
                if (ctrl.BackgroundImage != null)
                {
                    ctrl.BackgroundImage = null;
                }
                ctrl.BackgroundImage = newImage;
            }
            catch
            { }
        }
        public static Form GetTopMostForm()
        {
            Form topMostForm = null;
            foreach(Form form in Application.OpenForms)
            {
                if (form.Visible && form.Focused)
                {
                    topMostForm = form;
                    break;
                }
            }
            return topMostForm;
        }
    }
}
