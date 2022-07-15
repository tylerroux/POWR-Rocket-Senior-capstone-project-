namespace POWR_Upload_Portal_2
{
    partial class portalFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(portalFrame));
            this.serialConnectionBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.serConnStatusLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.serConnButton = new System.Windows.Forms.Button();
            this.arduinoInfoBox = new System.Windows.Forms.GroupBox();
            this.directoryEntry = new System.Windows.Forms.TextBox();
            this.uploadStatusLabel = new System.Windows.Forms.Label();
            this.dbUploadReading = new System.Windows.Forms.Label();
            this.storageUploadReading = new System.Windows.Forms.Label();
            this.atReading = new System.Windows.Forms.Label();
            this.dbUploadLabel = new System.Windows.Forms.Label();
            this.storageUploadLabel = new System.Windows.Forms.Label();
            this.atLabel = new System.Windows.Forms.Label();
            this.storageUploadButton = new System.Windows.Forms.Button();
            this.dbUploadButton = new System.Windows.Forms.Button();
            this.dbConnBox = new System.Windows.Forms.GroupBox();
            this.tableNameEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordEntry = new System.Windows.Forms.TextBox();
            this.usernameEntry = new System.Windows.Forms.TextBox();
            this.dbNameEntry = new System.Windows.Forms.TextBox();
            this.serverNameEntry = new System.Windows.Forms.TextBox();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.dbNameLabel = new System.Windows.Forms.Label();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.dbConnButton = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.serialConnectionBox.SuspendLayout();
            this.arduinoInfoBox.SuspendLayout();
            this.dbConnBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialConnectionBox
            // 
            this.serialConnectionBox.Controls.Add(this.button1);
            this.serialConnectionBox.Controls.Add(this.serConnStatusLabel);
            this.serialConnectionBox.Controls.Add(this.comboBox1);
            this.serialConnectionBox.Controls.Add(this.serConnButton);
            this.serialConnectionBox.Location = new System.Drawing.Point(18, 12);
            this.serialConnectionBox.Name = "serialConnectionBox";
            this.serialConnectionBox.Size = new System.Drawing.Size(400, 100);
            this.serialConnectionBox.TabIndex = 0;
            this.serialConnectionBox.TabStop = false;
            this.serialConnectionBox.Text = "Serial Connection";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(246, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // serConnStatusLabel
            // 
            this.serConnStatusLabel.AutoSize = true;
            this.serConnStatusLabel.Location = new System.Drawing.Point(247, 69);
            this.serConnStatusLabel.Name = "serConnStatusLabel";
            this.serConnStatusLabel.Size = new System.Drawing.Size(94, 16);
            this.serConnStatusLabel.TabIndex = 2;
            this.serConnStatusLabel.Text = "Not connected";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(119, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // serConnButton
            // 
            this.serConnButton.Location = new System.Drawing.Point(20, 42);
            this.serConnButton.Name = "serConnButton";
            this.serConnButton.Size = new System.Drawing.Size(75, 23);
            this.serConnButton.TabIndex = 0;
            this.serConnButton.Text = "Connect";
            this.serConnButton.UseVisualStyleBackColor = true;
            this.serConnButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // arduinoInfoBox
            // 
            this.arduinoInfoBox.Controls.Add(this.directoryEntry);
            this.arduinoInfoBox.Controls.Add(this.uploadStatusLabel);
            this.arduinoInfoBox.Controls.Add(this.dbUploadReading);
            this.arduinoInfoBox.Controls.Add(this.storageUploadReading);
            this.arduinoInfoBox.Controls.Add(this.atReading);
            this.arduinoInfoBox.Controls.Add(this.dbUploadLabel);
            this.arduinoInfoBox.Controls.Add(this.storageUploadLabel);
            this.arduinoInfoBox.Controls.Add(this.atLabel);
            this.arduinoInfoBox.Controls.Add(this.storageUploadButton);
            this.arduinoInfoBox.Controls.Add(this.dbUploadButton);
            this.arduinoInfoBox.Location = new System.Drawing.Point(18, 118);
            this.arduinoInfoBox.Name = "arduinoInfoBox";
            this.arduinoInfoBox.Size = new System.Drawing.Size(400, 207);
            this.arduinoInfoBox.TabIndex = 1;
            this.arduinoInfoBox.TabStop = false;
            // 
            // directoryEntry
            // 
            this.directoryEntry.Location = new System.Drawing.Point(9, 111);
            this.directoryEntry.Name = "directoryEntry";
            this.directoryEntry.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.directoryEntry.Size = new System.Drawing.Size(331, 22);
            this.directoryEntry.TabIndex = 10;
            this.directoryEntry.Text = "C:\\Users\\sageb\\OneDrive\\Desktop\\School Folder";
            this.directoryEntry.TextChanged += new System.EventHandler(this.directoryEntry_TextChanged);
            // 
            // uploadStatusLabel
            // 
            this.uploadStatusLabel.AutoSize = true;
            this.uploadStatusLabel.Location = new System.Drawing.Point(205, 181);
            this.uploadStatusLabel.Name = "uploadStatusLabel";
            this.uploadStatusLabel.Size = new System.Drawing.Size(70, 16);
            this.uploadStatusLabel.TabIndex = 9;
            this.uploadStatusLabel.Text = "No upload";
            // 
            // dbUploadReading
            // 
            this.dbUploadReading.AutoSize = true;
            this.dbUploadReading.Location = new System.Drawing.Point(167, 83);
            this.dbUploadReading.Name = "dbUploadReading";
            this.dbUploadReading.Size = new System.Drawing.Size(0, 16);
            this.dbUploadReading.TabIndex = 8;
            // 
            // storageUploadReading
            // 
            this.storageUploadReading.AutoSize = true;
            this.storageUploadReading.Location = new System.Drawing.Point(167, 58);
            this.storageUploadReading.Name = "storageUploadReading";
            this.storageUploadReading.Size = new System.Drawing.Size(0, 16);
            this.storageUploadReading.TabIndex = 7;
            // 
            // atReading
            // 
            this.atReading.AutoSize = true;
            this.atReading.Location = new System.Drawing.Point(167, 33);
            this.atReading.Name = "atReading";
            this.atReading.Size = new System.Drawing.Size(0, 16);
            this.atReading.TabIndex = 6;
            // 
            // dbUploadLabel
            // 
            this.dbUploadLabel.AutoSize = true;
            this.dbUploadLabel.Location = new System.Drawing.Point(6, 83);
            this.dbUploadLabel.Name = "dbUploadLabel";
            this.dbUploadLabel.Size = new System.Drawing.Size(141, 16);
            this.dbUploadLabel.TabIndex = 5;
            this.dbUploadLabel.Text = "Last database upload:";
            // 
            // storageUploadLabel
            // 
            this.storageUploadLabel.AutoSize = true;
            this.storageUploadLabel.Location = new System.Drawing.Point(18, 58);
            this.storageUploadLabel.Name = "storageUploadLabel";
            this.storageUploadLabel.Size = new System.Drawing.Size(129, 16);
            this.storageUploadLabel.TabIndex = 4;
            this.storageUploadLabel.Text = "Last storage upload:";
            this.storageUploadLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // atLabel
            // 
            this.atLabel.AutoSize = true;
            this.atLabel.Location = new System.Drawing.Point(124, 33);
            this.atLabel.Name = "atLabel";
            this.atLabel.Size = new System.Drawing.Size(23, 16);
            this.atLabel.TabIndex = 3;
            this.atLabel.Text = "ID:";
            this.atLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // storageUploadButton
            // 
            this.storageUploadButton.Location = new System.Drawing.Point(208, 150);
            this.storageUploadButton.Name = "storageUploadButton";
            this.storageUploadButton.Size = new System.Drawing.Size(132, 23);
            this.storageUploadButton.TabIndex = 2;
            this.storageUploadButton.Text = "Upload to storage";
            this.storageUploadButton.UseVisualStyleBackColor = true;
            this.storageUploadButton.Visible = false;
            this.storageUploadButton.Click += new System.EventHandler(this.storageUploadButton_Click);
            // 
            // dbUploadButton
            // 
            this.dbUploadButton.Location = new System.Drawing.Point(9, 150);
            this.dbUploadButton.Name = "dbUploadButton";
            this.dbUploadButton.Size = new System.Drawing.Size(147, 23);
            this.dbUploadButton.TabIndex = 1;
            this.dbUploadButton.Text = "Upload to database";
            this.dbUploadButton.UseVisualStyleBackColor = true;
            this.dbUploadButton.Visible = false;
            this.dbUploadButton.Click += new System.EventHandler(this.dbUploadButton_Click);
            // 
            // dbConnBox
            // 
            this.dbConnBox.Controls.Add(this.tableNameEntry);
            this.dbConnBox.Controls.Add(this.label1);
            this.dbConnBox.Controls.Add(this.passwordEntry);
            this.dbConnBox.Controls.Add(this.usernameEntry);
            this.dbConnBox.Controls.Add(this.dbNameEntry);
            this.dbConnBox.Controls.Add(this.serverNameEntry);
            this.dbConnBox.Controls.Add(this.connectionStatusLabel);
            this.dbConnBox.Controls.Add(this.passwordLabel);
            this.dbConnBox.Controls.Add(this.usernameLabel);
            this.dbConnBox.Controls.Add(this.dbNameLabel);
            this.dbConnBox.Controls.Add(this.serverNameLabel);
            this.dbConnBox.Controls.Add(this.dbConnButton);
            this.dbConnBox.Location = new System.Drawing.Point(18, 331);
            this.dbConnBox.Name = "dbConnBox";
            this.dbConnBox.Size = new System.Drawing.Size(400, 203);
            this.dbConnBox.TabIndex = 1;
            this.dbConnBox.TabStop = false;
            this.dbConnBox.Text = "Database Connection";
            // 
            // tableNameEntry
            // 
            this.tableNameEntry.Location = new System.Drawing.Point(169, 134);
            this.tableNameEntry.Name = "tableNameEntry";
            this.tableNameEntry.Size = new System.Drawing.Size(171, 22);
            this.tableNameEntry.TabIndex = 16;
            this.tableNameEntry.Text = "sages_demo_table";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Table:";
            // 
            // passwordEntry
            // 
            this.passwordEntry.Location = new System.Drawing.Point(170, 103);
            this.passwordEntry.Name = "passwordEntry";
            this.passwordEntry.Size = new System.Drawing.Size(171, 22);
            this.passwordEntry.TabIndex = 14;
            this.passwordEntry.Text = "uncgcapstone2022!";
            this.passwordEntry.UseSystemPasswordChar = true;
            // 
            // usernameEntry
            // 
            this.usernameEntry.Location = new System.Drawing.Point(170, 75);
            this.usernameEntry.Name = "usernameEntry";
            this.usernameEntry.Size = new System.Drawing.Size(171, 22);
            this.usernameEntry.TabIndex = 13;
            this.usernameEntry.Text = "powrrocket2022";
            // 
            // dbNameEntry
            // 
            this.dbNameEntry.Location = new System.Drawing.Point(170, 46);
            this.dbNameEntry.Name = "dbNameEntry";
            this.dbNameEntry.Size = new System.Drawing.Size(171, 22);
            this.dbNameEntry.TabIndex = 12;
            this.dbNameEntry.Text = "POWR-Rocket_DB";
            // 
            // serverNameEntry
            // 
            this.serverNameEntry.Location = new System.Drawing.Point(170, 18);
            this.serverNameEntry.Name = "serverNameEntry";
            this.serverNameEntry.Size = new System.Drawing.Size(171, 22);
            this.serverNameEntry.TabIndex = 11;
            this.serverNameEntry.Text = "powrrocketserver.database.windows.net";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(247, 174);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(94, 16);
            this.connectionStatusLabel.TabIndex = 10;
            this.connectionStatusLabel.Text = "Not connected";
            this.connectionStatusLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(18, 106);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(70, 16);
            this.passwordLabel.TabIndex = 9;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(18, 77);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(73, 16);
            this.usernameLabel.TabIndex = 8;
            this.usernameLabel.Text = "Username:";
            // 
            // dbNameLabel
            // 
            this.dbNameLabel.AutoSize = true;
            this.dbNameLabel.Location = new System.Drawing.Point(18, 49);
            this.dbNameLabel.Name = "dbNameLabel";
            this.dbNameLabel.Size = new System.Drawing.Size(69, 16);
            this.dbNameLabel.TabIndex = 7;
            this.dbNameLabel.Text = "DB Name:";
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Location = new System.Drawing.Point(17, 21);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(90, 16);
            this.serverNameLabel.TabIndex = 6;
            this.serverNameLabel.Text = "Server Name:";
            // 
            // dbConnButton
            // 
            this.dbConnButton.Location = new System.Drawing.Point(14, 174);
            this.dbConnButton.Name = "dbConnButton";
            this.dbConnButton.Size = new System.Drawing.Size(75, 23);
            this.dbConnButton.TabIndex = 2;
            this.dbConnButton.Text = "Connect";
            this.dbConnButton.UseVisualStyleBackColor = true;
            this.dbConnButton.Click += new System.EventHandler(this.dbConnButton_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.WriteTimeout = 1000;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // portalFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 548);
            this.Controls.Add(this.dbConnBox);
            this.Controls.Add(this.arduinoInfoBox);
            this.Controls.Add(this.serialConnectionBox);
            this.Name = "portalFrame";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "POWR Upload Portal";
            this.serialConnectionBox.ResumeLayout(false);
            this.serialConnectionBox.PerformLayout();
            this.arduinoInfoBox.ResumeLayout(false);
            this.arduinoInfoBox.PerformLayout();
            this.dbConnBox.ResumeLayout(false);
            this.dbConnBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox serialConnectionBox;
        private System.Windows.Forms.GroupBox arduinoInfoBox;
        private System.Windows.Forms.GroupBox dbConnBox;
        private System.Windows.Forms.Button serConnButton;
        private System.Windows.Forms.Button storageUploadButton;
        private System.Windows.Forms.Button dbUploadButton;
        private System.Windows.Forms.Button dbConnButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label dbUploadLabel;
        private System.Windows.Forms.Label storageUploadLabel;
        private System.Windows.Forms.Label atLabel;
        private System.Windows.Forms.Label dbUploadReading;
        private System.Windows.Forms.Label storageUploadReading;
        private System.Windows.Forms.Label atReading;
        private System.Windows.Forms.Label uploadStatusLabel;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label dbNameLabel;
        private System.Windows.Forms.Label serverNameLabel;
        private System.Windows.Forms.Label serConnStatusLabel;
        private System.Windows.Forms.TextBox passwordEntry;
        private System.Windows.Forms.TextBox usernameEntry;
        private System.Windows.Forms.TextBox dbNameEntry;
        private System.Windows.Forms.TextBox serverNameEntry;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox tableNameEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryEntry;
    }
}

