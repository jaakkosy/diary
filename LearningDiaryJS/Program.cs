using ClassLibraryJA;
using LearningDiaryJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LearningDiaryJS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Topic> topics = new List<Topic>();

            // Asking user choice
            while (true)
            {
                Console.WriteLine
                ("Choose 1 to add a topic to database\n" +
                 "Choose 2 to list current titles and descriptions from database\n" +
                 "Choose 3 to edit/delete topics in database\n" +
                 "Choose 4 to quit program.");
                Console.WriteLine();
                Console.WriteLine();

                int userChoice = GetIntInput();

                if (userChoice == 4)
                {
                    break;
                }

                switch (userChoice)
                {
                    case 1:
                        topics.Add(AddTopic());
                        SaveToSql(topics);
                        topics.Clear();
                        break;
                    case 2:
                        ListSqlTopics();
                        break;
                    case 3:
                        EditSqlTopic();
                        break;
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
            completionDate = startLearningDate.AddDays(inProgress == false ? timeSpent : estimatedTimeToMaster);

            // giving collected data to class

            Topic topicToAdd = new Topic(title, description, estimatedTimeToMaster, timeSpent,
                source, startLearningDate, inProgress, completionDate);

            return topicToAdd;
        }

        static void SaveToSql(List<Topic> topics)
        {

            using (LearningDiaryContext testConnection = new LearningDiaryContext())
            {
                foreach (var topic in topics)
                {
                    var newTopic = new LearningDiaryJS.Models.Topic()
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
                    testConnection.Topics.Add(newTopic);
                    testConnection.SaveChanges();
                }
            }
        }

        static void ListSqlTopics()
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

        static bool ListSqlTopicsVerify(string titleSearch)
        {
            using (LearningDiaryContext testConnection = new LearningDiaryContext())
            {
                var tablePrint = testConnection.Topics.Select(topic => topic);
                foreach (var row in tablePrint)
                {
                    if (row.Title == titleSearch)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        static void EditSqlTopic()
        {
            Console.WriteLine("Search for topics by Title:");
            string titleSearch = Console.ReadLine();

            using (LearningDiaryContext testConnection = new LearningDiaryContext())
            {

                while (true)
                {
                    if (ListSqlTopicsVerify(titleSearch))
                    {
                        break;
                    }
                    Console.WriteLine("Topic not found! Please try again!");
                    Console.WriteLine("Search for topics by Title:");
                    titleSearch = Console.ReadLine();
                }


                ListSqlTopicsVerify(titleSearch);

                var search = testConnection.Topics.FirstOrDefault(x => x.Title == titleSearch);
                
                Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                string editOrDeleteQuestion = Console.ReadLine();

                var tpc = (from i in testConnection.Topics where i.Title == titleSearch select i);


                if (editOrDeleteQuestion.ToLower() == "d")
                {
                    foreach (var i in tpc)
                    {
                        testConnection.Topics.Remove(i);
                    }
                }

                if (editOrDeleteQuestion.ToLower() == "e")
                {
                    Console.WriteLine("Which field would you like to edit?");
                    string whichField = Console.ReadLine();

                    switch (whichField.ToLower())
                    {
                        case "title":
                            Console.WriteLine("Give new topic title:");
                            search.Title = GetStringInput();
                            break;
                        case "description":
                            Console.WriteLine("Give new topic description:");
                            search.Description = GetStringInput();
                            break;
                        case "time consumption":
                            Console.WriteLine("Estimate new time consumption in days to master subject:");
                            search.TimeToMaster = GetDoubleInput();
                            break;
                        case "source":
                            Console.WriteLine("Give new source:");
                            search.Source = GetStringInput();
                            break;
                        case "beginning date":
                            Console.WriteLine("Edit the beginning time of the study in the format of YYYY-MM-DD:");
                            search.StartLearningDate = GetStartDate();
                            break;
                        case "progress":
                            Console.WriteLine("Are you still studying? (yes/no)");
                            search.InProgress = GetBoolean();
                            break;
                        case "time spent":
                            Console.WriteLine("Edit the time spent in days:");
                            search.TimeSpent = GetDoubleInput();
                            break;

                    }
                    Console.WriteLine("Saved!");
                }

                testConnection.SaveChanges();
            }
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
            var aikataulu = new Aikataulussa();

            DateTime date = new DateTime();
            while (true)
            {
                try
                {
                    date = Convert.ToDateTime(Console.ReadLine());
                    bool dateValidation = aikataulu.ReadBoolMethod2(date);
                    Console.WriteLine(dateValidation == true ? "It works! Date is valid!" : "Invalid date");
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




    