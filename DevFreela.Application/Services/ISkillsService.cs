using DevFreela.Application.Models;


namespace DevFreela.Application.Services
{
    public interface ISkillsService
    {
        ResultViewModel<int> Post(CreateSkillInputModel model);

    }
}
