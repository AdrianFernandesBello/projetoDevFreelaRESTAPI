using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly IProjectRepository _repository;
        private readonly IMediator _mediator;
        private IProjectRepository repository;

        public InsertProjectHandler(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public InsertProjectHandler(IProjectRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            var id = await _repository.Add(project);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated);

            return ResultViewModel<int>.Sucess(id);
        }
    }
}

