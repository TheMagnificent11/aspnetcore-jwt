using AspNetCore.Jwt.Sample.Models.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Jwt.Sample.Logic
{
    /// <summary>
    /// Tenant Role Requirement
    /// </summary>
    public sealed class TenantRoleRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantRoleRequirement"/> class
        /// </summary>
        /// <param name="role">Role requirement</param>
        public TenantRoleRequirement(TenantRoleType role)
        {
            Role = role;
        }

        /// <summary>
        /// Gets the role requirement
        /// </summary>
        public TenantRoleType Role { get; private set; }
    }
}
