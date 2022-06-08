using System;
using System.Collections.Generic;
using System.IO;

namespace LearningDiaryJ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Topic> topics = new List<Topic>();

            while (true)
            {
                Console.WriteLine("Choose 1 to add a topic, Choose 2 to list topics or Choose 0 to quit.");

                int userChoice;

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form.");
                    continue;
                }

                if (userChoice == 0)
                {
                    break;
                }

                else if (userChoice == 2)
                {
                    foreach (Topic topic in topics)
                    {
                        Console.WriteLine(topic);
                    }
                }
                else if (userChoice == 1)
                {
                    topics.Add(AddTopic());
                    Console.WriteLine("Topic added to list.");
                }
            }
        }

        static Topic AddTopic()
        {
            int id;

            while (true)
            {
                Console.WriteLine("Give topic id:");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;
                }
                break;
            }

            Console.WriteLine("Give topic title:");
            string title = Console.ReadLine();

            Console.WriteLine("Give topic description:");
            string description = Console.ReadLine();

            Console.WriteLine("Estimate time consumption:");
            double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());
               
            double timeSpent;
            while (true)
            {
                Console.WriteLine("Enter the time spent:");
                try
                {
                    timeSpent = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form (be careful with ',' and '.')");
                    continue;
                }
                break;
            }

            Console.WriteLine("Give possible source:");
            string source = Console.ReadLine();

            DateTime startLearningDate = new DateTime();
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the beginning time of the study in the format of YYYY-MM-DD:");
                    startLearningDate = Convert.ToDateTime(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;

                }
                break;
            }

            Console.WriteLine("Are you still studying? (yes/no)");
            string progressInput = Console.ReadLine();
            bool inProgress = false;
            if (progressInput.ToLower() == "yes")
            {
                inProgress = true;
            }
            else
            {
                inProgress = false;
            }

            DateTime completionDate = new DateTime();
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter completion time of the study in the format of YYYY-MM-DD:");
                    completionDate = Convert.ToDateTime(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;

                }
                break;
            }

            Topic topicToAdd = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, 
                source, startLearningDate, inProgress, completionDate);

            return topicToAdd;
        }
    }
}