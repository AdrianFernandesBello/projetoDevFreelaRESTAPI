using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel>
    {
        public InsertSkillCommand(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
