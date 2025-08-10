namespace Demo.Presentation.WinForms
{
    partial class MigrationForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnLoadPatientsCsv;
        private System.Windows.Forms.Button btnLoadTreatmentsCsv;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnIngest;
        private System.Windows.Forms.Button btnExportInvalid;

        private System.Windows.Forms.DataGridView gridPatients;
        private System.Windows.Forms.DataGridView gridTreatments;

        private System.Windows.Forms.CheckBox chkPatientsInvalidOnly;
        private System.Windows.Forms.CheckBox chkTreatmentsInvalidOnly;

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnLoadPatientsCsv = new System.Windows.Forms.Button();
            this.btnLoadTreatmentsCsv = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnIngest = new System.Windows.Forms.Button();
            this.btnExportInvalid = new System.Windows.Forms.Button();

            this.gridPatients = new System.Windows.Forms.DataGridView();
            this.gridTreatments = new System.Windows.Forms.DataGridView();

            this.chkPatientsInvalidOnly = new System.Windows.Forms.CheckBox();
            this.chkTreatmentsInvalidOnly = new System.Windows.Forms.CheckBox();

            this.txtLog = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)(this.gridPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTreatments)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // btnLoadPatientsCsv
            this.btnLoadPatientsCsv.Location = new System.Drawing.Point(12, 12);
            this.btnLoadPatientsCsv.Name = "btnLoadPatientsCsv";
            this.btnLoadPatientsCsv.Size = new System.Drawing.Size(140, 30);
            this.btnLoadPatientsCsv.Text = "Load Patients CSV";
            this.btnLoadPatientsCsv.UseVisualStyleBackColor = true;
            this.btnLoadPatientsCsv.Click += new System.EventHandler(this.btnLoadPatientsCsv_Click);

            // btnLoadTreatmentsCsv
            this.btnLoadTreatmentsCsv.Location = new System.Drawing.Point(158, 12);
            this.btnLoadTreatmentsCsv.Name = "btnLoadTreatmentsCsv";
            this.btnLoadTreatmentsCsv.Size = new System.Drawing.Size(160, 30);
            this.btnLoadTreatmentsCsv.Text = "Load Treatments CSV";
            this.btnLoadTreatmentsCsv.UseVisualStyleBackColor = true;
            this.btnLoadTreatmentsCsv.Click += new System.EventHandler(this.btnLoadTreatmentsCsv_Click);

            // btnValidate
            this.btnValidate.Location = new System.Drawing.Point(324, 12);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(90, 30);
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);

            // btnIngest
            this.btnIngest.Location = new System.Drawing.Point(420, 12);
            this.btnIngest.Name = "btnIngest";
            this.btnIngest.Size = new System.Drawing.Size(90, 30);
            this.btnIngest.Text = "Ingest";
            this.btnIngest.UseVisualStyleBackColor = true;
            this.btnIngest.Click += new System.EventHandler(this.btnIngest_Click);

            // btnExportInvalid
            this.btnExportInvalid.Location = new System.Drawing.Point(516, 12);
            this.btnExportInvalid.Name = "btnExportInvalid";
            this.btnExportInvalid.Size = new System.Drawing.Size(110, 30);
            this.btnExportInvalid.Text = "Export Invalid";
            this.btnExportInvalid.UseVisualStyleBackColor = true;
            this.btnExportInvalid.Click += new System.EventHandler(this.btnExportInvalid_Click);

            // gridPatients
            this.gridPatients.AllowUserToAddRows = false;
            this.gridPatients.AllowUserToDeleteRows = false;
            this.gridPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPatients.Location = new System.Drawing.Point(12, 60);
            this.gridPatients.Name = "gridPatients";
            this.gridPatients.ReadOnly = true;
            this.gridPatients.RowHeadersVisible = false;
            this.gridPatients.Size = new System.Drawing.Size(760, 220);

            // gridTreatments
            this.gridTreatments.AllowUserToAddRows = false;
            this.gridTreatments.AllowUserToDeleteRows = false;
            this.gridTreatments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTreatments.Location = new System.Drawing.Point(12, 320);
            this.gridTreatments.Name = "gridTreatments";
            this.gridTreatments.ReadOnly = true;
            this.gridTreatments.RowHeadersVisible = false;
            this.gridTreatments.Size = new System.Drawing.Size(760, 220);

            // chkPatientsInvalidOnly
            this.chkPatientsInvalidOnly.AutoSize = true;
            this.chkPatientsInvalidOnly.Location = new System.Drawing.Point(630, 36);
            this.chkPatientsInvalidOnly.Name = "chkPatientsInvalidOnly";
            this.chkPatientsInvalidOnly.Size = new System.Drawing.Size(130, 19);
            this.chkPatientsInvalidOnly.Text = "Show invalid only";
            this.chkPatientsInvalidOnly.CheckedChanged += new System.EventHandler(this.chkPatientsInvalidOnly_CheckedChanged);

            // chkTreatmentsInvalidOnly
            this.chkTreatmentsInvalidOnly.AutoSize = true;
            this.chkTreatmentsInvalidOnly.Location = new System.Drawing.Point(630, 296);
            this.chkTreatmentsInvalidOnly.Name = "chkTreatmentsInvalidOnly";
            this.chkTreatmentsInvalidOnly.Size = new System.Drawing.Size(130, 19);
            this.chkTreatmentsInvalidOnly.Text = "Show invalid only";
            this.chkTreatmentsInvalidOnly.CheckedChanged += new System.EventHandler(this.chkTreatmentsInvalidOnly_CheckedChanged);

            // txtLog
            this.txtLog.Location = new System.Drawing.Point(12, 560);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 90);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
            this.statusStrip.Location = new System.Drawing.Point(0, 660);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);

            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 17);
            this.lblStatus.Text = "Ready";

            // MigrationForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 682);
            this.Controls.Add(this.btnLoadPatientsCsv);
            this.Controls.Add(this.btnLoadTreatmentsCsv);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnIngest);
            this.Controls.Add(this.btnExportInvalid);
            this.Controls.Add(this.chkPatientsInvalidOnly);
            this.Controls.Add(this.chkTreatmentsInvalidOnly);
            this.Controls.Add(this.gridPatients);
            this.Controls.Add(this.gridTreatments);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.statusStrip);
            this.Name = "MigrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Core Practice CSV Migration";
            ((System.ComponentModel.ISupportInitialize)(this.gridPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTreatments)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
