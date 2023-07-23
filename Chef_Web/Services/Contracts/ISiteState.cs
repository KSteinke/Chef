namespace Chef_Web.Services.Contracts
{
    public interface ISiteState
    {
        public string NoNavMenu_AccountButton { get; set; }
        public bool UserLogged { get; set; }
        public string UserNameLogged { get; set; }

        public Task LoginUserCheck();
    }
}
