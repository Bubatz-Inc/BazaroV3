﻿using Bazaro.Data.Models;
using Bazaro.Data.Models.Base;
using Bazaro.Data.Models.References;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Data
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
            modelBuilder.Entity<FolderEntryReference>().HasKey(c => new { c.EntryId, c.FolderId });
            modelBuilder.Entity<UserFolderReference>().HasKey(c => new { c.UserId, c.FolderId });

            var relations = modelBuilder.Model.GetEntityTypes().SelectMany(c => c.GetForeignKeys());
            foreach (var item in relations)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
