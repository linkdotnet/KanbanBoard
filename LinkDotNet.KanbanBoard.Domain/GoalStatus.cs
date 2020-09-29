using LinkDotNet.EnumValueObject;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class GoalStatus : EnumValueObject<GoalStatus>
    {
        public static readonly GoalStatus Todo = new GoalStatus(nameof(Todo));
        public static readonly GoalStatus InProgress = new GoalStatus("In Progress");
        public static readonly GoalStatus Completed = new GoalStatus(nameof(Completed));

        private GoalStatus(string key) : base(key)
        {
        }
    }
}