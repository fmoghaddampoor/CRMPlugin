namespace ConfigurationGenerator
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyStatusBar = new System.Windows.Forms.StatusStrip();
            this.MyStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TCControl = new System.Windows.Forms.TabControl();
            this.tpCrmLogin = new System.Windows.Forms.TabPage();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.tpData = new System.Windows.Forms.TabPage();
            this.lblDataDescription = new System.Windows.Forms.Label();
            this.dgvCSVData = new System.Windows.Forms.DataGridView();
            this.tpSetting = new System.Windows.Forms.TabPage();
            this.cmbEntityLogicalNames = new System.Windows.Forms.ComboBox();
            this.lblDataEntityLogicalName = new System.Windows.Forms.Label();
            this.lblSetting = new System.Windows.Forms.Label();
            this.dgvSetting = new System.Windows.Forms.DataGridView();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.rtbConfiguration = new System.Windows.Forms.RichTextBox();
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.MyStatusBar.SuspendLayout();
            this.TCControl.SuspendLayout();
            this.tpCrmLogin.SuspendLayout();
            this.tpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSVData)).BeginInit();
            this.tpSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetting)).BeginInit();
            this.tpConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // MyStatusBar
            // 
            this.MyStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MyStatusLabel});
            this.MyStatusBar.Location = new System.Drawing.Point(0, 539);
            this.MyStatusBar.Name = "MyStatusBar";
            this.MyStatusBar.Size = new System.Drawing.Size(784, 22);
            this.MyStatusBar.TabIndex = 2;
            this.MyStatusBar.Text = "statusStrip1";
            // 
            // MyStatusLabel
            // 
            this.MyStatusLabel.Name = "MyStatusLabel";
            this.MyStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.MyStatusLabel.Text = "Ready";
            // 
            // TCControl
            // 
            this.TCControl.Controls.Add(this.tpCrmLogin);
            this.TCControl.Controls.Add(this.tpData);
            this.TCControl.Controls.Add(this.tpSetting);
            this.TCControl.Controls.Add(this.tpConfig);
            this.TCControl.Controls.Add(this.tpUpload);
            this.TCControl.Location = new System.Drawing.Point(0, 27);
            this.TCControl.Name = "TCControl";
            this.TCControl.SelectedIndex = 0;
            this.TCControl.Size = new System.Drawing.Size(784, 480);
            this.TCControl.TabIndex = 3;
            // 
            // tpCrmLogin
            // 
            this.tpCrmLogin.Controls.Add(this.btnLogin);
            this.tpCrmLogin.Controls.Add(this.btnShowPassword);
            this.tpCrmLogin.Controls.Add(this.lblPassword);
            this.tpCrmLogin.Controls.Add(this.txtPassword);
            this.tpCrmLogin.Controls.Add(this.lblUser);
            this.tpCrmLogin.Controls.Add(this.txtUser);
            this.tpCrmLogin.Controls.Add(this.lblURL);
            this.tpCrmLogin.Controls.Add(this.txtURL);
            this.tpCrmLogin.Location = new System.Drawing.Point(4, 22);
            this.tpCrmLogin.Name = "tpCrmLogin";
            this.tpCrmLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tpCrmLogin.Size = new System.Drawing.Size(776, 454);
            this.tpCrmLogin.TabIndex = 3;
            this.tpCrmLogin.Text = "CRM Login";
            this.tpCrmLogin.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(665, 83);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.BackgroundImage = global::ConfigurationGenerator.Properties.Resources.ShowPassword;
            this.btnShowPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPassword.Location = new System.Drawing.Point(747, 54);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(23, 23);
            this.btnShowPassword.TabIndex = 6;
            this.btnShowPassword.UseVisualStyleBackColor = true;
            this.btnShowPassword.Click += new System.EventHandler(this.btnShowPassword_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(64, 56);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(677, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(8, 33);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "User:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(64, 30);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(677, 20);
            this.txtUser.TabIndex = 2;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(8, 7);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(32, 13);
            this.lblURL.TabIndex = 1;
            this.lblURL.Text = "URL:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(64, 4);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(677, 20);
            this.txtURL.TabIndex = 0;
            // 
            // tpData
            // 
            this.tpData.Controls.Add(this.lblDataDescription);
            this.tpData.Controls.Add(this.dgvCSVData);
            this.tpData.Location = new System.Drawing.Point(4, 22);
            this.tpData.Name = "tpData";
            this.tpData.Padding = new System.Windows.Forms.Padding(3);
            this.tpData.Size = new System.Drawing.Size(776, 454);
            this.tpData.TabIndex = 0;
            this.tpData.Text = "Data";
            this.tpData.UseVisualStyleBackColor = true;
            // 
            // lblDataDescription
            // 
            this.lblDataDescription.AutoSize = true;
            this.lblDataDescription.Location = new System.Drawing.Point(0, 3);
            this.lblDataDescription.Name = "lblDataDescription";
            this.lblDataDescription.Size = new System.Drawing.Size(55, 13);
            this.lblDataDescription.TabIndex = 1;
            this.lblDataDescription.Text = "CSV data:";
            // 
            // dgvCSVData
            // 
            this.dgvCSVData.AllowUserToAddRows = false;
            this.dgvCSVData.AllowUserToDeleteRows = false;
            this.dgvCSVData.AllowUserToResizeColumns = false;
            this.dgvCSVData.AllowUserToResizeRows = false;
            this.dgvCSVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCSVData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCSVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCSVData.Location = new System.Drawing.Point(3, 19);
            this.dgvCSVData.Name = "dgvCSVData";
            this.dgvCSVData.ReadOnly = true;
            this.dgvCSVData.RowHeadersVisible = false;
            this.dgvCSVData.Size = new System.Drawing.Size(770, 429);
            this.dgvCSVData.TabIndex = 0;
            // 
            // tpSetting
            // 
            this.tpSetting.Controls.Add(this.cmbEntityLogicalNames);
            this.tpSetting.Controls.Add(this.lblDataEntityLogicalName);
            this.tpSetting.Controls.Add(this.lblSetting);
            this.tpSetting.Controls.Add(this.dgvSetting);
            this.tpSetting.Location = new System.Drawing.Point(4, 22);
            this.tpSetting.Name = "tpSetting";
            this.tpSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpSetting.Size = new System.Drawing.Size(776, 454);
            this.tpSetting.TabIndex = 1;
            this.tpSetting.Text = "Setting";
            this.tpSetting.UseVisualStyleBackColor = true;
            // 
            // cmbEntityLogicalNames
            // 
            this.cmbEntityLogicalNames.FormattingEnabled = true;
            this.cmbEntityLogicalNames.Location = new System.Drawing.Point(139, 4);
            this.cmbEntityLogicalNames.Name = "cmbEntityLogicalNames";
            this.cmbEntityLogicalNames.Size = new System.Drawing.Size(631, 21);
            this.cmbEntityLogicalNames.TabIndex = 5;
            // 
            // lblDataEntityLogicalName
            // 
            this.lblDataEntityLogicalName.AutoSize = true;
            this.lblDataEntityLogicalName.Location = new System.Drawing.Point(3, 7);
            this.lblDataEntityLogicalName.Name = "lblDataEntityLogicalName";
            this.lblDataEntityLogicalName.Size = new System.Drawing.Size(123, 13);
            this.lblDataEntityLogicalName.TabIndex = 4;
            this.lblDataEntityLogicalName.Text = "Data entity logical name:";
            // 
            // lblSetting
            // 
            this.lblSetting.AutoSize = true;
            this.lblSetting.Location = new System.Drawing.Point(3, 29);
            this.lblSetting.Name = "lblSetting";
            this.lblSetting.Size = new System.Drawing.Size(70, 13);
            this.lblSetting.TabIndex = 3;
            this.lblSetting.Text = "CSV settings:";
            // 
            // dgvSetting
            // 
            this.dgvSetting.AllowUserToAddRows = false;
            this.dgvSetting.AllowUserToDeleteRows = false;
            this.dgvSetting.AllowUserToResizeColumns = false;
            this.dgvSetting.AllowUserToResizeRows = false;
            this.dgvSetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSetting.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetting.Location = new System.Drawing.Point(3, 45);
            this.dgvSetting.Name = "dgvSetting";
            this.dgvSetting.RowHeadersVisible = false;
            this.dgvSetting.Size = new System.Drawing.Size(770, 403);
            this.dgvSetting.TabIndex = 2;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.rtbConfiguration);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(776, 454);
            this.tpConfig.TabIndex = 2;
            this.tpConfig.Text = "Configuration";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // rtbConfiguration
            // 
            this.rtbConfiguration.Location = new System.Drawing.Point(9, 7);
            this.rtbConfiguration.Name = "rtbConfiguration";
            this.rtbConfiguration.Size = new System.Drawing.Size(759, 441);
            this.rtbConfiguration.TabIndex = 0;
            this.rtbConfiguration.Text = "";
            // 
            // tpUpload
            // 
            this.tpUpload.Location = new System.Drawing.Point(4, 22);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpload.Size = new System.Drawing.Size(776, 454);
            this.tpUpload.TabIndex = 4;
            this.tpUpload.Text = "Upload";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(705, 513);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(624, 513);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.TCControl);
            this.Controls.Add(this.MyStatusBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration Generator";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MyStatusBar.ResumeLayout(false);
            this.MyStatusBar.PerformLayout();
            this.TCControl.ResumeLayout(false);
            this.tpCrmLogin.ResumeLayout(false);
            this.tpCrmLogin.PerformLayout();
            this.tpData.ResumeLayout(false);
            this.tpData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSVData)).EndInit();
            this.tpSetting.ResumeLayout(false);
            this.tpSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetting)).EndInit();
            this.tpConfig.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MyStatusBar;
        private System.Windows.Forms.TabControl TCControl;
        private System.Windows.Forms.TabPage tpData;
        private System.Windows.Forms.TabPage tpSetting;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Label lblDataDescription;
        private System.Windows.Forms.DataGridView dgvCSVData;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.ToolStripStatusLabel MyStatusLabel;
        private System.Windows.Forms.Label lblSetting;
        private System.Windows.Forms.DataGridView dgvSetting;
        private System.Windows.Forms.ComboBox cmbEntityLogicalNames;
        private System.Windows.Forms.Label lblDataEntityLogicalName;
        private System.Windows.Forms.TabPage tpCrmLogin;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.RichTextBox rtbConfiguration;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}

