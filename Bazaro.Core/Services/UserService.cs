using Bazaro.Core.Models;
using Bazaro.Data;
using Bazaro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Core.Services
{
    public class UserService
    {
        private readonly BazaroContext _context;

        public UserService(BazaroContext context)
        {
            _context = context;
        }

        public Task<UserModel?> GetById(string id)
        {
            return _context.Set<User>()
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    AvatarUrl = x.AvatarUrl
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<UserModel>> GetAll()
        {
            return _context.Set<User>()
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    AvatarUrl = x.AvatarUrl
                }).ToListAsync();
        }

        public Task Add(UserModel model)
        {
            _context.Add(new User
            {
                UserName = model.UserName,
                PasswordHash = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            });
            return _context.SaveChangesAsync();
        }

        public async Task Update(UserModel model)
        {
            var data = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (data == null)
                return;

            data.UserName = model.UserName;
            data.PasswordHash = model.Password;
            data.Email = model.Email;
            data.PhoneNumber = model.PhoneNumber;
            data.AvatarUrl = model.AvatarUrl;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var data = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return;

            _context.Remove(data);

            await _context.SaveChangesAsync();
        }



    }
}
