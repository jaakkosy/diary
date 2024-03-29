﻿using LearningDiaryJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static LearningDiaryJS.UserInputs;
using System.Diagnostics;
using System.Threading;
using ClassLibraryJA;


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
                        SaveToSql(AddTopic());
                        break;
                    case 2:
                        Console.Clear();
                        ListSqlTopics();
                        break;
                    case 3:
                        Console.Clear();
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
            Console.WriteLine("Enter the beginning time of the study in the format of dd.mm.yyyy:");
            DateTime startLearningDate = GetStartDate();
            Console.WriteLine("Are you still studying? (yes/no)");
            DateTime completionDate = new DateTime();

            Aikataulussa aikataulu = new Aikataulussa();
            bool dateValidation = aikataulu.ReadBoolMethod2(startLearningDate);
            if (dateValidation == false) {
                Console.WriteLine("Starting date is in past");
            } else {
               Console.WriteLine("Starting date is in future");
            }
            bool inProgress = GetBoolean();

            completionDate = startLearningDate.AddDays(inProgress == false ? timeSpent : estimatedTimeToMaster);

            // giving collected data to class

            Topic topic = new Topic(title, description, estimatedTimeToMaster, timeSpent,
                source, startLearningDate, inProgress, completionDate);

            return topic ;
        }

        static void SaveToSql(Topic topic)
        {

            using (LearningDiaryContext db = new LearningDiaryContext())
            {
                db.Topics.Add(topic);
                db.SaveChanges();
            }
        }

        static void ListSqlTopics()
        {
            using (LearningDiaryContext db = new LearningDiaryContext())
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                var tablePrint = db.Topics.Select(topic => topic);
                foreach (var row in tablePrint)
                {
                    Console.WriteLine(row.Title + " " + row.Description);
                }
                Console.WriteLine();
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
            }
        }

        static void EditSqlTopic()
        {

            using (LearningDiaryContext db = new LearningDiaryContext())
            {
                var search = SearchForTopic(db);

                Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                string editOrDeleteQuestion = GetStringInput().ToLower();

                if (editOrDeleteQuestion == "d")
                {
                    db.Topics.Remove(search);
                    Console.WriteLine("Topic deleted!");
                }

                if (editOrDeleteQuestion == "e")
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
                            search.Title = GetStringInput();
                            break;
                        case UserSelection.Description:
                            Console.WriteLine("Give new topic description:");
                            search.Description = GetStringInput();
                            break;
                        case UserSelection.Estimate:
                            Console.WriteLine("Estimate new time consumption in days to master subject:");
                            search.TimeToMaster = GetDoubleInput();
                            break;
                        case UserSelection.TimeSpent:
                            Console.WriteLine("Give new time spent value");
                            search.TimeSpent = GetDoubleInput();
                            break;
                        case UserSelection.Source:
                            Console.WriteLine("Give new source:");
                            search.Source = GetStringInput();
                            break;
                        case UserSelection.StartDate:
                            Console.WriteLine("Edit the beginning time of the study in the format of dd.mm.yyyy");
                            search.StartLearningDate = GetStartDate();
                            break;
                        case UserSelection.Progress:
                            Console.WriteLine("Are you still studying? (yes/no)");
                            search.InProgress = GetBoolean();
                            break;
                    }
                    Console.Clear();
                }
                db.SaveChanges();
            }
        }
        private static Models.Topic SearchForTopic(LearningDiaryContext db)
        {
            while (true)
            {
                Console.WriteLine("Search for topic by Title:");
                string titleSearch = Console.ReadLine();
                Models.Topic search = db.Topics.FirstOrDefault(x => x.Title == titleSearch);
                if (search != null)
                {
                    return search;
                }
                Console.WriteLine("Topic not found, search again!");
            }
        }
    }
}




    