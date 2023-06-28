using Chef_API.Entities;

namespace Chef_API.Repositories.Interfaces
{
    public interface IChefRepository 
    {
        Task<IEnumerable<Chef>>GetChefs();
    }
}
