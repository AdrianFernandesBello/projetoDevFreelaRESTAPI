using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Command.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _repository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository repository)
        {
            _authService = authService;
            _repository = repository;
        }

        public async Task<LoginViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passworHash = _authService.ComputeSha256Has(request.Password);

            var user = await _repository.GetUserByEmailAndPassworHashAsync(request.Email, passworHash);

            if (user == null)
            {
                return null;
            }

            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return new LoginViewModel(token);
        }
    }
}
