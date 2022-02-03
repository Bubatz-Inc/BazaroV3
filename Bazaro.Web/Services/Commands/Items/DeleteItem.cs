using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Items
{
    public static class DeleteItem
    {
        public class Command
        {
            public int Id { get; set; }
            public int Entry { get; set; }
            public int? PreviousItem { get; set; }
        }

        /// <summary>
        /// Deletes Item
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            if(data.NextItemId.HasValue && request.PreviousItem.HasValue)
            {
                // Set last item

                var previousItem = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (previousItem == null)
                    return;

                previousItem.NextItemId = data.NextItemId.Value;
            }
            else if(!data.NextItemId.HasValue && request.PreviousItem.HasValue)
            {
                // Set first item

                var entry = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entry == null)
                    return;

                entry.StartItemId = data.Id;
            }

            context.Remove(data);

            await context.SaveChangesAsync();
        }
    }
}
