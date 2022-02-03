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

        /// <summary>
        /// Returns all ContentTypes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request"></param>
        /// <returns>List of ContentTypeModel</returns>
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
