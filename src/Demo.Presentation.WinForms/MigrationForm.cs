using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.Application.Abstractions;
using Demo.Application.DTOs;
using Demo.Infrastructure.Services;

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
            this.WindowState = FormWindowState.Maximized;
            gridPatients.DataSource = _patients;
            gridTreatments.DataSource = _treatments;
            SetStatus("Ready");
        }

        private void MigrationForm_Load(object sender, EventArgs e)
        {
            DefinePatientGrdView();
            DefineTreatmentGrdView();
            Steps(1);
            UpdatePictureBox();
        }

        // Button handlers 
        private void btnLoadPatientsCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select patients.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadPatients(ofd.FileName);
                Steps(2);
                DisableBtns(2);
                UpdatePictureBox();
            }
        }

        private void btnLoadTreatmentsCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select treatments.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadTreatments(ofd.FileName);
                Steps(3);
                DisableBtns(3);
                UpdatePictureBox();
            }
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Validate patients as usual
                var (pValid, pInvalid) = await _migration.ValidatePatientsAsync(_patients.ToList());
                _invalidPatients = pInvalid;

                // 2) TEST MODE: gamitin muna lahat ng patients (hindi lang pValid)
                //    para hindi ma-inflate ang invalid count ng treatments dahil sa FK check.
                var existingPatients = _patients
                    .Select(v => new Demo.Domain.Entities.Patient
                    {
                        PatientIdentifier = (v.PatientIdentifier ?? string.Empty).Trim()
                    })
                    .Where(p => !string.IsNullOrWhiteSpace(p.PatientIdentifier))
                    .GroupBy(p => p.PatientIdentifier, StringComparer.OrdinalIgnoreCase)
                    .Select(g => g.First())
                    .ToList();

                // 3) Linisin ang treatments at i-skip ang whitespace-only/trailing rows
                var cleanedTreatments = _treatments
                    .Where(t =>
                        !(string.IsNullOrWhiteSpace(t.TreatmentIdentifier)
                       && string.IsNullOrWhiteSpace(t.ItemCode)
                       && string.IsNullOrWhiteSpace(t.Description)
                       && string.IsNullOrWhiteSpace(t.PatientIdentifier)
                       && string.IsNullOrWhiteSpace(t.Quantity)
                       && string.IsNullOrWhiteSpace(t.Fee)
                       && string.IsNullOrWhiteSpace(t.CompleteDate)))
                    .Select(t =>
                    {
                        t.ItemCode = t.ItemCode?.Trim();
                        t.Description = t.Description?.Trim();
                        t.PatientIdentifier = t.PatientIdentifier?.Trim();
                        return t;
                    })
                    .ToList();

                // 4) Validate treatments laban sa cleaned list + test-mode patients
                var (tValid, tInvalid) = await _migration.ValidateTreatmentsAsync(cleanedTreatments, existingPatients);
                _invalidTreatments = tInvalid;

                // 5) Log + UI updates
                Log($"Validation finished. Patients valid {pValid.Count}, invalid {pInvalid.Count}. " +
                    $"Treatments valid {tValid.Count}, invalid {tInvalid.Count}.");

                // Respect the checkboxes (refresh the bound grid accordingly)
                if (chkPatientsInvalidOnly.Checked) ApplyPatientsInvalidFilter(); else gridPatients.DataSource = _patients;
                if (chkTreatmentsInvalidOnly.Checked) ApplyTreatmentsInvalidFilter(); else gridTreatments.DataSource = _treatments;

                SetStatus("Validation complete");
                Steps(4);
                DisableBtns(4);
                UpdatePictureBox();
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
                btnIngest.Enabled = false;
                _waitOverlay.Visible = true;   // wating panel
                _waitOverlay.BringToFront();

                // Force a paint cycle so the overlay + marquee actually render
                _waitOverlay.Update();        // paints immediately
                await Task.Yield();           // give UI thread back to message loop
                await Task.Delay(50);         // tiny delay so marquee animation starts

                var result = await Task.Run(async () =>
                {
                    // call the real async method inside Task.Run and unwrap
                    return await _migration.IngestAsync(_patients.ToList(), _treatments.ToList());
                });

                Log($"Ingest done. Patients: {result.PatientsInserted}, Treatments: {result.TreatmentsInserted}, " +
                    $"Invoices: {result.InvoicesCreated}, Lines: {result.LineItemsCreated}");
                SetStatus("Ingestion complete");
                DisableBtns(5);
                UpdatePictureBox();

                // Show success message
                MessageBox.Show(
                    $"Data ingestion completed successfully!",
                    "Ingestion Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                Log($"Ingest error: {ex.Message}");
            }
            finally
            {
                HideBusy();
                btnIngest.Enabled = true;
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
                DisableBtns(1);
                UpdatePictureBox();
                ResetAll();
            }
            catch (Exception ex)
            {
                Log($"Export error: {ex.Message}");
            }
        }

        // Checkbox handlers 
        private void chkPatientsInvalidOnly_CheckedChanged(object sender, EventArgs e) => ApplyPatientsInvalidFilter();
        private void chkTreatmentsInvalidOnly_CheckedChanged(object sender, EventArgs e) => ApplyTreatmentsInvalidFilter();

        // Helpers 
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
                gridPatients.DataSource = _patients;
                return;
            }

            var invalidSet = _invalidPatients.Select(x => x.row).ToHashSet();
            var filtered = new BindingList<PatientCsvRow>(
                _patients.Where(p => invalidSet.Contains(p)).ToList()
            );
            gridPatients.DataSource = filtered;
        }
        private void ApplyTreatmentsInvalidFilter()
        {
            if (!chkTreatmentsInvalidOnly.Checked)
            {
                gridTreatments.DataSource = _treatments; // ADDED: return to live list
                return;
            }

            var invalidSet = _invalidTreatments.Select(x => x.row).ToHashSet();
            var filtered = new BindingList<TreatmentCsvRow>(
                _treatments.Where(t => invalidSet.Contains(t)).ToList()
            );
            gridTreatments.DataSource = filtered; // unchanged, filtered view
        }


        private void Log(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        void DefinePatientGrdView()
        {
            // 1. auto size columns based on content
            gridPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Optional: Adjust after loading data to fit
            gridPatients.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // 2. Header font bold and clor
            gridPatients.ColumnHeadersDefaultCellStyle.Font = new Font(gridPatients.Font, FontStyle.Bold);
            gridPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // header background color
            gridPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // header text color

            // 3. enable ang visual styles for the colors
            gridPatients.EnableHeadersVisualStyles = false;

            // 4. Hide PatientId column
            if (gridPatients.Columns.Contains("PatientId"))
            {
                gridPatients.Columns["PatientId"].Visible = false;
            }

            // 5. Rename PatientIdentifier header
            if (gridPatients.Columns.Contains("PatientIdentifier"))
            {
                gridPatients.Columns["PatientIdentifier"].HeaderText = "Patient Identifier";
            }
        }

        void DefineTreatmentGrdView()
        {
            // 1. auto size columns based on content
            gridTreatments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Optional: Adjust after loading data to fit
            gridTreatments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // 2. Header font bold and clor
            gridTreatments.ColumnHeadersDefaultCellStyle.Font = new Font(gridTreatments.Font, FontStyle.Bold);
            gridTreatments.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // header background color
            gridTreatments.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // header text color

            // 3. enable ang visual styles for the colors
            gridTreatments.EnableHeadersVisualStyles = false;

            // 4. Rename Treatment Identifier header
            if (gridTreatments.Columns.Contains("TreatmentIdentifier"))
            {
                gridTreatments.Columns["TreatmentIdentifier"].HeaderText = "Treatment Identifier";
            }
            // 6. Rename PatientIdentifier header
            if (gridTreatments.Columns.Contains("PatientIdentifier"))
            {
                gridTreatments.Columns["PatientIdentifier"].HeaderText = "Patient Identifier";
            }
            // 7. Rename CompleteDate header
            if (gridTreatments.Columns.Contains("CompleteDate"))
            {
                gridTreatments.Columns["CompleteDate"].HeaderText = "Complete Date";
            }
            // 8. Rename ItemCode header
            if (gridTreatments.Columns.Contains("ItemCode"))
            {
                gridTreatments.Columns["ItemCode"].HeaderText = "Item Code";
            }
        }


        private void Steps(int steps)
        {
            UpdatePictureBox();
            switch (steps)
            {
                case 1:
                    // Step 1 - Load Patients CSV
                    this.stepsTxt.Text =
                        "  \r\n 1. Click the \"Load Patients CSV\" button above to select your patient.csv file.\r\n" +
                        "  \r\n 2. Once selected, the system will display the patient details for review.";
                    break;

                case 2:
                    // Step 2 - Load Treatments CSV
                    this.stepsTxt.Text =
                        "  \r\n 1. Click the \"Load Treatments CSV\" button to select your treatment.csv file.\r\n" +
                        "  \r\n 2. The system will display the treatments linked to patients for review.";
                    break;

                case 3:
                    // Step 3 - Validate Data
                    this.stepsTxt.Text =
                        "  \r\n 1. Click the \"Validate\" button to check all records for missing or incorrect data.\r\n" +
                        "  \r\n 2. Invalid rows will be highlighted with clear error messages.";
                    break;

                case 4:
                    // Step 4 - Ingest Data
                    this.stepsTxt.Text =
                        "  \r\n 1. Click the \"Ingest\" button to save all valid records into the database.\r\n" +
                        "  \r\n 2. A summary will be shown for rows ingested, skipped, or with errors.";
                    break;

                case 5:
                    // Step 5 - Export Invalid
                    this.stepsTxt.Text =
                        "  \r\n 1. Click “Export Invalid” to save all invalid rows in the current grid to a CSV file." +
                        "  \r\n 2. The CSV includes the error details per row. Fix them, then reload the corrected file.";
                    break;
                default:
                    this.stepsTxt.Text = "Please follow the steps in order starting from Step 1.";
                    break;
            }
        }

        private void DisableBtns(int step)
        {
            switch (step)
            {
                case 1:
                    this.btnLoadTreatmentsCsv.Visible = false;
                    this.btnValidate.Visible = false;
                    this.btnExportInvalid.Visible = false;
                    this.btnIngest.Visible = false;
                    this.btnLoadPatientsCsv.Visible = true;
                    break;
                case 2:
                    this.btnLoadPatientsCsv.Visible = false;
                    this.btnExportInvalid.Visible = false;
                    this.btnLoadTreatmentsCsv.Visible = true;
                    break;
                case 3:
                    this.btnLoadPatientsCsv.Visible = false;
                    this.btnLoadTreatmentsCsv.Visible = false;
                    this.btnExportInvalid.Visible = false;
                    this.btnValidate.Visible = true;
                    break;
                case 4:
                    this.btnLoadPatientsCsv.Visible = false;
                    this.btnLoadTreatmentsCsv.Visible = false;
                    this.btnValidate.Visible = false;
                    this.btnExportInvalid.Visible = false;
                    this.btnIngest.Visible = true;
                    break;
                case 5:
                    this.btnLoadPatientsCsv.Visible = false;
                    this.btnLoadTreatmentsCsv.Visible = false;
                    this.btnValidate.Visible = false;
                    this.btnIngest.Visible = false;
                    this.btnExportInvalid.Visible = true;
                    break;
                default:
                    this.btnLoadTreatmentsCsv.Visible = false;
                    this.btnValidate.Visible = false;
                    this.btnExportInvalid.Visible = false;
                    this.btnIngest.Visible = false;
                    this.btnLoadPatientsCsv.Visible = true;
                    break;
            }
        }

        private void UpdatePictureBox()
        {
            if (this.btnLoadPatientsCsv.Visible)
            {
                string imgPath = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\common", "imgs", "loadPatients.png");
                if (File.Exists(imgPath))
                {
                    pBox.Image = Image.FromFile(imgPath);
                }
            }
            else if (this.btnLoadTreatmentsCsv.Visible)
            {
                string imgPath = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\common", "imgs", "LoadTreatment.png");
                if (File.Exists(imgPath))
                {
                    pBox.Image = Image.FromFile(imgPath);
                }
            }
            else if (this.btnValidate.Visible)
            {
                string imgPath = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\common", "imgs", "Validate.png");
                if (File.Exists(imgPath))
                {
                    pBox.Image = Image.FromFile(imgPath);
                }
            }
            else if (this.btnIngest.Visible)
            {
                string imgPath = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\common", "imgs", "Validate.png");
                if (File.Exists(imgPath))
                {
                    pBox.Image = Image.FromFile(imgPath);
                }
            }
            else if (this.btnExportInvalid.Visible)
            {
                string imgPath = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\common", "imgs", "ExportInvalid.png");
                if (File.Exists(imgPath))
                {
                    pBox.Image = Image.FromFile(imgPath);
                }
            }
            else
            {
                pBox.Image = null;
            }
        }

        private void EnsureWaitOverlay()
        {
            if (_waitOverlay != null) return;

            _waitOverlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                Visible = false
            };

            _waitLabel = new Label
            {
                AutoSize = true,
                Text = "Please wait... Ingestion in progress",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            _waitProgress = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Size = new Size(320, 28)
            };

            // simple center layout
            var center = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            center.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            center.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            center.Controls.Add(_waitLabel, 0, 0);
            center.Controls.Add(_waitProgress, 0, 1);
            center.SetCellPosition(_waitLabel, new TableLayoutPanelCellPosition(0, 0));
            center.SetCellPosition(_waitProgress, new TableLayoutPanelCellPosition(0, 1));
            _waitLabel.Anchor = AnchorStyles.None;
            _waitProgress.Anchor = AnchorStyles.None;
            _waitOverlay.Controls.Add(center);

            this.Controls.Add(_waitOverlay);
            _waitOverlay.BringToFront();
        }

        // show/hide helpers
        private async Task ShowBusyAsync(string message)
        {
            EnsureWaitOverlay();
            _waitLabel.Text = message;
            _waitOverlay.Visible = true;
            _waitOverlay.BringToFront();

            // Force a paint cycle before the heavy work
            _waitOverlay.Update();
            await Task.Yield();            // let the UI message loop run
            await Task.Delay(50);          // tiny delay to ensure marquee starts
        }


        private void HideBusy()
        {
            _waitOverlay.Visible = false;
        }


        // full reset to initial state
        private void ResetAll()
        {
            try
            {
                // clear in-memory data
                _patients.Clear();
                _treatments.Clear();
                _invalidPatients.Clear();
                _invalidTreatments.Clear();

                // clear grids
                gridPatients.DataSource = _patients;
                gridTreatments.DataSource = _treatments;

                // clear UI state
                chkPatientsInvalidOnly.Checked = false;
                chkTreatmentsInvalidOnly.Checked = false;
                txtLog.Clear();
                SetStatus("Ready");
                pBox.Image = null;

                // hide any wait overlay or cursors
                HideBusy();

                // go back to Step 1 visuals
                Steps(1);
                DisableBtns(1);
                UpdatePictureBox();
            }
            catch
            {
                // swallow reset errors to avoid blocking UX
            }
        }

    }
}
