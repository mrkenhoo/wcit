
using System.Windows.Forms;

namespace gui_app
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
            WindowsEditionIndexLabel = new Label();
            WindowsEditionIndex = new NumericUpDown();
            SourceDrive = new TextBox();
            ImageList = new DataGridView();
            SourceDriveLabel = new Label();
            DiskNumberLabel = new Label();
            EfiDriveLabel = new Label();
            EfiDrive = new TextBox();
            DiskList = new DataGridView();
            DiskNumber = new NumericUpDown();
            DestinationDrive = new TextBox();
            DestinationDriveLabel = new Label();
            MenuBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            InstallButton = new Button();
            ConfigureInstaller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowsEditionIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImageList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).BeginInit();
            MenuBar.SuspendLayout();
            SuspendLayout();
            // 
            // ConfigureInstaller
            // 
            ConfigureInstaller.Controls.Add(WindowsEditionIndexLabel);
            ConfigureInstaller.Controls.Add(WindowsEditionIndex);
            ConfigureInstaller.Controls.Add(SourceDrive);
            ConfigureInstaller.Controls.Add(ImageList);
            ConfigureInstaller.Controls.Add(SourceDriveLabel);
            ConfigureInstaller.Controls.Add(DiskNumberLabel);
            ConfigureInstaller.Controls.Add(EfiDriveLabel);
            ConfigureInstaller.Controls.Add(EfiDrive);
            ConfigureInstaller.Controls.Add(DiskList);
            ConfigureInstaller.Controls.Add(DiskNumber);
            ConfigureInstaller.Controls.Add(DestinationDrive);
            ConfigureInstaller.Controls.Add(DestinationDriveLabel);
            ConfigureInstaller.FlatStyle = FlatStyle.System;
            resources.ApplyResources(ConfigureInstaller, "ConfigureInstaller");
            ConfigureInstaller.Name = "ConfigureInstaller";
            ConfigureInstaller.TabStop = false;
            ConfigureInstaller.UseCompatibleTextRendering = true;
            // 
            // WindowsEditionIndexLabel
            // 
            resources.ApplyResources(WindowsEditionIndexLabel, "WindowsEditionIndexLabel");
            WindowsEditionIndexLabel.FlatStyle = FlatStyle.System;
            WindowsEditionIndexLabel.Name = "WindowsEditionIndexLabel";
            // 
            // WindowsEditionIndex
            // 
            resources.ApplyResources(WindowsEditionIndex, "WindowsEditionIndex");
            WindowsEditionIndex.Name = "WindowsEditionIndex";
            // 
            // SourceDrive
            // 
            resources.ApplyResources(SourceDrive, "SourceDrive");
            SourceDrive.Name = "SourceDrive";
            // 
            // ImageList
            // 
            ImageList.AllowUserToAddRows = false;
            ImageList.AllowUserToDeleteRows = false;
            ImageList.AllowUserToResizeColumns = false;
            ImageList.AllowUserToResizeRows = false;
            ImageList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(ImageList, "ImageList");
            ImageList.Name = "ImageList";
            ImageList.ReadOnly = true;
            // 
            // SourceDriveLabel
            // 
            resources.ApplyResources(SourceDriveLabel, "SourceDriveLabel");
            SourceDriveLabel.FlatStyle = FlatStyle.System;
            SourceDriveLabel.Name = "SourceDriveLabel";
            // 
            // DiskNumberLabel
            // 
            resources.ApplyResources(DiskNumberLabel, "DiskNumberLabel");
            DiskNumberLabel.FlatStyle = FlatStyle.System;
            DiskNumberLabel.Name = "DiskNumberLabel";
            // 
            // EfiDriveLabel
            // 
            resources.ApplyResources(EfiDriveLabel, "EfiDriveLabel");
            EfiDriveLabel.FlatStyle = FlatStyle.System;
            EfiDriveLabel.Name = "EfiDriveLabel";
            // 
            // EfiDrive
            // 
            resources.ApplyResources(EfiDrive, "EfiDrive");
            EfiDrive.Name = "EfiDrive";
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
            // DiskNumber
            // 
            resources.ApplyResources(DiskNumber, "DiskNumber");
            DiskNumber.Name = "DiskNumber";
            // 
            // DestinationDrive
            // 
            resources.ApplyResources(DestinationDrive, "DestinationDrive");
            DestinationDrive.Name = "DestinationDrive";
            // 
            // DestinationDriveLabel
            // 
            resources.ApplyResources(DestinationDriveLabel, "DestinationDriveLabel");
            DestinationDriveLabel.FlatStyle = FlatStyle.System;
            DestinationDriveLabel.Name = "DestinationDriveLabel";
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
            MaximizeBox = false;
            Name = "MainWindow";
            Load += MainWindow_Load;
            ConfigureInstaller.ResumeLayout(false);
            ConfigureInstaller.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowsEditionIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImageList).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).EndInit();
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
        private Label EfiDriveLabel;
        private TextBox EfiDrive;
        private Button InstallButton;
        private DataGridView ImageList;
        private TextBox SourceDrive;
        private Label DiskNumberLabel;
        private NumericUpDown DiskNumber;
        private Label SourceDriveLabel;
        private Label WindowsEditionIndexLabel;
        private NumericUpDown WindowsEditionIndex;
    }
}

