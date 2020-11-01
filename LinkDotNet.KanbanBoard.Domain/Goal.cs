using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Goal
    {
        [JsonConstructor]
        private Goal(string title, DateTime deadline, IEnumerable<Subtask> subtasks, Rank rank, GoalStatus goalStatus)
        {
            Title = title;
            Deadline = deadline;
            Subtasks = subtasks;
            Rank = rank;
            GoalStatus = goalStatus;
        }

        public string Id { get; set; }

        public string Title { get; private set; }

        public DateTime Deadline { get; private set; }

        public IEnumerable<Subtask> Subtasks { get; private set; }

        public Rank Rank { get; private set; }

        public GoalStatus GoalStatus { get; private set; }

        public Label Label { get; private set; }

        [JsonIgnore]
        public bool HasDeadline => Deadline != default;

        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public bool IsOverdue => HasDeadline && Deadline < DateTime.Now;

        public void SetGoalStatus(GoalStatus newStatus)
        {
            GoalStatus = newStatus ?? throw new ArgumentNullException(nameof(newStatus), "GoalStatus can not be null");
        }

        public void SetLabel(Label label)
        {
            Label = label;
        }

        public static Result<Goal> Create(string title, Rank rank, GoalStatus goalStatus, IEnumerable<Subtask> subtasks,
            DateTime? deadline)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<Goal>("A goal needs a title");
            }

            if (rank == null)
            {
                return Result.Failure<Goal>($"{nameof(rank)} can not be null");
            }

            if (goalStatus == null)
            {
                return Result.Failure<Goal>($"{nameof(goalStatus)} can not be null");
            }

            return new Goal(title, deadline ?? default, subtasks ?? Array.Empty<Subtask>(), rank, goalStatus);
        }
    }
}
