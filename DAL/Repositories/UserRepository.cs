using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _db;

        public UserRepository(BlogContext db)
        {
            _db = db;
        }

        public async Task Create(User item)
        {
            await _db.Users.AddAsync(item);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Users.Remove(await _db.Users.FirstAsync(c => c.Id == id));

            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _db.Users.Include(c => c.Articles).ThenInclude(c => c.Tags)
                .FirstOrDefaultAsync(c => c.Id == id);

            return user;
        }

        public async Task<ICollection<User>> GetAll()
        {
            var users = await _db.Users.Include(c => c.Articles).ThenInclude(c => c.Comments).ToListAsync();

            return users.Count == 0 ? null : users;
        }

        public async Task Update(User item)
        {
            _db.Users.Remove(await _db.Users.FirstAsync(a => a.Id == item.Id));

            await _db.Users.AddAsync(item);

            await _db.SaveChangesAsync();
        }
    }
}