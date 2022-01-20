using Bazaro.Web.Models;
using Bazaro.Web.Models.References;

namespace Bazaro.Web.Services.Commands.Entries
{
    public static class InsertEntry
    {
        public class Command
        {
            public string Title { get; set; }
            public string Description { get; set; }

            public int FolderId { get; set; }
        }

        public static Task Handle(BazaroContext context, Command request)
        {
            var entry = new Entry
            {
                Title = request.Title,
                Description = request.Description,
                Created = DateTime.Now,
            };

            context.Add(entry);

            context.Add(new FolderEntryReference
            {
                EntryId = entry.Id,
                FolderId = request.FolderId
            });

            return context.SaveChangesAsync();
        }
    }
}
