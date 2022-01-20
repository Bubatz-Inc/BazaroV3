﻿using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Entries
{
    public static class UpdateEntry
    {
        public class Command
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public int? StartItemId { get; set; }

            public int OldFolderId { get; set; }
            public int NewFolderId { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            data.Title = request.Title;
            data.Description = request.Description;
            data.StartItemId = request.StartItemId;
            data.Updated = DateTime.Now;

            if (request.OldFolderId != request.NewFolderId)
            {
                var reference = await context.Set<FolderEntryReference>().FirstOrDefaultAsync(x => x.EntryId == request.Id || x.FolderId == request.OldFolderId);

                reference.FolderId = request.NewFolderId;
            }

            await context.SaveChangesAsync();
        }
    }
}