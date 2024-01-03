using Chat.Data.Entities;
using Chat.Data.Entities.Models;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public class GroupUserRepository : BaseRepository
    {
        public GroupUserRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
        public ResponseResultType Add(GroupUser groupUser)
        {
            DbContext.GroupUsers.Add(groupUser);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var groupUserToDelete = DbContext.GroupUsers.Find(id);
            if (groupUserToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.GroupUsers.Remove(groupUserToDelete);

            return SaveChanges();
        }
    }
}
