using Bazaro.Core.Services.Models;
using Bazaro.Data;

namespace Bazaro.Core.Services
{
    public class EntryService
    {
        public readonly BazaroContext _context;

        public EntryService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<EntryModel>> GetViewEntriesByFolderId(int folderId) => Queries.Entries.GetViewEntriesByFolderId.Handle(_context, new Queries.Entries.GetViewEntriesByFolderId.Query
        {
            FolderId = folderId
        });

    }
}
