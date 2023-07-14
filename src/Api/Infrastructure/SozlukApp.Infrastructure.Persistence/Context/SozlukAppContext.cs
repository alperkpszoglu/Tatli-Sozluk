using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.Context
{
    public class SozlukAppContext : DbContext
    {
        public SozlukAppContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<EntryComment> EntryComments { get; set; }
        DbSet<EntryFavorite> EntryFavorites { get; set; }
        DbSet<EntryVote> EntryVotes { get; set; }
        DbSet<EmailConfirmation> EmailConfirmations { get; set; }
        DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
        DbSet<EntryCommentVote> EntryCommentVotes{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<User>();
            // we can execute all the entries instead of define by one by
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
