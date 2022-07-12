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

            while (true)
            {
                string str = Console.ReadLine();
                try
                {
                    string[] dtParser;
                    dtParser = str.Split('.');
                    Console.Clear();
                    var startDate = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]),Convert.ToInt32(dtParser[0]));
                    var sqlMinimum = new DateTime(1753, 01, 01);
                    if (startDate > sqlMinimum) return startDate;
                    Console.WriteLine("Date is too far away in the past for SQL, enter date again!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a valid date! Use dd.mm.yyyy...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public static bool GetBoolean()
        {
            string progressInput = Console.ReadLine();
            var inProgress = progressInput != null && progressInput.ToLower() == "yes";
            return inProgress;
        }
    }
}
