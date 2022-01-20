using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Folders
{
    public static class UpdateFolder
    {
        public class Command
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int? PreviousFolderId { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<Folder>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            data.Title = request.Title;
            data.Description = request.Description;
            data.PreviousFolderId = request.PreviousFolderId;
            data.Updated = DateTime.Now;

            await context.SaveChangesAsync();
        }
    }
}
