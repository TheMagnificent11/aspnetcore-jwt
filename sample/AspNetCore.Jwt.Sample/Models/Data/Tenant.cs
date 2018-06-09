using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntityManagement.Models;

namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// Tenant
    /// </summary>
    public class Tenant : IEntity<long>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the user roles
        /// </summary>
        public IList<TenantUserRole> UserRoles { get; set; }
    }
}
