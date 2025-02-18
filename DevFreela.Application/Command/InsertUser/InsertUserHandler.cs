using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public InsertUserHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.FullName, request.Email, request.BirthDate);

           // await _repository.Add(user);

            return ResultViewModel.Sucess();
        }
    }
}
