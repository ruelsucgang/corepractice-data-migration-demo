using Demo.Application.DTOs;
using FluentValidation;

namespace Demo.Application.Validation;

public class TreatmentCsvValidator : AbstractValidator<TreatmentCsvRow>
{

    public TreatmentCsvValidator()
    {
        //RuleFor(x => x.TreatmentIdentifier).NotEmpty();
        //RuleFor(x => x.PatientIdentifier).NotEmpty();
        //RuleFor(x => x.Description).NotEmpty();
        //RuleFor(x => x.ItemCode).NotEmpty();

        // for testing only
        RuleFor(t => t.Fee)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Fee is required.")
        .Must(f => decimal.TryParse(f, out var v) && v >= 0m)
        .WithMessage("Fee must be a valid non-negative number.");

        RuleFor(t => t.Quantity)
       .Cascade(CascadeMode.Stop)
       .NotEmpty().WithMessage("Quantity is required.")
       .Must(f => decimal.TryParse(f, out var v) && v >= 0m)
       .WithMessage("Quantity must be a valid non-negative number.");

        // add more validation here 

    }
}
