using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DayzJournal
{
	partial class DshForm
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

		private void InitializeComponent()
		{
			this.btClose = new System.Windows.Forms.Button();
			this.btRefresh = new System.Windows.Forms.Button();
			this.lvHistory = new System.Windows.Forms.ListView();
			this.header1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.header2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.header3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.header4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lbCsvFile = new System.Windows.Forms.Label();
			this.lbCfgFile = new System.Windows.Forms.Label();
			this.btGroup = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lbServerInfo1 = new System.Windows.Forms.Label();
			this.lbServerInfo2 = new System.Windows.Forms.Label();
			this.lvInfo = new System.Windows.Forms.ListView();
			this.headerServerInfo1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerServerInfo2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btClose.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btClose.Location = new System.Drawing.Point(316, 230);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(87, 29);
			this.btClose.TabIndex = 1;
			this.btClose.Text = "close";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btRefresh
			// 
			this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btRefresh.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btRefresh.Location = new System.Drawing.Point(223, 230);
			this.btRefresh.Name = "btRefresh";
			this.btRefresh.Size = new System.Drawing.Size(87, 29);
			this.btRefresh.TabIndex = 3;
			this.btRefresh.Text = "refresh";
			this.btRefresh.UseVisualStyleBackColor = true;
			this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
			// 
			// lvHistory
			// 
			this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvHistory.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header1,
            this.header2,
            this.header3,
            this.header4});
			this.lvHistory.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvHistory.FullRowSelect = true;
			this.lvHistory.HideSelection = false;
			this.lvHistory.Location = new System.Drawing.Point(0, 0);
			this.lvHistory.Margin = new System.Windows.Forms.Padding(0);
			this.lvHistory.Name = "lvHistory";
			this.lvHistory.Size = new System.Drawing.Size(406, 227);
			this.lvHistory.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvHistory.TabIndex = 0;
			this.lvHistory.UseCompatibleStateImageBehavior = false;
			this.lvHistory.View = System.Windows.Forms.View.Details;
			this.lvHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvHistoryColumn_Click);
			this.lvHistory.SelectedIndexChanged += new System.EventHandler(this.lvHistory_SelectedIndexChanged);
			this.lvHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvHistory_KeyDown);
			this.lvHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseClick);
			this.lvHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseDoubleClick);
			this.lvHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseMove);
			// 
			// header1
			// 
			this.header1.Text = "time";
			this.header1.Width = 100;
			// 
			// header2
			// 
			this.header2.Text = "ip";
			this.header2.Width = 100;
			// 
			// header3
			// 
			this.header3.Text = "player";
			this.header3.Width = 100;
			// 
			// header4
			// 
			this.header4.Text = "name";
			this.header4.Width = 100;
			// 
			// lbCsvFile
			// 
			this.lbCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbCsvFile.AutoSize = true;
			this.lbCsvFile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbCsvFile.Location = new System.Drawing.Point(3, 246);
			this.lbCsvFile.Name = "lbCsvFile";
			this.lbCsvFile.Size = new System.Drawing.Size(55, 13);
			this.lbCsvFile.TabIndex = 7;
			this.lbCsvFile.Text = "csv-file";
			this.lbCsvFile.DoubleClick += new System.EventHandler(this.lbCsvFile_DoubleClick);
			// 
			// lbCfgFile
			// 
			this.lbCfgFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbCfgFile.AutoSize = true;
			this.lbCfgFile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbCfgFile.Location = new System.Drawing.Point(3, 230);
			this.lbCfgFile.Name = "lbCfgFile";
			this.lbCfgFile.Size = new System.Drawing.Size(55, 13);
			this.lbCfgFile.TabIndex = 4;
			this.lbCfgFile.Text = "cfg-file";
			this.lbCfgFile.DoubleClick += new System.EventHandler(this.lbCfgFile_DoubleClick);
			// 
			// btGroup
			// 
			this.btGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btGroup.Location = new System.Drawing.Point(130, 230);
			this.btGroup.Name = "btGroup";
			this.btGroup.Size = new System.Drawing.Size(87, 29);
			this.btGroup.TabIndex = 8;
			this.btGroup.Text = "group";
			this.btGroup.UseVisualStyleBackColor = true;
			this.btGroup.Click += new System.EventHandler(this.btGroup_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lvHistory);
			this.splitContainer1.Panel1.Controls.Add(this.lbCfgFile);
			this.splitContainer1.Panel1.Controls.Add(this.lbCsvFile);
			this.splitContainer1.Panel1.Controls.Add(this.btGroup);
			this.splitContainer1.Panel1.Controls.Add(this.btClose);
			this.splitContainer1.Panel1.Controls.Add(this.btRefresh);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lbServerInfo1);
			this.splitContainer1.Panel2.Controls.Add(this.lbServerInfo2);
			this.splitContainer1.Panel2.Controls.Add(this.lvInfo);
			this.splitContainer1.Size = new System.Drawing.Size(590, 262);
			this.splitContainer1.SplitterDistance = 406;
			this.splitContainer1.TabIndex = 9;
			// 
			// lbServerInfo1
			// 
			this.lbServerInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbServerInfo1.AutoSize = true;
			this.lbServerInfo1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbServerInfo1.Location = new System.Drawing.Point(3, 230);
			this.lbServerInfo1.Name = "lbServerInfo1";
			this.lbServerInfo1.Size = new System.Drawing.Size(85, 13);
			this.lbServerInfo1.TabIndex = 100;
			this.lbServerInfo1.Text = "lbServerInfo1";
			// 
			// lbServerInfo2
			// 
			this.lbServerInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbServerInfo2.AutoSize = true;
			this.lbServerInfo2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbServerInfo2.Location = new System.Drawing.Point(3, 246);
			this.lbServerInfo2.Name = "lbServerInfo2";
			this.lbServerInfo2.Size = new System.Drawing.Size(85, 13);
			this.lbServerInfo2.TabIndex = 1;
			this.lbServerInfo2.Text = "lbServerInfo2";
			// 
			// lvInfo
			// 
			this.lvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerServerInfo1,
            this.headerServerInfo2});
			this.lvInfo.FullRowSelect = true;
			this.lvInfo.Location = new System.Drawing.Point(0, 0);
			this.lvInfo.Margin = new System.Windows.Forms.Padding(0);
			this.lvInfo.Name = "lvInfo";
			this.lvInfo.Size = new System.Drawing.Size(180, 227);
			this.lvInfo.TabIndex = 0;
			this.lvInfo.UseCompatibleStateImageBehavior = false;
			this.lvInfo.View = System.Windows.Forms.View.Details;
			this.lvInfo.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvInfoColumn_Click);
			// 
			// headerServerInfo1
			// 
			this.headerServerInfo1.Text = "time";
			this.headerServerInfo1.Width = 50;
			// 
			// headerServerInfo2
			// 
			this.headerServerInfo2.Text = "name";
			this.headerServerInfo2.Width = 56;
			// 
			// DshForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btClose;
			this.ClientSize = new System.Drawing.Size(590, 262);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = global::DayzJournal.Properties.Resources.list;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(600, 300);
			this.Name = "DshForm";
			this.Text = "DayzJournal";
			this.Load += new System.EventHandler(this.DSH_Form_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DSH_Form_KeyDown);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.ListView lvHistory;
		private System.Windows.Forms.Button btRefresh;
		private ColumnHeader header1;
		private ColumnHeader header2;
		private ColumnHeader header3;
		private ColumnHeader header4;
		private Label lbCfgFile;
		private Label lbCsvFile;
		private Button btGroup;
		private SplitContainer splitContainer1;
		private ListView lvInfo;
		private ColumnHeader headerServerInfo1;
		private ColumnHeader headerServerInfo2;

		private Label lbServerInfo2;
		private Label lbServerInfo1;

		private void refresh()
		{
			if (string.IsNullOrEmpty(CfgFullName))
				GetCfgFileName();

			lbCfgFile.ForeColor = !File.Exists(CfgFullName) ? Color.Red : SystemColors.ControlText;

			if (string.IsNullOrEmpty(CsvFullName))
				GetCsvFileName();

			var dayZProfile = new DayZProfile {CfgName = CfgFullName};
			dayZProfile.Read();

			if (_dayZProfile.LastWriteTime == dayZProfile.LastWriteTime)
				return;

			if (_dayZProfile.PlayerName == dayZProfile.PlayerName &&
				_dayZProfile.LastMpServerName == dayZProfile.LastMpServerName &&
				_dayZProfile.LastMpServer == dayZProfile.LastMpServer)
				return;

			_dayZProfile = dayZProfile;
			HistoryServerList.Update(_dayZProfile);
			HistoryServerList.Write();

			if (lvHistory.InvokeRequired)
				BeginInvoke(new Action(refresh_lvHistory));
			else
				refresh_lvHistory();
		}
	}
}

