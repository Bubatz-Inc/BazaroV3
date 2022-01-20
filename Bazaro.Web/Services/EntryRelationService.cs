using Bazaro.Web.Services.Commands.EntryReferences;
using Bazaro.Web.Services.Queries.EntryReferences;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class EntryRelationService
    {
        private readonly BazaroContext _context;

        public EntryRelationService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<EntryReferenceViewModel>> GetReferencesByEntryId(int entryId) => GetEntryReferencesByEntryId.Handle(_context, new GetEntryReferencesByEntryId.Query
        {
            EntryId = entryId
        });

        public Task Insert(InsertEntryReference.Command command) => InsertEntryReference.Handle(_context, command);
        public Task Delete(DeleteEntryRefernce.Command command) => DeleteEntryRefernce.Handle(_context, command);
    }
}
