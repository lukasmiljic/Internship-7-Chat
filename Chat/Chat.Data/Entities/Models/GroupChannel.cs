namespace Chat.Data.Entities.Models
{
    public class GroupChannel : MessageChannel
    {
        public string Title { get; set; }
        public ICollection<GroupUser> Users { get; set; } = new List<GroupUser>();
    }
}
