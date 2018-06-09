using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Jwt.Sample.Models.Data;
using EntityManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Jwt.Sample.Data
{
    /// <summary>
    /// Tenant User Role Repository
    /// </summary>
    public sealed class TenantUserRoleRepository : EntityRepository<TenantUserRole, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantUserRoleRepository"/> class
        /// </summary>
        /// <param name="context">Database context</param>
        public TenantUserRoleRepository(IDatabaseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Retrieves the tenant roles for a specific user
        /// </summary>
        /// <param name="userId">ID of user for which to retrieve the tenant roles</param>
        /// <returns>List of tenant roles if the user exists, otherwise null</returns>
        public Task<List<TenantUserRole>> RetrieveByUserId(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            return Context.EntitySet<TenantUserRole>()
                .Include(i => i.Tenant)
                .Include(i => i.User)
                .Include(i => i.RoleType)
                .Where(i =>
                    i.UserId.Equals(userId, StringComparison.Ordinal))
                .ToListAsync();
        }
    }
}
