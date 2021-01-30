using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBoards.Web.App.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SimpleBoards.Web.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<BoardsHttpClient>(c => c.BaseAddress = new Uri("https://localhost:6001"))
                .AddHttpMessageHandler(provider => 
                {
                    var handler = provider.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:6001" },
                            scopes: new[] { "simpleboards.web.api" }
                        );

                    return handler;
                });

            builder.Services.AddHttpClient<IssuesHttpClient>(c => c.BaseAddress = new Uri("https://localhost:6001"))
                .AddHttpMessageHandler(provider => 
                {
                    var handler = provider.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:6001" },
                            scopes: new[] { "simpleboards.web.api" }
                        );

                    return handler;
                });

            builder.Services.AddHttpClient<UsersHttpClient>(c => c.BaseAddress = new Uri("https://localhost:6001"))
                .AddHttpMessageHandler(provider => 
                {
                    var handler = provider.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:6001" },
                            scopes: new[] { "simpleboards.web.api" }
                        );

                    return handler;
                });

            builder.Services.AddScoped(
                provider => provider.GetRequiredService<IHttpClientFactory>().CreateClient("simpleboards.web.api"));

            builder.Services.AddOidcAuthentication(options => 
            {
                builder.Configuration.Bind("Identity", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("simpleboards.web.api");
            });

            await builder.Build().RunAsync();
        }
    }
}
