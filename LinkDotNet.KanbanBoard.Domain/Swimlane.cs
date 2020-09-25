using System.Collections.Generic;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Swimlane
    {
        public string Title { get; private set; }
        public IEnumerable<Goal> Goals { get; private set; }
    }
}