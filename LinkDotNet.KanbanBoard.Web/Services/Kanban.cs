using System.Threading.Tasks;
using Grpc.Core;

namespace LinkDotNet.KanbanBoard.Web.Services
{
    public class Kanban : Web.Kanban.KanbanBase
    {
        public override Task<HealthStatus> HealthCheck(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new HealthStatus
            {
                WelcomeMessage = "Hello from the Server"
            });
        }
    }
}