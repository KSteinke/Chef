using Microsoft.JSInterop;

namespace Chef_Web.Services.Contracts
{
    public interface ICookieManager
    {
        public Task<string> GetValueAsync(string key);
        public Task SetValueAsync<T>(string key, T value);
    }
}
