using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetByIdProject
{
    public class GetByIdProjectHandler : IRequestHandler<GetByIdProjectQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;
        public GetByIdProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetDetailsById(request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Este projeto nao existe");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }
    }
}
