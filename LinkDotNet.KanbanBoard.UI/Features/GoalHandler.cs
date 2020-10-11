﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.UI.Shared;
using LinkDotNet.KanbanBoard.Web;
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
                var goals = dto.GoalDto.Select(goalDto => Goal.Create(goalDto.Title, Rank.Create(goalDto.Rank).Value,
                    GoalStatus.Create(goalDto.GoalStatus).Value, Array.Empty<Subtask>(),
                    new DateTime(goalDto.Deadline)).Value);
                GoalState._goals.AddRange(goals);

                return Unit.Task;
            }
        }

        public class AddGoalHandler : ActionHandler<AddGoalAction>
        {
            private readonly Kanban.KanbanClient _kanbanClient;
            private GoalState GoalState => Store.GetState<GoalState>();

            public AddGoalHandler(IStore aStore, Kanban.KanbanClient kanbanClient) : base(aStore)
            {
                _kanbanClient = kanbanClient;
            }

            public override async Task<Unit> Handle(AddGoalAction aAction, CancellationToken aCancellationToken)
            {
                var goal = aAction.Goal;
                var newGoal = await _kanbanClient.AddGoalAsync(goal.ToGoalDto());
                GoalState._goals.Add(newGoal.Goal.ToGoal());
                return Unit.Value;
            }
        }
    }
}