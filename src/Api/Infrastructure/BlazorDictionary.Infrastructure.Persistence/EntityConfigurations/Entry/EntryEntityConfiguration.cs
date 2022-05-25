using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.Entry>
    {

        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
        {
            //BaseEntityConfiguration daki Id ve CreatedDate ler burada ayarlaniyor
            base.Configure(builder);

            builder.ToTable("entry", BlazorDictionaryContext.DEFAULT_SCHEMA);

            //Bir kullanicinin birden fazla entry si olabilir.
            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.Entries)
                .HasForeignKey(i => i.CreatedById);
        }

    }
}
