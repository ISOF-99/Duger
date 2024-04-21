using App.Filters;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels;

public class SignUpViewModel
{
    [Required]
    [Display(Name = "First name", Prompt = " Enter your First name")]
  
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name", Prompt = " Enter ypur Last name")]
    
    public string LastName { get; set; } = null!;
   
    [Required]
    [Display(Name = "E-mail address", Prompt = " Enter ypur e-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = " Enter ypur Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Required]
    [Display(Name = "Password", Prompt = " Enter ypur Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [CheckboxRequired]
    [Display(Name = "I agree to the Terms & Conditions ", Prompt = "Terms and Conditions")]
    public bool TermsAndConditions { get; set; }


    //public bool Ispresistent { get; set; }
}
