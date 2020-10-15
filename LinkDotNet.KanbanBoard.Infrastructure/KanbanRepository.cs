using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkDotNet.KanbanBoard.Domain;
using Raven.Client.Documents;

namespace LinkDotNet.KanbanBoard.Infrastructure
{
    public interface IKanbanRepository
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync();

        Task AddGoalAsync(Goal goal);
        Task UpdateGoalStatusAsync(string id, GoalStatus newStatus);
    }

    public class KanbanRepository : IKanbanRepository
    {
        private readonly IDocumentStore _documentStore;

        public KanbanRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            using var session = _documentStore.OpenAsyncSession();
            return await session.Query<Goal>().ToListAsync();
        }

        public async Task AddGoalAsync(Goal goal)
        {
            using var session = _documentStore.OpenAsyncSession();
            await session.StoreAsync(goal);
            await session.SaveChangesAsync();
        }

        public async Task UpdateGoalStatusAsync(string id, GoalStatus newStatus)
        {
            using var session = _documentStore.OpenAsyncSession();
            var goal = await session.LoadAsync<Goal>(id);
            goal.GoalStatus = newStatus;
            await session.SaveChangesAsync();
        }
    }
}