using System;
using System.Collections.Generic;

#nullable disable

namespace LearningDiaryJS.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? TimeToMaster { get; set; }
        public double? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public bool? InProgress { get; set; }
        public DateTime? CompletionDate { get; set; }

        public Topic(string title, string description, double estimatedTimeToMaster, double timeSpent, string source,
            DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Title = title;
            Description = description;
            TimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }

        public Topic()
        {

        }

        // Forming data

        public override string ToString()
        {
            return string.Format(
                "Title: {0}\n" +
                "Description: {1}\n" +
                "Estimated time to master: {2} days\n" +
                "Time spent: {3} days\n" +
                "Source material: {4}\n" +
                "Started learning: {5}\n" +
                "In progress: {6}\n" +
                "Estimated completion: {7}\n", Title, Description, TimeToMaster, TimeSpent,
                Source, StartLearningDate, InProgress, CompletionDate);
        }
    }
}
