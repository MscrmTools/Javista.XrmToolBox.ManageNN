    using System.Windows.Forms;
    using Microsoft.Xrm.Sdk;

namespace Javista.XrmToolBox.ManageNN
{
    public partial class MainControl
    {
        private ComboBox cbbFirstEntity;
        private ComboBox cbbRelationship;
        private ComboBox cbbSecondEntity;
        private ToolStrip tsMain;
        private ToolStripButton tsbLoadMetadata;
        private GroupBox gbFirst;
        private ComboBox cbbFirstEntityAttribute;
        private RadioButton rdbFirstAttribute;
        private RadioButton rdbFirstGuid;
        private System.Windows.Forms.Label label2;
        private GroupBox gbRelationship;
        private GroupBox gbSecond;
        private ComboBox cbbSecondEntityAttribute;
        private RadioButton rdbSecondAttribute;
        private RadioButton rdbSecondGuid;
        private System.Windows.Forms.Label label1;
        private GroupBox gbImportFile;
        private Button btnBrowse;
        private TextBox txtFilePath;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbImportNN;
        private GroupBox gbLog;

        /// <summary> 
        /// Variable n�cessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilis�es.
        /// </summary>
        /// <param name="disposing">true si les ressources manag�es doivent �tre supprim�es�; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.cbbFirstEntity = new System.Windows.Forms.ComboBox();
            this.cbbRelationship = new System.Windows.Forms.ComboBox();
            this.cbbSecondEntity = new System.Windows.Forms.ComboBox();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbLoadMetadata = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportNN = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbSeparator = new System.Windows.Forms.ToolStripDropDownButton();
            this.commaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semicolonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDebug = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tslLogs = new System.Windows.Forms.ToolStripLabel();
            this.tsbExportLogs = new System.Windows.Forms.ToolStripButton();
            this.tsbClearLogs = new System.Windows.Forms.ToolStripButton();
            this.gbFirst = new System.Windows.Forms.GroupBox();
            this.cbbFirstEntityAttribute = new System.Windows.Forms.ComboBox();
            this.rdbFirstAttribute = new System.Windows.Forms.RadioButton();
            this.rdbFirstGuid = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.gbSecond = new System.Windows.Forms.GroupBox();
            this.cbbSecondEntityAttribute = new System.Windows.Forms.ComboBox();
            this.rdbSecondAttribute = new System.Windows.Forms.RadioButton();
            this.rdbSecondGuid = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gbImportFile = new System.Windows.Forms.GroupBox();
            this.nudBatchCount = new System.Windows.Forms.NumericUpDown();
            this.lblBatchCount = new System.Windows.Forms.Label();
            this.chkImportInBatch = new System.Windows.Forms.CheckBox();
            this.chkCacheSecondEntity = new System.Windows.Forms.CheckBox();
            this.chkCacheFirstEntity = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.chLogStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogLineNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogFirstValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogSecondValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogStatusText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusImageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlFilterLogs = new System.Windows.Forms.Panel();
            this.chkHideAlreadyExists = new System.Windows.Forms.CheckBox();
            this.chkShowErrorsOnly = new System.Windows.Forms.CheckBox();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsMain.SuspendLayout();
            this.gbFirst.SuspendLayout();
            this.gbRelationship.SuspendLayout();
            this.gbSecond.SuspendLayout();
            this.gbImportFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchCount)).BeginInit();
            this.gbLog.SuspendLayout();
            this.pnlFilterLogs.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbFirstEntity
            // 
            this.cbbFirstEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbFirstEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFirstEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbFirstEntity.FormattingEnabled = true;
            this.cbbFirstEntity.Location = new System.Drawing.Point(3, 18);
            this.cbbFirstEntity.Name = "cbbFirstEntity";
            this.cbbFirstEntity.Size = new System.Drawing.Size(344, 26);
            this.cbbFirstEntity.Sorted = true;
            this.cbbFirstEntity.TabIndex = 0;
            this.cbbFirstEntity.SelectedIndexChanged += new System.EventHandler(this.cbbFirstEntity_SelectedIndexChanged);
            // 
            // cbbRelationship
            // 
            this.cbbRelationship.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRelationship.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbRelationship.FormattingEnabled = true;
            this.cbbRelationship.Location = new System.Drawing.Point(3, 18);
            this.cbbRelationship.Name = "cbbRelationship";
            this.cbbRelationship.Size = new System.Drawing.Size(344, 26);
            this.cbbRelationship.Sorted = true;
            this.cbbRelationship.TabIndex = 1;
            this.cbbRelationship.SelectedIndexChanged += new System.EventHandler(this.cbbRelationship_SelectedIndexChanged);
            // 
            // cbbSecondEntity
            // 
            this.cbbSecondEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbSecondEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSecondEntity.FormattingEnabled = true;
            this.cbbSecondEntity.Location = new System.Drawing.Point(3, 18);
            this.cbbSecondEntity.Name = "cbbSecondEntity";
            this.cbbSecondEntity.Size = new System.Drawing.Size(356, 26);
            this.cbbSecondEntity.Sorted = true;
            this.cbbSecondEntity.TabIndex = 2;
            this.cbbSecondEntity.SelectedIndexChanged += new System.EventHandler(this.cbbSecondEntity_SelectedIndexChanged);
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadMetadata,
            this.toolStripSeparator1,
            this.tsbImportNN,
            this.tsbExport,
            this.tsbDelete,
            this.tsbCancel,
            this.toolStripSeparator5,
            this.tsddbSeparator,
            this.toolStripSeparator6,
            this.tsbDebug,
            this.toolStripSeparator7,
            this.tslLogs,
            this.tsbExportLogs,
            this.tsbClearLogs});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1080, 39);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "tsMain";
            // 
            // tsbLoadMetadata
            // 
            this.tsbLoadMetadata.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.Dataverse_32x32;
            this.tsbLoadMetadata.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadMetadata.Name = "tsbLoadMetadata";
            this.tsbLoadMetadata.Size = new System.Drawing.Size(146, 36);
            this.tsbLoadMetadata.Text = "Load Metadata";
            this.tsbLoadMetadata.Click += new System.EventHandler(this.tsbLoadMetadata_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbImportNN
            // 
            this.tsbImportNN.Enabled = false;
            this.tsbImportNN.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.import;
            this.tsbImportNN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportNN.Name = "tsbImportNN";
            this.tsbImportNN.Size = new System.Drawing.Size(90, 36);
            this.tsbImportNN.Text = "Import";
            this.tsbImportNN.ToolTipText = "Import NN relationships";
            this.tsbImportNN.Click += new System.EventHandler(this.tsbImportNN_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Enabled = false;
            this.tsbExport.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.export;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(88, 36);
            this.tsbExport.Text = "Export";
            this.tsbExport.ToolTipText = "Export NN relationships";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(89, 36);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.ToolTipText = "Delete NN relationships";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.stop;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(89, 36);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Visible = false;
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // tsddbSeparator
            // 
            this.tsddbSeparator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbSeparator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commaToolStripMenuItem,
            this.semicolonToolStripMenuItem,
            this.pipeToolStripMenuItem,
            this.tabToolStripMenuItem});
            this.tsddbSeparator.Image = ((System.Drawing.Image)(resources.GetObject("tsddbSeparator.Image")));
            this.tsddbSeparator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbSeparator.Name = "tsddbSeparator";
            this.tsddbSeparator.Size = new System.Drawing.Size(151, 36);
            this.tsddbSeparator.Tag = ',';
            this.tsddbSeparator.Text = "Separator : Comma";
            this.tsddbSeparator.ToolTipText = "Select the separator to use to read/create the file that manages NN relationships" +
    "";
            this.tsddbSeparator.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbSeparator_DropDownItemClicked);
            // 
            // commaToolStripMenuItem
            // 
            this.commaToolStripMenuItem.Checked = true;
            this.commaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.commaToolStripMenuItem.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.comma;
            this.commaToolStripMenuItem.Name = "commaToolStripMenuItem";
            this.commaToolStripMenuItem.Size = new System.Drawing.Size(236, 38);
            this.commaToolStripMenuItem.Text = "Comma (,)";
            // 
            // semicolonToolStripMenuItem
            // 
            this.semicolonToolStripMenuItem.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.semicolon;
            this.semicolonToolStripMenuItem.Name = "semicolonToolStripMenuItem";
            this.semicolonToolStripMenuItem.Size = new System.Drawing.Size(236, 38);
            this.semicolonToolStripMenuItem.Text = "Semicolon (;)";
            // 
            // pipeToolStripMenuItem
            // 
            this.pipeToolStripMenuItem.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.bar;
            this.pipeToolStripMenuItem.Name = "pipeToolStripMenuItem";
            this.pipeToolStripMenuItem.Size = new System.Drawing.Size(236, 38);
            this.pipeToolStripMenuItem.Text = "Pipe (|)";
            // 
            // tabToolStripMenuItem
            // 
            this.tabToolStripMenuItem.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.key;
            this.tabToolStripMenuItem.Name = "tabToolStripMenuItem";
            this.tabToolStripMenuItem.Size = new System.Drawing.Size(236, 38);
            this.tabToolStripMenuItem.Text = "Tab";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbDebug
            // 
            this.tsbDebug.CheckOnClick = true;
            this.tsbDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsbDebug.Image")));
            this.tsbDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDebug.Name = "tsbDebug";
            this.tsbDebug.Size = new System.Drawing.Size(90, 36);
            this.tsbDebug.Text = "Debug : Off";
            this.tsbDebug.ToolTipText = "Add additional information in the log\r\n\r\nUse only when the operation does not wor" +
    "k as expected. It might impact performance.";
            this.tsbDebug.CheckedChanged += new System.EventHandler(this.tsbDebug_CheckedChanged);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
            // 
            // tslLogs
            // 
            this.tslLogs.Name = "tslLogs";
            this.tslLogs.Size = new System.Drawing.Size(47, 36);
            this.tslLogs.Text = "Logs :";
            // 
            // tsbExportLogs
            // 
            this.tsbExportLogs.Enabled = false;
            this.tsbExportLogs.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.Details32;
            this.tsbExportLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportLogs.Name = "tsbExportLogs";
            this.tsbExportLogs.Size = new System.Drawing.Size(88, 36);
            this.tsbExportLogs.Text = "Export";
            this.tsbExportLogs.Click += new System.EventHandler(this.tsbExportLogs_Click);
            // 
            // tsbClearLogs
            // 
            this.tsbClearLogs.Image = global::Javista.XrmToolBox.ImportNN.Properties.Resources.Eraser32;
            this.tsbClearLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearLogs.Name = "tsbClearLogs";
            this.tsbClearLogs.Size = new System.Drawing.Size(79, 36);
            this.tsbClearLogs.Text = "Clear";
            this.tsbClearLogs.Click += new System.EventHandler(this.tsbClearLogs_Click);
            // 
            // gbFirst
            // 
            this.gbFirst.Controls.Add(this.cbbFirstEntityAttribute);
            this.gbFirst.Controls.Add(this.rdbFirstAttribute);
            this.gbFirst.Controls.Add(this.rdbFirstGuid);
            this.gbFirst.Controls.Add(this.label2);
            this.gbFirst.Controls.Add(this.cbbFirstEntity);
            this.gbFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFirst.Location = new System.Drawing.Point(3, 3);
            this.gbFirst.Name = "gbFirst";
            this.gbFirst.Size = new System.Drawing.Size(350, 187);
            this.gbFirst.TabIndex = 5;
            this.gbFirst.TabStop = false;
            this.gbFirst.Text = "First Entity";
            // 
            // cbbFirstEntityAttribute
            // 
            this.cbbFirstEntityAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbFirstEntityAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFirstEntityAttribute.Enabled = false;
            this.cbbFirstEntityAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbFirstEntityAttribute.FormattingEnabled = true;
            this.cbbFirstEntityAttribute.Location = new System.Drawing.Point(3, 153);
            this.cbbFirstEntityAttribute.Name = "cbbFirstEntityAttribute";
            this.cbbFirstEntityAttribute.Size = new System.Drawing.Size(344, 26);
            this.cbbFirstEntityAttribute.Sorted = true;
            this.cbbFirstEntityAttribute.TabIndex = 4;
            // 
            // rdbFirstAttribute
            // 
            this.rdbFirstAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbFirstAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFirstAttribute.Location = new System.Drawing.Point(3, 119);
            this.rdbFirstAttribute.Name = "rdbFirstAttribute";
            this.rdbFirstAttribute.Size = new System.Drawing.Size(344, 34);
            this.rdbFirstAttribute.TabIndex = 3;
            this.rdbFirstAttribute.Text = "Specific attribute";
            this.rdbFirstAttribute.UseVisualStyleBackColor = true;
            // 
            // rdbFirstGuid
            // 
            this.rdbFirstGuid.Checked = true;
            this.rdbFirstGuid.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbFirstGuid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFirstGuid.Location = new System.Drawing.Point(3, 79);
            this.rdbFirstGuid.Name = "rdbFirstGuid";
            this.rdbFirstGuid.Size = new System.Drawing.Size(344, 40);
            this.rdbFirstGuid.TabIndex = 2;
            this.rdbFirstGuid.TabStop = true;
            this.rdbFirstGuid.Text = "Unique Identifier";
            this.rdbFirstGuid.UseVisualStyleBackColor = true;
            this.rdbFirstGuid.CheckedChanged += new System.EventHandler(this.rdbFirstGuid_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(344, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mapping attribute";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbRelationship
            // 
            this.gbRelationship.Controls.Add(this.cbbRelationship);
            this.gbRelationship.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRelationship.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRelationship.Location = new System.Drawing.Point(359, 3);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Size = new System.Drawing.Size(350, 187);
            this.gbRelationship.TabIndex = 6;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "Relationship";
            // 
            // gbSecond
            // 
            this.gbSecond.Controls.Add(this.cbbSecondEntityAttribute);
            this.gbSecond.Controls.Add(this.rdbSecondAttribute);
            this.gbSecond.Controls.Add(this.rdbSecondGuid);
            this.gbSecond.Controls.Add(this.label1);
            this.gbSecond.Controls.Add(this.cbbSecondEntity);
            this.gbSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSecond.Location = new System.Drawing.Point(715, 3);
            this.gbSecond.Name = "gbSecond";
            this.gbSecond.Size = new System.Drawing.Size(362, 187);
            this.gbSecond.TabIndex = 6;
            this.gbSecond.TabStop = false;
            this.gbSecond.Text = "Second Entity";
            // 
            // cbbSecondEntityAttribute
            // 
            this.cbbSecondEntityAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbSecondEntityAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondEntityAttribute.Enabled = false;
            this.cbbSecondEntityAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSecondEntityAttribute.FormattingEnabled = true;
            this.cbbSecondEntityAttribute.Location = new System.Drawing.Point(3, 153);
            this.cbbSecondEntityAttribute.Name = "cbbSecondEntityAttribute";
            this.cbbSecondEntityAttribute.Size = new System.Drawing.Size(356, 26);
            this.cbbSecondEntityAttribute.Sorted = true;
            this.cbbSecondEntityAttribute.TabIndex = 4;
            // 
            // rdbSecondAttribute
            // 
            this.rdbSecondAttribute.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbSecondAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSecondAttribute.Location = new System.Drawing.Point(3, 119);
            this.rdbSecondAttribute.Name = "rdbSecondAttribute";
            this.rdbSecondAttribute.Size = new System.Drawing.Size(356, 34);
            this.rdbSecondAttribute.TabIndex = 3;
            this.rdbSecondAttribute.Text = "Specific attribute";
            this.rdbSecondAttribute.UseVisualStyleBackColor = true;
            // 
            // rdbSecondGuid
            // 
            this.rdbSecondGuid.Checked = true;
            this.rdbSecondGuid.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbSecondGuid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSecondGuid.Location = new System.Drawing.Point(3, 79);
            this.rdbSecondGuid.Name = "rdbSecondGuid";
            this.rdbSecondGuid.Size = new System.Drawing.Size(356, 40);
            this.rdbSecondGuid.TabIndex = 2;
            this.rdbSecondGuid.TabStop = true;
            this.rdbSecondGuid.Text = "Unique Identifier";
            this.rdbSecondGuid.UseVisualStyleBackColor = true;
            this.rdbSecondGuid.CheckedChanged += new System.EventHandler(this.rdbSecondGuid_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mapping attribute";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbImportFile
            // 
            this.gbImportFile.Controls.Add(this.nudBatchCount);
            this.gbImportFile.Controls.Add(this.lblBatchCount);
            this.gbImportFile.Controls.Add(this.chkImportInBatch);
            this.gbImportFile.Controls.Add(this.chkCacheSecondEntity);
            this.gbImportFile.Controls.Add(this.chkCacheFirstEntity);
            this.gbImportFile.Controls.Add(this.btnBrowse);
            this.gbImportFile.Controls.Add(this.txtFilePath);
            this.gbImportFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbImportFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbImportFile.Location = new System.Drawing.Point(0, 232);
            this.gbImportFile.Name = "gbImportFile";
            this.gbImportFile.Size = new System.Drawing.Size(1080, 131);
            this.gbImportFile.TabIndex = 7;
            this.gbImportFile.TabStop = false;
            this.gbImportFile.Text = "Import/Delete file and settings";
            // 
            // nudBatchCount
            // 
            this.nudBatchCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudBatchCount.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBatchCount.Location = new System.Drawing.Point(684, 51);
            this.nudBatchCount.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudBatchCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudBatchCount.Name = "nudBatchCount";
            this.nudBatchCount.Size = new System.Drawing.Size(188, 27);
            this.nudBatchCount.TabIndex = 6;
            this.nudBatchCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblBatchCount
            // 
            this.lblBatchCount.AutoSize = true;
            this.lblBatchCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchCount.Location = new System.Drawing.Point(582, 55);
            this.lblBatchCount.Name = "lblBatchCount";
            this.lblBatchCount.Size = new System.Drawing.Size(76, 16);
            this.lblBatchCount.TabIndex = 5;
            this.lblBatchCount.Text = "Batch count";
            // 
            // chkImportInBatch
            // 
            this.chkImportInBatch.AutoSize = true;
            this.chkImportInBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkImportInBatch.Location = new System.Drawing.Point(405, 54);
            this.chkImportInBatch.Name = "chkImportInBatch";
            this.chkImportInBatch.Size = new System.Drawing.Size(128, 20);
            this.chkImportInBatch.TabIndex = 4;
            this.chkImportInBatch.Text = "Process in batch";
            this.chkImportInBatch.UseVisualStyleBackColor = true;
            // 
            // chkCacheSecondEntity
            // 
            this.chkCacheSecondEntity.AutoSize = true;
            this.chkCacheSecondEntity.Enabled = false;
            this.chkCacheSecondEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCacheSecondEntity.Location = new System.Drawing.Point(10, 84);
            this.chkCacheSecondEntity.Name = "chkCacheSecondEntity";
            this.chkCacheSecondEntity.Size = new System.Drawing.Size(217, 20);
            this.chkCacheSecondEntity.TabIndex = 3;
            this.chkCacheSecondEntity.Text = "Cache records for second entity";
            this.chkCacheSecondEntity.UseVisualStyleBackColor = true;
            // 
            // chkCacheFirstEntity
            // 
            this.chkCacheFirstEntity.AutoSize = true;
            this.chkCacheFirstEntity.Enabled = false;
            this.chkCacheFirstEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCacheFirstEntity.Location = new System.Drawing.Point(10, 54);
            this.chkCacheFirstEntity.Name = "chkCacheFirstEntity";
            this.chkCacheFirstEntity.Size = new System.Drawing.Size(192, 20);
            this.chkCacheFirstEntity.TabIndex = 2;
            this.chkCacheFirstEntity.Text = "Cache records for first entity";
            this.chkCacheFirstEntity.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(999, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 27);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(9, 21);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(984, 27);
            this.txtFilePath.TabIndex = 0;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.lvLogs);
            this.gbLog.Controls.Add(this.pnlFilterLogs);
            this.gbLog.Controls.Add(this.pnlStats);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLog.Location = new System.Drawing.Point(0, 363);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(1080, 378);
            this.gbLog.TabIndex = 8;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // lvLogs
            // 
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLogStatus,
            this.chLogLineNumber,
            this.chLogFirstValue,
            this.chLogSecondValue,
            this.chLogStatusText,
            this.chLogMessage});
            this.lvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLogs.FullRowSelect = true;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(316, 76);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(761, 299);
            this.lvLogs.SmallImageList = this.statusImageList;
            this.lvLogs.TabIndex = 6;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            // 
            // chLogStatus
            // 
            this.chLogStatus.Text = "";
            this.chLogStatus.Width = 33;
            // 
            // chLogLineNumber
            // 
            this.chLogLineNumber.Text = "Line";
            // 
            // chLogFirstValue
            // 
            this.chLogFirstValue.DisplayIndex = 3;
            this.chLogFirstValue.Text = "First Value";
            this.chLogFirstValue.Width = 250;
            // 
            // chLogSecondValue
            // 
            this.chLogSecondValue.DisplayIndex = 4;
            this.chLogSecondValue.Text = "Second Value";
            this.chLogSecondValue.Width = 250;
            // 
            // chLogStatusText
            // 
            this.chLogStatusText.DisplayIndex = 2;
            this.chLogStatusText.Text = "Result";
            this.chLogStatusText.Width = 80;
            // 
            // chLogMessage
            // 
            this.chLogMessage.Text = "Message";
            this.chLogMessage.Width = 300;
            // 
            // statusImageList
            // 
            this.statusImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("statusImageList.ImageStream")));
            this.statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.statusImageList.Images.SetKeyName(0, "tick.png");
            this.statusImageList.Images.SetKeyName(1, "cross.png");
            // 
            // pnlFilterLogs
            // 
            this.pnlFilterLogs.Controls.Add(this.chkHideAlreadyExists);
            this.pnlFilterLogs.Controls.Add(this.chkShowErrorsOnly);
            this.pnlFilterLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterLogs.Location = new System.Drawing.Point(316, 18);
            this.pnlFilterLogs.Name = "pnlFilterLogs";
            this.pnlFilterLogs.Size = new System.Drawing.Size(761, 58);
            this.pnlFilterLogs.TabIndex = 4;
            // 
            // chkHideAlreadyExists
            // 
            this.chkHideAlreadyExists.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkHideAlreadyExists.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideAlreadyExists.Location = new System.Drawing.Point(193, 0);
            this.chkHideAlreadyExists.Name = "chkHideAlreadyExists";
            this.chkHideAlreadyExists.Size = new System.Drawing.Size(281, 58);
            this.chkHideAlreadyExists.TabIndex = 1;
            this.chkHideAlreadyExists.Text = "Hide \"Already exists\" error";
            this.chkHideAlreadyExists.UseVisualStyleBackColor = true;
            this.chkHideAlreadyExists.CheckedChanged += new System.EventHandler(this.chkHideAlreadyExists_CheckedChanged);
            // 
            // chkShowErrorsOnly
            // 
            this.chkShowErrorsOnly.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkShowErrorsOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowErrorsOnly.Location = new System.Drawing.Point(0, 0);
            this.chkShowErrorsOnly.Name = "chkShowErrorsOnly";
            this.chkShowErrorsOnly.Size = new System.Drawing.Size(193, 58);
            this.chkShowErrorsOnly.TabIndex = 0;
            this.chkShowErrorsOnly.Text = "Show Errors only";
            this.chkShowErrorsOnly.UseVisualStyleBackColor = true;
            this.chkShowErrorsOnly.CheckedChanged += new System.EventHandler(this.chkShowErrorsOnly_CheckedChanged);
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.lblError);
            this.pnlStats.Controls.Add(this.label9);
            this.pnlStats.Controls.Add(this.lblSuccess);
            this.pnlStats.Controls.Add(this.label7);
            this.pnlStats.Controls.Add(this.lblProcessed);
            this.pnlStats.Controls.Add(this.label5);
            this.pnlStats.Controls.Add(this.lblTotal);
            this.pnlStats.Controls.Add(this.lblTotalLabel);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlStats.Location = new System.Drawing.Point(3, 18);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(313, 357);
            this.pnlStats.TabIndex = 1;
            this.pnlStats.Visible = false;
            // 
            // lblError
            // 
            this.lblError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(0, 217);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(313, 31);
            this.lblError.TabIndex = 7;
            this.lblError.Text = "0";
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(313, 31);
            this.label9.TabIndex = 6;
            this.label9.Text = "Error";
            // 
            // lblSuccess
            // 
            this.lblSuccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccess.Location = new System.Drawing.Point(0, 155);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(313, 31);
            this.lblSuccess.TabIndex = 5;
            this.lblSuccess.Text = "0";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(313, 31);
            this.label7.TabIndex = 4;
            this.label7.Text = "Success";
            // 
            // lblProcessed
            // 
            this.lblProcessed.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProcessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessed.Location = new System.Drawing.Point(0, 93);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(313, 31);
            this.lblProcessed.TabIndex = 3;
            this.lblProcessed.Text = "0";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(313, 31);
            this.label5.TabIndex = 2;
            this.label5.Text = "Processed";
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(0, 31);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(313, 31);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "0";
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLabel.Location = new System.Drawing.Point(0, 0);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(313, 31);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Number of lines";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Controls.Add(this.gbSecond, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbRelationship, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFirst, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1080, 193);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // MainControl
            // 
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbImportFile);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tsMain);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1080, 741);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.gbFirst.ResumeLayout(false);
            this.gbRelationship.ResumeLayout(false);
            this.gbSecond.ResumeLayout(false);
            this.gbImportFile.ResumeLayout(false);
            this.gbImportFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchCount)).EndInit();
            this.gbLog.ResumeLayout(false);
            this.pnlFilterLogs.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ToolStripButton tsbExport;
        private ToolStripButton tsbDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripDropDownButton tsddbSeparator;
        private ToolStripMenuItem commaToolStripMenuItem;
        private ToolStripMenuItem semicolonToolStripMenuItem;
        private ToolStripMenuItem pipeToolStripMenuItem;
        private ToolStripMenuItem tabToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton tsbDebug;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton tsbExportLogs;
        private ToolStripLabel tslLogs;
        private ToolStripButton tsbClearLogs;
        private CheckBox chkCacheSecondEntity;
        private CheckBox chkCacheFirstEntity;
        private CheckBox chkImportInBatch;
        private Panel pnlStats;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblProcessed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalLabel;
        private ToolStripButton tsbCancel;
        private ImageList statusImageList;
        private NumericUpDown nudBatchCount;
        private System.Windows.Forms.Label lblBatchCount;
        private ListView lvLogs;
        private ColumnHeader chLogStatus;
        private ColumnHeader chLogLineNumber;
        private ColumnHeader chLogFirstValue;
        private ColumnHeader chLogSecondValue;
        private ColumnHeader chLogStatusText;
        private ColumnHeader chLogMessage;
        private Panel pnlFilterLogs;
        private CheckBox chkHideAlreadyExists;
        private CheckBox chkShowErrorsOnly;
        private TableLayoutPanel tableLayoutPanel1;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  