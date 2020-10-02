using System.Threading.Tasks;
using Grpc.Core;
using LinkDotNet.KanbanBoard.Infrastructure;
using Google.Protobuf.WellKnownTypes;

namespace LinkDotNet.KanbanBoard.Web.Services
{
    public class Kanban : Web.Kanban.KanbanBase
    {
        private readonly IKanbanRepository _kanbanRepository;

        public Kanban(IKanbanRepository kanbanRepository)
        {
            _kanbanRepository = kanbanRepository;
        }

        public override async Task GetAllGoals(Empty request, IServerStreamWriter<GoalDto> responseStream, ServerCallContext context)
        {
            foreach (var goal in await _kanbanRepository.GetAllGoalsAsync())
            {
                var goalDto = new GoalDto
                {
                    Title = goal.Title,
                    GoalStatus = goal.GoalStatus.Key,
                    Deadline = goal.Deadline.Ticks,
                    Rank = goal.Rank.Key
                };
                await responseStream.WriteAsync(goalDto);
            }
        }
    }
}