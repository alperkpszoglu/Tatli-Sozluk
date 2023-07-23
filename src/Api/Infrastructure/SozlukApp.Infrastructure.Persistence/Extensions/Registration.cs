using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Infrastructure.Persistence.Context;
using SozlukApp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["SozlukAppDbConnectionString"].ToString();
            services.AddDbContext<SozlukAppContext>(conf =>
            {
                conf.UseSqlServer("connectionString");
            });


            // this lines for seeding the data
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
