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

        //public List<Message>? GetMessagesGroupChannels(GroupChannel groupChannel)
        //{
        //    List<Message>? messages = null;
        //    //foreach (var message in groupChannel.SentMessages)
        //    //{
        //    //    messages.Add(message);
        //    //}
        //    foreach (var message in groupChannel.RecievedMessages)
        //    {
        //        messages.Add(message);
        //    }
        //    //messages.Sort((x, y) => x.SendTime.CompareTo(y.SendTime));
        //    return messages;
        //}

        public List<Message>? GetMessagesWithGroup(GroupChannel group)
        {
            //var sentMessages = DbContext.Message
            //    .Where(s => s.SenderFK == sender.MessageChannelID)
            //    .Where(r => r.RecipientFK == recipient.MessageChannelID)
            //    .ToList();

            var messages = DbContext.Message
                .Where(r => r.RecipientFK == group.MessageChannelID)
                .ToList();

            //var messages = sentMessages.Concat(recievedMessages).OrderBy(message => message.SendTime).ToList();

            return messages;
        }
    }
}
