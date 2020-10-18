using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using BlazorState;
using LinkDotNet.KanbanBoard.UI.Shared;
using LinkDotNet.KanbanBoard.Web;
using MediatR;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class LoadGoalsHandler : ActionHandlerBase<LoadGoalsAction>
        {
            private readonly Kanban.KanbanClient _kanbanClient;
            private GoalState GoalState => Store.GetState<GoalState>();

            public LoadGoalsHandler(IStore store, IToastService toastService, Kanban.KanbanClient kanbanClient) : base(store, toastService)
            {
                _kanbanClient = kanbanClient;
            }

            protected override async Task<Unit> InnerHandleAsync(LoadGoalsAction aAction, CancellationToken aCancellationToken)
            {
                var goalListDto = await _kanbanClient.GetAllGoalsAsync(new Empty());
                var goals = goalListDto.GoalDto.Select(goalDto => goalDto.ToGoal());
                GoalState._goals.AddRange(goals);

                return Unit.Value;
            }
        }

        public class AddGoalHandler : ActionHandlerBase<AddGoalAction>
        {
            private readonly Kanban.KanbanClient _kanbanClient;
            private GoalState GoalState => Store.GetState<GoalState>();

            public AddGoalHandler(IStore aStore, IToastService toastService, Kanban.KanbanClient kanbanClient) : base(aStore, toastService)
            {
                _kanbanClient = kanbanClient;
            }

            protected override async Task<Unit> InnerHandleAsync(AddGoalAction aAction, CancellationToken aCancellationToken)
            {
                var goal = aAction.Goal;
                var newGoal = await _kanbanClient.AddGoalAsync(goal.ToGoalDto());
                GoalState._goals.Add(newGoal.Goal.ToGoal());
                return Unit.Value;
            }
        }

        public class ChangeGoalStatusHandler : ActionHandlerBase<ChangeGoalStatusAction>
        {
            private readonly IStore _aStore;
            private readonly Kanban.KanbanClient _kanbanClient;

            public ChangeGoalStatusHandler(IStore aStore, IToastService toastService, Kanban.KanbanClient kanbanClient) : base(aStore, toastService)
            {
                _aStore = aStore;
                _kanbanClient = kanbanClient;
            }

            protected override async Task<Unit> InnerHandleAsync(ChangeGoalStatusAction aAction, CancellationToken aCancellationToken)
            {
                var goal = _aStore.GetState<GoalState>()._goals.Single(g => g.Id == aAction.Id);
                goal.GoalStatus = aAction.NewStatus;
                await _kanbanClient.UpdateGoalAsync(goal.ToGoalDto());

                return Unit.Value;
            }
        }
    }
}