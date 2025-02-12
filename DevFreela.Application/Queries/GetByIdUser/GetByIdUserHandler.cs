using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public GetByIdUserHandler(DevFreelaDbContext context)
        {

            _context = context;
        }

        public async Task<ResultViewModel> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                    .ThenInclude(u => u.Skill)
                .SingleOrDefaultAsync(u => u.Id == request.Id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Error("Usuario inexistente");
            }

            UserViewModel.FromEntity(user);

            return (ResultViewModel<UserViewModel>)ResultViewModel<UserViewModel>.Sucess();
        }
    }
}
