using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Presentation
{
    public class UI
    {
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
                        LoginScreen();
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
            string passwordAgain;
            string captcha;
            bool inputSuccess;
            do
            {
                Console.Clear();
                Console.WriteLine("Register");

                do
                {
                    Console.WriteLine("Email: ");
                    email = Console.ReadLine();
                    if (RegisterVerifyEmailAndPrintMsg(email)) continue;
                } while (true);
                do
                {
                    Console.WriteLine("Password: ");
                    password = Console.ReadLine();
                    if (RegisterVerifyPasswordAndPrintMsg(password)) continue;
                } while (true);
                do
                {
                    Console.WriteLine("Confirm Password: ");
                    passwordAgain = Console.ReadLine();
                    if (password == passwordAgain) continue;
                    Console.WriteLine("Passwords don't match!");
                } while (true);
                do
                {
                    //generate rndm string for captcha
                    Console.WriteLine($"Enter captcha[{captcha}]: ");
                    passwordAgain = Console.ReadLine();
                    if (password == passwordAgain) continue;
                    Console.WriteLine("Captcha doesn't match!");
                } while (true);
            } while (true);
            //MainMenu(userID);
        }
        //razbit verify and print i to u odvojene helper fje 
        private static bool RegisterVerifyPasswordAndPrintMsg(string password)
        {
            if (password.Length < 1)
            {
                Console.WriteLine("Password field can't be empty!");
                return false;
            }
            return true;
        }

        private static bool RegisterVerifyEmailAndPrintMsg(string email)
        {
            int returnValue = 0;
            //domain.VerifyEmail(inputEmail)
            switch (returnValue)
            {
                case 1:
                    //email length < 1 cant leave email field empty
                    break;

                case 2:
                    //email doesnt  match pattern text@text.com
                    break;

                case 3:
                    //user with that email already exists
                    break;

                default:
                    return true;
            }
            return false;
        }

        private static void LoginScreen()
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
                    Console.WriteLine("Email: ");
                    email = Console.ReadLine();
                    if (LoginVerifyEmailAndPrintMsg(email)) continue;
                } while (true);

                Console.WriteLine("Password: ");
                password = Console.ReadLine();
                if (LoginVerifyPasswordAndPrintMsg(password)) continue;
                //korisnik ide u time out
            } while (true);
            //MainMenu(userID);
        }

        private static bool LoginVerifyPasswordAndPrintMsg(string password)
        {
            int temp = 5;
            switch (temp)//domain.VerifyPassword(password)
            {
                case 1:
                    //password length < 1 cant leave email field empty
                    break;

                case 2:
                    //password doesnt match
                    break;

                default:
                    return true;
            }
            return false;
        }
        private static bool LoginVerifyEmailAndPrintMsg(string inputEmail)
        {
            int returnValue = 0;
            //domain.VerifyEmail(inputEmail)
            switch (returnValue)
            {
                case 1:
                    //email length < 1 cant leave email field empty
                    break;

                case 2:
                    //email doesnt  match pattern text@text.com
                    break;

                case 3:
                    //no user with that email
                    break;

                default:
                    return true;
            }
            return false;
        }

        //saljem mail pa preko njega znam koje poruke ucitavat
        public static void MainMenu(string email)
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
            if (Helper.AreYouSure()) return true;
            else return false;
        }

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

                    case 0:
                        if (!LogOut()) userChoice = -1;
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

        private static void PrivateMessagesSubMenu()
        {
            var userChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("Private messages");
                Console.WriteLine("[1] New message");
                Console.WriteLine("[2] All chats");
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
                        NewMessageScreen();
                        break;

                    case 2:
                        AllChats();
                        break;

                    case 0:
                        if (!LogOut()) userChoice = -1;
                        break;
                }
            } while (userChoice != 0);
        }

        private static void AllChats()
        {
            //ispis svih korisnika s kojim je korisnik komunicirao
            throw new NotImplementedException();
        }

        private static void NewMessageScreen()
        {
            //popis svih korisnika odabir jednog od njih otvara se privatni chat s korisnikom
            //ako je vec prico sa njima prikazuju se stara poruke
            //moguce je tipkati novu poruku
            throw new NotImplementedException();
        }

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

                    case 0:
                        if (!LogOut()) userChoice = -1;
                        break;
                }
            } while (userChoice != 0);
        }

        private static void AllChannelsScreen()
        {
            //ispis svih kanala u koje je korisnik usao zajedno s
            //naredbom /open korisnik moze otvoriti chat
            throw new NotImplementedException();
        }

        private static void EnterGroupScreen()
        {
            //prikazat listu svih kanala u koje korisnik nije usao
            //zajedno s brojem korisnika u kanalu
            //naredbom /enter korisnik moze uc u neki od kanala
            throw new NotImplementedException();
        }

        private static void CreateNewGroupScreen()
        {
            //unosi se naziv kanala; korisnik koji kreaira kanal
            //automatski je dio kanala
            throw new NotImplementedException();
        }

        private static void ViewMessages()
        {
            //otvara se ulaskom u neki chat
            //za izlaz nazad se koristi komanda /exit
            //ispis poruka kronoloskim redosljedom
        }

        private static bool ExitApplication()
        {
            Console.Clear();
            if (Helper.AreYouSure())
            {
                Console.WriteLine("Goodbye...");
                Thread.Sleep(1000);
                return true;
            }
            else return false;
        }
    }
}
