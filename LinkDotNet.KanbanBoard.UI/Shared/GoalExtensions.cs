using System;
using System.Linq;
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
                IsDeleted = goal.IsDeleted
            };

            if (goal.Id != null)
            {
                goalDto.Id = goal.Id;
            }

            goalDto.Subtasks.AddRange(goal.Subtasks.Select(s => new SubtaskDto { Title = s.Title }));

            return goalDto;
        }

        public static Goal ToGoal(this GoalDto goalDto)
        {
            var rank = Rank.Create(goalDto.Rank).Value;
            var status = GoalStatus.Create(goalDto.GoalStatus).Value;
            var deadline = new DateTime(goalDto.Deadline);
            var subtasks = goalDto.Subtasks.Select(s => Subtask.Create(s.Title).Value);

            var goal = Goal.Create(goalDto.Title, rank, status, subtasks, deadline).Value;
            goal.Id = string.IsNullOrEmpty(goalDto.Id) ? null : goalDto.Id;
            goal.IsDeleted = goalDto.IsDeleted;
            return goal;
        }
    }
}