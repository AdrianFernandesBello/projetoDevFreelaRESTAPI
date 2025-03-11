
using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;
        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return
                await _context.Skills.AnyAsync(skill => skill.Id == id);
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                    .ThenInclude(u => u.Skill)
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetUserByEmailAndPassworHashAsync(string email, string passwordHash)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email && x.Password == passwordHash);
        }

        public Task PostSkill(User user)
        {
            throw new NotImplementedException();
        }
    }
}
