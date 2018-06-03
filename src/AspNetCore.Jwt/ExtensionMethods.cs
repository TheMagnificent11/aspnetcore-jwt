using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.Jwt
{
    /// <summary>
    /// JWT Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds JWT bearer authentication
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="tokenSigningKey">Token signing key</param>
        /// <param name="issuer">Token issuer</param>
        /// <param name="audience">Token audience</param>
        /// <param name="clockSkewMins">Minutes to add to time used when validation token</param>
        /// <param name="expiryMins">Minutes to token expiry</param>
        /// <returns>Authentication builder</returns>
        public static AuthenticationBuilder AddJwtBearerAuthentication(
            this IServiceCollection services,
            string tokenSigningKey,
            string issuer,
            string audience,
            int clockSkewMins,
            int expiryMins)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(new Settings()
            {
                SigningKey = tokenSigningKey,
                Issuer = issuer,
                Audience = audience,
                ExpiryMins = expiryMins
            });

            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSigningKey)),

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.FromMinutes(clockSkewMins)
                };
            });
        }
    }
}
