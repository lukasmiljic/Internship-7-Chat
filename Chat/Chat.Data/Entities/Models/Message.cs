using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data.Entities.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public DateTime SendTime { get; set; }
        [ForeignKey("MessageChannel")]
        public int SenderFK { get; set; }
        public MessageChannel Sender { get; set; }
        [ForeignKey("MessageChannel")]
        public int RecipientFK { get; set; }
        public MessageChannel Recipient { get; set; }
        //ovdi i u messagechannel je problem 

    }
}
