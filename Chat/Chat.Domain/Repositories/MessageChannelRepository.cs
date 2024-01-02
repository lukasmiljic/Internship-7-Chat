using Chat.Data.Entities;
using Chat.Data.Entities.Models;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public class MessageChannelRepository : BaseRepository
    {
        public MessageChannelRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}
