using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Json.Serialization.NewtonsoftJson;

namespace LinkDotNet.KanbanBoard.Infrastructure
{
    public static class RavenDbConnectionProvider
    {
        public static IDocumentStore Create(string url, string databaseName)
        {
            var documentStore = new DocumentStore {Urls = new[] {url}, Database = databaseName};
            documentStore.Initialize();
            return documentStore;
        }
    }
}