using System;
using Microsoft.Azure.WebJobs.Description;

namespace Alexandria.Api.Security;

[Binding]
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
public class JwtAttribute : Attribute { }
