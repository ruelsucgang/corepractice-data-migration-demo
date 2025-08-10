namespace Demo.Application.DTOs;

public class PatientCsvRow
{
    public int PatientId { get; set; }
    public string PatientIdentifier { get; set; } = string.Empty;
    public string? PatientNo { get; set; }
    public string Firstname { get; set; } = string.Empty; 
    public string Lastname { get; set; } = string.Empty;
    public string? Middlename { get; set; }
    public string? PreferredName { get; set; }
    public string? DateOfBirth { get; set; }  
    public string? Title { get; set; }
    public string? Sex { get; set; }
    public string? Email { get; set; }
    public string? HomePhone { get; set; }
    public string? Mobile { get; set; }
    public string? Occupation { get; set; }
    public string? CompanyName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? Suburb { get; set; }
    public string? Postcode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? IsDeleted { get; set; }
}
