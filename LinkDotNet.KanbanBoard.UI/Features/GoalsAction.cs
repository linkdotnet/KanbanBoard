using BlazorState;
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
    }
}