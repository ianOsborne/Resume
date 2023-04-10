using System.ComponentModel.DataAnnotations;

namespace ResumeWebsite.Shared
{
    public class ContactMessage
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

        public ContactMessage(string name, string email, string message)
        {
            Name = name;
            Email = email;
            Message = message;
        }
    }
}
