using LearningDiaryJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static LearningDiaryJS.UserInputs;


namespace LearningDiaryJS
{
    class Program
    {

        enum UserSelection
        {
            Title,
            Description,
            Estimate,
            TimeSpent,
            Source,
            StartDate,
            Progress
        };

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
                        Console.Clear();
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

                var search = testConnection.Topics.FirstOrDefault(x => x.Title == titleSearch);
                
                Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                string editOrDeleteQuestion = Console.ReadLine();

                var tpc = (from i in testConnection.Topics where i.Title == titleSearch select i);

                if (editOrDeleteQuestion != null && editOrDeleteQuestion.ToLower() == "d")
                {
                    foreach (var i in tpc)
                    {
                        testConnection.Topics.Remove(i);
                        Console.Clear();
                        Console.WriteLine("Topic deleted!");
                    }
                }

                if (editOrDeleteQuestion != null && editOrDeleteQuestion.ToLower() == "e")
                {
                    Console.Clear();
                    Console.WriteLine("Choose 0 to edit topic title"); 
                    Console.WriteLine("Choose 1 to edit topic description"); 
                    Console.WriteLine("Choose 2 to edit estimated time consumption"); 
                    Console.WriteLine("Choose 3 to edit time spent"); 
                    Console.WriteLine("Choose 4 to edit source of learning"); 
                    Console.WriteLine("Choose 5 to edit start date of studying");
                    Console.WriteLine("Choose 6 to edit progress status");
                    int whichField = GetIntInput();

                    switch ((UserSelection)(whichField))
                    {
                        case UserSelection.Title:
                            Console.WriteLine("Give new topic title:");
                            if (search != null) search.Title = GetStringInput();
                            break;
                        case UserSelection.Description:
                            Console.WriteLine("Give new topic description:");
                            if (search != null) search.Description = GetStringInput();
                            break;
                        case UserSelection.Estimate:
                            Console.WriteLine("Estimate new time consumption in days to master subject:");
                            if (search != null) search.TimeToMaster = GetDoubleInput();
                            break;
                        case UserSelection.TimeSpent:
                            Console.WriteLine("Give new time spent value");
                            if (search != null) search.TimeSpent = GetDoubleInput();
                            break;
                        case UserSelection.Source:
                            Console.WriteLine("Give new source:");
                            if (search != null) search.Source = GetStringInput();
                            break;
                        case UserSelection.StartDate:
                            Console.WriteLine("Edit the beginning time of the study in the format of dd.mm.yyyy");
                            if (search != null) search.StartLearningDate = GetStartDate();
                            break;
                        case UserSelection.Progress:
                            Console.WriteLine("Are you still studying? (yes/no)");
                            if (search != null) search.InProgress = GetBoolean();
                            break;
                    }
                    Console.Clear();
                }
                testConnection.SaveChanges();
            }
        }
    }
}




    