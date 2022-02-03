using Bazaro.Web.Models;
using Bazaro.Web.Models.Base;
using Bazaro.Web.Models.References;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web
{
    public class BazaroContext : IdentityDbContext<User>
    {
        public BazaroContext(DbContextOptions options) : base(options)
        {

        }

        public bool IsSqlConnection => Database.IsNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var type in typeof(IEntity).Assembly.GetExportedTypes()
                                        .Where(p => typeof(IEntity).IsAssignableFrom(p)))
                if (!type.IsAbstract && !type.IsInterface && type.IsClass)
                    modelBuilder.Entity(type);

            // Dual Keys
            modelBuilder.Entity<UserFolderReference>().HasKey(c => new { c.UserId, c.FolderId });
            modelBuilder.Entity<EntryReference>().HasKey(c => new { c.EntryId, c.ReferenceEntryId });

            var relations = modelBuilder.Model.GetEntityTypes().SelectMany(c => c.GetForeignKeys());
            foreach (var item in relations)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder
                .Entity<Entry>()
                .HasOne(e => e.StartItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Item>()
                .HasOne(e => e.NextItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateTime>()
                .HaveColumnType("timestamp without time zone");
        }
    }
}
