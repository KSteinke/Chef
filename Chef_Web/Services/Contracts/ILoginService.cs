using Chef_Models.Dtos;
using System.Buffers.Text;

namespace Chef_Web.Services.Contracts
{
    public interface ILoginService
    {
        public Task Login(LoginDto userCredentials);
        public Task Logout();

        public Task Register(LoginDto userCredentials);

    }
}
