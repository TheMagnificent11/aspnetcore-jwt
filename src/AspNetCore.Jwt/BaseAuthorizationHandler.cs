using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Jwt
{
    /// <summary>
    /// Base Authorization Handler
    /// </summary>
    /// <typeparam name="T">Authorization Requirement Types</typeparam>
    public abstract class BaseAuthorizationHandler<T> : AuthorizationHandler<T>
        where T : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the administrator role name
        /// </summary>
        protected abstract string AdministratorRoleName { get; }

        /// <summary>
        /// Gets a value indicating whether a user withe the administrator role can override the claim requirement
        /// </summary>
        protected abstract bool AllowAdministratorOverride { get; }

        /// <summary>
        /// Gets the URL segments of the request
        /// </summary>
        /// <param name="context">Authorization context</param>
        /// <returns>Segments of the request URL</returns>
        protected static string[] GetUrlSegments(AuthorizationHandlerContext context)
        {
            if (!(context.Resource is AuthorizationFilterContext)) return null;

            var mvcContext = context.Resource as AuthorizationFilterContext;

            if (mvcContext?.HttpContext?.Request?.Path.Value == null) return null;

            return mvcContext.HttpContext.Request.Path.Value.Split('/');
        }

        /// <summary>
        /// Determines whether a claim meets the requirment
        /// </summary>
        /// <param name="claim">Claim to check</param>
        /// <param name="requirement">Authorization requirement</param>
        /// <returns>True if claim matches requirement, otherwise false</returns>
        protected abstract bool MatchesClaimType(Claim claim, T requirement);

        /// <summary>
        /// Determines whether the request URL path has the requirement claim value
        /// </summary>
        /// <param name="claims">Claims that match authorization requirement</param>
        /// <param name="urlSegments">URL segmenets</param>
        /// <returns>True if required claim is found, otherwise false</returns>
        protected abstract bool HasRequiredClaim(IEnumerable<Claim> claims, string[] urlSegments);

        /// <summary>
        /// Determines if context has the appropriate tenant role claim
        /// </summary>
        /// <param name="context">Authroriztion context</param>
        /// <param name="requirement">Authorization requirement</param>
        /// <returns>Asychronous task</returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            T requirement)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (requirement == null) throw new ArgumentNullException(nameof(requirement));

            if (context.User == null)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (AllowAdministratorOverride && context.User.IsInRole(AdministratorRoleName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var matchingClaims = GetMatchingClaims(context, requirement);

            if (matchingClaims == null || matchingClaims.Count() == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var urlSegments = GetUrlSegments(context);

            if (urlSegments == null || urlSegments.Length == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!HasRequiredClaim(matchingClaims, urlSegments))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private IEnumerable<Claim> GetMatchingClaims(
            AuthorizationHandlerContext context,
            T requirement)
        {
            if (context.User?.Claims == null) return null;

            return context.User.Claims.Where(i => MatchesClaimType(i, requirement));
        }
    }
}
