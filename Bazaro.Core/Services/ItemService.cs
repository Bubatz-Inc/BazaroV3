using Bazaro.Core.Models;
using Bazaro.Data;
using Bazaro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Core.Services
{
    public class ItemService
    {
        private readonly BazaroContext _context;

        public ItemService(BazaroContext context)
        {
            _context = context;
        }

        public Task<Item?> GetById(int id)
        {
            return _context.Set<Item>()
                .Select(x => new Item
                {
                    Id = x.Id,
                    NextItem = x.NextItem,
                    ContentType = x.ContentType,
                    Content = x.Content,
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Item>> GetAll()
        {
            return _context.Set<Item>()
                .Select(x => new Item
                {
                    Id = x.Id,
                    NextItem = x.NextItem,
                    ContentType = x.ContentType,
                    Content = x.Content,
                }).ToListAsync();
        }

        public Task Add(ItemModel model)
        {
            _context.Add(new Item
            {
                Created = DateTime.Now,
                NextItemId = model.NextItemId,
                ContentType = model.ContentType,
                Content = model.Content,
            });

            return _context.SaveChangesAsync();
        }

        public async Task Update(ItemModel model)
        {
            var data = await _context.Set<Item>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (data == null)
                return;

            data.Updated = DateTime.Now;
            data.NextItemId = model.NextItemId;
            data.ContentType = model.ContentType;
            data.Content = model.Content;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var data = await _context.Set<Item>().FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return;

            _context.Remove(data);

            await _context.SaveChangesAsync();
        }
    }
}
