namespace Chat.Data.Entities.Models
{
    public class UserChannel : MessageChannel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; } = false;
        public ICollection<GroupChannel> GroupChannels { get; set; }
    }
}
