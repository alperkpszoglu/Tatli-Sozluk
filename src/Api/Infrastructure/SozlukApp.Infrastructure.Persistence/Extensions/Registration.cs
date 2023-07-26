using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Infrastructure.Persistence.Context;
using SozlukApp.Infrastructure.Persistence.Repositories;

namespace SozlukApp.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SozlukAppContext>(conf =>
            {
                conf.UseSqlServer(configuration.GetConnectionString("SozlukAppDbConnectionString"));
            });
            Console.WriteLine(configuration.GetConnectionString("SozlukAppDbConnectionString"));
            services.AddScoped<DbContext, SozlukAppContext>();

            // this lines for seeding the data
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

            return services;
        }
    }
}
