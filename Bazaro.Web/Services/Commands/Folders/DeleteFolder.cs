using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Folders
{
    public static class DeleteFolder
    {
        public class Command
        {
            public int Id { get; set; }
            public string UserId { get; set; }
        }

        /// <summary>
        /// Deletes Folder
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<UserFolderReference>().FirstOrDefaultAsync(x => x.UserId == request.UserId && x.FolderId == request.Id);

            if (data == null)
                return;

            data.IsDeleted = true;

            await context.SaveChangesAsync();
        }
    }
}
