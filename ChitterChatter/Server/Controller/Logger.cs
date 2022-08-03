using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;

namespace Server.Controller
{
    public static class Logger
    {
        private static DateTime LogTime;
        private static string LogFileName = ConfigurationManager.AppSettings.Get("LogFileName");

        public static void Log(string message, string category)
        {
            XElement messageXML = new XElement(
                category,
                new XElement("Reason", message)
                );

            Logger.LogTime = DateTime.Now;
            if (File.Exists(Logger.LogFileName))
            {
                try
                {
                    File.AppendAllText(Logger.LogFileName, $"{Logger.LogTime.ToString()} :\n{messageXML}\n\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
