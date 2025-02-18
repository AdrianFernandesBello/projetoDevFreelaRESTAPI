

using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkilllRepository
    {
        Task<List<Skill>> GetAllSkill();
        Task AddSkill(Skill skill);
    }
}
