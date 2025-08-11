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

        // Sex limited (test validation)
        RuleFor(p => p.Sex)
            .Must(s => string.IsNullOrWhiteSpace(s) || s is "M" or "F")
            .WithMessage("Sex must be M or F when provided.");

        // Postcode length per schema (nvarchar(6)) 
        RuleFor(p => p.Postcode)
            .Must(pc => string.IsNullOrWhiteSpace(pc) || pc.Trim().Length <= 6)
            .WithMessage("Postcode max length is 6.");

        // Duplicate Email Rule (case-insensitive)
        RuleFor(x => x.Email)
                 .Must((row, email, ctx) => IsUniqueEmail(email, ctx))
                 .When(x => !string.IsNullOrWhiteSpace(x.Email))
                 .WithMessage("Duplicate email found in CSV.");

    }

    private static bool BeValidDate(string? raw) => DateTime.TryParse(raw, out _);
    private static bool IsUniqueEmail(string email, ValidationContext<PatientCsvRow> ctx)
    {
        if (string.IsNullOrWhiteSpace(email)) return true;

        if (!ctx.RootContextData.TryGetValue("SeenEmails", out var value) ||
            value is not HashSet<string> seen)
        {
            seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            ctx.RootContextData["SeenEmails"] = seen;
        }

        var norm = email.Trim();
        if (seen.Contains(norm)) return false;
        seen.Add(norm);
        return true;
    }
}
