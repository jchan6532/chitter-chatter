using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Constants.Models;

namespace Server.Models
{
    public class Mesagage
    {
        public int messageID;
        public string contents;
        public int senderID;
        public int receiverID;
        public DateTime time;

        public static int lastReadMessageID;

        public Mesagage()
        {
            this.messageID = MessageConstants.MESSAGE_UNDEFINED;
        }

        public Mesagage(int messageID)
        {
            this.messageID = messageID;
            Mesagage.GetInfoAboutMessage(messageID, this);
        }

        public static Int64 GetNextMessageID()
        {
            Int64 count = 0;
            string whereClause = $"{MessageConstants.PK_COL}={(Mesagage.lastReadMessageID + 1).ToString()}";
            try
            {
                count = StaticDBConnector.GetCount(MessageConstants.TABLENAME, MessageConstants.PK_COL, whereClause);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            if (count == (Int64)1)
            {
                Mesagage.lastReadMessageID++;
            }
            else
            {
                throw new Exception("Reached last message in MESSAGES table");
            }
            return Mesagage.lastReadMessageID;
        }

        public static void GetInfoAboutMessage(int messageID, Mesagage message)
        {
            string whereClause = string.Empty;
            whereClause = $"{MessageConstants.PK_COL}={messageID}";

            message.contents = StaticDBConnector.GetFieldFromEntry(MessageConstants.TABLENAME, MessageConstants.CONTENT_COL, whereClause).ToString();
            message.senderID = (Int32)StaticDBConnector.GetFieldFromEntry(MessageConstants.TABLENAME, MessageConstants.SENDER_FK_COL, whereClause);
            message.receiverID = (Int32)StaticDBConnector.GetFieldFromEntry(MessageConstants.TABLENAME, MessageConstants.RECEIVER_FK_COL, whereClause);
            string time = StaticDBConnector.GetFieldFromEntry(MessageConstants.TABLENAME, MessageConstants.TIME_COL, whereClause).ToString();

            Dictionary<string, string> timeComponentsKVP = new Dictionary<string, string>();
            string[] dateTimeComponents = time.Split(' ');
            foreach (string dateTimeComponent in dateTimeComponents)
            {
                if (dateTimeComponent.Contains("-"))
                {
                    string[] dateComponents = dateTimeComponent.Split('-');
                    if (dateComponents.Length == 3)
                    {
                        timeComponentsKVP.Add("year", dateComponents[0]);
                        timeComponentsKVP.Add("month", dateComponents[1]);
                        timeComponentsKVP.Add("date", dateComponents[2]);
                    }
                }
                else if (dateTimeComponent.Contains(":"))
                {
                    string[] timeComponents = dateTimeComponent.Split(':');
                    if (timeComponents.Length == 3)
                    {
                        timeComponentsKVP.Add("hour", timeComponents[0]);
                        timeComponentsKVP.Add("minute", timeComponents[1]);
                        timeComponentsKVP.Add("second", timeComponents[2]);
                    }
                }
            }

            message.time = new DateTime(
                Int32.Parse(timeComponentsKVP["year"]),
                Int32.Parse(timeComponentsKVP["month"]),
                Int32.Parse(timeComponentsKVP["date"]),
                Int32.Parse(timeComponentsKVP["hour"]),
                Int32.Parse(timeComponentsKVP["minute"]),
                Int32.Parse(timeComponentsKVP["second"])
                );
        }
    }
}
