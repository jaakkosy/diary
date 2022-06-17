using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningDiaryJ
{
    class Program
    {
        static void Main(string[] args)
        {   
            // Defining file path
            List<Topic> topics = new List<Topic>();

            // Asking user choice
            while (true)
            {
                Console.WriteLine("Choose 1 to add a topic, Choose 2 to list topics, Choose 3 to edit topics or Choose 0 to quit.");

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

                else if (userChoice == 3)
                {
                    FindTopic(topics);
                }
            }
            WriteToFile(topics, "learningdiaryaw.csv");
        }

        // Saving collected data to csv file

        private static void WriteToFile(List<Topic> topics, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
                {
                    foreach (var topic in topics)
                    {
                        file.WriteLine(topic.Id);
                        file.WriteLine(topic.Title);
                        file.WriteLine(topic.Description);
                        file.WriteLine("testi");
                        file.WriteLine(topic.EstimatedTimeToMaster);
                        file.WriteLine(topic.TimeSpent);
                        file.WriteLine(topic.Source);
                        file.WriteLine(topic.StartLearningDate.ToShortDateString());
                        file.WriteLine(topic.InProgress);
                        file.WriteLine(topic.CompletionDate.ToShortDateString());
                        file.WriteLine("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("This program did an oopsie:", ex);
            }
        }

        // Collecting data from user
        static Topic AddTopic()
        {
            Console.WriteLine("Give topic id");
            int id = GetIntInput();
            Console.WriteLine("Give topic title:");
            string title = GetStringInput();
            Console.WriteLine("Give topic description");
            string description = GetStringInput();
            Console.WriteLine("Estimate time consumption in days to master subject:");
            double estimatedTimeToMaster = GetDoubleInput();
            Console.WriteLine("Enter the time spent in days:");
            double timeSpent = GetDoubleInput();
            Console.WriteLine("Give possible source:");
            string source = GetStringInput();
            Console.WriteLine("Enter the beginning time of the study in the format of YYYY-MM-DD:");
            DateTime startLearningDate = GetStartDate();
            Console.WriteLine("Are you still studying? (yes/no)");
            bool inProgress = GetBoolean();
            DateTime completionDate = new DateTime();

            if (inProgress == false)
            {
                completionDate = startLearningDate.AddDays(timeSpent);
            }
            else
            {
                completionDate = startLearningDate.AddDays(estimatedTimeToMaster);
            }
            
            // giving collected data to class
            
            Topic topicToAdd = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, 
                source, startLearningDate, inProgress, completionDate);

            return topicToAdd;
        }

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
                    Console.WriteLine("Give input in correct form (be careful with ',' and '.')");
                    continue;
                }
                break;
            }
            return input;
        }

        public static DateTime GetStartDate()
        {
            DateTime date = new DateTime();
            while (true)
            {
                try
                {
                    date = Convert.ToDateTime(Console.ReadLine());
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
            bool inProgress;
            string progressInput = Console.ReadLine();
            if (progressInput.ToLower() == "yes")
            {
                inProgress = true;
            }
            else
            {
                inProgress = false;
            }
            return inProgress;
        }

        public static DateTime GetCompletionDate(bool inProgress,double timeSpent,double estimate,DateTime StartLearningDate)
        {
            if (inProgress == false)
            {
                var completionDate = StartLearningDate.AddDays(timeSpent);
                return completionDate;
            }
            else
            {
                var completionEstimated = StartLearningDate.AddDays(estimate);
                return completionEstimated;
            }
        }

        public static void FindTopic(List<Topic> topics)
        {
            Console.WriteLine("Search for topics by ID:");
            int idSearch = Convert.ToInt32(Console.ReadLine());
            foreach (var topic in topics)
            {
                if (topic.Id == idSearch)
                {
                    Console.WriteLine("Topic found!");
                    Console.WriteLine();
                    Console.WriteLine(topic);
                    Console.WriteLine();
                    Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                    Console.WriteLine();
                    string editOrDeleteQuestion = Console.ReadLine();
                    if (editOrDeleteQuestion.ToLower() == "e")
                    {
                        Console.WriteLine("Which field would you like to edit?");
                        string whichField = Console.ReadLine();

                        if (whichField.ToLower() == "id")
                        {
                            Console.WriteLine("Give new topic id");
                            topic.Id = GetIntInput();
                        }
                        else if (whichField.ToLower() == "title")
                        {
                            Console.WriteLine("Give new topic title:");
                            topic.Title = GetStringInput();
                        }
                        else if (whichField.ToLower() == "description")
                        {
                            Console.WriteLine("Give new topic description:");
                            topic.Description = GetStringInput();
                        }
                        else if (whichField.ToLower() == "time consumption")
                        {
                            Console.WriteLine("Estimate new time consumption in days to master subject:");
                            topic.EstimatedTimeToMaster = GetDoubleInput();
                        }
                        else if (whichField.ToLower() == "source")
                        {
                            Console.WriteLine("Give new source:");
                            topic.Source = GetStringInput();
                        }
                        else if (whichField.ToLower() == "beginning date")
                        {
                            Console.WriteLine("Edit the beginning time of the study in the format of YYYY-MM-DD:");
                            topic.StartLearningDate = GetStartDate();
                        }
                        else if (whichField.ToLower() == "progress")
                        {
                            Console.WriteLine("Are you still studying? (yes/no)");
                            topic.InProgress = GetBoolean();
                        }
                        else if (whichField.ToLower() == "time spent")
                        {
                            Console.WriteLine("Edit the time spent in days:");
                            topic.TimeSpent = GetDoubleInput();
                        }
                    }
                    else if (editOrDeleteQuestion == "d")
                    {
                        Console.WriteLine("What number of diary note should be removed?");
                        int testi = Convert.ToInt32(Console.ReadLine());
                        topics.RemoveAt(testi);
                    }
                    break;
                }
            }
        }
    }
}