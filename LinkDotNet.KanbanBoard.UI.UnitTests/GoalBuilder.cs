using System;
using LinkDotNet.KanbanBoard.Domain;

namespace LinkDotNet.KanbanBoard.UI.UnitTests
{
    public class GoalBuilder
    {
        private string _title = "Test-Title";
        private DateTime? _deadline;
        private Rank _rank = Rank.Important;
        private GoalStatus _status = GoalStatus.Todo;

        public GoalBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public GoalBuilder WithDeadline(DateTime deadline)
        {
            _deadline = deadline;
            return this;
        }

        public GoalBuilder WithRank(Rank rank)
        {
            _rank = rank;
            return this;
        }

        public GoalBuilder WithGoalStatus(GoalStatus status)
        {
            _status = status;
            return this;
        }

        public Goal Build()
        {
            var goalResult = Goal.Create(_title, _rank, _status, Array.Empty<Subtask>(), _deadline);
            if (goalResult.IsFailure)
            {
                throw new ArgumentException($"Could not create {nameof(Goal)}: {goalResult.Error}");
            }

            return goalResult.Value;
        }
    }
}