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

        public Task<FolderModel> GetViewFolderStructureByuserId(string userId) => GetViewFolderStructureByUserId.Handle(_context, new GetViewFolderStructureByUserId.Query
        {
            UserId = userId
        });

        public Task Insert(InsertFolder.Command command) => InsertFolder.Handle(_context, command);

        public Task Update(UpdateFolder.Command command) => UpdateFolder.Handle(_context, command);

    }
}
