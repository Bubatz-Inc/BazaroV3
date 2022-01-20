using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Entries
{
    public static class DeleteEntry
    {
        public class Command
        {
            public int Id { get; set; }
            public int FolderId { get; set; }
        }

        public static async Task Handel(BazaroContext context, Command request)
        {
            var data = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            context.Remove(data);

            var reference = await context.Set<FolderEntryReference>().FirstOrDefaultAsync(x => x.EntryId == request.Id && x.FolderId == request.FolderId);

            if (reference != null)
                context.Remove(reference);

            await context.SaveChangesAsync();

        }
    }
}
