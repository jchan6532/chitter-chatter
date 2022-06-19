using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.Models;

namespace Server.Controller
{
    public class Server
    {
        public void Start()
        {
            var s = StaticDBConnector.GetConnectionString();
        }
    }
}
