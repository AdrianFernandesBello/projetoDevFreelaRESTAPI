using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Command.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public UpdateProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
            {
                return ResultViewModel<UpdateProjectInputModel>.Error("Projeto nao existe");
            }

            project.SetAsDeleted();
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
