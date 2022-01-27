using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetViewFolderStructureByUserId
    {
        public class Query
        {
            public string UserId { get; set; }
        }

        public static async Task<FolderModel> Handle(BazaroContext context, Query request)
        {
            var rootFolder = await context.Set<UserFolderReference>()
                .Include(x => x.Folder)
                .ThenInclude(x => x.SubFolder)
                .Where(x => x.UserId == request.UserId && (x.Folder.PreviousFolder == null || x.IsShared))
                .Select(x => x.Folder)
                .FirstOrDefaultAsync();

            return CreateFolderStructure(rootFolder);
        }

        private static FolderModel CreateFolderStructure(Folder folder)
        {
            if(folder == null)
            {
                return null;
            }
            else if (folder.SubFolder == null || folder.SubFolder.Count() == 0)
            {
                return new FolderModel
                {
                    Id = folder.Id,
                    Title = folder.Title,
                    Description = folder.Description,
                    SubFolders = null
                };
            }
                
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
