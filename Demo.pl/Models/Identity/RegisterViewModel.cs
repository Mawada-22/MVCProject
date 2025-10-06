using System.ComponentModel.DataAnnotations;

namespace Demo.pl.Models.Identity
{
    public class RegisterViewModel
    {
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Second Name")]
        public string SecondName { get; set; } = null!;

        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Passwords Don't match") ]
        public string ConfirmPassword { get; set; }=null!;

        public bool IsAgree {  get; set; }


    }
}
