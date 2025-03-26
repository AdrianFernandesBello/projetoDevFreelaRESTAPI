using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Command.LoginUser
{
    public class LoginUserCommand : IRequest<LoginViewModel>
    {
        public string Email { get; set; }
        public string  Password { get; set; }
    }
}
