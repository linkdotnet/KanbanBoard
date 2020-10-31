using System;
using LinkDotNet.KanbanBoard.Domain;

namespace LinkDotNet.KanbanBoard.Web.Services
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
                IsDeleted = goal.IsDeleted,
            };
        }

        public static Goal ToGoal(this GoalDto goalDto)
        {
            var rank = Rank.Create(goalDto.Rank).Value;
            var status = GoalStatus.Create(goalDto.GoalStatus).Value;
            var deadline = new DateTime(goalDto.Deadline);

            var goal = Goal.Create(goalDto.Title, rank, status, null, deadline).Value;
            goal.Id = string.IsNullOrEmpty(goalDto.Id) ? null : goalDto.Id;
            goal.IsDeleted = goalDto.IsDeleted;
            return goal;
        }
    }
}