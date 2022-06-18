using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants.Models
{
    public static class UserConstants
    {
        public static int USER_UNDEFINED => -1;
        public static int USER_ADMIN => 1;

        public static string TABLENAME => "users";
        public static string PK_COL => "userID";
        public static string USERNAME_COL => "userName";
        public static string PASSWORD_COL => "password";
        public static string ISADMIN_COL => "isAdmin";
    }
}
