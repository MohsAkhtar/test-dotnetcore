using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    // storing and accessing variables in Contact form
    public class ContactViewModel
    {
        // [Required] for validation
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Message is Too Long")]
        public string Message { get; set; }
    }
}