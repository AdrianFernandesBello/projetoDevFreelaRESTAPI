using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Command.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public CompleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto nao existe");
            }

            project.Complete();
            await _repository.Update(project);

            return ResultViewModel.Sucess();
        }
    }
}
