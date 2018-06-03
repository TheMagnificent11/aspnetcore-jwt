using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.Jwt
{
    /// <summary>
    /// Token Builder
    /// </summary>
    public class TokenBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenBuilder"/> class
        /// </summary>
        /// <param name="settings">JWT settings</param>
        public TokenBuilder(Settings settings)
        {
            Settings = settings;
        }

        private Settings Settings { get; set; }

        /// <summary>
        /// Generates a JWT security token
        /// </summary>
        /// <param name="user">Authenitcated user</param>
        /// <param name="authClaims">Auhorization claims for the authenticated users (excluding standard JWT claims)</param>
        /// <returns>A JWT security token</returns>
        public JwtSecurityToken GenerateToken(IdentityUser user, IEnumerable<Claim> authClaims)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

#pragma warning disable CA1305 // Specify IFormatProvider
            var expires = new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds().ToString();
#pragma warning restore CA1305 // Specify IFormatProvider

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
#pragma warning disable CA1305 // Specify IFormatProvider
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
#pragma warning restore CA1305 // Specify IFormatProvider
                new Claim(JwtRegisteredClaimNames.Exp, expires),
                new Claim(JwtRegisteredClaimNames.Aud, Settings.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, Settings.Issuer)
            };

            if (authClaims != null) claims.AddRange(authClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SigningKey));

            return new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));
        }
    }
}
