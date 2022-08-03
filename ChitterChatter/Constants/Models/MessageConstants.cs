using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants.Models
{
    public static class MessageConstants
    {
        public static int MESSAGE_UNDEFINED => -1;

        public static string TABLENAME => "messages";
        public static string PK_COL => "messageID";
        public static string CONTENT_COL => "contents";
        public static string SENDER_FK_COL => "senderID";
        public static string RECEIVER_FK_COL => "receiverID";
        public static string TIME_COL => "time";
    }
}
