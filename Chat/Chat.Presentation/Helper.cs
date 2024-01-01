namespace Chat.Presentation
{
    public class Helper
    {
        //general
        public static void PressAnything() {
            Console.WriteLine("Press anything to continue...");
            Console.ReadLine();
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
                    Console.WriteLine("Greska pri odabiru! Molimo ponovno unesite vrijednost");
                    break;
            }
            PressAnything();
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
        public static bool RegisterVerifyEmailAndPrintMsg(string email)
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
        public static bool LoginVerifyPasswordAndPrintMsg(string password)
        {
            int temp = 0;
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
        public static bool LoginVerifyEmailAndPrintMsg(string inputEmail)
        {
            int returnValue = 0;
            //returnValue = domain.VerifyEmail(inputEmail)
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
    }
}