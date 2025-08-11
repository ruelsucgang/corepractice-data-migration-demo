using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Demo.Application.Abstractions;
using Demo.Application.DTOs;

namespace Demo.Infrastructure.Services;

public class CsvReaderService : ICsvReaderService
{
    public async Task<List<PatientCsvRow>> ReadPatientsAsync(string filePath, CancellationToken ct = default)
    {
        using var reader = new StreamReader(filePath);
        using var csv = CreateCsv(reader);

        // Register our header maps
        csv.Context.RegisterClassMap<PatientMap>();

        var rows = new List<PatientCsvRow>();
        await foreach (var r in csv.GetRecordsAsync<PatientCsvRow>(ct))
            rows.Add(r);

        return rows;
    }

    public async Task<List<TreatmentCsvRow>> ReadTreatmentsAsync(string filePath, CancellationToken ct = default)
    {
        using var reader = new StreamReader(filePath);
        using var csv = CreateCsv(reader);

        csv.Context.RegisterClassMap<TreatmentMap>();

        var rows = new List<TreatmentCsvRow>();
        await foreach (var r in csv.GetRecordsAsync<TreatmentCsvRow>(ct))
            rows.Add(r);

        return rows;
    }

    private static CsvReader CreateCsv(TextReader reader)
    {
        var cfg = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            DetectDelimiter = true,
            PrepareHeaderForMatch = args => args.Header?.Trim(),
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true,

            // Important: huwag mag-throw kapag kulang o iba ang header,
            // para ma-handle natin via ClassMap.Optional()
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null
        };
        return new CsvReader(reader, cfg);
    }

    // Class Maps with header aliases 

    private sealed class PatientMap : ClassMap<PatientCsvRow>
    {
        public PatientMap()
        {
            Map(m => m.PatientIdentifier).Name("PatientIdentifier", "Id");
            Map(m => m.PatientNo).Name("PatientNo").Optional();

            Map(m => m.Firstname).Name("Firstname", "FirstName");
            Map(m => m.Lastname).Name("Lastname", "LastName");
            Map(m => m.Middlename).Name("Middlename", "MiddleName", "Middle").Optional();
            Map(m => m.PreferredName).Name("PreferredName", "Preferred").Optional();

            Map(m => m.DateOfBirth).Name("DateOfBirth", "DOB", "BirthDate").Optional();
            Map(m => m.Title).Name("Title").Optional();
            Map(m => m.Sex).Name("Sex", "Gender").Optional();

            Map(m => m.Email).Name("Email", "EmailAddress").Optional();
            Map(m => m.HomePhone).Name("HomePhone", "PhoneNumber", "Phone").Optional();
            Map(m => m.Mobile).Name("Mobile", "MobileNumber", "MobilePhone").Optional();
            Map(m => m.Occupation).Name("Occupation").Optional();
            Map(m => m.CompanyName).Name("CompanyName", "Company").Optional();

            Map(m => m.AddressLine1).Name("AddressLine1", "Street", "Address1").Optional();
            Map(m => m.AddressLine2).Name("AddressLine2", "Address2").Optional();
            Map(m => m.Suburb).Name("Suburb", "City", "Town").Optional();
            Map(m => m.Postcode).Name("Postcode", "Zip", "ZipCode").Optional();
            Map(m => m.State).Name("State", "Province").Optional();
            Map(m => m.Country).Name("Country").Optional();

            Map(m => m.IsDeleted).Name("IsDeleted").Optional();
        }
    }

    private sealed class TreatmentMap : ClassMap<TreatmentCsvRow>
    {
        public TreatmentMap()
        {
            Map(m => m.TreatmentIdentifier).Name("TreatmentIdentifier", "Id", "TreatmentId");
            Map(m => m.CompleteDate).Name("CompleteDate", "Date", "ServiceDate", "TxDate").Optional();
            Map(m => m.Description).Name("Description", "Notes", "ServiceDescription");
            Map(m => m.ItemCode).Name("ItemCode", "Code", "ServiceCode");
            Map(m => m.Tooth).Name("Tooth").Optional();
            Map(m => m.Surface).Name("Surface").Optional();
            Map(m => m.Quantity).Name("Quantity", "Qty").Optional();
            Map(m => m.Fee).Name("Fee", "Amount", "Price", "UnitAmount");
            Map(m => m.PatientIdentifier).Convert(ctx =>
            {
                // primary: PatientIdentifier
                if (ctx.Row.TryGetField("PatientIdentifier", out string v) && !string.IsNullOrWhiteSpace(v))
                    return v.Trim();

                // common legacy headers:
                string[] candidates = { "PatientId", "PatientID", "Patient_Id", "PatId", "PID", "Patient", "Patient Ref" };
                foreach (var name in candidates)
                    if (ctx.Row.TryGetField(name, out string x) && !string.IsNullOrWhiteSpace(x))
                        return x.Trim();

                return null; // wala talaga
            });
            Map(m => m.IsPaid).Name("IsPaid", "Paid").Optional();
            Map(m => m.IsVoided).Name("IsVoided", "Voided", "IsCancelled").Optional();
        }
    }
}
