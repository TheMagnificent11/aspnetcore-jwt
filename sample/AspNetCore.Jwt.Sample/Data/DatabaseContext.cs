using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Jwt.Sample.Constants;
using AspNetCore.Jwt.Sample.Helpers;
using AspNetCore.Jwt.Sample.Models.Data;
using AspNetCore.Jwt.Sample.Models.Data.EnumTypes;
using EntityManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Enums = AspNetCore.Jwt.Sample.Models.Data.Enums;

namespace AspNetCore.Jwt.Sample.Data
{
    /// <summary>
    /// Database Context
    /// </summary>
    public sealed class DatabaseContext :
        IdentityDbContext<User>,
        IDatabaseContext
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
        /// Gets or sets the number of attached repositories
        /// </summary>
        public int AttachedRepositories { get; set; }

        /// <summary>
        /// Gets the entity set for the specified entity type
        /// </summary>
        /// <typeparam name="T">Entity type of database set</typeparam>
        /// <returns>Database set</returns>
        public DbSet<T> EntitySet<T>()
            where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database
        /// </summary>
        /// <returns>The number of state entries written to the underlying database</returns>
        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }

        /// <summary>
        /// Seeds default data
        /// </summary>
        public void Seed()
        {
            var hasDataChanged = false;

            if (SeedGlobalRoles()) hasDataChanged = true;

            if (AddEnumReferenceData<TenantRole, Enums.TenantRoleType>(false))
            {
                hasDataChanged = true;
            }

            if (hasDataChanged) SaveChanges();
        }

        private bool SeedGlobalRoles()
        {
            var admin = Roles.FirstOrDefault(i => i.Name == GlobalRoles.Administrator);
            if (admin != null) return false;

            Roles.Add(new IdentityRole(GlobalRoles.Administrator));

            return true;
        }

        private bool AddEnumReferenceData<TRefData, TEnum>(bool allowZero)
            where TRefData : BaseEnumEntity<TEnum>, new()
            where TEnum : struct, IConvertible, IComparable
        {
            var dbSet = Set<TRefData>();
            var hasChanges = false;

            foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
            {
                var enumVal = (int)(object)item;

                if (!allowZero)
                {
                    if (enumVal == 0) continue;
                }

                var name = typeof(TEnum).GetDescription(enumVal);
                if (string.IsNullOrEmpty(name)) name = item.ToString();

                var dbItem = dbSet.FirstOrDefault(i => i.Id.Equals(item));

                if (dbItem == null)
                {
                    dbItem = new TRefData()
                    {
                        Id = item,
                        Name = name
                    };

                    dbSet.Add(dbItem);
                    hasChanges = true;
                }
                else
                {
                    if (dbItem.Name == name) continue;

                    dbItem.Name = name;
                    dbSet.Update(dbItem);
                    hasChanges = true;
                }
            }

            return hasChanges;
        }
    }
}
