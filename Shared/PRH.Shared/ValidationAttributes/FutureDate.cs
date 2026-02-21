using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.ValidationAttributes;
public class FutureDate : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateValue)
            return dateValue > DateTime.Now;

        return false;
    }
}
