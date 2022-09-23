using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;

namespace TCPHelpers.Services
{
    public static class LoggerService
    {
        private static DateTime LogTime;
        private static string LogFileName = ConfigurationManager.AppSettings.Get("LogFileName");

        public static void Log(string message, string category)
        {
            XElement messageXML = new XElement(
                category,
                new XElement("Reason", message)
                );

            LoggerService.LogTime = DateTime.Now;
            if (File.Exists(LoggerService.LogFileName))
            {
                try
                {
                    File.AppendAllText(LoggerService.LogFileName, $"{LoggerService.LogTime.ToString()} :\n{messageXML}\n\n");
                }
                catch (UnauthorizedAccessException uae)
                {
                    Console.WriteLine(uae.Message);
                    throw uae;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }
        }

        public static void ClearLogFile()
        {
            if (File.Exists(LoggerService.LogFileName))
            {
                try
                {
                    File.WriteAllText(LoggerService.LogFileName, string.Empty);
                }
                catch (UnauthorizedAccessException uae)
                {
                    Console.WriteLine(uae.Message);
                    throw uae;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }
        }

        public static bool CheckLogFile()
        {
            if (File.Exists(LoggerService.LogFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
