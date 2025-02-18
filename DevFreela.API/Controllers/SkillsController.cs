using DevFreela.Application.Command.InsertSkill;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new Application.Queries.GetAllProject.GetAllSkillQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = _mediator.Send(command);

            return NoContent();
        }
    }
}
