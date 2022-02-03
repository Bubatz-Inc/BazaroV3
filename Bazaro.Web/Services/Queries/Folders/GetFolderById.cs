using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetFolderById
    {
        public class Query
        {
            public int Id { get; set; }
        }

        /// <summary>
        /// Returns Folder with coresponding id
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns>FolderModel</returns>
        public static Task<FolderModel> Handle(BazaroContext context, Query request)
        {
            return context.Set<Folder>()
                .Join(context.Set<UserFolderReference>(),
                    f => f.Id,
                    ufr => ufr.FolderId,
                    (f, ufr) => new {f, ufr})
                .Select(x => new FolderModel
                {
                    Id = x.f.Id,
                    Title = x.f.Title,
                    Description = x.f.Description,
                    IsDeleted = x.ufr.IsDeleted,
                    IsShared = x.ufr.IsShared
                }).FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
