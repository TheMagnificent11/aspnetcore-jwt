using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Jwt.Sample.Constants;
using AspNetCore.Jwt.Sample.Models.Client;
using AspNetCore.Jwt.Sample.Models.Data;
using Identity = Microsoft.AspNetCore.Identity;

namespace AspNetCore.Jwt.Sample.Logic
{
    /// <summary>
    /// User Manager
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="userManager">Identity framework user manager</param>
        /// <param name="tokenBuilder">JWT token builder</param>
        public UserManager(
            Identity.UserManager<User> userManager,
            TokenBuilder tokenBuilder)
        {
            IdentityUserManager = userManager;
            TokenBuilder = tokenBuilder;
        }

        private Identity.UserManager<User> IdentityUserManager { get; set; }

        private TokenBuilder TokenBuilder { get; set; }

        /// <summary>
        /// Creates a user with the given password
        /// </summary>
        /// <param name="user">User to create</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public Task<Identity.IdentityResult> Create(User user, string password)
        {
            return IdentityUserManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Result</returns>
        public Task<Identity.IdentityResult> Update(User user)
        {
            return IdentityUserManager.UpdateAsync(user);
        }

        /// <summary>
        /// Finds the user with the specified email
        /// </summary>
        /// <param name="email">Email username of user to retrieve</param>
        /// <returns>User if exists, otherwise null</returns>
        public Task<User> FindUser(string email)
        {
            return IdentityUserManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Authenticates the specified user with the given username and password
        /// </summary>
        /// <param name="email">Email username used to identify the user</param>
        /// <param name="password">Password used to authenticate user</param>
        /// <returns>Authentication token if correction details are provided, otherwise null</returns>
        public async Task<AuthToken> SignIn(string email, string password)
        {
            var user = await IdentityUserManager.FindByEmailAsync(email);
            if (user == null) return null;

            var isValidPasswrod = await IdentityUserManager.CheckPasswordAsync(user, password);
            if (!isValidPasswrod) return null;

            var globalRoles = await IdentityUserManager.GetRolesAsync(user);
            var isAdmin = globalRoles.Any(i => i == GlobalRoles.Administrator);

            return GenerateToken(user, isAdmin);
        }

        private AuthToken GenerateToken(User user, bool isAdmin)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>()
            {
                new Claim(GlobalClaims.UserId, user.Id)
            };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, GlobalRoles.Administrator));
            }

            var jwtToken = TokenBuilder.GenerateToken(user, claims, out DateTimeOffset expires);

            return new AuthToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
#pragma warning disable CA1305 // Specify IFormatProvider
                Expires = expires.ToUnixTimeSeconds().ToString(),
#pragma warning restore CA1305 // Specify IFormatProvider
                Email = user.Email,
                UserId = user.Id
            };
        }
    }
}
