using Chef_Web;
using Chef_Web.Services;
using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Chef_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProviderService>();
            


            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44355/")  });
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<ICookieManager, CookieManager>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IAuthManager, JwtAuthManager>();
            
            await builder.Build().RunAsync();
        }
    }
}