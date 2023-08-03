using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;

namespace SozlukApp.Infrastructure.Persistence.EntityConfiguration.Entry
{
    public class EntryVoteEntityConfiguration: BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryvote", SozlukAppContext.DEFAULT_SCHEME);

            builder.HasOne(i => i.Entry)
                .WithMany(i => i.EntryVotes)
                .HasForeignKey(i => i.EntryId);

        }
    }
}
