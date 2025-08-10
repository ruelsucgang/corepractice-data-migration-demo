using Demo.Application.DTOs;
using Demo.Domain.Entities;

namespace Demo.Application.Mapping;

public static class PatientMapping
{
    public static Patient ToEntity(this PatientCsvRow r) =>
        new Patient
        {
            PatientIdentifier = r.PatientIdentifier,
            PatientNo = r.PatientNo,
            Firstname = r.Firstname,
            Lastname = r.Lastname,
            Middlename = r.Middlename,
            PreferredName = r.PreferredName,
            DateOfBirth = TryDate(r.DateOfBirth),
            Title = r.Title,
            Sex = r.Sex,
            Email = Normalize(r.Email),
            HomePhone = Digits(r.HomePhone),
            Mobile = Digits(r.Mobile),
            Occupation = r.Occupation,
            CompanyName = r.CompanyName,
            AddressLine1 = r.AddressLine1,
            AddressLine2 = r.AddressLine2,
            Suburb = r.Suburb,
            Postcode = r.Postcode,
            State = r.State,
            Country = r.Country,
            IsDeleted = false
        };

    private static DateTime? TryDate(string? s) => DateTime.TryParse(s, out var d) ? d : null;
    private static string? Normalize(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    private static string? Digits(string? s)
        => string.IsNullOrWhiteSpace(s) ? null : new string(s.Where(char.IsDigit).ToArray());
}