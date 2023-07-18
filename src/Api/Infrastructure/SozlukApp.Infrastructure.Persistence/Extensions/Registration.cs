using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SozlukApp.Infrastructure.Persistence.Context;
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

            return services;
        }
    }
}
