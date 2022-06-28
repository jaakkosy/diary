using System;

namespace LearningDiaryJ
{
    class Topic
    {
        public string Title;
        public string Description;
        public double EstimatedTimeToMaster;
        public double TimeSpent;
        public string Source;
        public DateTime StartLearningDate;
        public bool InProgress;
        public DateTime CompletionDate;


        public Topic(string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }
        
        // Forming data

        public override string ToString()
        {
            return string.Format(
                                 "Title: {0}\n" +
                                 "Description: {1}\n" +
                                 "Estimated time to master: {2}\n" +
                                 "Time spent: {3} days\n" +
                                 "Source material: {4}\n" +
                                 "Started learning: {5}\n" +
                                 "In progress: {6}\n" +
                                 "Estimated completion: {7}\n", Title, Description, EstimatedTimeToMaster, TimeSpent,
                Source, StartLearningDate.ToShortDateString(), InProgress, CompletionDate.ToShortDateString());
        }
    }
}