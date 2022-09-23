using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Server.Controller;
using Server.Models;
using TCPHelpers.Exceptions;
using TCPHelpers.Services;

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
            catch (LogFileNotExistException LOGFILENOTEXISTEXCEPTION)
            {
                XElement messageXML = new XElement(
                    nameof(LOGFILENOTEXISTEXCEPTION),
                    new XElement("Reason", LOGFILENOTEXISTEXCEPTION.Message)
                    );
                Console.WriteLine(messageXML);
            }
            catch (Exception EXCEPTION)
            {
                if (EXCEPTION.InnerException == null)
                {
                    LoggerService.Log(EXCEPTION.Message, nameof(EXCEPTION));
                }
                else
                {
                    LoggerService.Log(EXCEPTION.Message, nameof(EXCEPTION.InnerException));
                }
            }
        }
    }
}
