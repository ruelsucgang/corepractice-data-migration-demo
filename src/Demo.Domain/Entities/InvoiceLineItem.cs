namespace Demo.Domain.Entities;

public class InvoiceLineItem
{
    public int InvoiceLineItemId { get; set; }               // PK
    public string InvoiceLineItemIdentifier { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string ItemCode { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
    public decimal LineAmount { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public int TreatmentId { get; set; }
    public Treatment Treatment { get; set; } = null!;

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
}
