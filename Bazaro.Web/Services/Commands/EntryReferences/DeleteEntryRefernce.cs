using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.EntryReferences
{
    public static class DeleteEntryRefernce
    {
        public class Command
        {
            public int EntryId { get; set; }
            public int RefernceEntryId { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<EntryReference>().FirstOrDefaultAsync(x => x.EntryId == request.EntryId && x.ReferenceEntryId == request.RefernceEntryId);

            if (data == null)
                return;

            context.Remove(data);

            await context.SaveChangesAsync();
        }
    }
}
