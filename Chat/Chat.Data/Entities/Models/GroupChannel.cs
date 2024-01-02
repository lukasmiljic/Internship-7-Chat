namespace Chat.Data.Entities.Models
{
    public class GroupChannel : MessageChannel
    {
        public string Title { get; set; }
        public ICollection<UserChannel> Users { get; set; }
    }
}
