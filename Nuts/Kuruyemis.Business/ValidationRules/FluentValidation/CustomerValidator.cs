
using FluentValidation;
using Kuruyemis.Entities.Concrete;

namespace Nuts.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        { 

        }
    }
}
