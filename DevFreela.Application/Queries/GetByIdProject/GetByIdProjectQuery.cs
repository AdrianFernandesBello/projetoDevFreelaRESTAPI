﻿using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetByIdProject
{
    public class GetByIdProjectQuery : IRequest<ResultViewModel<ProjectViewModel>>
    {
        public GetByIdProjectQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
