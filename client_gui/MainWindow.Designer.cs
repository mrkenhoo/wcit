
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
            ConfigureInstaller = new GroupBox();
            ImageIndex = new DataGridView();
            ConfigureInstaller_Panel = new TableLayoutPanel();
            SourceDrive = new TextBox();
            EfiDrive = new TextBox();
            DestinationDrive = new TextBox();
            DestinationDriveLabel = new Label();
            EfiDriveLabel = new Label();
            DiskNumberLabel = new Label();
            DiskNumber = new NumericUpDown();
            SourceDriveLabel = new Label();
            DiskList = new DataGridView();
            MenuBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            InstallButton = new Button();
            ConfigureInstaller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageIndex).BeginInit();
            ConfigureInstaller_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).BeginInit();
            MenuBar.SuspendLayout();
            SuspendLayout();
            // 
            // ConfigureInstaller
            // 
            ConfigureInstaller.Controls.Add(ImageIndex);
            ConfigureInstaller.Controls.Add(ConfigureInstaller_Panel);
            ConfigureInstaller.Controls.Add(DiskList);
            ConfigureInstaller.FlatStyle = FlatStyle.System;
            resources.ApplyResources(ConfigureInstaller, "ConfigureInstaller");
            ConfigureInstaller.Name = "ConfigureInstaller";
            ConfigureInstaller.TabStop = false;
            ConfigureInstaller.UseCompatibleTextRendering = true;
            // 
            // ImageIndex
            // 
            ImageIndex.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(ImageIndex, "ImageIndex");
            ImageIndex.Name = "ImageIndex";
            // 
            // ConfigureInstaller_Panel
            // 
            resources.ApplyResources(ConfigureInstaller_Panel, "ConfigureInstaller_Panel");
            ConfigureInstaller_Panel.Controls.Add(SourceDrive, 1, 3);
            ConfigureInstaller_Panel.Controls.Add(EfiDrive, 1, 1);
            ConfigureInstaller_Panel.Controls.Add(DestinationDrive, 1, 0);
            ConfigureInstaller_Panel.Controls.Add(DestinationDriveLabel, 0, 0);
            ConfigureInstaller_Panel.Controls.Add(EfiDriveLabel, 0, 1);
            ConfigureInstaller_Panel.Controls.Add(DiskNumberLabel, 0, 2);
            ConfigureInstaller_Panel.Controls.Add(DiskNumber, 1, 2);
            ConfigureInstaller_Panel.Controls.Add(SourceDriveLabel, 0, 3);
            ConfigureInstaller_Panel.Name = "ConfigureInstaller_Panel";
            // 
            // SourceDrive
            // 
            SourceDrive.BorderStyle = BorderStyle.None;
            resources.ApplyResources(SourceDrive, "SourceDrive");
            SourceDrive.Name = "SourceDrive";
            // 
            // EfiDrive
            // 
            EfiDrive.BorderStyle = BorderStyle.None;
            resources.ApplyResources(EfiDrive, "EfiDrive");
            EfiDrive.Name = "EfiDrive";
            // 
            // DestinationDrive
            // 
            DestinationDrive.BorderStyle = BorderStyle.None;
            resources.ApplyResources(DestinationDrive, "DestinationDrive");
            DestinationDrive.Name = "DestinationDrive";
            // 
            // DestinationDriveLabel
            // 
            resources.ApplyResources(DestinationDriveLabel, "DestinationDriveLabel");
            DestinationDriveLabel.Name = "DestinationDriveLabel";
            // 
            // EfiDriveLabel
            // 
            resources.ApplyResources(EfiDriveLabel, "EfiDriveLabel");
            EfiDriveLabel.Name = "EfiDriveLabel";
            // 
            // DiskNumberLabel
            // 
            resources.ApplyResources(DiskNumberLabel, "DiskNumberLabel");
            DiskNumberLabel.Name = "DiskNumberLabel";
            // 
            // DiskNumber
            // 
            resources.ApplyResources(DiskNumber, "DiskNumber");
            DiskNumber.BorderStyle = BorderStyle.None;
            DiskNumber.Name = "DiskNumber";
            // 
            // SourceDriveLabel
            // 
            resources.ApplyResources(SourceDriveLabel, "SourceDriveLabel");
            SourceDriveLabel.Name = "SourceDriveLabel";
            // 
            // DiskList
            // 
            DiskList.AllowUserToAddRows = false;
            DiskList.AllowUserToDeleteRows = false;
            DiskList.AllowUserToResizeColumns = false;
            DiskList.AllowUserToResizeRows = false;
            DiskList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DiskList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DiskList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(DiskList, "DiskList");
            DiskList.Name = "DiskList";
            DiskList.ReadOnly = true;
            DiskList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            // 
            // MenuBar
            // 
            MenuBar.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            resources.ApplyResources(MenuBar, "MenuBar");
            MenuBar.Name = "MenuBar";
            MenuBar.RenderMode = ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            exitToolStripMenuItem.Click += CloseApplication;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // InstallButton
            // 
            resources.ApplyResources(InstallButton, "InstallButton");
            InstallButton.Name = "InstallButton";
            InstallButton.UseVisualStyleBackColor = true;
            InstallButton.Click += InstallButton_Click;
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(InstallButton);
            Controls.Add(ConfigureInstaller);
            Controls.Add(MenuBar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MainWindow";
            Load += MainWindow_Load;
            ConfigureInstaller.ResumeLayout(false);
            ConfigureInstaller.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageIndex).EndInit();
            ConfigureInstaller_Panel.ResumeLayout(false);
            ConfigureInstaller_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).EndInit();
            MenuBar.ResumeLayout(false);
            MenuBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox ConfigureInstaller;
        private DataGridView DiskList;
        private MenuStrip MenuBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label DestinationDriveLabel;
        private TextBox DestinationDrive;
        private TableLayoutPanel ConfigureInstaller_Panel;
        private Label EfiDriveLabel;
        private TextBox EfiDrive;
        private Label DiskNumberLabel;
        private NumericUpDown DiskNumber;
        private Button InstallButton;
        private Label SourceDriveLabel;
        private TextBox SourceDrive;
        private DataGridView ImageIndex;
    }
}

