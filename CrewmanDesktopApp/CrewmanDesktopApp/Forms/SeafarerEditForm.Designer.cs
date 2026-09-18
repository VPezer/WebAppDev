namespace CrewmanDesktopApp.Forms
{
    partial class SeafarerEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.layoutFields = new System.Windows.Forms.TableLayoutPanel();
            this.labelFirstName = new DevExpress.XtraEditors.LabelControl();
            this.textFirstName = new DevExpress.XtraEditors.TextEdit();
            this.labelLastName = new DevExpress.XtraEditors.LabelControl();
            this.textLastName = new DevExpress.XtraEditors.TextEdit();
            this.labelDateOfBirth = new DevExpress.XtraEditors.LabelControl();
            this.dateOfBirth = new DevExpress.XtraEditors.DateEdit();
            this.labelNationality = new DevExpress.XtraEditors.LabelControl();
            this.comboNationality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelEmail = new DevExpress.XtraEditors.LabelControl();
            this.textEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelRank = new DevExpress.XtraEditors.LabelControl();
            this.lookupRank = new DevExpress.XtraEditors.LookUpEdit();
            this.labelVessel = new DevExpress.XtraEditors.LabelControl();
            this.lookupVessel = new DevExpress.XtraEditors.LookUpEdit();
            this.labelEmbarkation = new DevExpress.XtraEditors.LabelControl();
            this.dateEmbarkation = new DevExpress.XtraEditors.DateEdit();
            this.labelMessage = new DevExpress.XtraEditors.LabelControl();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.layoutRoot.SuspendLayout();
            this.layoutFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOfBirth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOfBirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboNationality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupRank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupVessel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEmbarkation.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEmbarkation.Properties)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.AutoSize = true;
            this.layoutRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.layoutFields, 0, 0);
            this.layoutRoot.Controls.Add(this.labelMessage, 0, 1);
            this.layoutRoot.Controls.Add(this.panelButtons, 0, 2);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.Padding = new System.Windows.Forms.Padding(12);
            this.layoutRoot.RowCount = 3;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.Size = new System.Drawing.Size(484, 361);
            this.layoutRoot.TabIndex = 0;
            // 
            // layoutFields
            // 
            this.layoutFields.AutoSize = true;
            this.layoutFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutFields.ColumnCount = 2;
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFields.Controls.Add(this.labelFirstName, 0, 0);
            this.layoutFields.Controls.Add(this.textFirstName, 1, 0);
            this.layoutFields.Controls.Add(this.labelLastName, 0, 1);
            this.layoutFields.Controls.Add(this.textLastName, 1, 1);
            this.layoutFields.Controls.Add(this.labelDateOfBirth, 0, 2);
            this.layoutFields.Controls.Add(this.dateOfBirth, 1, 2);
            this.layoutFields.Controls.Add(this.labelNationality, 0, 3);
            this.layoutFields.Controls.Add(this.comboNationality, 1, 3);
            this.layoutFields.Controls.Add(this.labelEmail, 0, 4);
            this.layoutFields.Controls.Add(this.textEmail, 1, 4);
            this.layoutFields.Controls.Add(this.labelRank, 0, 5);
            this.layoutFields.Controls.Add(this.lookupRank, 1, 5);
            this.layoutFields.Controls.Add(this.labelVessel, 0, 6);
            this.layoutFields.Controls.Add(this.lookupVessel, 1, 6);
            this.layoutFields.Controls.Add(this.labelEmbarkation, 0, 7);
            this.layoutFields.Controls.Add(this.dateEmbarkation, 1, 7);
            this.layoutFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFields.Location = new System.Drawing.Point(12, 12);
            this.layoutFields.Margin = new System.Windows.Forms.Padding(0);
            this.layoutFields.Name = "layoutFields";
            this.layoutFields.RowCount = 8;
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFields.Size = new System.Drawing.Size(460, 256);
            this.layoutFields.TabIndex = 0;
            // 
            // labelFirstName
            // 
            this.labelFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFirstName.Location = new System.Drawing.Point(3, 8);
            this.labelFirstName.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(27, 15);
            this.labelFirstName.TabIndex = 0;
            this.labelFirstName.Text = "Ime *";
            // 
            // textFirstName
            // 
            this.textFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textFirstName.Location = new System.Drawing.Point(120, 4);
            this.textFirstName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textFirstName.Name = "textFirstName";
            this.textFirstName.Properties.MaxLength = 100;
            this.textFirstName.Size = new System.Drawing.Size(337, 24);
            this.textFirstName.TabIndex = 1;
            // 
            // labelLastName
            // 
            this.labelLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLastName.Location = new System.Drawing.Point(3, 40);
            this.labelLastName.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(48, 15);
            this.labelLastName.TabIndex = 2;
            this.labelLastName.Text = "Prezime *";
            // 
            // textLastName
            // 
            this.textLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textLastName.Location = new System.Drawing.Point(120, 36);
            this.textLastName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textLastName.Name = "textLastName";
            this.textLastName.Properties.MaxLength = 100;
            this.textLastName.Size = new System.Drawing.Size(337, 24);
            this.textLastName.TabIndex = 3;
            // 
            // labelDateOfBirth
            // 
            this.labelDateOfBirth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDateOfBirth.Location = new System.Drawing.Point(3, 72);
            this.labelDateOfBirth.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelDateOfBirth.Name = "labelDateOfBirth";
            this.labelDateOfBirth.Size = new System.Drawing.Size(80, 15);
            this.labelDateOfBirth.TabIndex = 4;
            this.labelDateOfBirth.Text = "Datum rođenja *";
            // 
            // dateOfBirth
            // 
            this.dateOfBirth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateOfBirth.EditValue = null;
            this.dateOfBirth.Location = new System.Drawing.Point(120, 68);
            this.dateOfBirth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateOfBirth.Name = "dateOfBirth";
            this.dateOfBirth.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dateOfBirth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateOfBirth.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            this.dateOfBirth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateOfBirth.Properties.Mask.EditMask = "dd.MM.yyyy";
            this.dateOfBirth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateOfBirth.Properties.NullText = "";
            this.dateOfBirth.Size = new System.Drawing.Size(337, 24);
            this.dateOfBirth.TabIndex = 5;
            // 
            // labelNationality
            // 
            this.labelNationality.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelNationality.Location = new System.Drawing.Point(3, 104);
            this.labelNationality.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelNationality.Name = "labelNationality";
            this.labelNationality.Size = new System.Drawing.Size(72, 15);
            this.labelNationality.TabIndex = 6;
            this.labelNationality.Text = "Nacionalnost *";
            // 
            // comboNationality
            // 
            this.comboNationality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNationality.Location = new System.Drawing.Point(120, 100);
            this.comboNationality.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboNationality.Name = "comboNationality";
            this.comboNationality.Properties.DropDownRows = 12;
            this.comboNationality.Properties.MaxLength = 100;
            this.comboNationality.Size = new System.Drawing.Size(337, 24);
            this.comboNationality.TabIndex = 7;
            this.comboNationality.ToolTip = "Odaberite iz popisa ili upišite vlastitu vrijednost";
            // 
            // labelEmail
            // 
            this.labelEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelEmail.Location = new System.Drawing.Point(3, 136);
            this.labelEmail.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(33, 15);
            this.labelEmail.TabIndex = 8;
            this.labelEmail.Text = "E-mail";
            // 
            // textEmail
            // 
            this.textEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textEmail.Location = new System.Drawing.Point(120, 132);
            this.textEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEmail.Name = "textEmail";
            this.textEmail.Properties.MaxLength = 255;
            this.textEmail.Size = new System.Drawing.Size(337, 24);
            this.textEmail.TabIndex = 9;
            // 
            // labelRank
            // 
            this.labelRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRank.Location = new System.Drawing.Point(3, 168);
            this.labelRank.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(34, 15);
            this.labelRank.TabIndex = 10;
            this.labelRank.Text = "Rang *";
            // 
            // lookupRank
            // 
            this.lookupRank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupRank.Location = new System.Drawing.Point(120, 164);
            this.lookupRank.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookupRank.Name = "lookupRank";
            this.lookupRank.Properties.NullText = "(odaberite rang)";
            this.lookupRank.Size = new System.Drawing.Size(337, 24);
            this.lookupRank.TabIndex = 11;
            this.lookupRank.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookupRank_ButtonClick);
            // 
            // labelVessel
            // 
            this.labelVessel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelVessel.Location = new System.Drawing.Point(3, 200);
            this.labelVessel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelVessel.Name = "labelVessel";
            this.labelVessel.Size = new System.Drawing.Size(25, 15);
            this.labelVessel.TabIndex = 12;
            this.labelVessel.Text = "Brod";
            // 
            // lookupVessel
            // 
            this.lookupVessel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupVessel.Location = new System.Drawing.Point(120, 196);
            this.lookupVessel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookupVessel.Name = "lookupVessel";
            this.lookupVessel.Properties.NullText = "(nije ukrcan)";
            this.lookupVessel.Size = new System.Drawing.Size(337, 24);
            this.lookupVessel.TabIndex = 13;
            this.lookupVessel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookupVessel_ButtonClick);
            this.lookupVessel.EditValueChanged += new System.EventHandler(this.lookupVessel_EditValueChanged);
            // 
            // labelEmbarkation
            // 
            this.labelEmbarkation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelEmbarkation.Location = new System.Drawing.Point(3, 232);
            this.labelEmbarkation.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.labelEmbarkation.Name = "labelEmbarkation";
            this.labelEmbarkation.Size = new System.Drawing.Size(74, 15);
            this.labelEmbarkation.TabIndex = 14;
            this.labelEmbarkation.Text = "Datum ukrcaja";
            // 
            // dateEmbarkation
            // 
            this.dateEmbarkation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEmbarkation.EditValue = null;
            this.dateEmbarkation.Location = new System.Drawing.Point(120, 228);
            this.dateEmbarkation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEmbarkation.Name = "dateEmbarkation";
            this.dateEmbarkation.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dateEmbarkation.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEmbarkation.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            this.dateEmbarkation.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEmbarkation.Properties.Mask.EditMask = "dd.MM.yyyy";
            this.dateEmbarkation.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEmbarkation.Properties.NullText = "";
            this.dateEmbarkation.Size = new System.Drawing.Size(337, 24);
            this.dateEmbarkation.TabIndex = 15;
            this.dateEmbarkation.ToolTip = "Dostupno kada je odabran brod";
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelMessage.Location = new System.Drawing.Point(12, 276);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(460, 15);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "* obavezna polja";
            // 
            // panelButtons
            // 
            this.panelButtons.AutoSize = true;
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtons.Location = new System.Drawing.Point(12, 299);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(460, 32);
            this.panelButtons.TabIndex = 2;
            this.panelButtons.WrapContents = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Odustani";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Spremi";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SeafarerEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.layoutRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeafarerEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pomorac";
            this.layoutRoot.ResumeLayout(false);
            this.layoutRoot.PerformLayout();
            this.layoutFields.ResumeLayout(false);
            this.layoutFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOfBirth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOfBirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboNationality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupRank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupVessel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEmbarkation.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEmbarkation.Properties)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.TableLayoutPanel layoutFields;
        private DevExpress.XtraEditors.LabelControl labelFirstName;
        private DevExpress.XtraEditors.TextEdit textFirstName;
        private DevExpress.XtraEditors.LabelControl labelLastName;
        private DevExpress.XtraEditors.TextEdit textLastName;
        private DevExpress.XtraEditors.LabelControl labelDateOfBirth;
        private DevExpress.XtraEditors.DateEdit dateOfBirth;
        private DevExpress.XtraEditors.LabelControl labelNationality;
        private DevExpress.XtraEditors.ComboBoxEdit comboNationality;
        private DevExpress.XtraEditors.LabelControl labelEmail;
        private DevExpress.XtraEditors.TextEdit textEmail;
        private DevExpress.XtraEditors.LabelControl labelRank;
        private DevExpress.XtraEditors.LookUpEdit lookupRank;
        private DevExpress.XtraEditors.LabelControl labelVessel;
        private DevExpress.XtraEditors.LookUpEdit lookupVessel;
        private DevExpress.XtraEditors.LabelControl labelEmbarkation;
        private DevExpress.XtraEditors.DateEdit dateEmbarkation;
        private DevExpress.XtraEditors.LabelControl labelMessage;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
    }
}
