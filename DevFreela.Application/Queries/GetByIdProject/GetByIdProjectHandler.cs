using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetByIdProject
{
    public class GetByIdProjectHandler : IRequestHandler<GetByIdProjectQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;
        public GetByIdProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Include(p => p.Comments)
               .SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Este projeto nao existe");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }
    }
}
