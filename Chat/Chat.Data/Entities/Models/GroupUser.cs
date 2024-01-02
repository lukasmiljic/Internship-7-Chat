namespace Chat.Data.Entities.Models
{
    public class GroupUser
    {
        public int GroupChannelId { get; set; }
        public GroupChannel GroupChannel { get; set; }
        public int UserChannelId { get; set; }
        public UserChannel UserChannel { get; set; }
    }
}
