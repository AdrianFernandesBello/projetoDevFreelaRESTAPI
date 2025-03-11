using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;
    
namespace DevFreela.Application.Queries.GetAllUser
{
    public class GetAllSkillHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public GetAllSkillHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _repository.GetAll();

            return (ResultViewModel<UserViewModel>)ResultViewModel<UserViewModel>.Sucess();
        }
    }
}
