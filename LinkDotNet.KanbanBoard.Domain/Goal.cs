using System;
using System.Collections.Generic;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Goal
    {
        public Goal(string title, DateTime deadline, IEnumerable<Subtask> subtasks, Rank rank)
        {
            Title = title;
            Deadline = deadline;
            Subtasks = subtasks;
            Rank = rank;
        }

        public string Title { get; private set; }
        public DateTime Deadline { get; private set; }
        public IEnumerable<Subtask> Subtasks { get; private set; }
        public Rank Rank { get; private set; }
    }
}
