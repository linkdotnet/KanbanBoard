using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using CSharpFunctionalExtensions;
using LinkDotNet.KanbanBoard.Domain;
using MediatR;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class GoalHandler : ActionHandler<AddGoalAction>
        {
            private GoalState GoalState => Store.GetState<GoalState>();

            public GoalHandler(IStore store) : base(store)
            {
            }

            public override Task<Unit> Handle(AddGoalAction aAction, CancellationToken aCancellationToken)
            {
                var dto = aAction.GoalDto;
                var goal = new Goal(dto.Title, new DateTime(dto.Deadline), Array.Empty<Subtask>(),
                    Rank.Create(dto.Rank).Value,
                    GoalStatus.Create(dto.GoalStatus).Value);
                GoalState._goals.Add(goal);

                return Unit.Task;
            }
        }
    }
}