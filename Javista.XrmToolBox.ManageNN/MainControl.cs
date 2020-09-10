using Javista.XrmToolBox.ManageNN.AppCode;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Javista.XrmToolBox.ManageNN
{
    public partial class MainControl : PluginControlBase, IGitHubPlugin, IHelpPlugin
    {
        private BackgroundWorker currentBw;
        private List<EntityMetadata> emds;

        public MainControl()
        {
            InitializeComponent();
        }

        public string HelpUrl => "https://github.com/MscrmTools/Javista.XrmToolBox.ManageNN/wiki";

        public string RepositoryName => "Javista.XrmToolBox.ManageNN";

        public string UserName => "MscrmTools";

        public static void AddItem(ListBox box, string item, ToolStripButton button)
        {
            MethodInvoker miAddItem = delegate
            {
                box.Items.Add(item);

                button.Enabled = true;
            };

            if (box.InvokeRequired)
            {
                box.Invoke(miAddItem);
            }
            else
            {
                miAddItem();
            }
        }

        public static void AddListViewItem(ListView lv, bool? success, string lineNumber, string firstValue, string secondValue, string message, ToolStripButton button)
        {
            lv.Invoke(new Action(() =>
            {
                lv.Items.Add(new ListViewItem("")
                {
                    SubItems =
                    {
                        lineNumber,
                        firstValue,
                        secondValue,
                        success.HasValue ? success.Value ? "Success" : "Error" : "",
                        message
                    },
                    ImageIndex = success.HasValue ? success.Value ? 0 : 1 : -1
                });

                button.Enabled = true;
            }));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = @"Specify the file to process"
            };

            if (ofd.ShowDialog(ParentForm) == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void cbbFirstEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFirstEntity.SelectedItem == null)
                return;

            var emd = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata;
            var relationships = emd.ManyToManyRelationships;

            cbbFirstEntityAttribute.Items.Clear();

            foreach (var amd in emd.Attributes.Where(a => a.AttributeOf == null
                && a.AttributeType != null && (a.AttributeType.Value == AttributeTypeCode.Integer
                || a.AttributeType.Value == AttributeTypeCode.Memo
                || a.AttributeType.Value == AttributeTypeCode.String)))
            {
                cbbFirstEntityAttribute.Items.Add(new AttributeInfo
                {
                    Metadata = amd
                });
            }

            cbbFirstEntityAttribute.DrawMode = DrawMode.OwnerDrawFixed;
            cbbFirstEntityAttribute.DrawItem += cbbAttribute_DrawItem;

            if (cbbFirstEntityAttribute.Items.Count > 0)
            {
                cbbFirstEntityAttribute.SelectedIndex = 0;
            }

            cbbRelationship.Items.Clear();

            foreach (var rel in relationships)
            {
                cbbRelationship.Items.Add(new RelationshipInfo { Metadata = rel });
            }

            if (cbbRelationship.Items.Count > 0)
            {
                cbbRelationship.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(ParentForm, @"No many to many relationships found for this entity!", @"Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cbbRelationship.DrawMode = DrawMode.OwnerDrawFixed;
            cbbRelationship.DrawItem += cbbRelationship_DrawItem;
        }

        private void cbbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbRelationship.SelectedItem == null)
                return;

            var rel = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata;
            cbbSecondEntity.Items.Clear();
            cbbSecondEntity.Items.Add(new EntityInfo
            {
                Metadata = emds.First(ent => (ent.LogicalName == rel.Entity1LogicalName && rel.Entity1LogicalName != ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName)
                || (ent.LogicalName == rel.Entity2LogicalName && rel.Entity2LogicalName != ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName)
                || (rel.Entity1LogicalName == rel.Entity2LogicalName && ent.LogicalName == rel.Entity2LogicalName && rel.Entity2LogicalName == ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName))
            });

            if (cbbSecondEntity.Items.Count > 0)
            {
                cbbSecondEntity.SelectedIndex = 0;
            }

            cbbSecondEntity.DrawMode = DrawMode.OwnerDrawFixed;
            cbbSecondEntity.DrawItem += cbbEntity_DrawItem;
        }

        private void cbbSecondEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSecondEntity.SelectedItem == null)
                return;

            var emd = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata;

            cbbSecondEntityAttribute.Items.Clear();

            foreach (var amd in emd.Attributes.Where(a => a.AttributeOf == null
               && a.AttributeType != null && (a.AttributeType.Value == AttributeTypeCode.Integer
               || a.AttributeType.Value == AttributeTypeCode.Memo
               || a.AttributeType.Value == AttributeTypeCode.String)))
            {
                cbbSecondEntityAttribute.Items.Add(new AttributeInfo
                {
                    Metadata = amd
                });
            }

            if (cbbSecondEntityAttribute.Items.Count > 0)
            {
                cbbSecondEntityAttribute.SelectedIndex = 0;
            }

            cbbSecondEntityAttribute.DrawMode = DrawMode.OwnerDrawFixed;
            cbbSecondEntityAttribute.DrawItem += cbbAttribute_DrawItem;
        }

        private void ee_RaiseError(object sender, ExportResultEventArgs e)
        {
            AddListViewItem(lvLogs, null, string.Empty, string.Empty, String.Empty, e.Message, tsbExportLogs);
        }

        private void Ee_SendInformation(object sender, ExportResultEventArgs e)
        {
            AddListViewItem(lvLogs, null, "", "", "", e.Message, tsbExportLogs);
        }

        private void ie_RaiseError(object sender, ResultEventArgs e)
        {
            AddListViewItem(lvLogs, e.Success, e.LineNumber.ToString(), e.FirstValue, e.SecondValue, e.Message, tsbExportLogs);

            lblProcessed.Text = (int.Parse(lblProcessed.Text) + 1).ToString();
            lblError.Text = (int.Parse(lblError.Text) + 1).ToString();
        }

        private void ie_RaiseSuccess(object sender, ResultEventArgs e)
        {
            AddListViewItem(lvLogs, e.Success, e.LineNumber.ToString(), e.FirstValue, e.SecondValue, e.Message, tsbExportLogs);

            lblProcessed.Text = (int.Parse(lblProcessed.Text) + 1).ToString();
            lblSuccess.Text = (int.Parse(lblSuccess.Text) + 1).ToString();
        }

        private void Ie_SendInformation(object sender, ResultEventArgs e)
        {
            AddListViewItem(lvLogs, null, string.Empty, string.Empty, String.Empty, e.Message, tsbExportLogs);
        }

        private void LoadMetadata()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading metadata...",
                Work = (bw, e) =>
                {
                    EntityQueryExpression entityQueryExpression = new EntityQueryExpression
                    {
                        Criteria = new MetadataFilterExpression(),
                        Properties = new MetadataPropertiesExpression
                        {
                            AllProperties = false,
                            PropertyNames = { "Attributes", "ManyToManyRelationships", "DisplayName", "LogicalName", "SchemaName" }
                        },
                        AttributeQuery = new AttributeQueryExpression
                        {
                            Criteria = new MetadataFilterExpression(),
                            Properties = new MetadataPropertiesExpression
                            {
                                AllProperties = false,
                                PropertyNames = { "DisplayName", "LogicalName", "SchemaName", "AttributeOf", "AttributeType" }
                            }
                        },
                    };

                    RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
                    {
                        Query = entityQueryExpression,
                        ClientVersionStamp = null
                    };

                    var response = (RetrieveMetadataChangesResponse)Service.Execute(retrieveMetadataChangesRequest);
                    e.Result = response.EntityMetadata.ToList();
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error == null)
                    {
                        emds = (List<EntityMetadata>)e.Result;

                        cbbFirstEntity.Items.Clear();

                        foreach (var emd in emds)
                        {
                            cbbFirstEntity.Items.Add(new EntityInfo { Metadata = emd });
                        }

                        if (cbbFirstEntity.Items.Count > 0)
                        {
                            cbbFirstEntity.SelectedIndex = 0;
                        }

                        cbbFirstEntity.DrawMode = DrawMode.OwnerDrawFixed;
                        cbbFirstEntity.DrawItem += cbbEntity_DrawItem;

                        tsbExport.Enabled = true;
                        tsbImportNN.Enabled = true;
                        tsbDelete.Enabled = true;
                    }
                    else
                    {
                        tsbExport.Enabled = false;
                        tsbImportNN.Enabled = false;
                        tsbDelete.Enabled = false;

                        MessageBox.Show(ParentForm, @"An error occured: " + e.Error.Message, @"Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            });
        }

        private void rdbFirstGuid_CheckedChanged(object sender, EventArgs e)
        {
            cbbFirstEntityAttribute.Enabled = rdbFirstAttribute.Checked;
            chkCacheFirstEntity.Enabled = rdbFirstAttribute.Checked;
            if (rdbFirstGuid.Checked) chkCacheFirstEntity.Checked = false;
        }

        private void rdbSecondGuid_CheckedChanged(object sender, EventArgs e)
        {
            cbbSecondEntityAttribute.Enabled = rdbSecondAttribute.Checked;
            chkCacheSecondEntity.Enabled = rdbSecondAttribute.Checked;
            if (rdbSecondGuid.Checked) chkCacheSecondEntity.Checked = false;
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            currentBw?.CancelAsync();
            currentBw?.ReportProgress(0, "Cancellation requested...");
            AddListViewItem(lvLogs, null, string.Empty, string.Empty, String.Empty, "Cancellation requested...", tsbExportLogs);
        }

        private void tsbClearLogs_Click(object sender, EventArgs e)
        {
            lvLogs.Items.Clear();
            tsbExportLogs.Enabled = false;
            pnlStats.Visible = false;
            lblProcessed.Text = @"0";
            lblSuccess.Text = @"0";
            lblError.Text = @"0";
            lblTotal.Text = @"0";
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbDebug_CheckedChanged(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).Text = ((ToolStripButton)sender).Text == @"Debug : Off" ? "Debug : On" : "Debug : Off";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Length == 0)
                return;

            var settings = new ImportFileSettings
            {
                FirstEntity = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName,
                FirstAttributeIsGuid = rdbFirstGuid.Checked,
                FirstAttributeName = ((AttributeInfo)cbbFirstEntityAttribute.SelectedItem).Metadata.LogicalName,
                Relationship = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata.SchemaName,
                SecondEntity = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.LogicalName,
                SecondAttributeIsGuid = rdbSecondGuid.Checked,
                SecondAttributeName = ((AttributeInfo)cbbSecondEntityAttribute.SelectedItem).Metadata.LogicalName,
                Separator = tsddbSeparator.Tag.ToString(),
                ImportInBatch = chkImportInBatch.Checked,
                BatchCount = nudBatchCount.Value
            };

            tsbClearLogs_Click(tsbClearLogs, new EventArgs());

            tsbCancel.Visible = true;
            pnlStats.Visible = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deleting many to many relationships...",
                AsyncArgument = new object[] { settings, txtFilePath.Text },
                IsCancelable = true,
                Work = (bw, evt) =>
                {
                    var innerSettings = (ImportFileSettings)((object[])evt.Argument)[0];
                    var filePath = ((object[])evt.Argument)[1].ToString();
                    var ie = new ImportEngine(filePath, Service, innerSettings, true);
                    ie.RaiseError += ie_RaiseError;
                    ie.RaiseSuccess += ie_RaiseSuccess;
                    ie.LoadFile();
                    ie.Import(bw);
                },
                PostWorkCallBack = evt =>
                {
                    tsbCancel.Visible = false;

                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured during delete: {evt.Error.Message}", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            pnlStats.Visible = false;

            var sfd = new SaveFileDialog { Title = @"Select where to save the file", Filter = @"CSV file|*.csv" };
            if (sfd.ShowDialog(ParentForm) != DialogResult.OK)
            {
                return;
            }

            var settings = new ImportFileSettings
            {
                FirstEntity = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName,
                FirstAttributeIsGuid = rdbFirstGuid.Checked,
                FirstAttributeName = ((AttributeInfo)cbbFirstEntityAttribute.SelectedItem).Metadata.LogicalName,
                Relationship = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata.IntersectEntityName,
                SecondEntity = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.LogicalName,
                SecondAttributeIsGuid = rdbSecondGuid.Checked,
                SecondAttributeName = ((AttributeInfo)cbbSecondEntityAttribute.SelectedItem).Metadata.LogicalName,
                Separator = tsddbSeparator.Tag.ToString(),
                Debug = tsbDebug.Checked
            };

            tsbClearLogs_Click(tsbClearLogs, new EventArgs());
            tsbCancel.Visible = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Exporting many to many relationship records...",
                AsyncArgument = new object[] { settings, sfd.FileName },
                IsCancelable = true,
                Work = (bw, evt) =>
                {
                    var innerSettings = (ImportFileSettings)((object[])evt.Argument)[0];
                    var filePath = ((object[])evt.Argument)[1].ToString();
                    var ee = new ExportEngine(filePath, Service, innerSettings);
                    ee.SendInformation += Ee_SendInformation;
                    ee.RaiseError += ee_RaiseError;
                    ee.Export(bw);
                },
                PostWorkCallBack = evt =>
                {
                    tsbCancel.Visible = false;
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured during export: {evt.Error.Message}", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        private void tsbExportLogs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = @"CSV file|*.csv"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                using (var writer = new StreamWriter(sfd.FileName, false))
                {
                    //  writer.WriteLine(string.Join(Environment.NewLine, listLog.Items.Cast<string>()));

                    foreach (ListViewItem item in lvLogs.Items)
                    {
                        writer.WriteLine(string.Join(",", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(i => i.Text).Skip(1)));
                    }
                }

                var result = MessageBox.Show(this, @"Export completed!

Do you want to open the file now?", @"Success",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Process.Start(sfd.FileName);
                }
            }
        }

        private void tsbImportNN_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Length == 0)
                return;

            var settings = new ImportFileSettings
            {
                FirstEntity = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName,
                FirstAttributeIsGuid = rdbFirstGuid.Checked,
                FirstAttributeName = rdbFirstGuid.Checked ? ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.PrimaryIdAttribute : ((AttributeInfo)cbbFirstEntityAttribute.SelectedItem).Metadata.LogicalName,
                Relationship = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata.SchemaName,
                SecondEntity = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.LogicalName,
                SecondAttributeIsGuid = rdbSecondGuid.Checked,
                SecondAttributeName = rdbSecondGuid.Checked ? ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.PrimaryIdAttribute : ((AttributeInfo)cbbSecondEntityAttribute.SelectedItem).Metadata.LogicalName,
                Separator = tsddbSeparator.Tag.ToString(),
                Debug = tsbDebug.Checked,
                CacheFirstEntity = chkCacheFirstEntity.Checked,
                CacheSecondEntity = chkCacheSecondEntity.Checked,
                ImportInBatch = chkImportInBatch.Checked,
                BatchCount = nudBatchCount.Value
            };

            tsbClearLogs_Click(tsbClearLogs, new EventArgs());

            pnlStats.Visible = true;
            tsbCancel.Visible = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Importing many to many relationship records...",
                AsyncArgument = new object[] { settings, txtFilePath.Text },
                IsCancelable = true,
                Work = (bw, evt) =>
                {
                    currentBw = bw;
                    var innerSettings = (ImportFileSettings)((object[])evt.Argument)[0];
                    var filePath = ((object[])evt.Argument)[1].ToString();
                    var ie = new ImportEngine(filePath, Service, innerSettings);
                    ie.SendInformation += Ie_SendInformation;
                    ie.RaiseError += ie_RaiseError;
                    ie.RaiseSuccess += ie_RaiseSuccess;
                    int linesNumber = ie.LoadFile();

                    Invoke(new Action(() => { lblTotal.Text = linesNumber.ToString(); }));

                    ie.Import(bw);
                },
                PostWorkCallBack = evt =>
                {
                    tsbCancel.Visible = false;

                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured during import: {evt.Error.Message}", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        private void tsbLoadMetadata_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadMetadata);
        }

        #region combobox drawing methods

        private void cbbAttribute_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var attr = (AttributeInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string displayName = attr.Metadata.DisplayName.UserLocalizedLabel != null
                ? attr.Metadata.DisplayName.UserLocalizedLabel.Label
                : "N/A";
            string logicalName = attr.Metadata.LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(displayName, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 2;
            r2.Width /= 2;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(logicalName, e.Font, sb, r2);
            }
        }

        private void cbbEntity_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var attr = (EntityInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string displayName = attr.Metadata.DisplayName.UserLocalizedLabel != null
                ? attr.Metadata.DisplayName.UserLocalizedLabel.Label
                : "N/A";
            string logicalName = attr.Metadata.LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(displayName, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 2;
            r2.Width /= 2;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(logicalName, e.Font, sb, r2);
            }
        }

        private void cbbRelationship_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var rel = (RelationshipInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string name = rel.Metadata.IntersectEntityName;
            string entity1 = rel.Metadata.Entity1LogicalName;
            string entity2 = rel.Metadata.Entity2LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 3;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(name, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 3;
            r2.Width /= 3;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(entity1, e.Font, sb, r2);
            }

            // Get the bounds for the third column
            Rectangle r3 = e.Bounds;
            r3.X = e.Bounds.Width / 3 * 2;
            r3.Width /= 3;

            // Draw the text on the third column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(entity2, e.Font, sb, r3);
            }
        }

        #endregion combobox drawing methods

        private void tsddbSeparator_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = "Separator : {0}";
            string value;
            char separator;

            var item = e.ClickedItem;
            if (item == commaToolStripMenuItem)
            {
                value = "Comma";
                separator = ',';
            }
            else if (item == semicolonToolStripMenuItem)
            {
                value = "Semicolon";
                separator = ';';
            }
            else if (item == pipeToolStripMenuItem)
            {
                value = "Pipe";
                separator = '|';
            }
            else
            {
                value = "Tab";
                separator = '\t';
            }

            foreach (ToolStripMenuItem tsmi in tsddbSeparator.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsddbSeparator.Text = string.Format(text, value);
            tsddbSeparator.Tag = separator;
        }
    }
}