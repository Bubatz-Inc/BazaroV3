using Bazaro.Web;
using Bazaro.Web.Services.Models;
using Bazaro.Web.Services.Queries.Folders;

namespace Bazaro.Web.Services
{
    public class FolderService
    {
        private readonly BazaroContext _context;

        public FolderService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<FolderModel>> GetViewFolderStructureByuserId(string userId) => GetViewFolderStructureByUserId.Handle(_context, new GetViewFolderStructureByUserId.Query
        {
            UserId = userId
        });
    }
}
