using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;
using MediatR;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class LoadGoalsHandler : ActionHandler<LoadGoalsAction>
        {
            private GoalState GoalState => Store.GetState<GoalState>();

            public LoadGoalsHandler(IStore store) : base(store)
            {
            }

            public override Task<Unit> Handle(LoadGoalsAction aAction, CancellationToken aCancellationToken)
            {
                var dto = aAction.GoalListDto;
                var goals = dto.GoalDto.Select(goalDto => new Goal(goalDto.Title, new DateTime(goalDto.Deadline), Array.Empty<Subtask>(),
                    Rank.Create(goalDto.Rank).Value,
                    GoalStatus.Create(goalDto.GoalStatus).Value));
                GoalState._goals.AddRange(goals);

                return Unit.Task;
            }
        }

        public class AddGoalHandler : ActionHandler<AddGoalAction>
        {
            private GoalState GoalState => Store.GetState<GoalState>();

            public AddGoalHandler(IStore aStore) : base(aStore)
            {
            }

            public override Task<Unit> Handle(AddGoalAction aAction, CancellationToken aCancellationToken)
            {
                GoalState._goals.Add(aAction.Goal);
                return Unit.Task;
            }
        }
    }
}