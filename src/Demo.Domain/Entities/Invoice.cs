namespace Demo.Domain.Entities;

public class Invoice
{
    public int InvoiceId { get; set; } // PK
    public string InvoiceIdentifier { get; set; } = null!;
    public int InvoiceNo { get; set; }

    public DateTime? InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Note { get; set; }

    public decimal Total { get; set; }
    public decimal? Paid { get; set; }
    public decimal? Discount { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();
}
