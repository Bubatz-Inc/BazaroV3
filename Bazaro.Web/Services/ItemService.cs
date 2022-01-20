using Bazaro.Web.Services.Commands.Items;
using Bazaro.Web.Services.Queries.Items;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class ItemService
    {
        private readonly BazaroContext _context;

        public ItemService(BazaroContext context)
        {
            _context = context;
        }

        public Task<ItemModel> GetViewItemByStartId(int startId) => GetItemsByStartId.Handle(_context, new GetItemsByStartId.Query { StartId = startId });

        public Task Insert(InsertItem.Command command) => InsertItem.Handler(_context, command);

        public Task Update(UpdateItem.Command command) => UpdateItem.Handler(_context, command);

        public Task Delete(DeleteItem.Command command) => DeleteItem.Handle(_context, command);
    }
}
