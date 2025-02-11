using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertUserSkill
{
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertUserSkillHandler(DevFreelaDbContext context)
        {

            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {

            var userSkills =  request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
