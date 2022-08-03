using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

using Server.Models;

namespace Server.Controller
{
    public class Server
    {
        public TcpListener listener;
        public IPAddress iPAddress;
        public int port;
        public IPEndPoint localEP;

        public Dictionary<int, User> sessions;
        public volatile bool done;

        public Server()
        {
            try
            {
                this.iPAddress = IPAddress.Parse(ConfigurationManager.AppSettings.Get("LocalIP"));
                this.port = Int32.Parse(ConfigurationManager.AppSettings.Get("LocalPort"));
                this.localEP = new IPEndPoint(this.iPAddress, this.port);
                this.listener = new TcpListener(this.localEP);

                var s = StaticDBConnector.GetConnectionString();
                if(string.IsNullOrEmpty(s) == true)
                {
                    throw new Exception("database failed to connect");
                }

                Mesagage.lastReadMessageID = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            this.sessions = new Dictionary<int, User>();
            this.done = false;
        }
        public void Start()
        {
            try
            {
                this.listener.Start();
                while (this.done == false)
                {
                    if (this.listener.Pending() == true)
                    {
                        TcpClient client = this.listener.AcceptTcpClient();
                        this.HandleClient((object)client);
                    }
                }
                this.listener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            User user = new User();
            user.stream = client.GetStream();
        }
    }
}
