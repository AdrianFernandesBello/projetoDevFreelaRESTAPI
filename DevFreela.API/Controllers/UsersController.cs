using DevFreela.Application.Command.InsertUser;
using DevFreela.Application.Command.InsertUserSkill;
using DevFreela.Application.Queries.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> PostProfilePictureAsync(int id, IFormFile file)
        {
            var result = await _mediator.Send(id);

            // Processar a imagem

            return Ok(result);
        }
    }
}
