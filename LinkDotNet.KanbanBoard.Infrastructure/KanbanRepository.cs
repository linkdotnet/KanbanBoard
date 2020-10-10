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

        Task AddGoalAsync(Goal goal);
    }

    public class KanbanRepository : IKanbanRepository
    {
        public Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return Task.FromResult(Array.Empty<Goal>().AsEnumerable());
        }

        public Task AddGoalAsync(Goal goal)
        {
            return Task.CompletedTask;
        }
    }
}