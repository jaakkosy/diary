using System;
using System.Collections.Generic;

namespace LearningDiaryJ
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Give topic identifier:");
            int idInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Give topic title:");
            string titleInput = Console.ReadLine();

            Console.WriteLine("Give topic description:");
            string descriptionInput = Console.ReadLine();

            Console.WriteLine("Estimate time consumption:");
            double timeEstimateInput = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the time spent:");
            double timeSpentInput = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Give possible source:");
            string sourceInput = Console.ReadLine();

            Console.WriteLine("Enter the time of the study in the format of YYYY-MM-DD:");
            DateTime dayOfStart = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Are you still studying? (yes/no)");
            string progressInput = Console.ReadLine();
            bool Boolean = false;
            if (progressInput == "yes" || progressInput == "Yes" || progressInput == "YES")
            {
                Boolean = true;
            }
            else
            {
                Boolean = false;
            }

            Console.WriteLine("Enter completion time of the study in the format of YYYY-MM-DD:");
            DateTime dayOfCompletion = Convert.ToDateTime(Console.ReadLine());



            Diary diary1 = new Diary(idInput, titleInput, descriptionInput, timeEstimateInput, timeSpentInput, sourceInput, dayOfStart, Boolean, dayOfCompletion);

            diary1.id = idInput;
            diary1.title = titleInput;
            diary1.description = descriptionInput;
            diary1.estimate = timeEstimateInput;
            diary1.timespent = timeSpentInput;
            diary1.source = sourceInput;
            diary1.startday = dayOfStart;
            diary1.progress = Boolean;
            diary1.completionday = dayOfCompletion;

            Console.WriteLine(diary1.completionday);
            Console.WriteLine(diary1.progress);

        }
    }
}