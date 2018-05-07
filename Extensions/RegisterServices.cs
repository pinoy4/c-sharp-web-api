using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MWTest.Auth;
using MWTest.ConfigurationOptions;
using MWTest.Db;
using MWTest.Filters;
using MWTest.Managers;
using MWTest.Model;
using System;
using System.Text;

namespace MWTest.Extensions
{
    public static class RegisterServices
    {
        public static void AddMWTestDbService(this IServiceCollection services, IConfigurationSection dbConnectionOptions)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<MWTestDb>(
                options => options.UseNpgsql(dbConnectionOptions["ConnectionString"])
            );
        }

        public static void AddMWTestJwtServices(this IServiceCollection services, IConfigurationSection jwtAppSettingOptions)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions["Secret"]));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256
                );
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RoleUser", policy => policy.RequireClaim("Role", new[] {
                    UserRole.User.ToString(),
                    UserRole.Admin.ToString(),
                    UserRole.Developer.ToString()
                }));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RoleAdmin", policy => policy.RequireClaim("Role", new[] {
                    UserRole.Admin.ToString(),
                    UserRole.Developer.ToString()
                }));
            });
        }

        public static void AddMWTestServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<ValidateModelAttribute>();
        }
    }
}