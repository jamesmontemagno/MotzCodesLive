using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using XamChat.BlazorClient.Services;

namespace XamChat.BlazorClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddSingleton<BlazorChatService>((o) => new BlazorChatService());
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
