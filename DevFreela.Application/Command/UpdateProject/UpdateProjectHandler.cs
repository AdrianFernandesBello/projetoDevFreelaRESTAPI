﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Command.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public UpdateProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto nao existe");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            await _repository.Update(project);

            return ResultViewModel.Sucess();
        }
    }
}
