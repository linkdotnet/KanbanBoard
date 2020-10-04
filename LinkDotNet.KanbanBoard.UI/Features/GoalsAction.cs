using BlazorState;
using LinkDotNet.KanbanBoard.Web;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public partial class GoalState
    {
        public class AddGoalAction : IAction
        {
            public AddGoalAction(GoalDto goalDto)
            {
                GoalDto = goalDto;
            }

            public GoalDto GoalDto { get; }
        }
    }
}