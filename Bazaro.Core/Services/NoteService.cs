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

        public Task Add(NoteModel nm)
        {
            _context.Add(new Entry
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Title = nm.Title,
            });
            return _context.SaveChangesAsync();
        }

        public async Task Update(NoteModel nm)
        {
            var found = await _context.Set<Entry>().FirstOrDefaultAsync(x => x.Id == nm.Id);

            if (found is null)
                return;

            found.Title = nm.Title;
            found.Updated = DateTime.Now;
            found.ItemId = nm.StartItemId;

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
