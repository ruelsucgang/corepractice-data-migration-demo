using Demo.Application.DTOs;
using FluentValidation;

namespace Demo.Application.Validation;

public class PatientCsvValidator : AbstractValidator<PatientCsvRow>
{
    public PatientCsvValidator()
    {
        RuleFor(x => x.PatientIdentifier).NotEmpty();
        RuleFor(x => x.Firstname).NotEmpty();
        RuleFor(x => x.Lastname).NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.DateOfBirth)
            .Must(BeValidDate)
            .When(x => !string.IsNullOrWhiteSpace(x.DateOfBirth))
            .WithMessage("Invalid date format, use yyyy-MM-dd or dd/MM/yyyy");
    }

    private static bool BeValidDate(string? raw) => DateTime.TryParse(raw, out _);
}
