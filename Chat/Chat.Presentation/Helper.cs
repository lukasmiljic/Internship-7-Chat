using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Presentation
{
    public class Helper
    {
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
    }
}