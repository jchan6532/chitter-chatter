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
        #region Public Properties

        public string EnteredUsername { get; set; }
        public string EnteredPassword { get; set; }

        public Dictionary<int, Message> AllMessagesHistory;
        public Dictionary<int, Message> IncomingMessages;

        public TcpClient TcpClient;
        public NetworkStream Stream;

        public volatile bool done;

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor, Initializes a new instance of the <see cref="ClientAPPMODEL"/> class.
        /// </summary>
        public ClientAPPMODEL()
        {
            this.EnteredPassword = string.Empty;
            this.EnteredUsername = string.Empty;
            this.AllMessagesHistory = new Dictionary<int, Message>();
            this.IncomingMessages = new Dictionary<int, Message>();
            this.TcpClient = null;
            this.Stream = null;
            this.done = false;
        }

        #endregion


        #region Public Methods

        public void Connect(string userName, string password)
        {
            this.EnteredUsername = userName;
            this.EnteredPassword = password;

            try
            {
                string host = ConfigurationManager.AppSettings.Get("RemoteIP");
                int port = 0;
                if (Int32.TryParse(ConfigurationManager.AppSettings.Get("RemotePort"), out port) == false)
                {
                    throw new Exception("Remote Port is invalid");
                }

                this.TcpClient = new TcpClient(host, port);
                this.Stream = this.TcpClient.GetStream();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Diconnect()
        {
            this.done = true;
            this.Stream.Close();
            this.Stream = null;
            this.TcpClient.Close();
            this.TcpClient = null;
        }

        #endregion
    }
}
