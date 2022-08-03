using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using Constants.Models;

namespace Server.Models
{
    public class User
    {
        public int userID;
        public string userName;
        public string password;
        public bool isAdmin;
        public bool isLoggedIn;
        public List<int> sentMessageIDs;
        public List<int> receivedMessageIDs;

        public NetworkStream stream;

        public User()
        {
            this.userID = UserConstants.USER_UNDEFINED;
            this.userName = null;
            this.password = null;
            this.isAdmin = false;
            this.stream = null;
            this.isLoggedIn = false;

            User.GetUserSentMessages(this);
        }

        public User(string userName, string password)
        {
            this.userID = UserConstants.USER_UNDEFINED;
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
            this.isAdmin = false;
            this.stream = null;
            this.isLoggedIn = false;

            User.GetUserSentMessages(this);
        }

        public static void RegisterNewUser(string userName, string password, int isAdmin, string firstName, string lastName)
        {
            string[] registerCols = { UserConstants.USERNAME_COL, UserConstants.PASSWORD_COL, UserConstants.ISADMIN_COL, UserConstants.FIRSTNAME_COL, UserConstants.LASTNAME_COL};
            string[] registerVals = { $"'{userName}'", $"'{password}'", isAdmin.ToString(), $"'{firstName}'", $"'{lastName}'" };
            StaticDBConnector.Create(UserConstants.TABLENAME, registerCols, registerVals);
        }

        public static void GetUserSentMessages(User user)
        {
            user.sentMessageIDs = new List<int>();
        }
    }
}
