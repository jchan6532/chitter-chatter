using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.Controller;
using Server.Models;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Controller.Server server = new Controller.Server();
                server.Start();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, "EXCEPTION");
            }
        }
    }
}
