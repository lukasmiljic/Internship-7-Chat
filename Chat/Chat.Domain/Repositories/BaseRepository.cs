using Chat.Data.Entities;
using TodoApp.Domain.Enums;

namespace Chat.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ChatDbContext DbContext;

        protected BaseRepository(ChatDbContext dbContext)
        {
            DbContext = dbContext;
        }
        protected ResponseResultType SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;
            if (hasChanges)
                return ResponseResultType.Success;

            return ResponseResultType.NoChanges;
        }
    }
}
