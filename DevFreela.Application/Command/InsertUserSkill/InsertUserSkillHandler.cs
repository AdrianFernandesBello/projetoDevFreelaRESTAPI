using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertUserSkill
{
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public InsertUserSkillHandler (IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {

            var userSkills = _repository.GetById(request.Id);

            //await _repository.Add(userSkills);

            return ResultViewModel.Sucess();
        }
    }
}
