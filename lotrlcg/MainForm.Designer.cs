namespace lotrlcg
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.PlaceholdersButton = new System.Windows.Forms.Button();
			this.CoreProxiesButton = new System.Windows.Forms.Button();
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.PlaceholdersBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.CoreProxiesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.HoverToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.CardBacksCheckbox = new System.Windows.Forms.CheckBox();
			this.UpdateDbButton = new System.Windows.Forms.Button();
			this.DbBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.ClearDbButton = new System.Windows.Forms.Button();
			this.MainStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// PlaceholdersButton
			// 
			this.PlaceholdersButton.Location = new System.Drawing.Point(12, 70);
			this.PlaceholdersButton.Name = "PlaceholdersButton";
			this.PlaceholdersButton.Size = new System.Drawing.Size(222, 23);
			this.PlaceholdersButton.TabIndex = 0;
			this.PlaceholdersButton.Text = "Placeholders";
			this.PlaceholdersButton.UseVisualStyleBackColor = true;
			this.PlaceholdersButton.Click += new System.EventHandler(this.PlaceholdersButton_Click);
			// 
			// CoreProxiesButton
			// 
			this.CoreProxiesButton.Location = new System.Drawing.Point(13, 99);
			this.CoreProxiesButton.Name = "CoreProxiesButton";
			this.CoreProxiesButton.Size = new System.Drawing.Size(222, 23);
			this.CoreProxiesButton.TabIndex = 1;
			this.CoreProxiesButton.Text = "Core Proxies";
			this.CoreProxiesButton.UseVisualStyleBackColor = true;
			this.CoreProxiesButton.Click += new System.EventHandler(this.CoreProxiesButton_Click);
			// 
			// MainStatusStrip
			// 
			this.MainStatusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusLabel});
			this.MainStatusStrip.Location = new System.Drawing.Point(0, 150);
			this.MainStatusStrip.Name = "MainStatusStrip";
			this.MainStatusStrip.Size = new System.Drawing.Size(251, 25);
			this.MainStatusStrip.SizingGrip = false;
			this.MainStatusStrip.TabIndex = 2;
			this.HoverToolTip.SetToolTip(this.MainStatusStrip, "ToolTip");
			this.MainStatusStrip.MouseHover += new System.EventHandler(this.StatusStrip_MouseHover);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(50, 19);
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoToolTip = true;
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(67, 20);
			this.StatusLabel.Text = "StatusLabel";
			// 
			// PlaceholdersBackgroundWorker
			// 
			this.PlaceholdersBackgroundWorker.WorkerReportsProgress = true;
			this.PlaceholdersBackgroundWorker.WorkerSupportsCancellation = true;
			this.PlaceholdersBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PlaceholdersBackgroundWorker_DoWork);
			this.PlaceholdersBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.PlaceholdersBackgroundWorker_ProgressChanged);
			this.PlaceholdersBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PlaceholdersBackgroundWorker_RunWorkerCompleted);
			// 
			// CoreProxiesBackgroundWorker
			// 
			this.CoreProxiesBackgroundWorker.WorkerReportsProgress = true;
			this.CoreProxiesBackgroundWorker.WorkerSupportsCancellation = true;
			this.CoreProxiesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CoreProxiesBackgroundWorker_DoWork);
			this.CoreProxiesBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CoreProxiesBackgroundWorker_ProgressChanged);
			this.CoreProxiesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CoreProxiesBackgroundWorker_RunWorkerCompleted);
			// 
			// CardBacksCheckbox
			// 
			this.CardBacksCheckbox.AutoSize = true;
			this.CardBacksCheckbox.Checked = true;
			this.CardBacksCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CardBacksCheckbox.Location = new System.Drawing.Point(13, 128);
			this.CardBacksCheckbox.Name = "CardBacksCheckbox";
			this.CardBacksCheckbox.Size = new System.Drawing.Size(80, 17);
			this.CardBacksCheckbox.TabIndex = 3;
			this.CardBacksCheckbox.Text = "Card backs";
			this.CardBacksCheckbox.UseVisualStyleBackColor = true;
			// 
			// UpdateDbButton
			// 
			this.UpdateDbButton.Location = new System.Drawing.Point(12, 41);
			this.UpdateDbButton.Name = "UpdateDbButton";
			this.UpdateDbButton.Size = new System.Drawing.Size(222, 23);
			this.UpdateDbButton.TabIndex = 4;
			this.UpdateDbButton.Text = "Update DB";
			this.UpdateDbButton.UseVisualStyleBackColor = true;
			this.UpdateDbButton.Click += new System.EventHandler(this.UpdateDbButton_Click);
			// 
			// DbBackgroundWorker
			// 
			this.DbBackgroundWorker.WorkerReportsProgress = true;
			this.DbBackgroundWorker.WorkerSupportsCancellation = true;
			this.DbBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DbBackgroundWorker_DoWork);
			this.DbBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DbBackgroundWorker_ProgressChanged);
			this.DbBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DbBackgroundWorker_RunWorkerCompleted);
			// 
			// ClearDbButton
			// 
			this.ClearDbButton.Location = new System.Drawing.Point(13, 12);
			this.ClearDbButton.Name = "ClearDbButton";
			this.ClearDbButton.Size = new System.Drawing.Size(222, 23);
			this.ClearDbButton.TabIndex = 5;
			this.ClearDbButton.Text = "Clear DB";
			this.ClearDbButton.UseVisualStyleBackColor = true;
			this.ClearDbButton.Click += new System.EventHandler(this.ClearDbButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(251, 175);
			this.Controls.Add(this.ClearDbButton);
			this.Controls.Add(this.UpdateDbButton);
			this.Controls.Add(this.CardBacksCheckbox);
			this.Controls.Add(this.MainStatusStrip);
			this.Controls.Add(this.CoreProxiesButton);
			this.Controls.Add(this.PlaceholdersButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "LotR LCG Tool";
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PlaceholdersButton;
		private System.Windows.Forms.Button CoreProxiesButton;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripProgressBar ProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.ComponentModel.BackgroundWorker PlaceholdersBackgroundWorker;
		private System.ComponentModel.BackgroundWorker CoreProxiesBackgroundWorker;
		private System.Windows.Forms.ToolTip HoverToolTip;
		private System.Windows.Forms.CheckBox CardBacksCheckbox;
		private System.Windows.Forms.Button UpdateDbButton;
		private System.ComponentModel.BackgroundWorker DbBackgroundWorker;
		private System.Windows.Forms.Button ClearDbButton;
	}
}

