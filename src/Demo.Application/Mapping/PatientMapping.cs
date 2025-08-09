using Demo.Application.DTOs;
using Demo.Domain.Entities;
using Demo.Application.Validation;

namespace Demo.Application.Mapping;

public static class PatientMapping
{
    public static Patient ToEntity(this PatientCsvRow row)
    {
        DateTime? dob = null;
        if (!string.IsNullOrWhiteSpace(row.DateOfBirth) && PatientValidator.TryParseDate(row.DateOfBirth, out var d))
            dob = d;

        return new Patient
        {
            PatientIdentifier = row.PatientIdentifier.Trim(),
            PatientNo = row.PatientNo?.Trim(),
            Firstname = row.Firstname.Trim(),
            Lastname = row.Lastname.Trim(),
            Middlename = row.Middlename?.Trim(),
            PreferredName = row.PreferredName?.Trim(),
            DateOfBirth = dob,
            Title = row.Title?.Trim(),
            Sex = row.Sex?.Trim(),
            Email = row.Email?.Trim(),
            HomePhone = row.HomePhone?.Trim(),
            Mobile = row.Mobile?.Trim(),
            Occupation = row.Occupation?.Trim(),
            CompanyName = row.CompanyName?.Trim(),
            AddressLine1 = row.AddressLine1?.Trim(),
            AddressLine2 = row.AddressLine2?.Trim(),
            Suburb = row.Suburb?.Trim(),
            Postcode = row.Postcode?.Trim(),
            State = row.State?.Trim(),
            Country = row.Country?.Trim(),
            IsDeleted = string.Equals(row.IsDeleted, "true", StringComparison.OrdinalIgnoreCase)
        };
    }
}
