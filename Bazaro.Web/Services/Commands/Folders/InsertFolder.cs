using Bazaro.Web.Models;
using Bazaro.Web.Models.References;

namespace Bazaro.Web.Services.Commands.Folders
{
    public static class InsertFolder
    {
        public class Command
        {
            public string UserId { get; set; }

            public string Title { get; set; }
            public string Description { get; set; }

            public int PreviousFolder { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = new Folder()
            {
                Title = request.Title,
                Description = request.Description,
                Created = DateTime.Now,
            };

            if(context.Set<UserFolderReference>().Any(x => x.FolderId == request.PreviousFolder && x.UserId == request.UserId))
            {
                data.PreviousFolderId = request.PreviousFolder;
            }

            context.Add(data);

            context.Add(new UserFolderReference
            {
                FolderId = data.Id,
                UserId = request.UserId
            });

            await context.SaveChangesAsync();
        }
    }
}
