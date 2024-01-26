using System;
namespace ImageAuthenticationSystem.Model
{
    public class ImageAuthenticationModel
    {
        public string Status { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public List<string> SelectedImageNames { get; set; }
        public string FirstName { get; internal set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
    }
}

