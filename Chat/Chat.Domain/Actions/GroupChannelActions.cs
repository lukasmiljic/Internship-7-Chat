using Chat.Data.Entities.Models;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;

namespace Chat.Domain.Actions
{
    public class GroupChannelActions
    {
        public static void CreateGroupChannel(string groupChanelTitle, UserChannel user)
        {
            var group = new GroupChannel() { Title = groupChanelTitle };
            var groupChannelRepository = new GroupChannelRepository(DbContextFactory.GetDbContext());
            groupChannelRepository.Add(group);
            var groupUserRepository = new GroupUserRepository(DbContextFactory.GetDbContext());
            groupUserRepository.Add(new GroupUser() { GroupChannel = group, GroupChannelId = group.MessageChannelID, UserChannel = user, UserChannelId = user.MessageChannelID});
        }
    }
}
