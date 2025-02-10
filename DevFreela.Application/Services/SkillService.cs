using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;


namespace DevFreela.Application.Services
{
    public class SkillService : ISkillsService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<int> Post(CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Sucess(skill.Id);
        }
    }
}
