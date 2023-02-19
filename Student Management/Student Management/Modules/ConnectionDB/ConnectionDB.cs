using log4net;
using Student_Management.Modules.LoggerManager;
using System.Data.SqlClient;

namespace Student_Management.Modules.DB_Connection
{
    internal class DB
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(DB));
        protected string ConnectionString;
        protected SqlConnection sqlConnection;
        protected SqlCommand sqlCommand;
        protected SqlDataReader sqlDataReader;
        protected SqlDataAdapter sqlDataAdapter;

        public string ConnectionString1 { get => ConnectionString; set => ConnectionString = value; }
        public SqlConnection SqlConnection1 { get => sqlConnection; set => sqlConnection = value; }
        public SqlCommand SqlCommand1 { get => sqlCommand; set => sqlCommand = value; }
        public SqlDataReader SqlDataReader1 { get => sqlDataReader; set => sqlDataReader = value; }
        public SqlDataAdapter SqlDataAdapter1 { get => sqlDataAdapter; set => sqlDataAdapter = value; }

        public DB() 
        {
            ConnectionString = "Data Source=DESKTOP-TCAMG9C\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True";
        }

        public void OpenConnection()
        {
            sqlConnection.Open();
        }

        public void CloseConnection()
        {
            sqlConnection.Close();
        }
    }
}
