using DevFreela.Application.Command.InsertUser;
using DevFreela.Application.Command.InsertUserSkill;
using DevFreela.Application.Command.LoginUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetByIdUser;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly DevFreelaDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;

        public UsersController(IMediator mediator, IAuthService authService, DevFreelaDbContext context, IMemoryCache cache, IEmailService emailService)
        {
            _authService = authService;
            _mediator = mediator;
            _context = context;
            _cache = cache;
            _emailService = emailService;
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
        [AllowAnonymous]
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

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            //calculando password baseado na hash recebido
            var hash = _authService.ComputeSha256Has(model.Password);

            //buscando se o usuario existe baseado no email e password
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == hash);

            //se nao encontar retorna Erro
            if (user is null)
            {
                var error = ResultViewModel<LoginViewModel?>.Error("Error de login");
                return BadRequest(error);
            }

            //gerando um token passando um email e role do usuario
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            //gerar um viwmodel baseado nas resposta do token
            var viewModel = new LoginViewModel(token);

            //gerando uma resposta de sucess
            var result= ResultViewModel<LoginViewModel?>.Sucess(viewModel);

            return Ok(result);
        }
        
        [HttpPost("password-recovery/request")]
        public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryRequestInputModel model)
        {
            //buscando Usuario
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            //Validando se usuario existe
            if (user is null)
            {
                return BadRequest();
            }

            //gerando um codigo entre qtd de caracteres
            var code = new Random().Next(10000, 99999).ToString();

            //definindo uma chave para usar em cache em memoria
            var cacheKey = $"RecoveryCode:{model.Email}";

            //salvando na memoria a chave com codigo com 10 minutos de expiração
            _cache.Set(cacheKey, code, TimeSpan.FromHours(10));

            //enviando o email com esse codigo
            await _emailService.SendEmailAsync(user.Email, "Codigo de Recuperação" , $"Seu Codigo de Recuperação é: {code}");

            return NoContent();
        }

        [HttpPost("password-recovery/validate")]
        public async Task<IActionResult> ValidateRecoveryCode(ValidateRecoveryCodeInputModel model)
        {
            //construir uma chacheKey
            var cacheKey = $"RecoveryCode:{model.Email}";

            if(!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost("password-recovery/change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";

            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            _cache.Remove(cacheKey);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user is null)
            {
                return BadRequest();
            }

            var hash = _authService.ComputeSha256Has(model.NewPassword);

            user.UpdatePassword(hash);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
