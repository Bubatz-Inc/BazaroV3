using Bazaro.Core.Services.Models;
using Bazaro.Data;

namespace Bazaro.Core.Services
{
    public class FolderService
    {
        private readonly BazaroContext _context;

        public FolderService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<FolderModel>> GetViewFolderStructureByuserId(int userId) => Queries.Folders.GetViewFolderStructureByUserId.Handle(_context, new Queries.Folders.GetViewFolderStructureByUserId.Query
        {
            UserId = userId
        });
    }
}
