using Chat.Data.Entities;
using Chat.Data.Entities.Models;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public class UserChannelRepository : BaseRepository
    {
        public UserChannelRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
        public ResponseResultType Add(UserChannel userChannel)
        {
            DbContext.UserChannels.Add(userChannel);

            return SaveChanges();
        }
        public ResponseResultType Delete(int id)
        {
            var userChannelToDelete = DbContext.UserChannels.Find(id);
            if (userChannelToDelete is null)
            {
                return ResponseResultType.NotFound;
            }
            DbContext.UserChannels.Remove(userChannelToDelete);
            return SaveChanges();
        }
        public ResponseResultType Update(UserChannel userChannel, int id)
        {
            var userChannelToUpdate = DbContext.UserChannels.Find(id);
            if (userChannelToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }
            userChannelToUpdate.Email = userChannel.Email;
            userChannelToUpdate.Password = userChannel.Password;
            userChannelToUpdate.Username = userChannel.Username;
            userChannelToUpdate.IsAdmin = userChannel.IsAdmin;

            return SaveChanges();
        }
        public UserChannel? GetUserByEmail (string email)
        {
            if (!DbContext.UserChannels.Any(x => x.Email == email)) return null;
            else return DbContext.UserChannels.First(x => x.Email == email);
        }
        public List<GroupChannel>? PrintUsersGroupChannels(UserChannel user)
        {
            var groups = DbContext.GroupChannels
                .Where(g => !g.Users.Any(gu => gu.UserChannelId == user.MessageChannelID))
                .ToList();

            if (groups.Count() == 0) return null;

            return groups;
            //foreach (var group in user.GroupChannels)
            //{
            //    Console.WriteLine($"[{group.GroupChannel.MessageChannelID}] {group.GroupChannel.Title}");
            //}
            //return true;
        }
    }
}
