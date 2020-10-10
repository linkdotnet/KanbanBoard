using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.Infrastructure;

namespace LinkDotNet.KanbanBoard.Web.Services
{
    public class Kanban : Web.Kanban.KanbanBase
    {
        private readonly IKanbanRepository _kanbanRepository;

        public Kanban(IKanbanRepository kanbanRepository)
        {
            _kanbanRepository = kanbanRepository;
        }

        public override async Task<GoalListDto> GetAllGoals(Empty request, ServerCallContext context)
        {
            var allGoals = await _kanbanRepository.GetAllGoalsAsync();
            var goalsDto = allGoals.Select(goal => new GoalDto
            {
                Title = goal.Title,
                GoalStatus = goal.GoalStatus.Key,
                Deadline = goal.Deadline.Ticks,
                Rank = goal.Rank.Key
            });
            return new GoalListDto { GoalDto = { goalsDto } };
        }

        public override async Task<Empty> AddGoal(GoalDto request, ServerCallContext context)
        {
            var rankResult = Rank.Create(request.Rank);
            var goalStatusResult = GoalStatus.Create(request.GoalStatus);
            var goalResult = Goal.Create(request.Title, rankResult.Value, goalStatusResult.Value, Array.Empty<Subtask>(),
                new DateTime(request.Deadline));
            await _kanbanRepository.AddGoalAsync(goalResult.Value);

            return new Empty();
        }
    }
}