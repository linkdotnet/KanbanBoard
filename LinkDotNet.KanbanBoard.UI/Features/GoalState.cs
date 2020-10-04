using System;
using System.Collections.Generic;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState : State<GoalState>, ICloneable
    {
        private List<Goal> _goals;
        public IReadOnlyList<Goal> Goals => _goals;

        public override void Initialize()
        {
            _goals = new List<Goal>();
        }

        public object Clone()
        {
            var newState = new GoalState {_goals = new List<Goal>(_goals)};
            return newState;
        }
    }
}