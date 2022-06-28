using System;
using LearningDiaryJS.Models;
using System.Collections.Generic;
using System.Linq;
using ClassLibraryJA;

namespace LearningDiaryJ
{
    class Program
    {
        public static int GlobalID = 1;
        static void Main(string[] args)
        {
            List<Topic> topics = new List<Topic>();
            
            try
            {
                using (var testConnection = new LearningDiaryContext())
                {
                    var sqlId = testConnection.Topics.Max(topic => topic.Id);
                    sqlId++;
                    GlobalID = sqlId;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No topics found, starting with ID 1");
            }

            // Asking user choice
            while (true)
            {
                Console.WriteLine("Choose 1 to add a topic, Choose 2 to list topics, Choose 3 to edit/delete topics or Choose 0 to quit.");

                int userChoice = GetIntInput();

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
                    EditSqlTopic(topics);
                }
            }
            //WriteToFile(topics, "learningdiaryaw.csv");
            SaveToSql(topics);

            static void SaveToSql(List<Topic> topics)
            {
                foreach (var topic in topics)
                {
                    using (LearningDiaryContext testConnection = new LearningDiaryContext())
                    {
                        var table = testConnection.Topics.Select(topic => topic);
                        LearningDiaryJS.Models.Topic newtopic = new LearningDiaryJS.Models.Topic()
                        {
                            Id = topic.Id,
                            Title = topic.Title,
                            Description = topic.Description,
                            TimeToMaster = Convert.ToInt32(topic.EstimatedTimeToMaster),
                            TimeSpent = Convert.ToInt32(topic.TimeSpent),
                            Source = topic.Source,
                            StartLearningDate = topic.StartLearningDate,
                            InProgress = topic.InProgress,
                            CompletionDate = topic.CompletionDate
                        };

                        testConnection.Topics.Add(newtopic);
                        testConnection.SaveChanges();

                        //table = testConnection.Topics.Select(topic => topic);
                        //foreach (var row in table)
                        //{
                        //    Console.WriteLine(row.Description);
                        //}
                    }
                }
            }

            static void EditSqlTopic(List<Topic> topics)
            {
                Console.WriteLine("Search for topics by Title:");
                string titleSearch = Console.ReadLine();

                using (LearningDiaryContext testConnection = new LearningDiaryContext())
                {
                    var search = testConnection.Topics.Where(x => x.Title == titleSearch).Single();
                    Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                    string editOrDeleteQuestion = Console.ReadLine();

                    var std = (from i in testConnection.Topics where i.Title == titleSearch select i);


                    if (editOrDeleteQuestion.ToLower() == "d")
                    {
                        foreach (var i in std)
                        {
                            testConnection.Topics.Remove(i);
                        }
                    }

                    if (editOrDeleteQuestion.ToLower() == "e")
                    {
                        Console.WriteLine("Which field would you like to edit?");
                        string whichField = Console.ReadLine();

                        if (whichField.ToLower() == "id")
                        {
                            Console.WriteLine("Give new topic id");
                            search.Id = GetIntInput();
                        }
                        else if (whichField.ToLower() == "title")
                        {
                            Console.WriteLine("Give new topic title:");
                            search.Title = GetStringInput();
                        }
                        else if (whichField.ToLower() == "description")
                        {
                            Console.WriteLine("Give new topic description:");
                            search.Description = GetStringInput();
                        }
                        else if (whichField.ToLower() == "time consumption")
                        {
                            Console.WriteLine("Estimate new time consumption in days to master subject:");
                            search.TimeToMaster = GetDoubleInput();
                        }
                        else if (whichField.ToLower() == "source")
                        {
                            Console.WriteLine("Give new source:");
                            search.Source = GetStringInput();
                        }
                        else if (whichField.ToLower() == "beginning date")
                        {
                            Console.WriteLine("Edit the beginning time of the study in the format of YYYY-MM-DD:");
                            search.StartLearningDate = GetStartDate();
                        }
                        else if (whichField.ToLower() == "progress")
                        {
                            Console.WriteLine("Are you still studying? (yes/no)");
                            search.InProgress = GetBoolean();
                        }
                        else if (whichField.ToLower() == "time spent")
                        {
                            Console.WriteLine("Edit the time spent in days:");
                            search.TimeSpent = GetDoubleInput();
                        }
                    }
                    testConnection.SaveChanges();
                    Console.WriteLine("Saved!");
                }
            }
        }
    
        // Collecting data from user
        static Topic AddTopic()
        {
            
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

            Aikataulussa Aikataulu = new Aikataulussa();

            bool result = Aikataulu.ReadBoolMethod((float)estimatedTimeToMaster, (float)timeSpent);

            Console.WriteLine(result);

            // giving collected data to class

            Topic topicToAdd = new Topic(GlobalID, title, description, estimatedTimeToMaster, timeSpent,
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
            Aikataulussa Aikataulu = new Aikataulussa();

            DateTime date = new DateTime();
            while (true)
            {
                try
                {
                    date = Convert.ToDateTime(Console.ReadLine()); 
                    bool dateValidation = Aikataulu.ReadBoolMethod2(date);
                    if (dateValidation == false)
                    {
                        Console.WriteLine("It works! Date is valid!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date");
                    }
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

        public static DateTime GetCompletionDate(bool inProgress, double timeSpent, double estimate, DateTime StartLearningDate)
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
    }
}

