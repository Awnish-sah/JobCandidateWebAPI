using Contract.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Abstraction;
using Repo;
using Repo.Abstraction;
using Swashbuckle.AspNetCore.SwaggerUI;
using Service.DIResolver;
using System.Net;
using Domain;
using Repository;
using Extensions.Helpers;

namespace JobCandidateWebApp
{
    public static class RegisterStartupServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.AddControllers()
                            .AddApplicationPart(typeof(Common.AssemblyReference).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new CustomValidationResponseActionFilter());
            });

            // Register IRepositoryManager and ServiceManager
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            // DbContext with proper configuration
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

            builder.Services.AddCors();

            // Swagger services
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Candidate API",
                    Version = "v1"
                });
            });

            return builder;
        }
    }

    public static class RegisterStartupMiddlewares
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate API v1");
            });

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
