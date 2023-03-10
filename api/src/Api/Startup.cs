using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using Alexandria.Api;
using Alexandria.Api.Security;
using Alexandria.Api.Services;
using Alexandria.Api.Services.Abstractions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Alexandria.Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        IConfiguration configuration = builder.GetContext().Configuration;

        builder.Services.AddOptions();
        builder.Services.Configure<JwtConfig>(options => configuration.GetSection("Jwt").Bind(options));
        builder.Services.AddSingleton(cfg => cfg.GetService<IOptions<JwtConfig>>().Value);

        builder.Services.TryAddTransient<ISayHello, SayHello>();

        builder.Services.Configure<JsonSerializerOptions>(o =>
        {
            o.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IExtensionConfigProvider, JwtBinding>());
        builder.Services.AddSingleton<ISecurityTokenValidator, JwtSecurityTokenHandler>();
        builder.Services.AddSingleton<JwtValidationService>();
        builder.Services.AddSingleton<JwtCreationService>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        FunctionsHostBuilderContext context = builder.GetContext();

        builder.ConfigurationBuilder
               .SetBasePath(context.ApplicationRootPath)
               .AddJsonFile("local.settings.json", true, false);
    }
}
