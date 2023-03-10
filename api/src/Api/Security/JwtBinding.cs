using System;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Alexandria.Api.Security;

[Extension(nameof(JwtBinding))]
public class JwtBinding : IExtensionConfigProvider
{
    private readonly JwtValidationService _customAuthorizationService;

    public JwtBinding(
        JwtValidationService customAuthorizationService)
    {
        _customAuthorizationService = customAuthorizationService;
    }

    public void Initialize(ExtensionConfigContext context)
    {
#pragma warning disable CS0618
        FluentBindingRule<JwtAttribute> rule = context.AddBindingRule<JwtAttribute>();
#pragma warning restore
        rule.BindToInput(GetAzureAdTokenAsync);
    }

    private JwtValidationResult GetAzureAdTokenAsync(JwtAttribute attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);

        return _customAuthorizationService.GetClaimsPrincipalAsync();
    }
}
