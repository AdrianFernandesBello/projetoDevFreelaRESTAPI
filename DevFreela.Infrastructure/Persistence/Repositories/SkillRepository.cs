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
    public class SkillRepository : ISkilllRepository
    {
        private readonly DevFreelaDbContext _context;
        public SkillRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }

        public async Task<List<Skill>> GetAllSkill()
        {
            var users = await _context.Skills
                .Include(u => u.Description)
                .ToListAsync();

            return users;
        }
    }
}
