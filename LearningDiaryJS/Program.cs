using System;
using LearningDiaryJS.Models;
using System.Collections.Generic;
using System.Linq;
using ClassLibraryJA;

namespace LearningDiaryJ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Topic> topics = new List<Topic>();

            // Asking user choice
            while (true)
            {
                Console.WriteLine("Choose 1 to add a topic to database\n" +
                    "Choose 2 to list current titles and descriptions from database\n" +
                    "Choose 3 to edit/delete topics in database\n" +
                    "Choose 4 to quit.");
                    Console.WriteLine();
                    Console.WriteLine();

                int userChoice = GetIntInput();

                if (userChoice == 4)
                {
                    break;
                }

                else if (userChoice == 2)
                {
                    using (LearningDiaryContext testConnection = new LearningDiaryContext())
                    {
                        var tablePrint = testConnection.Topics.Select(topic => topic);
                        foreach (var row in tablePrint)
                        {
                            Console.WriteLine(row.Title + " " + row.Description);
                        }
                        Console.WriteLine();
                    }
                }
                else if (userChoice == 1)
                {
                    topics.Add(AddTopic());
                    SaveToSql(topics);
                }

                else if (userChoice == 3)
                {
                    EditSqlTopic();
                }
            }

            static void SaveToSql(List<Topic> topics)
            {

                using (LearningDiaryContext testConnection = new LearningDiaryContext())
                {
                    foreach (var topic in topics)
                    { 
                        var table = testConnection.Topics.Select(topic => topic);
                        LearningDiaryJS.Models.Topic newtopic = new LearningDiaryJS.Models.Topic()
                        {
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
                    }

                    var tablePrint = testConnection.Topics.Select(topic => topic);
                    foreach (var row in tablePrint)
                    {
                        Console.WriteLine(row.Title + "-" + row.Description);
                    }
                }
            }

            static void EditSqlTopic()
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

            if (result == false)
            {
                Console.WriteLine("Et ole aikataulussa!!");
            }

            // giving collected data to class

            Topic topicToAdd = new Topic(title, description, estimatedTimeToMaster, timeSpent,
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
                    Console.WriteLine("Give input in correct form");
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
    }
}