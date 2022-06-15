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
                    Console.WriteLine("Testi");
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
            
            int id = GetId();
            string title = GetTitle();
            string description = GetDescription();
            double estimatedTimeToMaster = GetEstimatedTime();
            double timeSpent = GetTimeSpent();
            string source = GetSource();
            DateTime startLearningDate = GetStartDate();
            bool inProgress = GetProgress();
            DateTime completionDate = GetCompletionDate(inProgress,timeSpent,estimatedTimeToMaster,startLearningDate);
            
            
            // giving collected data to class
            
            Topic topicToAdd = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, 
                source, startLearningDate, inProgress, completionDate);

            return topicToAdd;
        }

        public static string GetTitle()
        {
            Console.WriteLine("Give topic title:");
            string title = Console.ReadLine();
            return title;
        }

        public static int GetId()
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
            return id;
        }

        public static string GetDescription()
        {
            Console.WriteLine("Give topic description:");
            string description = Console.ReadLine();
            return description;
        }


        public static double GetEstimatedTime()
        {
            double estimatedTimeToMaster;
            while (true)
            {
                Console.WriteLine("Estimate time consumption in days to master subject:");
                try
                {
                    estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form (be careful with ',' and '.')");
                    continue;
                }
                break;
            }
            return estimatedTimeToMaster;
        }

        public static string GetSource()
        {
            Console.WriteLine("Give possible source:");
            string source = Console.ReadLine();
            return source;
        }

        public static DateTime GetStartDate()
        {
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
            return startLearningDate;
        }

        public static double GetTimeSpent()
        {
            double timeSpent = 0;
            while (true)
            {
                Console.WriteLine("Enter the time spent in days:");
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
            return timeSpent;
        }

        public static bool GetProgress()
        {
            Console.WriteLine("Are you still studying? (yes/no)");
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
                            topic.Id = GetId();
                        }
                        else if (whichField.ToLower() == "title")
                        {
                            topic.Title = GetTitle();
                        }
                        else if (whichField.ToLower() == "description")
                        {
                            topic.Description = GetDescription();
                        }
                        else if (whichField.ToLower() == "time consumption")
                        {
                            topic.EstimatedTimeToMaster = GetEstimatedTime();
                        }
                        else if (whichField.ToLower() == "source")
                        {
                            topic.Source = GetSource();
                        }
                        else if (whichField.ToLower() == "beginning date")
                        {
                            topic.StartLearningDate = GetStartDate();
                        }
                        else if (whichField.ToLower() == "progress")
                        {
                            topic.InProgress = GetProgress();
                        }
                        else if (whichField.ToLower() == "time spent")
                        {
                            topic.TimeSpent = GetTimeSpent();
                        }
                        else if (whichField.ToLower() == "completion date")
                        {
                            topic.CompletionDate = GetCompletionDate(topic.InProgress,topic.TimeSpent,topic.EstimatedTimeToMaster,topic.StartLearningDate);
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