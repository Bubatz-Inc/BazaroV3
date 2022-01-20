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

            return CreateItemModel(data);
        }

        private static ItemModel CreateItemModel(Item item)
        {
            if (item == null)
                return null;

            return new ItemModel
            {
                Id = item.Id,
                ContentType = new ContentTypeModel
                {
                    Id = item.ContentType.Id,
                    Title = item.ContentType.Title
                },
                Content = item.Content,
                NextItem = CreateItemModel(item.NextItem)
            };
        }
    }
}
