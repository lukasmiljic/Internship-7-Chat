using Chat.Data.Entities.Models;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;

namespace Chat.Domain.Actions
{
    public class GroupChannelActions
    {
        public static bool AddUserToGroup(UserChannel user, List<GroupChannel> groups, string? input)
        {
            var targetGroup = groups.FirstOrDefault(x => x.Title == input);
            if (targetGroup is null)
                return false;

            var groupUserRepository = new GroupUserRepository(DbContextFactory.GetDbContext());
            groupUserRepository.Add(new GroupUser(){ GroupChannelId = targetGroup.MessageChannelID, UserChannelId = user.MessageChannelID});
            return true;
        }

        public static void CreateGroupChannel(string groupChanelTitle, UserChannel user)
        {
            var group = new GroupChannel() { Title = groupChanelTitle };
            var groupChannelRepository = new GroupChannelRepository(DbContextFactory.GetDbContext());
            groupChannelRepository.Add(group);
            var groupUserRepository = new GroupUserRepository(DbContextFactory.GetDbContext());
            groupUserRepository.Add(new GroupUser() 
            {   
                //GroupChannel = group, 
                GroupChannelId = group.MessageChannelID, 
                //UserChannel = user, 
                UserChannelId = user.MessageChannelID
            });
        }
    }
}
