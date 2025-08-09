using Demo.Application.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Demo.Application.Validation;

public class PatientValidator : AbstractValidator<PatientCsvRow>
{
    public PatientValidator()
    {
        RuleFor(x => x.PatientIdentifier)
            .NotEmpty().WithMessage("PatientIdentifier is required.")
            .MaximumLength(50);

        RuleFor(x => x.Firstname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Lastname).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email)
            .MaximumLength(256)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Mobile)
            .Must(IsPhoneLike).When(x => !string.IsNullOrWhiteSpace(x.Mobile))
            .WithMessage("Mobile must be a valid phone number.");

        RuleFor(x => x.DateOfBirth)
            .Must(BeValidDate).When(x => !string.IsNullOrWhiteSpace(x.DateOfBirth))
            .WithMessage("DateOfBirth must be a valid date (yyyy-MM-dd or dd/MM/yyyy).");
    }

    public static bool BeValidDate(string? value)
        => TryParseDate(value, out _);

    private static bool IsPhoneLike(string? value)
        => string.IsNullOrWhiteSpace(value) || Regex.IsMatch(value!, @"^[0-9+\-\s()]{6,25}$");

    public static bool TryParseDate(string? input, out DateTime result)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy" };
        return DateTime.TryParseExact(input, formats, null, System.Globalization.DateTimeStyles.None, out result)
               || DateTime.TryParse(input, out result);
    }
}
