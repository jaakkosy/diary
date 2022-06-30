using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryJA;

namespace LearningDiaryJS
{
    class UserInputs
    {
        public static int GetIntInput()
        {
            int input;
            while (true)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;
                }
                break;
            }
            return input;
        }

        public static string GetStringInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public static double GetDoubleInput()
        {
            double input;
            while (true)
            {
                try
                {
                    input = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;
                }
                break;
            }
            return input;
        }

        public static DateTime GetStartDate()
        {
            var aikataulu = new Aikataulussa();

            DateTime date = new DateTime();
            while (true)
            {
                try
                {
                    date = Convert.ToDateTime(Console.ReadLine());
                    bool dateValidation = aikataulu.ReadBoolMethod2(date);
                    Console.WriteLine(dateValidation == true ? "Date is in future!" : "Date is in past!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;
                }
                break;
            }
            return date;
        }

        public static bool GetBoolean()
        {
            string progressInput = Console.ReadLine();
            var inProgress = progressInput != null && progressInput.ToLower() == "yes";
            return inProgress;
        }
    }
}
