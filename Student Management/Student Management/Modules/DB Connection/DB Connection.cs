using log4net;
using Student_Management.Modules.LoggerManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Modules.DB_Connection
{
    internal class DB_Connection
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(DB_Connection));
        private string ConnectionString;
        private SqlConnection SqlConnection;
        private SqlCommand SqlCommand;
        private SqlDataReader SqlDataReader;
        private SqlDataAdapter SqlDataAdapter;

        public string ConnectionString1 { get => ConnectionString; set => ConnectionString = value; }
        public SqlConnection SqlConnection1 { get => SqlConnection; set => SqlConnection = value; }
        public SqlCommand SqlCommand1 { get => SqlCommand; set => SqlCommand = value; }
        public SqlDataReader SqlDataReader1 { get => SqlDataReader; set => SqlDataReader = value; }
        public SqlDataAdapter SqlDataAdapter1 { get => SqlDataAdapter; set => SqlDataAdapter = value; }

        public DB_Connection() 
        {
            ConnectionString = "Data Source=DESKTOP-TCAMG9C\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True";
            SqlConnection = new SqlConnection(ConnectionString);
            SqlCommand = new SqlCommand();
            SqlDataAdapter = new SqlDataAdapter();
        }

        public void OpenConnection()
        {
            SqlConnection.Open();
        }

        public void CloseConnection()
        {
            SqlConnection.Close();
        }
    }
}
