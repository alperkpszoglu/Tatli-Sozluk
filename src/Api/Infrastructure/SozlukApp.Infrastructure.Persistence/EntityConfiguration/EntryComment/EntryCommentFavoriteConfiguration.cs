using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.EntityConfiguration.Entry
{
    public class EntryCommentFavoriteConfiguration: BaseEntityConfiguration<EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentfavorite", SozlukAppContext.DEFAULT_SCHEME);

            builder.HasOne(i => i.EntryComment)
               .WithMany(i => i.EntryCommentFavorites)
               .HasForeignKey(i => i.EntryCommentId);

            builder.HasOne(i => i.CreatedUser)
                .WithMany(i => i.EntryCommentFavorites)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
