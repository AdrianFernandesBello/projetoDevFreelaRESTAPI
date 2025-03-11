using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Command.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        private readonly IAuthService _authService;
        public InsertUserHandler(IProjectRepository repository, IAuthService authService)
        {
            _authService = authService;
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var passawordHas = _authService.ComputeSha256Has(request.Password);

            var user = new User(request.FullName, request.Email, request.BirthDate, request.Password, request.Role);

            return ResultViewModel.Sucess();
        }
    }
}
