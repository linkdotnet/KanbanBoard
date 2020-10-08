using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorState;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using LinkDotNet.KanbanBoard.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDotNet.KanbanBoard.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            RegisterGrpcWebEndpoint(builder);
            RegisterBlazorState(builder);

            var host = builder.Build();
            await host.RunAsync();
        }

        private static void RegisterGrpcWebEndpoint(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var grpcEndpointUrl = config["BackendUrl"];

                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());

                return GrpcChannel.ForAddress(grpcEndpointUrl, new GrpcChannelOptions {HttpHandler = httpHandler});
            });

            builder.Services.AddSingleton(sp =>
            {
                var service = sp.GetService<GrpcChannel>();
                return new Kanban.KanbanClient(service);
            });
        }

        private static void RegisterBlazorState(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddBlazorState(opt => opt.Assemblies = new[] {typeof(Program).Assembly});
        }
    }
}
