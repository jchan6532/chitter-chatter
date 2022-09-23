using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

using Server.Models;
using TCPHelpers.Exceptions;
using TCPHelpers.Services;

namespace Server.Controller
{
    public class Server
    {
        #region Public Properties

        public TcpListener listener;
        public IPAddress iPAddress;
        public int port;
        public IPEndPoint localEP;

        public Dictionary<int, User> sessions;
        public Dictionary<int, Mesagage> messages;
        public volatile bool done;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <exception cref="Server.Exceptions.LogFileNotExistException">log file does not exist</exception>
        public Server()
        {
            if (!LoggerService.CheckLogFile())
            {
                throw new LogFileNotExistException("log file does not exist");
            }
            try
            {
                LoggerService.ClearLogFile();
                LoggerService.Log("Server initiating", "ACTION");

                this.iPAddress = IPAddress.Parse(ConfigurationManager.AppSettings.Get("LocalIP"));
                this.port = Int32.Parse(ConfigurationManager.AppSettings.Get("LocalPort"));
                this.localEP = new IPEndPoint(this.iPAddress, this.port);
                this.listener = new TcpListener(this.localEP);

                string connectionStr = StaticDBConnector.GetConnectionString();
            }
            catch (Exception e)
            {
                throw e;
            }
 
            this.sessions = new Dictionary<int, User>();

            Mesagage.lastReadMessageID = 0;
            this.messages = new Dictionary<int, Mesagage>();
            try
            {
                int newID = 0;
                do
                {
                    newID = (Int32)Mesagage.GetNextMessageID();
                    this.messages.Add(newID, new Mesagage(newID));
                } while (true);
            }
            catch (Exception e)
            {
                LoggerService.Log(e.Message, "EXCEPTION");
            }


            this.done = false;
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                LoggerService.Log("Server starting", "ACTION");

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

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            User user = new User();
            user.stream = client.GetStream();
        }

        #endregion
    }
}
