using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Sucess(skill.Id);
        }
    }
}
