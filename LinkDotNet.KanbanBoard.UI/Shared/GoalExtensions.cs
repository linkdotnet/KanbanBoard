using System;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.Web;

namespace LinkDotNet.KanbanBoard.UI.Shared
{
    public static class GoalExtensions
    {
        public static GoalDto ToGoalDto(this Goal goal)
        {
            return new GoalDto
            {
                Id = goal.Id,
                Title = goal.Title,
                Rank = goal.Rank.Key,
                GoalStatus = goal.GoalStatus.Key,
                Deadline = goal.Deadline.Ticks,
            };
        }

        public static Goal ToGoal(this GoalDto goalDto)
        {
            var rank = Rank.Create(goalDto.Rank).Value;
            var status = GoalStatus.Create(goalDto.GoalStatus).Value;
            var deadline = new DateTime(goalDto.Deadline);

            return Goal.Create(goalDto.Title, rank, status, null, deadline).Value;
        }
    }
}