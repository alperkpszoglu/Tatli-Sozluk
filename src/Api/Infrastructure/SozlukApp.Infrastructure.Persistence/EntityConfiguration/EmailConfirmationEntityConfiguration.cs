using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;

namespace SozlukApp.Infrastructure.Persistence.EntityConfiguration
{
    public class EmailConfirmationEntityConfiguration: BaseEntityConfiguration<EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
        {
            base.Configure(builder);

            builder.ToTable("emailconfirmation", SozlukAppContext.DEFAULT_SCHEME);

        }
    }
}
