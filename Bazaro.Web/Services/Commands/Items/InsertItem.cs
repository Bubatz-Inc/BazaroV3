﻿using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Items
{
    public static class InsertItem
    {
        public class Command
        {
            public int ContentTypeId { get; set; }
            public byte[] Content { get; set; }

            public int EntryId { get; set; }
            public int? PreviousItemId { get; set; }
        }

        public static async Task Handler(BazaroContext context, Command request)
        {
            var newItem = new Item
            {
                ContentTypeId = request.ContentTypeId,
                Content = request.Content,
                Created = DateTime.Now,
            };

            context.Add(newItem);

            if (!request.PreviousItemId.HasValue)
            {
                var entry = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.EntryId);

                if (entry == null)
                    return;

                newItem.NextItemId = entry.StartItemId;

                entry.StartItemId = newItem.Id;
            }
            else
            {
                var previousItem = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.PreviousItemId);

                if (previousItem == null)
                    return;

                previousItem.NextItemId = newItem.Id;
            }

            await context.SaveChangesAsync();
        }
    }
}
