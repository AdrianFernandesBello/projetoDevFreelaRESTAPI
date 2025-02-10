using DevFreela.Application.Models;
using Microsoft.AspNetCore.Http;


namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel Post(CreateUserInputModel model);
        ResultViewModel PostSkill(int id, UserSkillsInputModel model);
        ResultViewModel<IFormFile> PostProfilePicture(int id, IFormFile file);
    }

}
