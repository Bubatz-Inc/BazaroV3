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

        /// <summary>
        /// Deletes Entry
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static async Task Handel(BazaroContext context, Command request)
        {
            var reference = await context.Set<FolderEntryReference>().FirstOrDefaultAsync(x => x.EntryId == request.Id && x.FolderId == request.FolderId);

            if (reference != null)
                context.Remove(reference);

            var data = await context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            context.Remove(data);

            await context.SaveChangesAsync();
        }
    }
}
