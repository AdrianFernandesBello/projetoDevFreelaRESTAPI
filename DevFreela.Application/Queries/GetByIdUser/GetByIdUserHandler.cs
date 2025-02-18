using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public GetByIdUserHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Error("Usuario inexistente");
            }

            return (ResultViewModel<UserViewModel>)ResultViewModel<UserViewModel>.Sucess();
        }
    }
}
