using Crm_Api.Contracts.Request;
using Crm_Api.Shared.Constants;
using FluentValidation;

namespace Crm_Api.Features.RegisterCustomer;

public class RegisterCustomerRequestValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("Customer's first name is required.")
            .NotEmpty().WithMessage("Customer's first name is required.")
            .MaximumLength(150).WithMessage("Customer's first name cannot exceed 150 characters")
            .Matches(RegexPatterns.LetterOnlyRegex).WithMessage("Customer's first name is not in the correct format");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("Customer's last name is required.")
            .NotEmpty().WithMessage("Customer's last name is required.")
            .MaximumLength(200).WithMessage("Customer's last name cannot exceed 100 characters")
            .Matches(RegexPatterns.LetterOnlyRegex).WithMessage("Customer's last name is not in the correct format");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("Customer's phone number is required.")
            .NotEmpty().WithMessage("Customer's phone number is required.")
            .Matches(RegexPatterns.PhoneNumberRegex).WithMessage("Customer's's phone number is not in the correct format");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Customer's email is required.")
            .NotEmpty().WithMessage("Customer's email is required.")
            .EmailAddress().WithMessage("Customer's email is not in the correct format.");

        RuleFor(x => x.DateOfBirth)
            .NotNull().WithMessage("Customer's date of birth is required.");
    }
}