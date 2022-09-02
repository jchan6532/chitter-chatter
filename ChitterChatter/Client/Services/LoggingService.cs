using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace Client.Services
{
    public static class LoggingService
    {
        private static DateTime LogTime;
        private static string LogFileName = ConfigurationManager.AppSettings.Get("LogFileName");

        public static void Log(string message, string category)
        {
            XElement messageXML = new XElement(
                category,
                new XElement("Reason", message)
                );

            LoggingService.LogTime = DateTime.Now;
            if (File.Exists(LoggingService.LogFileName))
            {
                try
                {
                    File.AppendAllText(LoggingService.LogFileName, $"{LoggingService.LogTime.ToString()} :\n{messageXML}\n\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
