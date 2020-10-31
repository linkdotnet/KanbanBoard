using CSharpFunctionalExtensions;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Subtask
    {
        public string Title { get; private set; }
        public double DurationTime { get; private set; }

        public static Result<Subtask> Create(string title)
        {
            return new Subtask {Title = title};
        }
    }
}