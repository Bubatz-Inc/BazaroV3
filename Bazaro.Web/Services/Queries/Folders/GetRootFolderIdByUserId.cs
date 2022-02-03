using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetRootFolderIdByUserId
    {
        public class Query
        {
            public string UserId { get; set; }
        }

        /// <summary>
        /// Returns RootFolderId by UserId
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns>RootFolderId as int</returns>
        public static Task<int> Handle(BazaroContext context, Query request)
        {
            return context.Set<UserFolderReference>()
                .Where(x => x.UserId == request.UserId && x.Folder.PreviousFolder == null)
                .Select(x => x.FolderId)
                .FirstOrDefaultAsync();
        }
    }
}
