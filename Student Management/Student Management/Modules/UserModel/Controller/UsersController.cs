using System;
using System.Collections;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Student_Management.Modules.UserModel.Controller
{
    internal class UsersController : Users
    {
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

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public string Authentification(string newName, string newPassword)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "Authentification",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter P1 = new SqlParameter()
            {
                ParameterName = "@UserName",
                SqlDbType = SqlDbType.VarChar,
                Value = newName,
                Direction = ParameterDirection.Input
            };
            SqlParameter P2 = new SqlParameter()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Value = newPassword,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(P1);
            this.sqlCommand.Parameters.Add(P2);
            this.sqlCommand.Parameters.Add(Output);
            OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            CloseConnection();
            return Output.Value.ToString();
        }

    }
}
