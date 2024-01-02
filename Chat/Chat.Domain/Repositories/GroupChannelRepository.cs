using Chat.Data.Entities;
using Chat.Data.Entities.Models;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public class GroupChannelRepository : BaseRepository
    {
        public GroupChannelRepository(ChatDbContext dbContext) : base(dbContext) 
        {
        }
        public ResponseResultType Add(GroupChannel groupChannel)
        {
            DbContext.GroupChannels.Add(groupChannel);
            
            return SaveChanges();
        }
        public ResponseResultType Delete(int id)
        {
            var groupChannelToDelete = DbContext.GroupChannels.Find(id);
            if (groupChannelToDelete is null) 
            {
                return ResponseResultType.NotFound;
            }
            DbContext.GroupChannels.Remove(groupChannelToDelete);
            return SaveChanges();
        }
        public ResponseResultType Update(GroupChannel groupChannel, int id) 
        {
            var groupChannelToUpdate = DbContext.GroupChannels.Find(id);
            if (groupChannelToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }
            groupChannelToUpdate.Title = groupChannel.Title;

            return SaveChanges();
        }
    }
}
