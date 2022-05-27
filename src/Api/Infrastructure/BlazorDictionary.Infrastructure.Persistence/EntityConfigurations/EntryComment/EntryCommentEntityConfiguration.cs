using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.EntryComment>
    {

        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycomment", BlazorDictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.EntryComments)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Entry)
                .WithMany(i => i.EntryComments)
                .HasForeignKey(i => i.EntryId);
        }


    }
}
