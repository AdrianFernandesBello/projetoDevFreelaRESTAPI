using DevFreela.Application.Command.InsertSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISkillsService _service;
        private readonly DevFreelaDbContext _context;
        public SkillsController(DevFreelaDbContext context, ISkillsService service, IMediator mediator)
        {
            _mediator = mediator;
            _service = service;
            _context = context;
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
