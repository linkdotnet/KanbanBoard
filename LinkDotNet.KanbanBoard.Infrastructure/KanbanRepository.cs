using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkDotNet.KanbanBoard.Domain;

namespace LinkDotNet.KanbanBoard.Infrastructure
{
    public interface IKanbanRepository
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync();
    }

    public class KanbanRepository : IKanbanRepository
    {
        private readonly List<Goal> _goals = new List<Goal>
        {
            new Goal("Done Item", DateTime.Now, Array.Empty<Subtask>(), Rank.Important, GoalStatus.Completed),
            new Goal("Important Item to handle", DateTime.Now, Array.Empty<Subtask>(), Rank.Important, GoalStatus.InProgress),
            new Goal("I don't have that much priority", DateTime.Now, Array.Empty<Subtask>(), Rank.None, GoalStatus.InProgress),
            new Goal("I am very important and urgent to do", DateTime.Now, Array.Empty<Subtask>(), Rank.ImportantAndUrgent, GoalStatus.Todo),
            new Goal("I am a goal as well", DateTime.Now, Array.Empty<Subtask>(), Rank.Urgent, GoalStatus.Todo),
        };

        public Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return Task.FromResult(_goals.AsEnumerable());
        }
    }
}