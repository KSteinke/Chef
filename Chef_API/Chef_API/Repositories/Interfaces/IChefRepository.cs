using Chef_API.Entities;
using Chef_Models.Dtos;

namespace Chef_API.Repositories.Interfaces
{
    public interface IChefRepository 
    {
        Task<IEnumerable<Chef>>GetChefs();
        Task<bool> veryfiLoginCredentials(LoginDto chefCredentials);
        Task<bool> CheckUserExist(LoginDto userCredentials);
        Task<Chef> RegisterUser(LoginDto userCredentials);
    }
}
