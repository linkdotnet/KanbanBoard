using System.Collections.Generic;
using BlazorState;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.Web;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class LoadGoalsAction : IAction
        {
            public LoadGoalsAction(GoalListDto goalListDto)
            {
                GoalListDto = goalListDto;
            }

            public GoalListDto GoalListDto { get; }
        }

        public class AddGoalAction : IAction
        {
            public AddGoalAction(Goal goal)
            {
                Goal = goal;
            }

            public Goal Goal { get; }
        }
    }
}