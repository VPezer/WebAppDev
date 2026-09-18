namespace CrewmanDesktopApp.Forms
{
    partial class MainForm
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
            this.panelActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.layoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.textSearch = new DevExpress.XtraEditors.TextEdit();
            this.lookupRank = new DevExpress.XtraEditors.LookUpEdit();
            this.lookupVessel = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClearFilters = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmbarkationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelStatus = new DevExpress.XtraEditors.LabelControl();
            this.timerSearch = new System.Windows.Forms.Timer(this.components);
            this.layoutRoot.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.layoutFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupRank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupVessel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.panelActions, 0, 0);
            this.layoutRoot.Controls.Add(this.layoutFilters, 0, 1);
            this.layoutRoot.Controls.Add(this.gridControl, 0, 2);
            this.layoutRoot.Controls.Add(this.labelStatus, 0, 3);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.Padding = new System.Windows.Forms.Padding(10);
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.Size = new System.Drawing.Size(1000, 600);
            this.layoutRoot.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.AutoSize = true;
            this.panelActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActions.Location = new System.Drawing.Point(10, 10);
            this.panelActions.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(980, 32);
            this.panelActions.TabIndex = 0;
            this.panelActions.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 32);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Dodaj pomorca";
            this.btnAdd.ToolTip = "Dodaj novog pomorca (Ctrl+N)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(148, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 32);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Uredi";
            this.btnEdit.ToolTip = "Uredi označenog pomorca (Enter ili dvoklik na redak)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(266, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 32);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Obriši";
            this.btnDelete.ToolTip = "Obriši označenog pomorca (Delete)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // layoutFilters
            // 
            this.layoutFilters.AutoSize = true;
            this.layoutFilters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutFilters.ColumnCount = 4;
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutFilters.Controls.Add(this.textSearch, 0, 0);
            this.layoutFilters.Controls.Add(this.lookupRank, 1, 0);
            this.layoutFilters.Controls.Add(this.lookupVessel, 2, 0);
            this.layoutFilters.Controls.Add(this.btnClearFilters, 3, 0);
            this.layoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFilters.Location = new System.Drawing.Point(10, 50);
            this.layoutFilters.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.layoutFilters.Name = "layoutFilters";
            this.layoutFilters.RowCount = 1;
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutFilters.Size = new System.Drawing.Size(980, 32);
            this.layoutFilters.TabIndex = 1;
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearch.Location = new System.Drawing.Point(0, 4);
            this.textSearch.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.textSearch.Name = "textSearch";
            this.textSearch.Properties.MaxLength = 100;
            this.textSearch.Properties.NullValuePrompt = "Pretraži po imenu, prezimenu, rangu ili brodu...";
            this.textSearch.Properties.NullValuePromptShowForEmptyValue = true;
            this.textSearch.Size = new System.Drawing.Size(492, 24);
            this.textSearch.TabIndex = 0;
            this.textSearch.EditValueChanged += new System.EventHandler(this.filter_EditValueChanged);
            // 
            // lookupRank
            // 
            this.lookupRank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupRank.Location = new System.Drawing.Point(500, 4);
            this.lookupRank.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lookupRank.Name = "lookupRank";
            this.lookupRank.Properties.NullText = "Svi rangovi";
            this.lookupRank.Size = new System.Drawing.Size(182, 24);
            this.lookupRank.TabIndex = 1;
            this.lookupRank.ToolTip = "Filtriraj po rangu";
            this.lookupRank.EditValueChanged += new System.EventHandler(this.filter_EditValueChanged);
            // 
            // lookupVessel
            // 
            this.lookupVessel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupVessel.Location = new System.Drawing.Point(690, 4);
            this.lookupVessel.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lookupVessel.Name = "lookupVessel";
            this.lookupVessel.Properties.NullText = "Svi brodovi";
            this.lookupVessel.Size = new System.Drawing.Size(182, 24);
            this.lookupVessel.TabIndex = 2;
            this.lookupVessel.ToolTip = "Filtriraj po brodu";
            this.lookupVessel.EditValueChanged += new System.EventHandler(this.filter_EditValueChanged);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClearFilters.Location = new System.Drawing.Point(880, 0);
            this.btnClearFilters.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(100, 32);
            this.btnClearFilters.TabIndex = 3;
            this.btnClearFilters.Text = "Očisti";
            this.btnClearFilters.ToolTip = "Ukloni sve filtre i prikaži sve pomorce (Esc)";
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(10, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(980, 470);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFirstName,
            this.colLastName,
            this.colRankName,
            this.colVesselName,
            this.colEmbarkationDate});
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AutoPopulateColumns = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsView.ColumnAutoWidth = true;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowIndicator = false;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            // 
            // colFirstName
            // 
            this.colFirstName.Caption = "Ime";
            this.colFirstName.FieldName = "FirstName";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.Visible = true;
            this.colFirstName.VisibleIndex = 0;
            // 
            // colLastName
            // 
            this.colLastName.Caption = "Prezime";
            this.colLastName.FieldName = "LastName";
            this.colLastName.Name = "colLastName";
            this.colLastName.Visible = true;
            this.colLastName.VisibleIndex = 1;
            // 
            // colRankName
            // 
            this.colRankName.Caption = "Rang";
            this.colRankName.FieldName = "RankName";
            this.colRankName.Name = "colRankName";
            this.colRankName.Visible = true;
            this.colRankName.VisibleIndex = 2;
            // 
            // colVesselName
            // 
            this.colVesselName.Caption = "Brod";
            this.colVesselName.FieldName = "VesselName";
            this.colVesselName.Name = "colVesselName";
            this.colVesselName.Visible = true;
            this.colVesselName.VisibleIndex = 3;
            // 
            // colEmbarkationDate
            // 
            this.colEmbarkationDate.Caption = "Datum ukrcaja";
            this.colEmbarkationDate.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.colEmbarkationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEmbarkationDate.FieldName = "EmbarkationDate";
            this.colEmbarkationDate.Name = "colEmbarkationDate";
            this.colEmbarkationDate.Visible = true;
            this.colEmbarkationDate.VisibleIndex = 4;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStatus.Location = new System.Drawing.Point(10, 570);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 15);
            this.labelStatus.TabIndex = 3;
            // 
            // timerSearch
            // 
            this.timerSearch.Interval = 300;
            this.timerSearch.Tick += new System.EventHandler(this.timerSearch_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.layoutRoot);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(860, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crewman - Upravljanje pomorcima";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.layoutRoot.ResumeLayout(false);
            this.layoutRoot.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.layoutFilters.ResumeLayout(false);
            this.layoutFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupRank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupVessel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.FlowLayoutPanel panelActions;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.TableLayoutPanel layoutFilters;
        private DevExpress.XtraEditors.TextEdit textSearch;
        private DevExpress.XtraEditors.LookUpEdit lookupRank;
        private DevExpress.XtraEditors.LookUpEdit lookupVessel;
        private DevExpress.XtraEditors.SimpleButton btnClearFilters;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn colLastName;
        private DevExpress.XtraGrid.Columns.GridColumn colRankName;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmbarkationDate;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private System.Windows.Forms.Timer timerSearch;
    }
}
