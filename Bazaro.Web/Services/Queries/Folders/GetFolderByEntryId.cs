using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetFolderByEntryId
    {
        public class Query 
        {
            public string UserId { get; set; }
            public int EntryId { get; set; }
        }

        /// <summary>
        /// Returns Folder by EntryId
        /// </summary>
        /// <param name="context">Database-Cotnext</param>
        /// <param name="request">Request-Data</param>
        /// <returns>List of FolderModel</returns>
        public static Task<FolderModel> Handle(BazaroContext context, Query request)
        {
            return context.Set<FolderEntryReference>()
                .Join(context.Set<UserFolderReference>(),
                    fer => fer.FolderId,
                    ufr => ufr.FolderId,
                    (fer, ufr) => new { fer, ufr })
                .Where(x => x.ufr.UserId == request.UserId
                    && x.fer.EntryId == request.EntryId)
                .Select(x => new FolderModel
                {
                    Id = x.ufr.Folder.Id,
                    Description = x.ufr.Folder.Description,
                    Title = x.ufr.Folder.Title,
                    IsDeleted = x.ufr.IsDeleted,
                    IsShared = x.ufr.IsShared,
                }).FirstOrDefaultAsync();
        }
    }
}
