using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using XamChat.Core;

namespace XamChat.BlazorClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddSingleton<ChatService>((o) => new ChatService());
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
