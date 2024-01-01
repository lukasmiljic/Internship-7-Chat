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
                        if (!LoginScreen()) Helper.TimeOut();
                        MainMenu();
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
        private static bool LoginScreen()
        {
            //Odabirom logina se od korisnika traži prvo mail, a zatim lozinka.
            //Ukoliko je netočna kombinacija maila i lozinke unesena, korisnika
            //se vraća na login proces ali uz timeout login procesa za 30 sekundi
            //kako bi bili sigurni da nije bot

            string email;
            string password;
            bool inputSuccess;
            do
            {
                Console.Clear();
                Console.WriteLine("Login");
                do 
                {
                    Console.Write("Email: ");
                    email = Console.ReadLine();
                    if (Helper.LoginVerifyEmailAndPrintMsg(email)) break;
                } while (true);

                Console.Write("Password: ");
                password = Console.ReadLine();
                if (Helper.LoginVerifyPasswordAndPrintMsg(password)) break;
                return false;
            } while (true);
            
            Console.Clear();
            Console.WriteLine("Welcome back {user.username}!");  //add
            Helper.PressAnything();
            return true;
        }
        private static void RegisterScreen()
        {
            //Ukoliko odabere registraciju, od korisnika se očekuje unos maila,
            //nove i potvrdne lozinke koje moraju biti identične te kao treći
            //korak generira se i ispiše random string (sastavljena od barem
            //jednog slova i jedne brojke) koja služi kao captcha da se ne
            //registrira bot. Korisnik mora ponoviti unos ispisane generirane
            //riječi. Potrebno je provjeriti ispravnost mail adrese i provjeriti
            //da ne postoji vec neki korisnik s istom mail adresom
            string email;
            string password;
            string captcha = "some txt";
            string input;
            bool inputSuccess;
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
                    //generate rndm string for captcha
                    Console.Write($"Enter captcha[{captcha}]: ");
                    input = Console.ReadLine();
                    if (captcha == input) break;
                    Console.WriteLine("Captcha doesn't match!");
                } while (true);
                break;
            } while (true);
            Console.Clear();
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

        //how do i track who is logged in?
        //main menu
        public static void MainMenu()
        {
            //admin ima dodatno polje za upravljanje s korisnicima
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("[1] Group channels");
                Console.WriteLine("[2] Private messages");
                Console.WriteLine("[3] Settings");
                Console.WriteLine("[0] Log out");

                if (!Helper.ValidateInput(ref userChoice, 3))
                {
                    Helper.ErrorMessage(0);
                    userChoice = -1;
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        GroupChannelsSubMenu();
                        break;

                    case 2:
                        PrivateMessagesSubMenu();
                        break;

                    case 3:
                        SettingsSubMenu();
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
        private static void GroupChannelsSubMenu()
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
                        CreateNewGroupScreen();
                        break;

                    case 2:
                        EnterGroupScreen();
                        break;

                    case 3:
                        AllChannelsScreen();
                        break;

                    default:
                        break;
                }
            } while (userChoice != 0);
        }
        private static void CreateNewGroupScreen()
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
            //create channel and add user
            //domain.creategroupchannel
            //domain.addusertogroup
            Console.WriteLine("Successfully created new group!");
            Helper.PressAnything();
        }
        private static void EnterGroupScreen()
        {
            //modify so it uses /enter command 
            //store user input as string trim last part of said string for groupIdToEnter
            //see https://stackoverflow.com/questions/4603911/extract-the-last-word-from-a-string-using-c-sharp
            //check if part of string is "/enter" if not print error message
            Console.Clear();
            Console.WriteLine("Enter Group Channel\t\t/enter [CHANNEL_ID]\t/exit");
            // print out all the channels that user is not in with the number of users beside it
            //domain.printavailablechannels()
            int groupIdToEnter = 0;
            Helper.EnterNumeric(ref groupIdToEnter);
            //if (domain.entergroupchannel() == false) //check that gropIdToEnter is a valid id
            Console.WriteLine("Successfully joined group channel");
            Helper.PressAnything();
            ViewMessages(groupIdToEnter);
        }
        private static void AllChannelsScreen()
        { 
            Console.Clear();
            Console.WriteLine("All Group Channels");
            //domain.printusersgroups //print all the group channels that the user is in
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
            //potrebno je unjet ponvno lozinku
            throw new NotImplementedException();
        }
        private static void ChangePasswordScreen()
        {
            //prije promjene potrebno je unjeti staru lozinku
            throw new NotImplementedException();
        }
    }
}
