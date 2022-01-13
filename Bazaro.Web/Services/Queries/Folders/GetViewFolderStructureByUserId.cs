using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetViewFolderStructureByUserId
    {
        public class Query
        {
            public int UserId { get; set; }
        }

        public static async Task<List<FolderModel>> Handle(BazaroContext context, Query request)
        {
            var topFolders = await context.Set<UserFolderReference>()
                .Where(x => x.UserId == request.UserId && x.Folder.PreviousFolder == null)
                .Select(x => x.Folder)
                .ToListAsync();

            var result = new List<FolderModel>();
            foreach (var folder in topFolders)
            {
                result.Add(CreateFolderStructure(folder));
            }

            return result;
        }

        private static FolderModel CreateFolderStructure(Folder folder)
        {
            if (folder == null || folder.SubFolder == null || folder.SubFolder.Count() == 0)
                return null;

            var list = new List<FolderModel>();
            foreach (var subFolder in folder.SubFolder)
            {
                var tempFolder = CreateFolderStructure(subFolder);

                if (tempFolder != null)
                    list.Add(tempFolder);
            }

            return new FolderModel
            {
                Id = folder.Id,
                Title = folder.Title,
                Description = folder.Description,
                SubFolders = list,
            };
        }

    }
}
