using System;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.Web;

namespace LinkDotNet.KanbanBoard.UI.Shared
{
    public static class GoalExtensions
    {
        public static GoalDto ToGoalDto(this Goal goal)
        {
            var goalDto = new GoalDto
            {
                Title = goal.Title,
                Rank = goal.Rank.Key,
                GoalStatus = goal.GoalStatus.Key,
                Deadline = goal.Deadline.Ticks,
            };

            if (goal.Id != null)
            {
                goalDto.Id = goal.Id;
            }

            return goalDto;
        }

        public static Goal ToGoal(this GoalDto goalDto)
        {
            var rank = Rank.Create(goalDto.Rank).Value;
            var status = GoalStatus.Create(goalDto.GoalStatus).Value;
            var deadline = new DateTime(goalDto.Deadline);

            var value = Goal.Create(goalDto.Title, rank, status, null, deadline).Value;
            value.Id = goalDto.Id;
            return value;
        }
    }
}