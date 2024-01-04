using Chat.Data.Entities.Models;
using Chat.Domain.Actions;
using Chat.Domain.Enums;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;

namespace Chat.Presentation
{
    public class Helper
    {
        //general
        public static void PressAnything() {
            Console.WriteLine("Press anything to continue...");
            Console.ReadKey(true);
        }
        public static bool AreYouSure() {
            do {
                Console.Write("Are you sure [y/n]: ");
                var success = char.TryParse(Console.ReadLine(), out var userChoice);
                if (!success) userChoice='\0';
                if (userChoice == 'y') {
                    return true;
                }
                else if (userChoice == 'n') {
                    return false;
                }
                Console.WriteLine("Invalid input!");
            } while (true);
        }
        public static bool ValidateInput(ref int userChoice, int maxValue) {
            var inputSuccess = int.TryParse(Console.ReadLine(), out userChoice);
            if (inputSuccess == false || userChoice > maxValue || userChoice < 0) return false;
            else return true;
        }
        public static void ErrorMessage(int errorCode){
            Console.Clear();
            switch (errorCode) {
                case 0:
                    Console.WriteLine("Error! Input out of range");
                    break;
            }
            PressAnything();
        }
        internal static void EnterNumeric(ref int groupIdToEnter)
        {
            var isNumeric = int.TryParse(Console.ReadLine(), out groupIdToEnter);
            if (isNumeric == false)
            {
                Console.WriteLine("Error! Enter a number");
                PressAnything();
            }
        }
        //user authentication
        public static void TimeOut()
        {
            Console.Clear();
            Console.WriteLine("Wrong password.\nPlease wait 5sec");
            Thread.Sleep(5000);
        }
        public static bool RegisterVerifyPasswordAndPrintMsg(string password)
        {
            if (password.Length < 1)
            {
                Console.WriteLine("Password field can't be empty!");
                return false;
            }
            return true;
        }
        public static bool RegisterVerifyEmailAndPrintMsg(string inputEmail)
        {
            switch (UserAuthentication.ValidateEmail(inputEmail))
            {
                case EmailResultType.NotFound:
                    return true;

                case EmailResultType.InvalidFormat:
                    Console.WriteLine("Error! Invalid email format");
                    PressAnything();
                    break;

                case EmailResultType.InvalidLength:
                    Console.WriteLine("Error! Email field cant be empty");
                    PressAnything();
                    break;

                default:
                    Console.WriteLine("Error! Email is already taken");
                    PressAnything();
                    break;
            }
            return false;
        }
        public static UserChannel? LoginVerifyEmailAndPrintMsg(string inputEmail, ref UserChannel user)
        {
            switch (UserAuthentication.ValidateEmail(inputEmail, ref user))
            {
                case EmailResultType.NotFound:
                    Console.WriteLine($"Error! No user with email {inputEmail}");
                    PressAnything();
                    break;

                case EmailResultType.InvalidFormat:
                    Console.WriteLine("Error! Invalid email format");
                    PressAnything();
                    break;

                case EmailResultType.InvalidLength:
                    Console.WriteLine("Error! Email field cant be empty");
                    PressAnything();
                    break;

                default:
                    return user;
            }
            return null;
        }
        public static void Greetings(UserChannel user)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {user.Username}!");
            PressAnything();
        }
        public static string GenerateCaptcha()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static List<GroupChannel> PrintAvailableGroupChannels(UserChannel user)
        {
            var userChannelRepository = new UserChannelRepository(DbContextFactory.GetDbContext());
            var groups = userChannelRepository.GetUsersAvailableGroupChannels(user);
            if (groups is null)
            {
                Console.WriteLine("No available group channels");
                PressAnything();
                return null;
            }
            foreach( var group in groups)
            {
                Console.WriteLine($"{group.Title}");
            }
            return groups;
        }
        public static List<GroupChannel> PrintUsersGroupChannels(UserChannel user)
        {
            var userChannelRepository = new UserChannelRepository(DbContextFactory.GetDbContext());
            var groups = userChannelRepository.GetUsersGroupChannels(user);
            if (groups is null)
            {
                Console.WriteLine("User is not in any group channels");
                PressAnything();
                return null;
            }
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Title}");
            }
            return groups;
        }

        public static void PrintGroupMessages(GroupChannel groupChannel)
        {
            var groupChannelRepository = new GroupChannelRepository(DbContextFactory.GetDbContext());
            var messages = groupChannelRepository.GetMessagesGroupChannels(groupChannel);
            if (messages is null)
            {
                Console.WriteLine("sNo messages yet");
                return;
            }
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.Body}");
            }
            return;
        }
        public static void NewMessage(MessageChannel reciever, UserChannel sender, string message)
        {
            var messageRepository = new MessageRepository(DbContextFactory.GetDbContext());
            messageRepository.Add(new Message() { Body = message, RecipientFK = reciever.MessageChannelID, SenderFK = sender.MessageChannelID});
        }

        public static List<UserChannel>? PrintAllUsers()
        {
            var userChannelRepository = new UserChannelRepository(DbContextFactory.GetDbContext());
            var users = userChannelRepository.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username}");
            }
            return users;
        }

        public static void PrintPrivateMessages(UserChannel recipient, UserChannel sender)
        {
            var userChannelRepository = new UserChannelRepository(DbContextFactory.GetDbContext());
            var messages = userChannelRepository.GetMessagesWithUser(recipient, sender);
            if (messages is null)
            {
                Console.WriteLine("\nNo messages yet");
                return;
            }
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.Body}");
            }
            return;
        }
    }
}