using DevFreela.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.InsertUserSkill
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel>
    {
        public InsertUserSkillCommand(int id)
        {
            Id = id;
        }

        public int[] SkillIds { get; set; }
        public int Id { get; set; }
    }
}
