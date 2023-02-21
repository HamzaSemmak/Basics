using System;
using System.Collections;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Guna.UI2.WinForms;

namespace Student_Management.Modules.UserModel.Controller
{
    internal class UsersController : Users
    {
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

        public int Authentification(string newName, string newPassword)
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
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(P1);
            this.sqlCommand.Parameters.Add(P2);
            this.sqlCommand.Parameters.Add(Output);
            OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            CloseConnection();
            return Convert.ToInt32(Output.Value);
        }

    }
}
