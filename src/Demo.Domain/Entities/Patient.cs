namespace Demo.Domain.Entities;

public class Patient
{
    public int PatientId { get; set; }  // PK (SQLite identity)
    public string PatientIdentifier { get; set; } = null!;
    public string? PatientNo { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;  
    public string? Middlename { get; set; }
    public string? PreferredName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Title { get; set; }
    public string? Sex { get; set; }  // keep as string per schema (M/F/Other)
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

    public bool IsDeleted { get; set; }
    public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
