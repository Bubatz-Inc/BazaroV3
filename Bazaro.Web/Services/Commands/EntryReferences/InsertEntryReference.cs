using Bazaro.Web.Models.References;

namespace Bazaro.Web.Services.Commands.EntryReferences
{
    public static class InsertEntryReference
    {
        public class Command
        {
            public int EntryId { get; set; }
            public int RefernceEntryId { get; set; }
        }

        public static Task Handle(BazaroContext context, Command request)
        {
            context.Add(new EntryReference
            {
                EntryId = request.EntryId,
                ReferenceEntryId = request.RefernceEntryId
            });

            return context.SaveChangesAsync();
        }
    }
}
