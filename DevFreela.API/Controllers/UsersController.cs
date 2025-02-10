using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly DevFreelaDbContext _context;
        public UsersController(DevFreelaDbContext context, UserService service)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, UserViewModel model)
        {
            var result = _service.GetById(id);

            return Ok(model);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _service.Post(model);

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var result = _service.PostSkill(id, model);

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
