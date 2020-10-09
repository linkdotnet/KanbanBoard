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
        public Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return Task.FromResult(Array.Empty<Goal>().AsEnumerable());
        }
    }
}