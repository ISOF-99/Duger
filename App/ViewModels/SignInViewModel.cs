using System.ComponentModel.DataAnnotations;

namespace App.ViewModels;

public class SignInViewModel
{
    [Required]
    [Display(Name ="E-mail address", Prompt =" Enter ypur e-mail address")]
    [DataType(DataType.EmailAddress)]

    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = " Enter ypur Password")]
    [DataType(DataType.Password)]


    public string Password { get; set; } = null!;

 
    [Display(Name = "Remember me", Prompt = " Remember me")]
    
    public bool Ispresistent { get; set; }


}
