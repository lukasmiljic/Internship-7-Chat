using Chat.Data.Entities;
using Chat.Data.Entities.Models;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public class MessageRepository : BaseRepository
    {
        public MessageRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
        public ResponseResultType Add(Message message)
        {
            DbContext.Message.Add(message);

            return SaveChanges();
        }
    }
}
