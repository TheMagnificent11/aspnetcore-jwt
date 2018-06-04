using System.ComponentModel;

namespace AspNetCore.Jwt.Sample.Models.Data.Enums
{
    /// <summary>
    /// Tenant Role Type
    /// </summary>
    public enum TenantRoleType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Member
        /// </summary>
        [Description("Member")]
        Member,

        /// <summary>
        /// Owner
        /// </summary>
        [Description("Owner")]
        Owner,
    }
}
