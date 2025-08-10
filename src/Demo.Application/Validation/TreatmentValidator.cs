using Demo.Application.DTOs;
using FluentValidation;

namespace Demo.Application.Validation;

public class TreatmentCsvValidator : AbstractValidator<TreatmentCsvRow>
{
    public TreatmentCsvValidator()
    {
        RuleFor(x => x.TreatmentIdentifier).NotEmpty();
        RuleFor(x => x.PatientIdentifier).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.ItemCode).NotEmpty();

        RuleFor(x => x.Quantity)
            .Must(q => int.TryParse(q, out var n) && n > 0)
            .WithMessage("Quantity must be a positive integer");

        RuleFor(x => x.Fee)
            .Must(f => decimal.TryParse(f, out var d) && d >= 0)
            .WithMessage("Fee must be a non-negative number");
    }
}
