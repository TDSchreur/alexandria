using System;
using Alexandria.Api.Services.Abstractions;

namespace Alexandria.Api.Services;

public class SayHello : ISayHello
{
    public string Hello(string firstName, string lastName)
    {
        return $"Hello, {firstName} {lastName}. The time is {DateTime.Now:HH:mm:ss}.";
    }
}
