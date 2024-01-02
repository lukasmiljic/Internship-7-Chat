using Chat.Domain.Enums;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using System.Net.Mail;

namespace Chat.Domain.Actions
{
    public class UserAuthentication
    {
        public static EmailResultType ValidateEmail(string inputEmail)
        {
            if (inputEmail.Length < 1) return EmailResultType.InvalidLength;
            if (!IsValid(inputEmail)) return EmailResultType.InvalidFormat;
            var userChannelRepository = new UserChannelRepository(DbContextFactory.GetDbContext());
            if (userChannelRepository.GetUserByEmail(inputEmail) is null) return EmailResultType.NotFound;
            return EmailResultType.Valid;
        }
        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
