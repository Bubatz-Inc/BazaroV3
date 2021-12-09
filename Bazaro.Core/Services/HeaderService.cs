using Bazaro.Core.Models;
using Bazaro.Data;
using Bazaro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Core.Services
{
    public class HeaderService
    {
        private readonly BazaroContext _context;

        public HeaderService(BazaroContext context)
        {
            _context = context;
        }

        public Task<HeaderModel?> GetById(int id)
        {
            return _context.Set<Header>()
                .Select(x => new HeaderModel
                {
                    Id = x.Id,
                    Type = x.Type,
                    Content = x.Content,
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<HeaderModel>> GetAll()
        {
            return _context.Set<Header>()
                .Select(x => new HeaderModel
                {
                    Id = x.Id,
                    Type = x.Type,
                    Content = x.Content,
                }).ToListAsync();
        }

        public Task Add(HeaderModel model)
        {
            _context.Add(new Header
            {
                Created = DateTime.Now,
                Type = model.Type,
                Content = model.Content,
            });

            return _context.SaveChangesAsync();
        }

        public async Task Update(HeaderModel model)
        {
            var data = await _context.Set<Header>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (data == null)
                return;

            data.Updated = DateTime.Now;
            data.Type = model.Type;
            data.Content = model.Content;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var data = await _context.Set<Header>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (data == null)
                return;

            _context.Remove(data);

            await _context.SaveChangesAsync();
        }
    }
}
