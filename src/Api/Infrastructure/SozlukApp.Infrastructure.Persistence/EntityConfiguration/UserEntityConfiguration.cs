using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;

namespace SozlukApp.Infrastructure.Persistence.EntityConfiguration
{
    public class UserEntityConfiguration: BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("user", SozlukAppContext.DEFAULT_SCHEME);

        }
    }
}
