using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SozlukApp.Api.Domain.Models;
using SozlukAppCommon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr") //we've used Bogus package
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password()))
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .Generate(500);

            return result;
        }


        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();

            dbContextBuilder.UseSqlServer(configuration["SozlukAppDbConnectionString"]);

            var context = new SozlukAppContext(dbContextBuilder.Options);

            var users = GetUsers();

            var userIds = users.Select(u => u.Id);

            // added users
            await context.Users.AddRangeAsync(users);


            // added entries

            var guids = Enumerable.Range(0,150).Select(Range => Guid.NewGuid()).ToList();

            int count = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(i => i.Id, i => guids[count++])
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);


            // added comments
            var entryComments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id,i =>  Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.EntryId, i => i.PickRandom(guids))
                .Generate(1000);

            await context.EntryComments.AddRangeAsync(entryComments);

            await context.SaveChangesAsync();
        }
    }
}
