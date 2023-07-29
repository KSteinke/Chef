using Chef_Web.Services.Contracts;
using Microsoft.JSInterop;

namespace Chef_Web.Services
{
    public class CookieManager : ICookieManager
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public CookieManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/CookieManager.js"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }


        public async Task<string> GetValueAsync(string key)
        {
            await WaitForReference();
            var result = await _accessorJsRef.Value.InvokeAsync<string>("get", key);
            
            return result;
        }

        public async Task SetValueAsync<T>(string key, T value)
        {

            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, value);
        }



    }
}
