using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.Controller;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.Server server = new Controller.Server();
            server.Start();
        }
    }
}
