using log4net;
using Log4Net;
using Point_Of_Sale.IHM_s;
using Point_Of_Sale.IHM_s.ComposantsGraphique;
using Point_Of_Sale.Modules.Config;
using System;
using System.Windows.Forms;

namespace Point_Of_Sale
{
    internal static class Program
    {
        private static ILog Logger = LoggerHelper.GetLogger(typeof(Program));
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PointOfSaleConfig PointOfSaleConfig = new PointOfSaleConfig();
            PointOfSaleConfig.IP = "192.0.0.1";
            Application.EnableVisualStyles();
            Logger.Info("Start Application Point Of Sale");
            PointOfSaleConfig.splashForm();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadingForm _spalsh = new LoadingForm();
            Application.Run(_spalsh);
        }

        public static void CloseApp()
        {
            System.Windows.Forms.Application.Exit();
            Logger.Info("Exit Application Point Of Sale");
        }

        public static void LaunchAuthentification()
        {
            Authentification _Authentification = new Authentification();
            _Authentification.Show();
        }
    }
}
