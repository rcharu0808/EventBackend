namespace TravelWebsiteBackend.Models
{
    public class Userlogin
    {
        public string Name { get; set; }    
        public string PhoneNo { get; set; }    
        public string Mail { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }
    }
    public class Login
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
