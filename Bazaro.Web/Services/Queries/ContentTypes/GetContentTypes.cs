using Bazaro.Web.Models;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.ContentTypes
{
    public static class GetContentTypes
    {
        public class Query
        {
        }

        public static Task<List<ContentTypeModel>> Handle(BazaroContext context, Query request)
        {
            return context.Set<ContentType>()
                .Select(x => new ContentTypeModel
                {
                    Id = x.Id,
                    Title = x.Title,
                }).ToListAsync();
        }
    }
}
