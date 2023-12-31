using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Entities.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public DateTime SendTime { get; set; }
        public MessageChannel Sender { get; set; }
        public MessageChannel Recipient { get; set; }

    }
}
