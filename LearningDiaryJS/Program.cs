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
            string filePath = @"C:\Users\JaakkoSyrjämäki\source\repos\LearningDiaryJS\learningdiary.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

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
                        lines.Add(Convert.ToString(topic.Id));
                        lines.Add(topic.Title);
                        lines.Add(topic.Description);
                        lines.Add(Convert.ToString(topic.EstimatedTimeToMaster));
                        lines.Add(Convert.ToString(topic.TimeSpent));
                        lines.Add(topic.Source);
                        lines.Add(Convert.ToString(topic.StartLearningDate.ToShortDateString()));
                        lines.Add(Convert.ToString(topic.InProgress));
                        lines.Add(Convert.ToString(topic.CompletionDate.ToShortDateString()));
                        lines.Add("");
                        File.WriteAllLines(filePath, lines);
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
        }

        // Collecting data from user
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


            double estimatedTimeToMaster;
            while (true)
            {
                Console.WriteLine("Estimate time consumption to master subject:");
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

            double timeSpent = 0;

            Console.WriteLine("Are you still studying? (yes/no)");
            string progressInput = Console.ReadLine();
            bool inProgress = (progressInput.ToLower() == "yes");
            if (progressInput.ToLower() == "yes")
            {
                inProgress = true;
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
            }
            else
            {
                inProgress = false;
            }

            DateTime completionDate = new DateTime();

            if (inProgress == false)
            {
                while (true)
                {
                    Console.WriteLine("Enter completion time of the study in the format of YYYY-MM-DD:");
                    try
                    {
                        completionDate = Convert.ToDateTime(Console.ReadLine());

                        TimeSpan difference = completionDate.Subtract(startLearningDate);
                        timeSpent = (double)Convert.ToDecimal(difference.TotalHours);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Give input in correct form");
                        continue;
                    }
                    break;
                }
            }

            if (inProgress == true)
            {
                while (true)
                {
                    Console.WriteLine("Estimate completion time of the study in the format of YYYY-MM-DD:");
                    try
                    {
                        completionDate = Convert.ToDateTime(Console.ReadLine());
                        
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Give input in correct form");
                        continue;
                    }
                    break;
                }
            }

            // giving collected data to class
            
            Topic topicToAdd = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, 
                source, startLearningDate, inProgress, completionDate);

            return topicToAdd;
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
                    Console.WriteLine("Would you like to edit fields(e) or delete topic(d)?");
                    Console.WriteLine();
                    string editOrDeleteQuestion = Console.ReadLine();
                    if (editOrDeleteQuestion.ToLower() == "e")
                    {
                        Console.WriteLine("Which field would you like to edit?");
                        string whichField = Console.ReadLine();
                        Console.WriteLine("Give new value to the field");
                        string newValue = Console.ReadLine();

                        if (whichField.ToLower() == "id")
                        {
                            topic.Id = Convert.ToInt32(newValue);
                        }
                        else if (whichField.ToLower() == "title")
                        {
                            topic.Title = newValue;
                        }
                        else if (whichField.ToLower() == "description")
                        {
                            topic.Description = newValue;
                        }
                        else if (whichField.ToLower() == "time consumption")
                        {
                            topic.EstimatedTimeToMaster = Convert.ToDouble(newValue);
                        }
                        else if (whichField.ToLower() == "source")
                        {
                            topic.Source = newValue;
                        }
                        else if (whichField.ToLower() == "beginning date")
                        {
                            topic.StartLearningDate = Convert.ToDateTime(newValue);
                        }
                        else if (whichField.ToLower() == "progress")
                        {
                            topic.InProgress = Convert.ToBoolean(newValue);
                        }
                        else if (whichField.ToLower() == "time spent")
                        {
                            topic.TimeSpent = Convert.ToDouble(newValue);
                        }
                        else if (whichField.ToLower() == "completion date")
                        {
                            topic.CompletionDate = Convert.ToDateTime(newValue);
                        }
                    }
                    else if (editOrDeleteQuestion == "d")
                    {
                        Console.WriteLine("Monesko Merkintä poistetaan?");
                        int testi = Convert.ToInt32(Console.ReadLine());
                        topics.RemoveAt(testi);
                    }
                    break;
                }
            }
        }
    }
}