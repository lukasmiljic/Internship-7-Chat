using Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Chat.Domain.Factories
{
    public static class DbContextFactory
    {
        public static ChatDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql(ConfigurationManager.ConnectionStrings["ChatDb"].ConnectionString)
                .Options;

            return new ChatDbContext(options);
        }
    }
}
