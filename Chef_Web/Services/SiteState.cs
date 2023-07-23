using Chef_Web.Services.Contracts;

namespace Chef_Web.Services
{
    public class SiteState : ISiteState
    {
        public ICookieManager _cookieManager { get; set; }

        public string NoNavMenu_AccountButton {get; set;}
        public bool UserLogged { get; set;} = false;
        public string UserNameLogged { get; set; } 
        public SiteState(ICookieManager cookieManager)
        {
            NoNavMenu_AccountButton = "Account";
            _cookieManager = cookieManager;
        }

        public async Task LoginUserCheck()
        {
            var response = await _cookieManager.GetValueAsync("UserName");
            if (response != "")
            {
                UserNameLogged = response;
                NoNavMenu_AccountButton = UserNameLogged;
                UserLogged = true;
            }
        }

    }
}
