using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// Base Enum Entity
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    public abstract class BaseEnumEntity<TKey> :
        BaseEntity<TKey>,
        IEntity<TKey>
        where TKey : struct, IConvertible, IComparable
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
