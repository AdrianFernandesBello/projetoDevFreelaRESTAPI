using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProject
{
    public class GetAllSkillQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
    }
}
