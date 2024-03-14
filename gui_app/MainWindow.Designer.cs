
using System.Windows.Forms;

namespace wit
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
            InstallButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            EfiDriveLabel = new Label();
            WindowsEditionIndex = new NumericUpDown();
            WindowsEditionIndexLabel = new Label();
            ImageFilePath = new Label();
            DiskNumberLabel = new Label();
            DiskNumber = new NumericUpDown();
            ImageFileLabel = new Label();
            ChooseISOImage = new Button();
            DestinationDriveLabel = new Label();
            DestinationDrive = new ComboBox();
            EfiDrive = new ComboBox();
            ImageList = new DataGridView();
            DiskList = new DataGridView();
            MenuBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutTheProgramToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowsEditionIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImageList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).BeginInit();
            MenuBar.SuspendLayout();
            SuspendLayout();
            // 
            // InstallButton
            // 
            resources.ApplyResources(InstallButton, "InstallButton");
            InstallButton.Name = "InstallButton";
            InstallButton.UseVisualStyleBackColor = true;
            InstallButton.Click += InstallButton_Click;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(EfiDriveLabel, 0, 1);
            tableLayoutPanel1.Controls.Add(WindowsEditionIndex, 1, 4);
            tableLayoutPanel1.Controls.Add(WindowsEditionIndexLabel, 0, 4);
            tableLayoutPanel1.Controls.Add(ImageFilePath, 1, 3);
            tableLayoutPanel1.Controls.Add(DiskNumberLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(DiskNumber, 1, 2);
            tableLayoutPanel1.Controls.Add(ImageFileLabel, 0, 3);
            tableLayoutPanel1.Controls.Add(ChooseISOImage, 2, 3);
            tableLayoutPanel1.Controls.Add(DestinationDriveLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(DestinationDrive, 1, 0);
            tableLayoutPanel1.Controls.Add(EfiDrive, 1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // EfiDriveLabel
            // 
            resources.ApplyResources(EfiDriveLabel, "EfiDriveLabel");
            EfiDriveLabel.FlatStyle = FlatStyle.System;
            EfiDriveLabel.Name = "EfiDriveLabel";
            // 
            // WindowsEditionIndex
            // 
            resources.ApplyResources(WindowsEditionIndex, "WindowsEditionIndex");
            WindowsEditionIndex.BorderStyle = BorderStyle.FixedSingle;
            WindowsEditionIndex.Name = "WindowsEditionIndex";
            // 
            // WindowsEditionIndexLabel
            // 
            resources.ApplyResources(WindowsEditionIndexLabel, "WindowsEditionIndexLabel");
            WindowsEditionIndexLabel.FlatStyle = FlatStyle.System;
            WindowsEditionIndexLabel.Name = "WindowsEditionIndexLabel";
            // 
            // ImageFilePath
            // 
            ImageFilePath.AutoEllipsis = true;
            resources.ApplyResources(ImageFilePath, "ImageFilePath");
            ImageFilePath.FlatStyle = FlatStyle.System;
            ImageFilePath.Name = "ImageFilePath";
            // 
            // DiskNumberLabel
            // 
            resources.ApplyResources(DiskNumberLabel, "DiskNumberLabel");
            DiskNumberLabel.FlatStyle = FlatStyle.System;
            DiskNumberLabel.Name = "DiskNumberLabel";
            // 
            // DiskNumber
            // 
            DiskNumber.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(DiskNumber, "DiskNumber");
            DiskNumber.Name = "DiskNumber";
            // 
            // ImageFileLabel
            // 
            resources.ApplyResources(ImageFileLabel, "ImageFileLabel");
            ImageFileLabel.FlatStyle = FlatStyle.System;
            ImageFileLabel.Name = "ImageFileLabel";
            // 
            // ChooseISOImage
            // 
            resources.ApplyResources(ChooseISOImage, "ChooseISOImage");
            ChooseISOImage.Name = "ChooseISOImage";
            ChooseISOImage.UseVisualStyleBackColor = false;
            ChooseISOImage.Click += ChooseISOImage_Click;
            // 
            // DestinationDriveLabel
            // 
            resources.ApplyResources(DestinationDriveLabel, "DestinationDriveLabel");
            DestinationDriveLabel.FlatStyle = FlatStyle.System;
            DestinationDriveLabel.Name = "DestinationDriveLabel";
            // 
            // DestinationDrive
            // 
            resources.ApplyResources(DestinationDrive, "DestinationDrive");
            DestinationDrive.DropDownStyle = ComboBoxStyle.DropDownList;
            DestinationDrive.FormattingEnabled = true;
            DestinationDrive.Name = "DestinationDrive";
            // 
            // EfiDrive
            // 
            resources.ApplyResources(EfiDrive, "EfiDrive");
            EfiDrive.DropDownStyle = ComboBoxStyle.DropDownList;
            EfiDrive.FormattingEnabled = true;
            EfiDrive.Name = "EfiDrive";
            // 
            // ImageList
            // 
            ImageList.AllowUserToAddRows = false;
            ImageList.AllowUserToDeleteRows = false;
            ImageList.AllowUserToResizeColumns = false;
            ImageList.AllowUserToResizeRows = false;
            ImageList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ImageList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            ImageList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(ImageList, "ImageList");
            ImageList.Name = "ImageList";
            ImageList.ReadOnly = true;
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
            DiskList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutTheProgramToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // aboutTheProgramToolStripMenuItem
            // 
            aboutTheProgramToolStripMenuItem.Name = "aboutTheProgramToolStripMenuItem";
            resources.ApplyResources(aboutTheProgramToolStripMenuItem, "aboutTheProgramToolStripMenuItem");
            aboutTheProgramToolStripMenuItem.Click += AboutWindow_Click;
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(InstallButton);
            Controls.Add(ImageList);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(DiskList);
            Controls.Add(MenuBar);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainWindow";
            Load += MainWindow_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowsEditionIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImageList).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskList).EndInit();
            MenuBar.ResumeLayout(false);
            MenuBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView DiskList;
        private MenuStrip MenuBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label DestinationDriveLabel;
        private Label EfiDriveLabel;
        private Button InstallButton;
        private DataGridView ImageList;
        private TableLayoutPanel tableLayoutPanel1;
        private NumericUpDown WindowsEditionIndex;
        private Label WindowsEditionIndexLabel;
        private Button ChooseISOImage;
        private Label ImageFilePath;
        private Label DiskNumberLabel;
        private NumericUpDown DiskNumber;
        private Label ImageFileLabel;
        private ToolStripMenuItem aboutTheProgramToolStripMenuItem;
        private ComboBox DestinationDrive;
        private ComboBox EfiDrive;
    }
}
