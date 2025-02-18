using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public InsertSkillHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            //await _repository.Add(skill);

            return ResultViewModel<int>.Sucess(skill.Id);
        }
    }
}
