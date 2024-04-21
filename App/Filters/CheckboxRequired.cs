using System.ComponentModel.DataAnnotations;

namespace App.Filters;

public class CheckboxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
    
}
