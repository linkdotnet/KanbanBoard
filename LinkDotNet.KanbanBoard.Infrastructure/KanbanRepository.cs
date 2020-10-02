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
            new Goal("Some Goal Title", DateTime.Now, Array.Empty<Subtask>(), Rank.Important, GoalStatus.Completed),
            new Goal("Some Goal Title 2", DateTime.Now, Array.Empty<Subtask>(), Rank.Important, GoalStatus.InProgress),
            new Goal("Some Goal Title 3", DateTime.Now, Array.Empty<Subtask>(), Rank.Important, GoalStatus.InProgress),
            new Goal("Some Goal Title 4", DateTime.Now, Array.Empty<Subtask>(), Rank.ImportantAndUrgent, GoalStatus.Todo),
            new Goal("Some Goal Title 5", DateTime.Now, Array.Empty<Subtask>(), Rank.Urgent, GoalStatus.Todo),
        };

        public Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return Task.FromResult(_goals.AsEnumerable());
        }
    }
}