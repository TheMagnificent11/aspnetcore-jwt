using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Jwt.Sample.Logic
{
    /// <summary>
    /// Own User Requirement
    /// </summary>
    public sealed class OwnUserRequirement : IAuthorizationRequirement
    {
    }
}
