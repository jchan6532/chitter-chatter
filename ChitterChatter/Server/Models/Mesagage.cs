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

        public static Int64 GetNextMessageID()
        {
            Int64 count = 0;
            Mesagage.lastReadMessageID++;
            string whereClause = string.Format("{0}={1}", MessageConstants.PK_COL, Mesagage.lastReadMessageID.ToString());
            try
            {
                count = StaticDBConnector.GetCount(MessageConstants.TABLENAME, MessageConstants.PK_COL, whereClause);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            if (count != (Int64)1)
            {
                Mesagage.lastReadMessageID--;
            }
            return Mesagage.lastReadMessageID;
        }
    }
}
