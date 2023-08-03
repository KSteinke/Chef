namespace Chef_API.Entities
{
    public class Chef
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
