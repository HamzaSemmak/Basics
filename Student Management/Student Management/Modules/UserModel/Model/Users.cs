using log4net;
using Student_Management.Modules.DB_Connection;
using System.Collections;

namespace Student_Management.Modules.UserModel.Model
{
    internal class Users : DB
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Users));
        private string name;
        private string password;
        private string email;
        private string phone;
        private string status;
        private int checks;

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Status { get => status; set => status = value; }
        public int Checks { get => checks; set => checks = value; }

        public Users() { }

        public Users(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }

        public Users(string name, string password, string email, string phone)
        {
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.Checks = 1;
            this.status = "InLocked";
        }

        public ArrayList User()
        {
            ArrayList User = new ArrayList();
            User.Add($"Name : {this.name}");
            User.Add($"Password : {this.password}");
            User.Add($"Email : {this.email}");
            User.Add($"Phone : {this.phone}");
            User.Add($"Status : {this.status}");
            User.Add($"Checks : {this.checks}");
            return User;
        }




        //public void Test(string newName, string newPassword)
        //{
        //    try
        //    {
        //        this.sqlConnection = new SqlConnection(this.ConnectionString);
        //        this.sqlCommand = new SqlCommand()
        //        {
        //            CommandText = "Authentification",
        //            Connection = this.sqlConnection,
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        SqlParameter P1 = new SqlParameter()
        //        {
        //            ParameterName = "@UserName",
        //            SqlDbType = SqlDbType.VarChar,
        //            Value = newName,
        //        };
        //        SqlParameter P2 = new SqlParameter()
        //        {
        //            ParameterName = "@Password",
        //            SqlDbType = SqlDbType.VarChar,
        //            Value = newPassword,
        //        };
        //        this.sqlCommand.Parameters.Add(P1);
        //        this.sqlCommand.Parameters.Add(P2);
        //        OpenConnection();
        //        this.sqlCommand.ExecuteNonQuery();
        //        Logger.Info("(1 row(s) affected) To User Table");
        //        CloseConnection();
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex.Message);
        //    }
        //}
    }
}
