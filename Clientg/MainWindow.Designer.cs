
namespace client_gui
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ConfigureInstaller = new System.Windows.Forms.GroupBox();
            this.DestinationDriveLabel = new System.Windows.Forms.Label();
            this.DestinationDrive = new System.Windows.Forms.TextBox();
            this.DiskList = new System.Windows.Forms.DataGridView();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigureInstaller_Panel = new System.Windows.Forms.TableLayoutPanel();
            this.EfiDriveLabel = new System.Windows.Forms.Label();
            this.EfiDrive = new System.Windows.Forms.TextBox();
            this.DiskNumberLabel = new System.Windows.Forms.Label();
            this.DiskNumber = new System.Windows.Forms.NumericUpDown();
            this.InstallButton = new System.Windows.Forms.Button();
            this.SourceDriveLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ConfigureInstaller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiskList)).BeginInit();
            this.MenuBar.SuspendLayout();
            this.ConfigureInstaller_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiskNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // ConfigureInstaller
            // 
            this.ConfigureInstaller.Controls.Add(this.ConfigureInstaller_Panel);
            this.ConfigureInstaller.Controls.Add(this.DiskList);
            this.ConfigureInstaller.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.ConfigureInstaller, "ConfigureInstaller");
            this.ConfigureInstaller.Name = "ConfigureInstaller";
            this.ConfigureInstaller.TabStop = false;
            this.ConfigureInstaller.UseCompatibleTextRendering = true;
            // 
            // DestinationDriveLabel
            // 
            resources.ApplyResources(this.DestinationDriveLabel, "DestinationDriveLabel");
            this.DestinationDriveLabel.Name = "DestinationDriveLabel";
            // 
            // DestinationDrive
            // 
            this.DestinationDrive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.DestinationDrive, "DestinationDrive");
            this.DestinationDrive.Name = "DestinationDrive";
            // 
            // DiskList
            // 
            this.DiskList.AllowUserToAddRows = false;
            this.DiskList.AllowUserToDeleteRows = false;
            this.DiskList.AllowUserToResizeColumns = false;
            this.DiskList.AllowUserToResizeRows = false;
            this.DiskList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DiskList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DiskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.DiskList, "DiskList");
            this.DiskList.Name = "DiskList";
            this.DiskList.ReadOnly = true;
            this.DiskList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.MenuBar, "MenuBar");
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.CloseApplication);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // ConfigureInstaller_Panel
            // 
            resources.ApplyResources(this.ConfigureInstaller_Panel, "ConfigureInstaller_Panel");
            this.ConfigureInstaller_Panel.Controls.Add(this.textBox1, 1, 3);
            this.ConfigureInstaller_Panel.Controls.Add(this.EfiDrive, 1, 1);
            this.ConfigureInstaller_Panel.Controls.Add(this.DestinationDrive, 1, 0);
            this.ConfigureInstaller_Panel.Controls.Add(this.DestinationDriveLabel, 0, 0);
            this.ConfigureInstaller_Panel.Controls.Add(this.EfiDriveLabel, 0, 1);
            this.ConfigureInstaller_Panel.Controls.Add(this.DiskNumberLabel, 0, 2);
            this.ConfigureInstaller_Panel.Controls.Add(this.DiskNumber, 1, 2);
            this.ConfigureInstaller_Panel.Controls.Add(this.SourceDriveLabel, 0, 3);
            this.ConfigureInstaller_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.ConfigureInstaller_Panel.Name = "ConfigureInstaller_Panel";
            // 
            // EfiDriveLabel
            // 
            resources.ApplyResources(this.EfiDriveLabel, "EfiDriveLabel");
            this.EfiDriveLabel.Name = "EfiDriveLabel";
            // 
            // EfiDrive
            // 
            this.EfiDrive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.EfiDrive, "EfiDrive");
            this.EfiDrive.Name = "EfiDrive";
            // 
            // DiskNumberLabel
            // 
            resources.ApplyResources(this.DiskNumberLabel, "DiskNumberLabel");
            this.DiskNumberLabel.Name = "DiskNumberLabel";
            // 
            // DiskNumber
            // 
            resources.ApplyResources(this.DiskNumber, "DiskNumber");
            this.DiskNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DiskNumber.Name = "DiskNumber";
            // 
            // InstallButton
            // 
            this.InstallButton.AutoEllipsis = true;
            resources.ApplyResources(this.InstallButton, "InstallButton");
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // SourceDriveLabel
            // 
            resources.ApplyResources(this.SourceDriveLabel, "SourceDriveLabel");
            this.SourceDriveLabel.Name = "SourceDriveLabel";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.ConfigureInstaller);
            this.Controls.Add(this.MenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ConfigureInstaller.ResumeLayout(false);
            this.ConfigureInstaller.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiskList)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ConfigureInstaller_Panel.ResumeLayout(false);
            this.ConfigureInstaller_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiskNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox ConfigureInstaller;
        private System.Windows.Forms.DataGridView DiskList;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label DestinationDriveLabel;
        private System.Windows.Forms.TextBox DestinationDrive;
        private System.Windows.Forms.TableLayoutPanel ConfigureInstaller_Panel;
        private System.Windows.Forms.Label EfiDriveLabel;
        private System.Windows.Forms.TextBox EfiDrive;
        private System.Windows.Forms.Label DiskNumberLabel;
        private System.Windows.Forms.NumericUpDown DiskNumber;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Label SourceDriveLabel;
        private System.Windows.Forms.TextBox textBox1;
    }
}

