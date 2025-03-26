using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using System.Text;

namespace DevFreela.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.
                AddRepositories()
            .AddData(configuration)
            .AddAuth(configuration)
            .AddEmailService(configuration);

            return services;
        }

        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DevFreelaCs");

            services.AddDbContext<DevFreelaDbContext>(o => o.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkilllRepository, SkillRepository>();
    
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(JwtBearerDefault.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            return services;
        }

        private static IServiceCollection AddEmailService (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(o =>
            {
                o.ApiKey = configuration.GetValue<string>("SendGrid : ApiKey");
            });

            //Passo 1: Adicionar scoped e email service e implementar

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
