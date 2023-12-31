using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Entities.Models
{
    public class MessageChannel
    {
        public int MessageChannelID { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> RecievedMessages { get; set; }
    }
}
