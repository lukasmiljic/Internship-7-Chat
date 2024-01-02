using System.ComponentModel.DataAnnotations.Schema;

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

    }
}
