using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repo.Abstraction;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DIResolver
{
    public static class DataContextResolver
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
    }
}
