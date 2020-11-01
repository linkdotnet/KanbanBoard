using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
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

        public override async Task<GoalListDto> GetAllGoals(GetAllGoalsDto request, ServerCallContext context)
        {
            var allGoals = await _kanbanRepository.GetAllGoalsAsync(request.GetDeleted);
            var goalsDto = allGoals.Select(goal => goal.ToGoalDto());
            return new GoalListDto { GoalDto = { goalsDto } };
        }

        public override async Task<GoalAdded> AddGoal(GoalDto request, ServerCallContext context)
        {
            var goal = request.ToGoal();
            await _kanbanRepository.StoreGoalAsync(goal);
            request.Id = goal.Id;

            return new GoalAdded
            {
                Goal = request
            };
        }

        public override async Task<Empty> UpdateGoal(GoalDto request, ServerCallContext context)
        {
            var goal = request.ToGoal();
            await _kanbanRepository.StoreGoalAsync(goal);
            return new Empty();
        }
    }
}