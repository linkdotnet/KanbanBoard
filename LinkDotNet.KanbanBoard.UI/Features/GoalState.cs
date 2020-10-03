using System;
using System.Collections.Generic;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState : State<GoalState>
    {
        public IList<Goal> Goals { get; private set; }

        public override void Initialize()
        {
            Goals = new List<Goal>();
        }
    }
}