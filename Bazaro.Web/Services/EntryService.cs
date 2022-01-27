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

        public Task<List<EntryModel>> GetViewEntriesByFolderId(GetViewEntriesByFolderId.Query request) => Queries.Entries.GetViewEntriesByFolderId.Handle(_context, request);

        public Task<List<EntryModel>> GetViewEntriesByTitle(GetEntriesByTitle.Query request) => GetEntriesByTitle.Handler(_context, request);

        public Task Insert(InsertEntry.Command command) => InsertEntry.Handle(_context, command);

        public Task Update(UpdateEntry.Command command) => UpdateEntry.Handle(_context, command);

        public Task Delete(DeleteEntry.Command command) => DeleteEntry.Handel(_context, command);

    }
}
