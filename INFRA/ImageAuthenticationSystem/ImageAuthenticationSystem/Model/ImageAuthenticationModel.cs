using System;
namespace ImageAuthenticationSystem.Model
{
    public class ImageAuthenticationModel
    {
        public string ?Status { get; set; }
        public string ?Message { get; set; }
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public string ?EmailId { get; set; }
        public List<string> ?SelectedImageNames { get; set; }
        public List<string> ? PasswordList { get; set; }
        public string ?Password { get; internal set; }
    }
}

