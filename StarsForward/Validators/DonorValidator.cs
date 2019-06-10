using FluentValidation;
using StarsForward.ViewModels;

namespace StarsForward.Validators
{
    public class DonorValidator : AbstractValidator<DonorViewModel>
    {
        public DonorValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Address1).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.Zip).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}