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
            var goalsDto = allGoals.Select(goal => goal.ToGoalDto());
            return new GoalListDto { GoalDto = { goalsDto } };
        }

        public override async Task<GoalAdded> AddGoal(GoalDto request, ServerCallContext context)
        {
            var goal = request.ToGoal();
            await _kanbanRepository.AddGoalAsync(goal);
            request.Id = goal.Id;

            return new GoalAdded
            {
                Goal = request
            };
        }

        public override async Task<Empty> ChangeGoalStatus(GoalRankChangedDto request, ServerCallContext context)
        {
            var goalId = request.Id;
            var newGoalStatus = GoalStatus.Create(request.GoalStatus).Value;

            await _kanbanRepository.UpdateGoalStatusAsync(goalId, newGoalStatus);

            return new Empty();
        }
    }
}