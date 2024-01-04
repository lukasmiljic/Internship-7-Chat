using Chat.Data.Entities.Models;
using Chat.Domain.Actions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace Chat.Presentation
{
    public class UI
    {
        //user authentication
        public static void StartMenu()
        {
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register");
                Console.WriteLine("[0] Exit");

                if (!Helper.ValidateInput(ref userChoice, 6))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        var user = LoginScreen();
                        if (user is null) Helper.TimeOut();
                        else MainMenu(user);
                        break;

                    case 2:
                        RegisterScreen();
                        break;

                    case 0:
                        if (!ExitApplication()) userChoice = -1;
                        break;
                }
            } while (userChoice != 0);
        }
        private static UserChannel? LoginScreen()
        {
            string inputEmail, inputPassword;
            UserChannel user = null;
            do
            {
                Console.Clear();
                Console.WriteLine("Login");
                do
                {
                    Console.Write("Email: ");
                    inputEmail = Console.ReadLine();
                    Helper.LoginVerifyEmailAndPrintMsg(inputEmail, ref user);
                    if (user != null) break;
                } while (true);

                Console.Write("Password: ");
                inputPassword = Console.ReadLine();
                if (inputPassword == user.Password) break;
                return null;
            } while (true);
            Helper.Greetings(user);
            return user;
        }
        private static void RegisterScreen()
        {
            string email, password, captcha, input;
            UserChannel user;
            do
            {
                Console.Clear();
                Console.WriteLine("Register");
                do
                {
                    Console.Write("Email: ");
                    email = Console.ReadLine();
                    if (Helper.RegisterVerifyEmailAndPrintMsg(email)) break;
                } while (true);
                do
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                    if (Helper.RegisterVerifyPasswordAndPrintMsg(password)) break;
                } while (true);
                do
                {
                    Console.Write("Confirm Password: ");
                    input = Console.ReadLine();
                    if (password == input) break;
                    Console.WriteLine("Passwords don't match!");
                } while (true);
                do
                {
                    captcha = Helper.GenerateCaptcha();
                    Console.Write($"Enter captcha [{captcha}]: ");
                    input = Console.ReadLine();
                    if (captcha == input) break;
                    Console.WriteLine("Captcha doesn't match!");
                } while (true);
                break;
            } while (true);
            user = UserAuthentication.CreateNewUser(email, password);
            Helper.Greetings(user);
        }
        private static bool ExitApplication()
        {
            Console.Clear();
            if (Helper.AreYouSure())
            {
                Console.Clear();
                Console.WriteLine("Goodbye...");
                return true;
            }
            else return false;
        }

        //main menu
        public static void MainMenu(UserChannel loggedInUser)
        {
            bool adminFlag = loggedInUser.IsAdmin ? true : false;
            var userChoice = -1;
            int possibleChoices = 3;
            do
            {
                Console.Clear();
                Console.WriteLine($"Main menu - {loggedInUser.Username}");
                Console.WriteLine("[1] Group channels");
                Console.WriteLine("[2] Private messages");
                Console.WriteLine("[3] Settings");
                if (adminFlag) { Console.WriteLine("[4] User management"); possibleChoices = 4; }
                Console.WriteLine("[0] Log out");

                if (!Helper.ValidateInput(ref userChoice, possibleChoices))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        GroupChannelsSubMenu(loggedInUser);
                        break;

                    case 2:
                        PrivateMessagesSubMenu(loggedInUser);
                        break;

                    case 3:
                        SettingsSubMenu(loggedInUser);
                        break;

                    case 4:
                        UserManagmentSubMenu();
                        break;

                    case 0:
                        if (!LogOut()) userChoice = -1;
                        break;
                }
            } while (userChoice != 0);
        }
        private static bool LogOut()
        {
            Console.Clear();
            Console.WriteLine("Log out");
            if (Helper.AreYouSure()) return true;
            else return false;
        }

        //group messages
        private static void GroupChannelsSubMenu(UserChannel user)
        {
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("Group channels");
                Console.WriteLine("[1] Create new channel");
                Console.WriteLine("[2] Enter a channel");
                Console.WriteLine("[3] Show all channels");
                Console.WriteLine("[0] Main menu");

                if (!Helper.ValidateInput(ref userChoice, 3))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        CreateNewGroupScreen(user);
                        break;

                    case 2:
                        EnterGroupScreen(user);
                        break;

                    case 3:
                        AllChannelsScreen(user);
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void CreateNewGroupScreen(UserChannel user)
        {
            string groupTitle;
            bool confirmation;
            do
            {
                Console.Clear();
                Console.WriteLine("Create new Group Channel");
                Console.Write("Group title: ");
                groupTitle = Console.ReadLine();
                Console.WriteLine($"Create new group named {groupTitle}?");
                confirmation = Helper.AreYouSure();
            } while (!confirmation);
            GroupChannelActions.CreateGroupChannel(groupTitle, user);
            Console.WriteLine("Successfully created new group!");
            Helper.PressAnything();
        }
        private static void EnterGroupScreen(UserChannel user)
        {
            string input;
            GroupChannel targetGroup = null;

            Console.Clear();
            Console.WriteLine("Enter Group Channel");
            var groups = Helper.PrintAvailableGroupChannels(user);
            if (groups is null) return;
            do
            {
                Console.WriteLine("Enter group channel title to join channel");
                input = Console.ReadLine();
                targetGroup = GroupChannelActions.AddUserToGroup(user, groups, input);
                if (targetGroup is null)
                {
                    Console.WriteLine($"Error! {input} is not a valid group title");
                    continue;
                }
                else Console.WriteLine("Successfully joined group channel");
                break;
            } while (true);
            Helper.PressAnything();
            ViewGroupMessages(targetGroup, user);
        }
        private static void AllChannelsScreen(UserChannel user)
        {
            GroupChannel targetGroup = null;

            Console.Clear();
            Console.WriteLine("All Group Channels");
            var groups = Helper.PrintUsersGroupChannels(user);
            do
            {
                Console.WriteLine("Enter group channel title to view messages");
                string input = Console.ReadLine();
                targetGroup = groups.FirstOrDefault(x => x.Title == input);
                if (targetGroup is not null) break;
                else Console.WriteLine($"Error! {input} is not a valid group title");
            } while (true);
            ViewGroupMessages(targetGroup, user);
        }
        private static void ViewGroupMessages(GroupChannel groupChannel, UserChannel user)
        {
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine($"{groupChannel.Title}\t\t/exit");
                Helper.PrintGroupMessages(groupChannel);
                Console.Write("New message: ");
                input = Console.ReadLine();
                if (input == "/exit") break;
                Helper.NewMessage(groupChannel, user, input);
            } while (true);
        }

        //private messages
        private static void PrivateMessagesSubMenu(UserChannel user)
        {
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("Private Messages");
                Console.WriteLine("[1] New Message");
                Console.WriteLine("[2] All Chats");
                Console.WriteLine("[0] Main Menu");

                if (!Helper.ValidateInput(ref userChoice, 2))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        NewMessageScreen(user);
                        break;

                    case 2:
                        AllChats();
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void NewMessageScreen(UserChannel user)
        {
            string input;
            UserChannel targetUser;
            Console.Clear();
            Console.WriteLine("New Message\t\t/exit");
            var users = Helper.PrintAllUsers();
            do
            {
                Console.WriteLine("Enter username to view messages with that user");
                input = Console.ReadLine();
                targetUser = users.FirstOrDefault(x => x.Username == input);
                if (targetUser is not null) break;
                else
                {
                    Console.WriteLine($"Error! {input} is not a valid username");
                    Helper.PressAnything();
                }
            } while (true);
            ViewPrivateMessages(targetUser, user);
        }
        private static void ViewPrivateMessages(UserChannel user, UserChannel loggedinUser)
        {
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine($"Private messages - {user.Username}\t\t\t/exit");
                Helper.PrintPrivateMessages(user, loggedinUser);
                Console.Write("New message: ");
                input = Console.ReadLine();
                if (input == "/exit") break;
                Helper.NewMessage(user, loggedinUser, input);
            } while (true);
        }
        private static void AllChats()
        {
            Console.WriteLine("All Chats");
            //domain.userallchats() //list of every user that logged in user has chatted with
            Helper.PressAnything();
        }

        //settings
        private static void SettingsSubMenu(UserChannel user)
        {
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("Settings");
                Console.WriteLine("[1] Change password");
                Console.WriteLine("[2] Change e-mail");
                Console.WriteLine("[0] Main menu");

                if (!Helper.ValidateInput(ref userChoice, 2))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        ChangePasswordScreen(user);
                        break;

                    case 2:
                        ChangeEmailScreen(user);
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void ChangeEmailScreen(UserChannel user)
        {
            Console.Clear();
            Console.WriteLine("Change Email");
            Console.WriteLine($"Old mail: {user.Email}");
            string newMail;
            do
            {
                Console.Write("New mail: ");
                newMail = Console.ReadLine();
                if (Helper.RegisterVerifyEmailAndPrintMsg(newMail)) break;
            } while (true);
            do
            {
                Console.Write("Password : ");
                if (user.Password != Console.ReadLine())
                {
                    Console.WriteLine("Error! Wrong password");
                    continue;
                }
                break;
            } while (true);
            if (!Helper.AreYouSure()) return;
            Helper.UpdateUserEmail(user, newMail);
            Console.WriteLine("Successfully changed Email!");
            Helper.PressAnything();
        }
        private static void ChangePasswordScreen(UserChannel user)
        {
            Console.Clear();
            Console.WriteLine("Change Password");
            string newPassword;
            do
            {
                Console.Write("New password: ");
                newPassword = Console.ReadLine();
                if (Helper.RegisterVerifyPasswordAndPrintMsg(newPassword)) break;
            } while (true);
            do
            {
                Console.Write("Old Password : ");
                if (user.Password != Console.ReadLine())
                {
                    Console.WriteLine("Error! Wrong password");
                    continue;
                }
                break;
            } while (true);
            if (!Helper.AreYouSure()) return;
            Helper.UpdateUserPassword(user, newPassword);
            Console.WriteLine("Successfully changed Password!");
            Helper.PressAnything();
        }

        //admin
        private static void UserManagmentSubMenu()
        {
            string input;
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("User management");
                Console.WriteLine("[1] Promote to admin");
                Console.WriteLine("[2] Change password");
                Console.WriteLine("[3] Change email");
                Console.WriteLine("[0] Main menu");

                if (!Helper.ValidateInput(ref userChoice, 3))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                Console.Clear();
                var users = Helper.PrintAllUsers();
                UserChannel targetUser;
                do
                {
                    Console.WriteLine("Enter username of user you wish to modify");
                    input = Console.ReadLine();
                    targetUser = users.FirstOrDefault(x => x.Username == input);
                    if (targetUser is not null) break;
                    else
                    {
                        Console.WriteLine($"Error! {input} is not a valid username");
                        Helper.PressAnything();
                    }
                } while (true);

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine($"Promote {targetUser.Username} to admin?");
                        if (Helper.AreYouSure())
                        {
                            Helper.PromoteUserToAdmin(targetUser);
                            Console.WriteLine($"Successfully promoted {targetUser.Username} to admin!");
                        }
                        else
                            Console.WriteLine("Promotion canceled");
                        Helper.PressAnything();
                        break;

                    case 2:
                        Console.Write("New password: ");
                        string newPassword = Console.ReadLine();
                        Console.WriteLine($"Change {targetUser} password?");
                        if (Helper.AreYouSure())
                        {
                            Helper.UpdateUserPassword(targetUser, newPassword);
                            Console.WriteLine($"Successfully changed {targetUser.Username} password!");
                        }
                        else
                            Console.WriteLine("Change canceled");
                        Helper.PressAnything();
                        break;

                    case 3:
                        Console.Write("New email: ");
                        string newEmail = Console.ReadLine();
                        Console.WriteLine($"Change {targetUser} email?");
                        if (Helper.AreYouSure())
                        {
                            Helper.UpdateUserEmail(targetUser, newEmail);
                            Console.WriteLine($"Successfully changed {targetUser.Username} email!");
                        }
                        else
                            Console.WriteLine("Change canceled");
                        Helper.PressAnything();
                        break;

                    default:
                        break;
                }
            } while (true);
        }
    }
}