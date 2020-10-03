using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;
using MediatR;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class GoalHandler : ActionHandler<GoalState.AddGoalAction>
        {
            private GoalState GoalState => Store.GetState<GoalState>();

            public GoalHandler(IStore store) : base(store)
            {
            }

            public override Task<Unit> Handle(AddGoalAction aAction, CancellationToken aCancellationToken)
            {
                var dto = aAction.GoalDto;
                var cw = Rank.Create(dto.Rank);
                var rank = cw.Value;
                var goal = new Goal(dto.Title, new DateTime(dto.Deadline), Array.Empty<Subtask>(), rank,
                    GoalStatus.Create(dto.GoalStatus).Value);
                GoalState.Goals.Add(goal);

                return Unit.Task;
            }
        }
    }
}