using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNetCore.Jwt.Sample.Constants;

namespace AspNetCore.Jwt.Sample.Logic
{
    /// <summary>
    /// Tenant Role Handler
    /// </summary>
    public sealed class TenantRoleHandler : BaseAuthorizationHandler<TenantRoleRequirement>
    {
        /// <summary>
        /// Gets the administrator role name
        /// </summary>
        protected override string AdministratorRoleName => GlobalRoles.Administrator;

        /// <summary>
        /// Gets a value indicating whether a user withe the administrator role can override the claim requirement
        /// </summary>
        protected override bool AllowAdministratorOverride => true;

        /// <summary>
        /// Determines whether a claim meets the requirment
        /// </summary>
        /// <param name="claim">Claim to check</param>
        /// <param name="requirement">Authorization requirement</param>
        /// <returns>True if claim matches requirement, otherwise false</returns>
        protected override bool MatchesClaimType(Claim claim, TenantRoleRequirement requirement)
        {
            if (!int.TryParse(claim.Type, out int claimTypeValue)) return false;

            var roleValue = (int)requirement.Role;

            return claimTypeValue >= roleValue;
        }

        /// <summary>
        /// Determines whether the request URL path has the requirement claim value
        /// </summary>
        /// <param name="claims">Claims that match authorization requirement</param>
        /// <param name="urlSegments">URL segmenets</param>
        /// <returns>True if required claim is found, otherwise false</returns>
        protected override bool HasRequiredClaim(IEnumerable<Claim> claims, string[] urlSegments)
        {
            var tenantsLabelIdx = Array.IndexOf(urlSegments, "tenants");

            if (tenantsLabelIdx == -1 || urlSegments.Length <= tenantsLabelIdx) return false;

            var tenant = urlSegments[tenantsLabelIdx + 1];

            return claims.Any(i => i.Value == tenant);
        }
    }
}
