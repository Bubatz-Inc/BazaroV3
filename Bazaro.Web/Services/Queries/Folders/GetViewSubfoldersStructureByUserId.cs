using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Folders
{
    public static class GetViewSubfoldersStructureByUserId
    {
        public class Query
        {
            public int? FolderId { get; set; }
            public string UserId { get; set; }
        }

        public static async Task<FolderModel> Handle(BazaroContext context, Query request)
        {
            var rootFolder = await context.Set<UserFolderReference>()
                .Include(x => x.Folder)
                .ThenInclude(x => x.SubFolder)
                .Where(x => x.UserId == request.UserId 
                    && (request.FolderId == null 
                        ? x.Folder.PreviousFolder == null
                        : x.FolderId == request.FolderId))
                .FirstOrDefaultAsync();

            if (rootFolder == null)
                return null;

            var dataList = new List<FolderModel>();
            foreach (var item in rootFolder.Folder.SubFolder)
            {
                var reference = await context.Set<UserFolderReference>()
                    .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.FolderId == item.Id);

                if(reference == null)
                    continue;

                dataList.Add(new FolderModel
                {
                    Id = item.Id,
                    Description = item.Description,
                    Title = item.Title,
                    IsDeleted = reference.IsDeleted,
                    IsShared = reference.IsShared,
                });
            }

            return new FolderModel
            {
                Id = rootFolder.Folder.Id,
                Description = rootFolder.Folder.Description,
                SubFolders = dataList,
                Title = rootFolder.Folder.Title,
                IsDeleted = rootFolder.IsDeleted,
                IsShared = rootFolder.IsShared,
            };
        }

    }
}
