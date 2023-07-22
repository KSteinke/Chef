using Chef_Models.Dtos;
using System.Buffers.Text;

namespace Chef_Web.Services.Contracts
{
    public interface ILoginService
    {
        Task<string> Login(LoginDto userCredentials);
    }
}
