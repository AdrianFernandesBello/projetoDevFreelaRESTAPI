using DevFreela.Application.Command.InsertUser;
using DevFreela.Application.Command.InsertUserSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetByIdUser;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _service;
        private readonly DevFreelaDbContext _context;
        public UsersController(DevFreelaDbContext context, UserService service, IMediator mediator)
        {
            _mediator = mediator;
            _service = service;
            _context = context;
        }

        //GET api/user/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, GetByIdUserQuery command)
        {
            var result = _mediator.Send(command);

            return Ok(command);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            return NoContent();
        }

        //POST api/user/1/skill
        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillCommand command)
        {
            var result = _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var result = _service.PostProfilePicture(id, file);

            // Processar a imagem

            return Ok(result);
        }
    }
}
