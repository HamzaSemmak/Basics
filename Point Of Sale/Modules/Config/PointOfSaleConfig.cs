using log4net;
using Log4Net;
using Point_Of_Sale.IHM_s.ComposantsGraphique;
using System;
using System.Text;
using System.Windows.Forms;

namespace Point_Of_Sale.Modules.Config
{
    internal class PointOfSaleConfig
    {
        public static readonly ILog logger = LoggerHelper.GetLogger(typeof(PointOfSaleConfig));
        public string PointOfSaleVersion { get; set; }
        public bool develop { get; set; }
        public string IP { get; set; }

        public PointOfSaleConfig() 
        {
            PointOfSaleVersion = "2023";
            develop = true;
            IP = String.Empty;
        }

        public string SetPointOfSaleVersion()
        {
            return PointOfSaleVersion;
        }

        public bool SetTypeMode()
        {
            return develop;
        }

        public void splashForm()
        {
            logger.Info("initialisation Config...");
            try
            {
                if (!develop)
                {
                    logger.Error("Erreur d'initialisation Mode Application...");
                } 
                else if (PointOfSaleVersion != "2023")
                {
                    logger.Error("Erreur d'initialisation Version...");
                }
                else if (IP != "192.0.0.1")
                {
                    logger.Error("Erreur d'initialisation Version...");
                }
            }
            catch
            {
                logger.Info("Connect...");
            }
        }
    }
}
