using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.Application.Abstractions;
using Demo.Application.DTOs;

namespace Demo.Presentation.WinForms
{
    public partial class MigrationForm : Form
    {
        private readonly ICsvReaderService _csv;
        private readonly IMigrationService _migration;

        private readonly BindingList<PatientCsvRow> _patients = new();
        private readonly BindingList<TreatmentCsvRow> _treatments = new();

        private System.Collections.Generic.List<(PatientCsvRow row, System.Collections.Generic.List<ValidationIssue> issues)> _invalidPatients = new();
        private System.Collections.Generic.List<(TreatmentCsvRow row, System.Collections.Generic.List<ValidationIssue> issues)> _invalidTreatments = new();

        public MigrationForm(ICsvReaderService csv, IMigrationService migration)
        {
            _csv = csv;
            _migration = migration;
            InitializeComponent();

            gridPatients.DataSource = _patients;
            gridTreatments.DataSource = _treatments;
            SetStatus("Ready");
        }

        // ------------- Button handlers (exact signatures for Designer) -------------
        private void btnLoadPatientsCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select patients.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK) LoadPatients(ofd.FileName);
        }

        private void btnLoadTreatmentsCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select treatments.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK) LoadTreatments(ofd.FileName);
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                var (pValid, pInvalid) = await _migration.ValidatePatientsAsync(_patients.ToList());
                _invalidPatients = pInvalid;

                var existingPatients = pValid.Select(v => new Demo.Domain.Entities.Patient
                {
                    PatientIdentifier = v.PatientIdentifier
                }).ToList();

                var (tValid, tInvalid) = await _migration.ValidateTreatmentsAsync(_treatments.ToList(), existingPatients);
                _invalidTreatments = tInvalid;

                Log($"Validation finished. Patients valid {pValid.Count}, invalid {pInvalid.Count}. " +
                    $"Treatments valid {tValid.Count}, invalid {tInvalid.Count}.");

                if (chkPatientsInvalidOnly.Checked) ApplyPatientsInvalidFilter();
                if (chkTreatmentsInvalidOnly.Checked) ApplyTreatmentsInvalidFilter();

                SetStatus("Validation complete");
            }
            catch (Exception ex)
            {
                Log($"Validate error: {ex.Message}");
            }
        }

        private async void btnIngest_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _migration.IngestAsync(_patients.ToList(), _treatments.ToList());
                Log($"Ingest done. Patients: {result.PatientsInserted}, Treatments: {result.TreatmentsInserted}, " +
                    $"Invoices: {result.InvoicesCreated}, Lines: {result.LineItemsCreated}");
                SetStatus("Ingestion complete");
            }
            catch (Exception ex)
            {
                Log($"Ingest error: {ex.Message}");
            }
        }

        private void btnExportInvalid_Click(object sender, EventArgs e)
        {
            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Export invalid rows as CSV",
                    FileName = "invalid_rows.csv"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using var sw = new StreamWriter(sfd.FileName);
                sw.WriteLine("Type,RowIndex,Field,Message");

                foreach (var (_, issues) in _invalidPatients)
                    foreach (var i in issues)
                        sw.WriteLine($"Patient,{i.RowIndex},{i.Field},\"{i.Message.Replace("\"", "\"\"")}\"");

                foreach (var (_, issues) in _invalidTreatments)
                    foreach (var i in issues)
                        sw.WriteLine($"Treatment,{i.RowIndex},{i.Field},\"{i.Message.Replace("\"", "\"\"")}\"");

                Log($"Exported invalid rows to {sfd.FileName}");
                SetStatus("Invalid rows exported");
            }
            catch (Exception ex)
            {
                Log($"Export error: {ex.Message}");
            }
        }

        // ------------- Checkbox handlers (exact signatures for Designer) -------------
        private void chkPatientsInvalidOnly_CheckedChanged(object sender, EventArgs e) => ApplyPatientsInvalidFilter();
        private void chkTreatmentsInvalidOnly_CheckedChanged(object sender, EventArgs e) => ApplyTreatmentsInvalidFilter();

        // ------------- Helpers -------------
        private async void LoadPatients(string path)
        {
            try
            {
                var rows = await _csv.ReadPatientsAsync(path);
                _patients.Clear();
                foreach (var r in rows) _patients.Add(r);
                Log($"Loaded patients: {rows.Count}");
                SetStatus($"Patients loaded: {rows.Count}");
            }
            catch (Exception ex)
            {
                Log($"Error loading patients: {ex.Message}");
            }
        }

        private async void LoadTreatments(string path)
        {
            try
            {
                var rows = await _csv.ReadTreatmentsAsync(path);
                _treatments.Clear();
                foreach (var r in rows) _treatments.Add(r);
                Log($"Loaded treatments: {rows.Count}");
                SetStatus($"Treatments loaded: {rows.Count}");
            }
            catch (Exception ex)
            {
                Log($"Error loading treatments: {ex.Message}");
            }
        }

        private void ApplyPatientsInvalidFilter()
        {
            if (!chkPatientsInvalidOnly.Checked)
            {
                gridPatients.DataSource = new BindingList<PatientCsvRow>(_patients.ToList());
                return;
            }
            var invalidSet = _invalidPatients.Select(x => x.row).ToHashSet();
            gridPatients.DataSource = new BindingList<PatientCsvRow>(_patients.Where(p => invalidSet.Contains(p)).ToList());
        }

        private void ApplyTreatmentsInvalidFilter()
        {
            if (!chkTreatmentsInvalidOnly.Checked)
            {
                gridTreatments.DataSource = new BindingList<TreatmentCsvRow>(_treatments.ToList());
                return;
            }
            var invalidSet = _invalidTreatments.Select(x => x.row).ToHashSet();
            gridTreatments.DataSource = new BindingList<TreatmentCsvRow>(_treatments.Where(t => invalidSet.Contains(t)).ToList());
        }

        private void Log(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }
    }
}
