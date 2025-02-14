using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.InsertProject
{
    public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context)
        {

            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExist = _context.Users.Any(x => x.Id == request.IdClient); //verifica se o cliente existe
            var FreeLancerExist = _context.Users.Any(x => x.Id == request.IdFreelancer);

            if (!clientExist || !FreeLancerExist)
            {
                return (ResultViewModel<int>)ResultViewModel.Error("Cliente ou Freelancer Invalidos");
            }

            return await next();
        }
    }
}
