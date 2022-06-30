using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User
    {
        private int userID;
        public string userName;
        public string password;
        public bool isAdmin;

        public User()
        {
            this.userName = null;
            this.password = null;
        }

        public User(string userName, string password)
        {
            this.userName = null;
            if(string.IsNullOrEmpty(userName) == false)
            {
                this.userName = userName;
            }

            this.password = null;
            if (string.IsNullOrEmpty(password) == false)
            {
                this.password = userName;
            }
        }
    }
}
