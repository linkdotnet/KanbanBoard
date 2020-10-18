using System.Collections.Generic;
using System.Threading.Tasks;
using LinkDotNet.KanbanBoard.Domain;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace LinkDotNet.KanbanBoard.Infrastructure
{
    public interface IKanbanRepository
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync(bool retrieveDeleted);

        Task AddGoalAsync(Goal goal);

        Task UpdateGoalAsync(Goal goal);
    }

    public class KanbanRepository : IKanbanRepository
    {
        private readonly IDocumentStore _documentStore;

        public KanbanRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<IEnumerable<Goal>> GetAllGoalsAsync(bool retrieveDeleted)
        {
            using var session = _documentStore.OpenAsyncSession();
            if (retrieveDeleted)
            {
                return await session.Query<Goal>().ToListAsync();
            }

            return await session.Query<Goal>().Where(g => g.IsDeleted == false).ToListAsync();
        }

        public async Task AddGoalAsync(Goal goal)
        {
            using var session = _documentStore.OpenAsyncSession();
            await session.StoreAsync(goal);
            await session.SaveChangesAsync();
        }

        public async Task UpdateGoalAsync(Goal goal)
        {
            using var session = _documentStore.OpenAsyncSession();
            await session.StoreAsync(goal);
            await session.SaveChangesAsync();
        }
    }
}