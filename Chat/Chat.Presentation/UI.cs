using Chat.Data.Entities.Models;
using Chat.Domain.Actions;

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
            user = UserAuthentication.CreateNewUser(email,password);
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
                        PrivateMessagesSubMenu();
                        break;

                    case 3:
                        SettingsSubMenu();
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

            Console.Clear();
            Console.WriteLine("Enter Group Channel");
            var groups = Helper.PrintAvailableGroupChannels(user);
            if (groups is null) return;
            do
            {
                Console.WriteLine("Enter group channel title to join channel");
                input = Console.ReadLine();
                if (!GroupChannelActions.AddUserToGroup(user, groups, input))
                {
                    Console.WriteLine($"Error! {input} is not a valid group title");
                    continue;
                }
                else Console.WriteLine("Successfully joined group channel");
                break;
            } while (true);
            Helper.PressAnything();
            //ViewMessages(groupIdToEnter);
        }

        private static void AllChannelsScreen(UserChannel user)
        { 
            Console.Clear();
            Console.WriteLine("All Group Channels");
            Helper.PrintUsersGroupChannels(user);
            Helper.PressAnything();
        }
        private static void ViewMessages(int channelID)
        {
            //Console.Write("${channelID.Title} \t\t/exit")
            //domain.ViewMessages(channelId)
            Console.Write("New message: ");
            //similar code as in entergroup for /enter
        }
        
        //private messages
        private static void PrivateMessagesSubMenu()
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
                        NewMessageScreen();
                        break;

                    case 2:
                        AllChats();
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void NewMessageScreen()
        {
            Console.WriteLine("New Message\t\t/enter [USER_ID]\t/exit");
            //domain.allusers() //print out every user
            //similar code to entergroupscreen()
            //call viewmessages() after getting userid
        }
        private static void AllChats()
        {
            Console.WriteLine("All Chats");
            //domain.userallchats() //list of every user that logged in user has chatted with
            Helper.PressAnything();
        }

        //settings
        private static void SettingsSubMenu()
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
                        ChangePasswordScreen();
                        break;

                    case 2:
                        ChangeEmailScreen();
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void ChangeEmailScreen()
        {
            Console.WriteLine("Change Email");
            //Console.WriteLine($"Old mail: {user.mail}")
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
                //if(user.password == console.readline()) break;
                break;
            } while (true);
            if (!Helper.AreYouSure()) return;
            //domain.changemail()
            Console.WriteLine("Successfully changed Email!");
            Helper.PressAnything();
        }
        private static void ChangePasswordScreen()
        {
            Console.WriteLine("Change Password");
            //Console.WriteLine($"Old mail: {user.mail}")
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
                //if(user.password == console.readline()) break;
                break;
            } while (true);
            if (!Helper.AreYouSure()) return;
            //domain.changemail()
            Console.WriteLine("Successfully changed Password!");
            Helper.PressAnything();
        }

        //admin
        private static void UserManagmentSubMenu()
        {
            throw new NotImplementedException();
        }
    }
}