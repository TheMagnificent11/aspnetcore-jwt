using System.Linq;
using AspNetCore.Jwt.Sample.Constants;
using AspNetCore.Jwt.Sample.Models.Data;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Seeds default data
        /// </summary>
        public void Seed()
        {
            var hasDataChanged = false;

            if (SeedGlobalRoles()) hasDataChanged = true;

            if (hasDataChanged) SaveChanges();
        }

        private bool SeedGlobalRoles()
        {
            var admin = Roles.FirstOrDefault(i => i.Name == GlobalRoles.Administrator);
            if (admin != null) return false;

            Roles.Add(new IdentityRole(GlobalRoles.Administrator));

            return true;
        }
    }
}
