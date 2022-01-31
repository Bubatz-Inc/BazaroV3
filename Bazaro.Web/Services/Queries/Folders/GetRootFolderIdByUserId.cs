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

        public static Task<int> Handle(BazaroContext context, Query request)
        {
            return context.Set<UserFolderReference>()
                .Where(x => x.UserId == request.UserId && x.Folder.PreviousFolder == null)
                .Select(x => x.FolderId)
                .FirstOrDefaultAsync();
        }
    }
}
