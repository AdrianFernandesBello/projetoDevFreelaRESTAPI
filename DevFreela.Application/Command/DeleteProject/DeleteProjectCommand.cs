using DevFreela.Application.Command.CompleteProject;
using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.DeleteProject
{
    public class DeleteProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
    }
}
