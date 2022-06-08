using System;

namespace LearningDiaryJ
{
    class Topic
    {
        public int id;
        public string title;
        public string description;
        public double estimate;
        public double timespent;
        public string source;
        public DateTime startday;
        public bool progress;
        public DateTime completionday;

        public Topic(int aId, string aTitle, string aDescription, double aEstimate, double aTimespent, string aSource, DateTime adayOfStart, bool aProgress, DateTime adayOfCompletion)
        {
            id = aId;
            title = aTitle;
            description = aDescription;
            estimate = aEstimate;
            timespent = aTimespent;
            source = aSource;
            startday = adayOfStart;
            progress = aProgress;
            completionday = adayOfCompletion;
        }

        public override string ToString()
        {
            return string.Format("ID : {0}\n" +
                                 "Title: {1}\n" +
                                 "Description: {2}\n" +
                                 "Time to master: {3}\n" +
                                 "Time spent: {4}\n" +
                                 "Source material: {5}\n" +
                                 "Started learning: {6}\n" +
                                 "In progress: {7}\n" +
                                 "Estimated completion: {8}", id, Title, Description, EstimatedTimeToMaster, TimeSpent,
                Source, StartLearningDate.Date, InProgress, CompletionDate.Date);
        }
    }
}