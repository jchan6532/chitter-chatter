using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants.Controllers
{
    public static class ServerResponseCodes
    {
        public static int LOGIN_SUCCESS => 1;
        public static int USER_NOTRECOGNIZED => -1;
        public static int INCORRECT_PASSWORD => -2;
    }
}
