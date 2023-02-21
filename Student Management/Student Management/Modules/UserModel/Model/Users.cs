using log4net;
using Student_Management.Modules.DB_Connection;
using System.Collections;

namespace Student_Management.Modules.UserModel.Model
{
    internal class Users : DB
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Users));
        private string name;
        protected string password;
        protected string email;
        protected string phone;
        protected string status;
        protected int checks;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public string Password 
        { 
            get => password; 
            set => password = value; 
        }
        public string Email 
        { 
            get => email; 
            set => email = value; 
        }
        public string Phone 
        { 
            get => phone;
            set => phone = value; 
        }
        public string Status 
        { 
            get => status; 
            set => status = value; 
        }
        public int Checks 
        { 
            get => checks; 
            set => checks = value; 
        }

        public Users() { }

        public Users(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }

        public Users(string name, string password, string email, string phone)
        {
            this.Name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.Checks = 1;
            this.status = "InLocked";
        }
    }
}
