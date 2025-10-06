using System.ComponentModel.DataAnnotations;

namespace Demo.pl.Models.Identity
{
    public class loginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        
        public bool RememberMe { get; set; }

    
    
    }
}
