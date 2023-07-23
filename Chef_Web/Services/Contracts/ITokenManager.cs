namespace Chef_Web.Services.Contracts
{
    public interface ITokenManager
    {
        public Task WriteToken(string token);
        public Task<string> ReadToken();

    }
}
