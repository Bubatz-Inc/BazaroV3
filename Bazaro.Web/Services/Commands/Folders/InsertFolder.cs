using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Folders
{
    public static class InsertFolder
    {
        public class Command
        {
            public string UserId { get; set; }

            public string Title { get; set; }
            public string Description { get; set; }

            public int? PreviousFolder { get; set; }
        }

        /// <summary>
        /// Inserts Folder
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = new Folder()
            {
                Title = request.Title,
                Description = request.Description,
                Created = DateTime.Now,
                SubFolder = new List<Folder>()
            };

            data.PreviousFolderId = request.PreviousFolder;

            context.Add(data);

            await context.SaveChangesAsync();

            context.Add(new UserFolderReference
            {
                FolderId = data.Id,
                UserId = request.UserId
            });

            await context.SaveChangesAsync();
        }
    }
}
