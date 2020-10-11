using LinkDotNet.EnumValueObject;
using LinkDotNet.EnumValueObject.Converter.NewtonsoftJsonConverter;
using Newtonsoft.Json;

namespace LinkDotNet.KanbanBoard.Domain
{
    [JsonConverter(typeof(EnumValueObjectNewtonsoftJsonConverter<GoalStatus>))]
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