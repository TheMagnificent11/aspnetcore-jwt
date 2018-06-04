using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's given name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the user's surname
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the tenant roles
        /// </summary>
        public IList<TenantUserRole> TenantRoles { get; set; }
    }
}
