using Bazaro.Core.Models;
using Bazaro.Data;
using Bazaro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Core.Services
{
    public class NoteSerice
    {
        private readonly BazaroContext _context;

        public NoteSerice(BazaroContext context)
        {
            _context = context;
        }

        public Task<NoteModel?> GetById(int id)
        {
            return _context.Set<Entry>()
                .Select(x => new NoteModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartItemId = x.ItemId
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<NoteModel>> GetAll()
        {
            return _context.Set<Entry>()
                .Select(x => new NoteModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartItemId = x.ItemId
                }).ToListAsync();
        }

        public Task Add(NoteModel model)
        {
            _context.Add(new Entry
            {
                Created = DateTime.Now,
                Title = model.Title,
                ItemId = model.StartItemId
            });
            return _context.SaveChangesAsync();
        }

        public async Task Update(NoteModel model)
        {
            var found = await _context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (found is null)
                return;

            found.Updated = DateTime.Now;
            found.Title = model.Title;
            found.ItemId = model.StartItemId;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var found = await _context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == id);

            if (found is null)
                return;

            _context.Remove(found);
            await _context.SaveChangesAsync();
        }
    }
}
