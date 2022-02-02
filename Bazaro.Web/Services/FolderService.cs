using Bazaro.Web.Services.Commands.Folders;
using Bazaro.Web.Services.Queries.Folders;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class FolderService
    {
        private readonly BazaroContext _context;

        public FolderService(BazaroContext context)
        {
            _context = context;
        }

        public Task<FolderModel> GetViewFolderStructureByUserId(GetViewSubfoldersStructureByUserId.Query request) => GetViewSubfoldersStructureByUserId.Handle(_context, request);

        public Task<FolderModel> GetViewFolderById(GetFolderById.Query request) => GetFolderById.Handle(_context, request);

        public Task<FolderModel> GetViewFolderByEntryId(GetFolderByEntryId.Query request) => GetFolderByEntryId.Handle(_context, request);

        public Task Insert(InsertFolder.Command command) => InsertFolder.Handle(_context, command);
        public Task Update(UpdateFolder.Command command) => UpdateFolder.Handle(_context, command);
        public Task Delete(DeleteFolder.Command command) => DeleteFolder.Handle(_context, command);

    }
}
