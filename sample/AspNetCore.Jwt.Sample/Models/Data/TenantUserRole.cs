using System.ComponentModel.DataAnnotations.Schema;
using EntityManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// Tenant User Role
    /// </summary>
    public class TenantUserRole : IEntity<long>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// Gets or sets the tenant
        /// </summary>
        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }

        /// <summary>
        /// Gets or sets the user ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the role type ID
        /// </summary>
        public Enums.TenantRoleType RoleTypeId { get; set; }

        /// <summary>
        /// Gets or sets the role type
        /// </summary>
        [ForeignKey(nameof(RoleTypeId))]
        public EnumTypes.TenantRole RoleType { get; set; }
    }
}
