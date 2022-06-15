using System;

namespace LearningDiaryJ
{
    class Topic
    {
        public int Id;
        public string Title;
        public string Description;
        public double EstimatedTimeToMaster;
        public double TimeSpent;
        public string Source;
        public DateTime StartLearningDate;
        public bool InProgress;
        public DateTime CompletionDate;


        public Topic(int id, string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Id = id;
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
            return string.Format("ID : {0}\n" +
                                 "Title: {1}\n" +
                                 "Description: {2}\n" +
                                 "Estimated time to master: {3}\n" +
                                 "Time spent: {4} hours\n" +
                                 "Source material: {5}\n" +
                                 "Started learning: {6}\n" +
                                 "In progress: {7}\n" +
                                 "Estimated completion: {8}\n", Id, Title, Description, EstimatedTimeToMaster, TimeSpent,
                Source, StartLearningDate.ToShortDateString(), InProgress, CompletionDate.ToShortDateString());
        }
    }
}