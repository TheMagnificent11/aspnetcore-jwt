using AspNetCore.Jwt.Sample.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Jwt.Sample.Data
{
    /// <summary>
    /// Database Context
    /// </summary>
    public sealed class DatabaseContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class
        /// </summary>
        /// <param name="options">Database context options</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }
}
