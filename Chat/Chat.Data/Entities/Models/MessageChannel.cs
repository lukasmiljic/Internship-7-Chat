namespace Chat.Data.Entities.Models
{
    public class MessageChannel
    {
        public int MessageChannelID { get; set; }
        
        public ICollection<Message> RecievedMessages { get; set; } = new List<Message>();
    }
}
