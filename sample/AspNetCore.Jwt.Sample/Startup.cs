using AspNetCore.Jwt.Sample.Constants;
using AspNetCore.Jwt.Sample.Data;
using AspNetCore.Jwt.Sample.Logic;
using AspNetCore.Jwt.Sample.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCore.Jwt.Sample
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private const string ApiName = "Sample ASP.Net Core JWT API";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Configures the HTTP request pipeline
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData(app, env);
        }

        /// <summary>
        /// Configures application services
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddJwtBearerAuthentication(
                Configuration["Authentication:TokenSigningKey"],
                Configuration["Authentication:Issuer"],
                Configuration["Authentication:Audience"],
                60,
                1440);

            ConfigureAuthorizationPolicies(services);

            services.AddTransient<UserManager>();

            var apiKeyScheme = new ApiKeyScheme()
            {
                Description = "JWT Authorization Scheme",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            };

            services.AddSwaggerGen(i =>
            {
                i.SwaggerDoc("v1", new Info() { Title = ApiName, Version = "v1" });
                i.AddSecurityDefinition("Bearer", apiKeyScheme);
            });
        }

        private static void ConfigureAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ClaimPolicies.OwnUser, policy =>
                {
                    policy.Requirements.Add(new OwnUserRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, OwnUserHandler>();
        }

        private static void SeedData(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                SeedData(env, serviceScope);
            }
        }

        private static void SeedData(IHostingEnvironment env, IServiceScope serviceScope)
        {
            using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
            {
                if (env.IsDevelopment())
                {
                    context.Database.Migrate();
                }

                context.Seed();
            }
        }
    }
}
