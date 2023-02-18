using Guna.UI2;
using log4net;
using log4net.Repository.Hierarchy;
using Student_Management.IHM_s;
using Student_Management.IHM_s.ComposentsGraphique;
using Student_Management.Modules.DB_Connection;
using Student_Management.Modules.LoggerManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(Program));

        
        public static LoadingForm _Splash()
        {
            return new LoadingForm();
        }

        public static void CloseApplication()
        {
            Application.Exit();
            logger.Info("Exit Application Student Management System");
        }

        public static void LaunchAuthentification()
        {
            Authentification authentification = new Authentification();
            authentification.Show();
        }

        [STAThread]
        static void Main()
        {
            logger.Info("Start Application Student Management System");
            DB_Connection _ConnectionDB = new DB_Connection();
            _ConnectionDB.OpenConnection();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_Splash());
        }
    }
}
