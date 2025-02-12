using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllUser
{
    public class GetAllSkillQuery : IRequest<ResultViewModel>
    {
        public GetAllSkillQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class GetAllUserHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
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
