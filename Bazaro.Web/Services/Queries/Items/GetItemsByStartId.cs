using Bazaro.Web.Models;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Items
{
    public static class GetItemsByStartId
    {
        public class Query
        {
            public int StartId { get; set; }
        }

        public static async Task<ItemModel> Handle(BazaroContext context, Query request)
        {
            var data = await context.Set<Item>()
                .Include(x => x.ContentType)
                .Include(x => x.NextItem)
                .FirstOrDefaultAsync(x => x.Id == request.StartId);

            if (data == null)
                return null;

            return await CreateItemModel(context, data);
        }

        private static async Task<ItemModel> CreateItemModel(BazaroContext context, Item item)
        {
            if (item == null)
                return null;

            var nextItem = await context.Set<Item>()
                .Include(x => x.ContentType)
                .Include(x => x.NextItem)
                .FirstOrDefaultAsync(x => x.Id == item.NextItemId);

            return new ItemModel
            {
                Id = item.Id,
                ContentType = new ContentTypeModel
                {
                    Id = item.ContentType.Id,
                    Title = item.ContentType.Title
                },
                Content = item.Content,
                NextItem = (nextItem == null ? null : await CreateItemModel(context, item.NextItem)) 
            };
        }
    }
}
