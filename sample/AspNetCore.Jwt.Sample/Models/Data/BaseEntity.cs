using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Jwt.Sample.Models.Data
{
    /// <summary>
    /// Base Entity
    /// </summary>
    /// <typeparam name="T">ID type</typeparam>
    public abstract class BaseEntity<T> : IEntity<T>
        where T : IComparable
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}