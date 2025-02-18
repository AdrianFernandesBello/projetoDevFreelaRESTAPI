using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProject
{

    public class GetAllProjectHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly IProjectRepository _repository;
        public GetAllProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }
    }
}
