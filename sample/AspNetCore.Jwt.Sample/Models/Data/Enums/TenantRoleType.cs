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
        /// Content Consumer
        /// </summary>
        [Description("Content Consumer")]
        ContentConsumer,

        /// <summary>
        /// Content Distributor
        /// </summary>
        [Description("Content Distributor")]
        ContentDistributor,

        /// <summary>
        /// Content Creator
        /// </summary>
        [Description("Content Creator")]
        ContentCreator,

        /// <summary>
        /// Administrator
        /// </summary>
        [Description("Administrator")]
        Administrator
    }
}
