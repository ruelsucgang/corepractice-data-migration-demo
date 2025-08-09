using Demo.Application.DTOs;
using FluentValidation;

namespace Demo.Application.Validation;
public class TreatmentValidator : AbstractValidator<TreatmentCsvRow>
{
    public TreatmentValidator()
    {
        RuleFor(x => x.TreatmentIdentifier)
            .NotEmpty().WithMessage("TreatmentIdentifier is required.")
            .MaximumLength(50);   

        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
        RuleFor(x => x.ItemCode).NotEmpty().MaximumLength(10);

        RuleFor(x => x.Quantity)
            .Must(x => int.TryParse(x, out var q) && q > 0)
            .WithMessage("Quantity must be a positive integer.");

        RuleFor(x => x.Fee)
            .Must(x => decimal.TryParse(x, out var f) && f >= 0)
            .WithMessage("Fee must be a non-negative decimal.");

        RuleFor(x => x.CompleteDate)
            .Must(PatientValidator.BeValidDate)
            .When(x => !string.IsNullOrWhiteSpace(x.CompleteDate))
            .WithMessage("CompleteDate must be a valid date.");

        RuleFor(x => x.PatientIdentifier)  
            .NotEmpty().WithMessage("PatientIdentifier is required to link treatment to a patient.");  
    }
}
