using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Items
{
    public static class UpdateItem
    {
        public class Command
        {
            public int Id { get; set; }

            public int ContentTypeId { get; set; }
            public byte[] Content { get; set; }

            public int EntryId { get; set; }
            public int? OldPreviousItemId { get; set; }
            public int? NewPreviousItemId { get; set; }
        }

        /// <summary>
        /// Updates Items (can change itempos)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task Handler(BazaroContext context, Command request)
        {
            var data = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            data.ContentTypeId = request.ContentTypeId;
            data.Content = request.Content;

            if(request.NewPreviousItemId != request.OldPreviousItemId)
            {
                var oldPreviousItem = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.OldPreviousItemId);
                var newPreviousItem = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.NewPreviousItemId);

                if (request.OldPreviousItemId == null)
                {
                    var entry = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.EntryId);

                    if (entry == null || newPreviousItem == null)
                        return;

                    entry.StartItem = data.NextItem;
                    data.NextItemId = newPreviousItem.NextItemId;
                    newPreviousItem.NextItemId = data.Id;
                }
                else if(request.NewPreviousItemId == null)
                {
                    var entry = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.EntryId);

                    if (entry == null)
                        return;

                    data.NextItemId = entry.StartItemId;
                    entry.StartItemId = data.Id;
                    oldPreviousItem.NextItemId = null;
                }
                else
                {
                    if (oldPreviousItem == null || newPreviousItem == null)
                        return;

                    oldPreviousItem.NextItemId = data.NextItemId;
                    data.NextItemId = newPreviousItem.NextItemId;
                    newPreviousItem.NextItemId = data.Id;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
