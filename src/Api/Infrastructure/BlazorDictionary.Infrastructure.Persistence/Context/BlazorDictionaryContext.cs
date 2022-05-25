using System.Reflection;
using BlazorDictionary.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Context
{
    public class BlazorDictionaryContext : DbContext
    {
        public const string? DEFAULT_SCHEMA = "dbo";

        public BlazorDictionaryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryVote> EntryVotes { get; set; }

        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryComment> EntryComments { get; set; }

        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }

        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Select(i => (BaseEntity)i.Entity);

            PrepareAddedEntities(addedEntities);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedDate == DateTime.MinValue)
                    entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
