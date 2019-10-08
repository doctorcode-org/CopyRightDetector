namespace CopyRightDetector
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.progWorker = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wbBrowser = new System.Windows.Forms.WebBrowser();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grbForms = new System.Windows.Forms.GroupBox();
            this.chbStopByFirstDetect = new System.Windows.Forms.CheckBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.lblWordToValidate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblFileSelect = new System.Windows.Forms.Label();
            this.btnSelectDocument = new System.Windows.Forms.Button();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnSaveInDb = new System.Windows.Forms.Button();
            this.btnSaveAsXps = new System.Windows.Forms.Button();
            this.btnSaveAsHtml = new System.Windows.Forms.Button();
            this.btnSaveAsPdf = new System.Windows.Forms.Button();
            this.btnSaveAsXml = new System.Windows.Forms.Button();
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.lblPages = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalPages = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblParagraphs = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalParagraphs = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWords = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalWords = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCharacters = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalCharacters = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtWordToValidate = new CopyRightDetector.Controls.NumberTextBox();
            this.txtParagraphToValidate = new CopyRightDetector.Controls.NumberTextBox();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grbForms.SuspendLayout();
            this.grbButtons.SuspendLayout();
            this.MainStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.progWorker);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.flowLayoutPanel1);
            this.MainPanel.Controls.Add(this.MainStatus);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MainPanel.Size = new System.Drawing.Size(784, 365);
            this.MainPanel.TabIndex = 0;
            // 
            // progWorker
            // 
            this.progWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progWorker.Location = new System.Drawing.Point(7, 339);
            this.progWorker.Name = "progWorker";
            this.progWorker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progWorker.Size = new System.Drawing.Size(180, 18);
            this.progWorker.TabIndex = 7;
            this.progWorker.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wbBrowser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 187);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(784, 143);
            this.panel1.TabIndex = 6;
            // 
            // wbBrowser
            // 
            this.wbBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBrowser.IsWebBrowserContextMenuEnabled = false;
            this.wbBrowser.Location = new System.Drawing.Point(10, 10);
            this.wbBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBrowser.Name = "wbBrowser";
            this.wbBrowser.Size = new System.Drawing.Size(764, 123);
            this.wbBrowser.TabIndex = 7;
            this.wbBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbBrowser_Navigating);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.grbForms);
            this.flowLayoutPanel1.Controls.Add(this.grbButtons);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(784, 187);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // grbForms
            // 
            this.grbForms.Controls.Add(this.chbStopByFirstDetect);
            this.grbForms.Controls.Add(this.btnValidate);
            this.grbForms.Controls.Add(this.txtWordToValidate);
            this.grbForms.Controls.Add(this.lblWordToValidate);
            this.grbForms.Controls.Add(this.txtParagraphToValidate);
            this.grbForms.Controls.Add(this.label1);
            this.grbForms.Controls.Add(this.lblFilePath);
            this.grbForms.Controls.Add(this.lblFileSelect);
            this.grbForms.Controls.Add(this.btnSelectDocument);
            this.grbForms.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbForms.Location = new System.Drawing.Point(28, 3);
            this.grbForms.Name = "grbForms";
            this.grbForms.Size = new System.Drawing.Size(743, 100);
            this.grbForms.TabIndex = 6;
            this.grbForms.TabStop = false;
            // 
            // chbStopByFirstDetect
            // 
            this.chbStopByFirstDetect.AutoSize = true;
            this.chbStopByFirstDetect.Location = new System.Drawing.Point(129, 62);
            this.chbStopByFirstDetect.Name = "chbStopByFirstDetect";
            this.chbStopByFirstDetect.Size = new System.Drawing.Size(232, 20);
            this.chbStopByFirstDetect.TabIndex = 9;
            this.chbStopByFirstDetect.Text = "توقف جستجو با اولین سند یافت شده";
            this.chbStopByFirstDetect.UseVisualStyleBackColor = true;
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.Enabled = false;
            this.btnValidate.Location = new System.Drawing.Point(18, 57);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(100, 30);
            this.btnValidate.TabIndex = 8;
            this.btnValidate.Text = "بررسی سند";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // lblWordToValidate
            // 
            this.lblWordToValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWordToValidate.AutoSize = true;
            this.lblWordToValidate.Location = new System.Drawing.Point(467, 64);
            this.lblWordToValidate.Name = "lblWordToValidate";
            this.lblWordToValidate.Size = new System.Drawing.Size(77, 16);
            this.lblWordToValidate.TabIndex = 5;
            this.lblWordToValidate.Text = "تعداد کلمات:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(642, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "تعداد پاراگراف:";
            // 
            // lblFilePath
            // 
            this.lblFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilePath.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePath.Location = new System.Drawing.Point(55, 22);
            this.lblFilePath.MaximumSize = new System.Drawing.Size(600, 27);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(584, 27);
            this.lblFilePath.TabIndex = 2;
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileSelect
            // 
            this.lblFileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileSelect.AutoSize = true;
            this.lblFileSelect.Location = new System.Drawing.Point(642, 27);
            this.lblFileSelect.Name = "lblFileSelect";
            this.lblFileSelect.Size = new System.Drawing.Size(78, 16);
            this.lblFileSelect.TabIndex = 1;
            this.lblFileSelect.Text = "انتخاب سند:";
            // 
            // btnSelectDocument
            // 
            this.btnSelectDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDocument.Location = new System.Drawing.Point(18, 22);
            this.btnSelectDocument.Name = "btnSelectDocument";
            this.btnSelectDocument.Size = new System.Drawing.Size(35, 27);
            this.btnSelectDocument.TabIndex = 0;
            this.btnSelectDocument.Text = "...";
            this.btnSelectDocument.UseVisualStyleBackColor = true;
            this.btnSelectDocument.Click += new System.EventHandler(this.btnSelectDocument_Click);
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnSaveInDb);
            this.grbButtons.Controls.Add(this.btnSaveAsXps);
            this.grbButtons.Controls.Add(this.btnSaveAsHtml);
            this.grbButtons.Controls.Add(this.btnSaveAsPdf);
            this.grbButtons.Controls.Add(this.btnSaveAsXml);
            this.grbButtons.Location = new System.Drawing.Point(189, 109);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(582, 65);
            this.grbButtons.TabIndex = 7;
            this.grbButtons.TabStop = false;
            // 
            // btnSaveInDb
            // 
            this.btnSaveInDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveInDb.Enabled = false;
            this.btnSaveInDb.Location = new System.Drawing.Point(8, 22);
            this.btnSaveInDb.Name = "btnSaveInDb";
            this.btnSaveInDb.Size = new System.Drawing.Size(145, 30);
            this.btnSaveInDb.TabIndex = 11;
            this.btnSaveInDb.Text = "ذخیره در پایگاه داده";
            this.btnSaveInDb.UseVisualStyleBackColor = true;
            this.btnSaveInDb.Click += new System.EventHandler(this.btnSaveInDb_Click);
            // 
            // btnSaveAsXps
            // 
            this.btnSaveAsXps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsXps.Enabled = false;
            this.btnSaveAsXps.Location = new System.Drawing.Point(158, 22);
            this.btnSaveAsXps.Name = "btnSaveAsXps";
            this.btnSaveAsXps.Size = new System.Drawing.Size(100, 30);
            this.btnSaveAsXps.TabIndex = 10;
            this.btnSaveAsXps.Text = "خروجی Xps";
            this.btnSaveAsXps.UseVisualStyleBackColor = true;
            this.btnSaveAsXps.Click += new System.EventHandler(this.btnSaveAsXps_Click);
            // 
            // btnSaveAsHtml
            // 
            this.btnSaveAsHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsHtml.Enabled = false;
            this.btnSaveAsHtml.Location = new System.Drawing.Point(264, 22);
            this.btnSaveAsHtml.Name = "btnSaveAsHtml";
            this.btnSaveAsHtml.Size = new System.Drawing.Size(100, 30);
            this.btnSaveAsHtml.TabIndex = 9;
            this.btnSaveAsHtml.Text = "خروجی Html";
            this.btnSaveAsHtml.UseVisualStyleBackColor = true;
            this.btnSaveAsHtml.Click += new System.EventHandler(this.btnSaveAsHtml_Click);
            // 
            // btnSaveAsPdf
            // 
            this.btnSaveAsPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsPdf.Enabled = false;
            this.btnSaveAsPdf.Location = new System.Drawing.Point(370, 22);
            this.btnSaveAsPdf.Name = "btnSaveAsPdf";
            this.btnSaveAsPdf.Size = new System.Drawing.Size(100, 30);
            this.btnSaveAsPdf.TabIndex = 8;
            this.btnSaveAsPdf.Text = "خروجی Pdf";
            this.btnSaveAsPdf.UseVisualStyleBackColor = true;
            this.btnSaveAsPdf.Click += new System.EventHandler(this.btnSaveAsPdf_Click);
            // 
            // btnSaveAsXml
            // 
            this.btnSaveAsXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsXml.Enabled = false;
            this.btnSaveAsXml.Location = new System.Drawing.Point(476, 22);
            this.btnSaveAsXml.Name = "btnSaveAsXml";
            this.btnSaveAsXml.Size = new System.Drawing.Size(100, 30);
            this.btnSaveAsXml.TabIndex = 7;
            this.btnSaveAsXml.Text = "خروجی Xml";
            this.btnSaveAsXml.UseVisualStyleBackColor = true;
            this.btnSaveAsXml.Click += new System.EventHandler(this.btnSaveAsXml_Click);
            // 
            // MainStatus
            // 
            this.MainStatus.AutoSize = false;
            this.MainStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPages,
            this.lblTotalPages,
            this.lblParagraphs,
            this.lblTotalParagraphs,
            this.lblWords,
            this.lblTotalWords,
            this.lblCharacters,
            this.lblTotalCharacters});
            this.MainStatus.Location = new System.Drawing.Point(0, 330);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(784, 35);
            this.MainStatus.SizingGrip = false;
            this.MainStatus.TabIndex = 4;
            this.MainStatus.Text = "statusStrip1";
            // 
            // lblPages
            // 
            this.lblPages.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblPages.Name = "lblPages";
            this.lblPages.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblPages.Size = new System.Drawing.Size(62, 25);
            this.lblPages.Text = "صفحات: ";
            this.lblPages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTotalPages.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(18, 25);
            this.lblTotalPages.Text = "0";
            this.lblTotalPages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblParagraphs
            // 
            this.lblParagraphs.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblParagraphs.Name = "lblParagraphs";
            this.lblParagraphs.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblParagraphs.Size = new System.Drawing.Size(71, 25);
            this.lblParagraphs.Text = "پاراگراف‌ها:";
            this.lblParagraphs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalParagraphs
            // 
            this.lblTotalParagraphs.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTotalParagraphs.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblTotalParagraphs.Name = "lblTotalParagraphs";
            this.lblTotalParagraphs.Size = new System.Drawing.Size(18, 25);
            this.lblTotalParagraphs.Text = "0";
            this.lblTotalParagraphs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWords
            // 
            this.lblWords.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblWords.Name = "lblWords";
            this.lblWords.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblWords.Size = new System.Drawing.Size(52, 25);
            this.lblWords.Text = "کلمات:";
            this.lblWords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalWords
            // 
            this.lblTotalWords.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTotalWords.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblTotalWords.Name = "lblTotalWords";
            this.lblTotalWords.Size = new System.Drawing.Size(18, 25);
            this.lblTotalWords.Text = "0";
            this.lblTotalWords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharacters
            // 
            this.lblCharacters.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblCharacters.Size = new System.Drawing.Size(49, 25);
            this.lblCharacters.Text = "حروف:";
            this.lblCharacters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCharacters
            // 
            this.lblTotalCharacters.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTotalCharacters.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lblTotalCharacters.Name = "lblTotalCharacters";
            this.lblTotalCharacters.Size = new System.Drawing.Size(18, 25);
            this.lblTotalCharacters.Text = "0";
            this.lblTotalCharacters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "Word Document |*.docx;*.doc";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 10;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // txtWordToValidate
            // 
            this.txtWordToValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWordToValidate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWordToValidate.Location = new System.Drawing.Point(376, 61);
            this.txtWordToValidate.Name = "txtWordToValidate";
            this.txtWordToValidate.Size = new System.Drawing.Size(90, 23);
            this.txtWordToValidate.TabIndex = 6;
            this.txtWordToValidate.Text = "5";
            this.toolTip.SetToolTip(this.txtWordToValidate, "حداقل تعداد کلماتی که جمله باید داشته باشد تا بررسی شود");
            // 
            // txtParagraphToValidate
            // 
            this.txtParagraphToValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParagraphToValidate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParagraphToValidate.Location = new System.Drawing.Point(550, 61);
            this.txtParagraphToValidate.Name = "txtParagraphToValidate";
            this.txtParagraphToValidate.Size = new System.Drawing.Size(90, 23);
            this.txtParagraphToValidate.TabIndex = 4;
            this.txtParagraphToValidate.Text = "5";
            this.toolTip.SetToolTip(this.txtParagraphToValidate, "حداکثر تعداد پاراگرافهایی که باید پردازش شوند");
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 365);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "frmMain";
            this.Text = "کپی‌یاب";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.grbForms.ResumeLayout(false);
            this.grbForms.PerformLayout();
            this.grbButtons.ResumeLayout(false);
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblPages;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalPages;
        private System.Windows.Forms.ToolStripStatusLabel lblWords;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalWords;
        private System.Windows.Forms.ToolStripStatusLabel lblCharacters;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalCharacters;
        private System.Windows.Forms.ToolStripStatusLabel lblParagraphs;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalParagraphs;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Button btnSaveAsXml;
        private System.Windows.Forms.Button btnSaveAsPdf;
        private System.Windows.Forms.Button btnSaveAsHtml;
        private System.Windows.Forms.Button btnSaveAsXps;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox grbForms;
        private CopyRightDetector.Controls.NumberTextBox txtWordToValidate;
        private System.Windows.Forms.Label lblWordToValidate;
        private CopyRightDetector.Controls.NumberTextBox txtParagraphToValidate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblFileSelect;
        private System.Windows.Forms.Button btnSelectDocument;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser wbBrowser;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.ProgressBar progWorker;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chbStopByFirstDetect;
        private System.Windows.Forms.Button btnSaveInDb;
    }
}

