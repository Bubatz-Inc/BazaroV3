using Bazaro.Web;
using Bazaro.Web.Services.Models;
using Bazaro.Web.Services.Queries.Entries;

namespace Bazaro.Web.Services
{
    public class EntryService
    {
        public readonly BazaroContext _context;

        public EntryService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<EntryModel>> GetViewEntriesByFolderId(int folderId) => Queries.Entries.GetViewEntriesByFolderId.Handle(_context, new GetViewEntriesByFolderId.Query
        {
            FolderId = folderId
        });

    }
}
