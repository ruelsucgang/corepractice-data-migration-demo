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
            btnLoadPatientsCsv = new Button();
            btnLoadTreatmentsCsv = new Button();
            btnValidate = new Button();
            btnIngest = new Button();
            btnExportInvalid = new Button();
            gridPatients = new DataGridView();
            gridTreatments = new DataGridView();
            chkPatientsInvalidOnly = new CheckBox();
            chkTreatmentsInvalidOnly = new CheckBox();
            txtLog = new TextBox();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            panel1 = new Panel();
            panel3 = new Panel();
            _waitOverlay = new Panel();
            _waitLabel = new Label();
            _waitProgress = new ProgressBar();
            panel13 = new Panel();
            stepsTxt = new TextBox();
            panel2 = new Panel();
            pBox = new PictureBox();
            panel4 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            label1 = new Label();
            panel8 = new Panel();
            panel10 = new Panel();
            panel12 = new Panel();
            panel11 = new Panel();
            label3 = new Label();
            panel9 = new Panel();
            panel7 = new Panel();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)gridPatients).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridTreatments).BeginInit();
            statusStrip.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            _waitOverlay.SuspendLayout();
            panel13.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pBox).BeginInit();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel8.SuspendLayout();
            panel10.SuspendLayout();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel9.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadPatientsCsv
            // 
            btnLoadPatientsCsv.Dock = DockStyle.Top;
            btnLoadPatientsCsv.FlatStyle = FlatStyle.Flat;
            btnLoadPatientsCsv.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLoadPatientsCsv.ForeColor = Color.FromArgb(192, 192, 255);
            btnLoadPatientsCsv.Location = new Point(0, 264);
            btnLoadPatientsCsv.Margin = new Padding(3, 4, 3, 4);
            btnLoadPatientsCsv.Name = "btnLoadPatientsCsv";
            btnLoadPatientsCsv.Size = new Size(308, 66);
            btnLoadPatientsCsv.TabIndex = 0;
            btnLoadPatientsCsv.Text = "Step -1: Load Patients CSV";
            btnLoadPatientsCsv.UseVisualStyleBackColor = true;
            btnLoadPatientsCsv.Click += btnLoadPatientsCsv_Click;
            // 
            // btnLoadTreatmentsCsv
            // 
            btnLoadTreatmentsCsv.Dock = DockStyle.Top;
            btnLoadTreatmentsCsv.FlatStyle = FlatStyle.Flat;
            btnLoadTreatmentsCsv.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLoadTreatmentsCsv.ForeColor = Color.FromArgb(192, 192, 255);
            btnLoadTreatmentsCsv.Location = new Point(0, 0);
            btnLoadTreatmentsCsv.Margin = new Padding(3, 4, 3, 4);
            btnLoadTreatmentsCsv.Name = "btnLoadTreatmentsCsv";
            btnLoadTreatmentsCsv.Size = new Size(308, 66);
            btnLoadTreatmentsCsv.TabIndex = 1;
            btnLoadTreatmentsCsv.Text = "Step - 2: Load Treatments CSV";
            btnLoadTreatmentsCsv.UseVisualStyleBackColor = true;
            btnLoadTreatmentsCsv.Visible = false;
            btnLoadTreatmentsCsv.Click += btnLoadTreatmentsCsv_Click;
            // 
            // btnValidate
            // 
            btnValidate.Dock = DockStyle.Top;
            btnValidate.FlatStyle = FlatStyle.Flat;
            btnValidate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnValidate.ForeColor = Color.FromArgb(192, 192, 255);
            btnValidate.Location = new Point(0, 66);
            btnValidate.Margin = new Padding(3, 4, 3, 4);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(308, 66);
            btnValidate.TabIndex = 2;
            btnValidate.Text = "Step - 3: Validate";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Visible = false;
            btnValidate.Click += btnValidate_Click;
            // 
            // btnIngest
            // 
            btnIngest.Dock = DockStyle.Top;
            btnIngest.FlatStyle = FlatStyle.Flat;
            btnIngest.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnIngest.ForeColor = Color.FromArgb(192, 192, 255);
            btnIngest.Location = new Point(0, 132);
            btnIngest.Margin = new Padding(3, 4, 3, 4);
            btnIngest.Name = "btnIngest";
            btnIngest.Size = new Size(308, 66);
            btnIngest.TabIndex = 3;
            btnIngest.Text = "Step - 5: Ingest";
            btnIngest.UseVisualStyleBackColor = true;
            btnIngest.Visible = false;
            btnIngest.Click += btnIngest_Click;
            // 
            // btnExportInvalid
            // 
            btnExportInvalid.Dock = DockStyle.Top;
            btnExportInvalid.FlatStyle = FlatStyle.Flat;
            btnExportInvalid.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnExportInvalid.ForeColor = Color.FromArgb(192, 192, 255);
            btnExportInvalid.Location = new Point(0, 198);
            btnExportInvalid.Margin = new Padding(3, 4, 3, 4);
            btnExportInvalid.Name = "btnExportInvalid";
            btnExportInvalid.Size = new Size(308, 66);
            btnExportInvalid.TabIndex = 4;
            btnExportInvalid.Text = "Step-4: Export Invalid";
            btnExportInvalid.UseVisualStyleBackColor = true;
            btnExportInvalid.Visible = false;
            btnExportInvalid.Click += btnExportInvalid_Click;
            // 
            // gridPatients
            // 
            gridPatients.AllowUserToAddRows = false;
            gridPatients.AllowUserToDeleteRows = false;
            gridPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPatients.ColumnHeadersHeight = 29;
            gridPatients.Dock = DockStyle.Fill;
            gridPatients.Location = new Point(0, 0);
            gridPatients.Margin = new Padding(3, 4, 3, 4);
            gridPatients.Name = "gridPatients";
            gridPatients.ReadOnly = true;
            gridPatients.RowHeadersVisible = false;
            gridPatients.RowHeadersWidth = 51;
            gridPatients.Size = new Size(1064, 298);
            gridPatients.TabIndex = 7;
            // 
            // gridTreatments
            // 
            gridTreatments.AllowUserToAddRows = false;
            gridTreatments.AllowUserToDeleteRows = false;
            gridTreatments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridTreatments.ColumnHeadersHeight = 29;
            gridTreatments.Dock = DockStyle.Fill;
            gridTreatments.Location = new Point(0, 0);
            gridTreatments.Margin = new Padding(3, 4, 3, 4);
            gridTreatments.Name = "gridTreatments";
            gridTreatments.ReadOnly = true;
            gridTreatments.RowHeadersVisible = false;
            gridTreatments.RowHeadersWidth = 51;
            gridTreatments.Size = new Size(1064, 391);
            gridTreatments.TabIndex = 8;
            // 
            // chkPatientsInvalidOnly
            // 
            chkPatientsInvalidOnly.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPatientsInvalidOnly.AutoSize = true;
            chkPatientsInvalidOnly.ForeColor = Color.White;
            chkPatientsInvalidOnly.Location = new Point(911, 7);
            chkPatientsInvalidOnly.Margin = new Padding(3, 4, 3, 4);
            chkPatientsInvalidOnly.Name = "chkPatientsInvalidOnly";
            chkPatientsInvalidOnly.Size = new Size(147, 24);
            chkPatientsInvalidOnly.TabIndex = 5;
            chkPatientsInvalidOnly.Text = "Show invalid only";
            chkPatientsInvalidOnly.CheckedChanged += chkPatientsInvalidOnly_CheckedChanged;
            // 
            // chkTreatmentsInvalidOnly
            // 
            chkTreatmentsInvalidOnly.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkTreatmentsInvalidOnly.AutoSize = true;
            chkTreatmentsInvalidOnly.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkTreatmentsInvalidOnly.ForeColor = Color.White;
            chkTreatmentsInvalidOnly.ImageAlign = ContentAlignment.TopRight;
            chkTreatmentsInvalidOnly.Location = new Point(905, 7);
            chkTreatmentsInvalidOnly.Margin = new Padding(3, 4, 3, 4);
            chkTreatmentsInvalidOnly.Name = "chkTreatmentsInvalidOnly";
            chkTreatmentsInvalidOnly.Size = new Size(153, 24);
            chkTreatmentsInvalidOnly.TabIndex = 6;
            chkTreatmentsInvalidOnly.Text = "Show invalid only";
            chkTreatmentsInvalidOnly.CheckedChanged += chkTreatmentsInvalidOnly_CheckedChanged;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtLog.ForeColor = Color.Navy;
            txtLog.Location = new Point(0, 0);
            txtLog.Margin = new Padding(3, 4, 3, 4);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1064, 161);
            txtLog.TabIndex = 9;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 993);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1374, 26);
            statusStrip.TabIndex = 10;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(50, 20);
            lblStatus.Text = "Ready";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(310, 993);
            panel1.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel13);
            panel3.Controls.Add(btnLoadPatientsCsv);
            panel3.Controls.Add(btnExportInvalid);
            panel3.Controls.Add(btnIngest);
            panel3.Controls.Add(btnValidate);
            panel3.Controls.Add(btnLoadTreatmentsCsv);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 183);
            panel3.Name = "panel3";
            panel3.Size = new Size(308, 808);
            panel3.TabIndex = 1;
            // 
            // _waitOverlay
            // 
            _waitOverlay.Controls.Add(_waitLabel);
            _waitOverlay.Controls.Add(_waitProgress);
            _waitOverlay.Dock = DockStyle.Top;
            _waitOverlay.Location = new Point(0, 372);
            _waitOverlay.Name = "_waitOverlay";
            _waitOverlay.Size = new Size(308, 116);
            _waitOverlay.TabIndex = 8;
            _waitOverlay.Visible = false;
            // 
            // _waitLabel
            // 
            _waitLabel.AutoSize = true;
            _waitLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _waitLabel.ForeColor = SystemColors.MenuBar;
            _waitLabel.Location = new Point(11, 18);
            _waitLabel.Name = "_waitLabel";
            _waitLabel.Size = new Size(251, 20);
            _waitLabel.TabIndex = 1;
            _waitLabel.Text = "Please wait... Ingestion in progress";
            // 
            // _waitProgress
            // 
            _waitProgress.Location = new Point(11, 52);
            _waitProgress.Name = "_waitProgress";
            _waitProgress.Size = new Size(280, 29);
            _waitProgress.Style = ProgressBarStyle.Marquee;
            _waitProgress.TabIndex = 0;
            // 
            // panel13
            // 
            panel13.Controls.Add(_waitOverlay);
            panel13.Controls.Add(stepsTxt);
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(0, 330);
            panel13.Name = "panel13";
            panel13.Size = new Size(308, 445);
            panel13.TabIndex = 7;
            // 
            // stepsTxt
            // 
            stepsTxt.BackColor = Color.FromArgb(0, 0, 64);
            stepsTxt.BorderStyle = BorderStyle.None;
            stepsTxt.Cursor = Cursors.SizeAll;
            stepsTxt.Dock = DockStyle.Top;
            stepsTxt.Enabled = false;
            stepsTxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            stepsTxt.ForeColor = Color.White;
            stepsTxt.Location = new Point(0, 0);
            stepsTxt.Multiline = true;
            stepsTxt.Name = "stepsTxt";
            stepsTxt.ReadOnly = true;
            stepsTxt.Size = new Size(308, 372);
            stepsTxt.TabIndex = 6;
            stepsTxt.Text = "\\r\\nInstructions";
            // 
            // panel2
            // 
            panel2.Controls.Add(pBox);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(308, 183);
            panel2.TabIndex = 0;
            // 
            // pBox
            // 
            pBox.Location = new Point(67, 23);
            pBox.Name = "pBox";
            pBox.Size = new Size(155, 140);
            pBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pBox.TabIndex = 0;
            pBox.TabStop = false;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(310, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1064, 334);
            panel4.TabIndex = 12;
            // 
            // panel6
            // 
            panel6.Controls.Add(gridPatients);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 36);
            panel6.Name = "panel6";
            panel6.Size = new Size(1064, 298);
            panel6.TabIndex = 8;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(74, 144, 226);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(chkPatientsInvalidOnly);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1064, 36);
            panel5.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 3);
            label1.Name = "label1";
            label1.Size = new Size(138, 28);
            label1.TabIndex = 0;
            label1.Text = "PATIENT LIST";
            // 
            // panel8
            // 
            panel8.Controls.Add(panel10);
            panel8.Controls.Add(panel9);
            panel8.Controls.Add(panel7);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(310, 334);
            panel8.Name = "panel8";
            panel8.Size = new Size(1064, 659);
            panel8.TabIndex = 14;
            // 
            // panel10
            // 
            panel10.Controls.Add(panel12);
            panel10.Controls.Add(panel11);
            panel10.Dock = DockStyle.Top;
            panel10.Location = new Point(0, 431);
            panel10.Name = "panel10";
            panel10.Size = new Size(1064, 201);
            panel10.TabIndex = 12;
            // 
            // panel12
            // 
            panel12.Controls.Add(txtLog);
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(0, 40);
            panel12.Name = "panel12";
            panel12.Size = new Size(1064, 161);
            panel12.TabIndex = 12;
            // 
            // panel11
            // 
            panel11.BackColor = Color.FromArgb(74, 144, 226);
            panel11.Controls.Add(label3);
            panel11.Dock = DockStyle.Top;
            panel11.Location = new Point(0, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(1064, 40);
            panel11.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(18, 4);
            label3.Name = "label3";
            label3.Size = new Size(62, 28);
            label3.TabIndex = 7;
            label3.Text = "LOGS";
            // 
            // panel9
            // 
            panel9.Controls.Add(gridTreatments);
            panel9.Dock = DockStyle.Top;
            panel9.Location = new Point(0, 40);
            panel9.Name = "panel9";
            panel9.Size = new Size(1064, 391);
            panel9.TabIndex = 11;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(74, 144, 226);
            panel7.Controls.Add(label2);
            panel7.Controls.Add(chkTreatmentsInvalidOnly);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(1064, 40);
            panel7.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(18, 4);
            label2.Name = "label2";
            label2.Size = new Size(176, 28);
            label2.TabIndex = 7;
            label2.Text = "TREATMENT LIST";
            // 
            // MigrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1374, 1019);
            Controls.Add(panel8);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Controls.Add(statusStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MigrationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Core Practice CSV Migration";
            Load += MigrationForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridPatients).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridTreatments).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            _waitOverlay.ResumeLayout(false);
            _waitOverlay.PerformLayout();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pBox).EndInit();
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel8.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel9.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private Panel panel5;
        private Label label1;
        private Panel panel6;
        private Panel panel8;
        private Panel panel9;
        private Panel panel7;
        private Label label2;
        private Panel panel10;
        private Panel panel11;
        private Label label3;
        private Panel panel12;
        private PictureBox pBox;
        private Panel panel13;
        private TextBox stepsTxt;
        private Panel _waitOverlay;
        private Label _waitLabel;
        private ProgressBar _waitProgress;
    }
}
