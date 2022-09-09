using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

using Client.Models;

namespace Client.AppModel
{
    public class ClientAPPMODEL
    {
        public string enteredUserName;
        public string enteredPassword;

        public Dictionary<int, Message> AllMessagesHistory;
        public Dictionary<int, Message> IncomingMessages;

        public TcpClient client;
        public NetworkStream stream;

        public volatile bool done;

        public ClientAPPMODEL()
        {
            this.enteredPassword = string.Empty;
            this.enteredUserName = string.Empty;
            this.AllMessagesHistory = new Dictionary<int, Message>();
            this.client = null;
            this.stream = null;
            this.done = false;
        }

        public void Connect(string userName, string password)
        {
            this.enteredUserName = userName;
            this.enteredPassword = password;

            try
            {
                string host = ConfigurationManager.AppSettings.Get("RemoteIP");
                int port = 0;
                if (Int32.TryParse(ConfigurationManager.AppSettings.Get("RemotePort"), out port) == false)
                {
                    throw new Exception("Remote Port is invalid");
                }

                this.client = new TcpClient(host, port);
                this.stream = this.client.GetStream();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Diconnect()
        {
            this.done = true;
            this.stream.Close();
            this.stream = null;
            this.client.Close();
            this.client = null;
        }
    }
}
