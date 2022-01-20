using Bazaro.Web.Services.Commands.Entries;
using Bazaro.Web.Services.Queries.Entries;
using Bazaro.Web.Services.ViewModels;

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

        public Task Insert(InsertEntry.Command command) => InsertEntry.Handle(_context, command);

        public Task Update(UpdateEntry.Command command) => UpdateEntry.Handle(_context, command);

        public Task Delete(DeleteEntry.Command command) => DeleteEntry.Handel(_context, command);

    }
}
