namespace VarletUi
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnServices = new System.Windows.Forms.Button();
            this.btnTerminal = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLogfileSmtp = new System.Windows.Forms.Label();
            this.lblReloadSmtp = new System.Windows.Forms.Label();
            this.lblLogfileHttpd = new System.Windows.Forms.Label();
            this.lblReloadHttpd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictStatusSmtp = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictStatusHttpd = new System.Windows.Forms.PictureBox();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblHostFile = new System.Windows.Forms.Label();
            this.comboPhpVersion = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblPhpIni = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictStatusSmtp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictStatusHttpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnServices
            // 
            this.btnServices.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.btnServices.Location = new System.Drawing.Point(22, 363);
            this.btnServices.Name = "btnServices";
            this.btnServices.Size = new System.Drawing.Size(157, 39);
            this.btnServices.TabIndex = 0;
            this.btnServices.Text = "Start Services";
            this.btnServices.UseVisualStyleBackColor = true;
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // btnTerminal
            // 
            this.btnTerminal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.btnTerminal.Location = new System.Drawing.Point(197, 363);
            this.btnTerminal.Name = "btnTerminal";
            this.btnTerminal.Size = new System.Drawing.Size(157, 39);
            this.btnTerminal.TabIndex = 1;
            this.btnTerminal.Text = "&Terminal";
            this.btnTerminal.UseVisualStyleBackColor = true;
            this.btnTerminal.Click += new System.EventHandler(this.btnTerminal_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLogfileSmtp);
            this.groupBox1.Controls.Add(this.lblReloadSmtp);
            this.groupBox1.Controls.Add(this.lblLogfileHttpd);
            this.groupBox1.Controls.Add(this.lblReloadHttpd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictStatusSmtp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictStatusHttpd);
            this.groupBox1.Location = new System.Drawing.Point(22, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Services Status";
            // 
            // lblLogfileSmtp
            // 
            this.lblLogfileSmtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogfileSmtp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblLogfileSmtp.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblLogfileSmtp.Location = new System.Drawing.Point(251, 97);
            this.lblLogfileSmtp.Name = "lblLogfileSmtp";
            this.lblLogfileSmtp.Size = new System.Drawing.Size(58, 23);
            this.lblLogfileSmtp.TabIndex = 15;
            this.lblLogfileSmtp.Text = "Log file";
            this.lblLogfileSmtp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLogfileSmtp.Click += new System.EventHandler(this.lblLogfileSmtp_Click);
            // 
            // lblReloadSmtp
            // 
            this.lblReloadSmtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReloadSmtp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblReloadSmtp.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblReloadSmtp.Location = new System.Drawing.Point(187, 97);
            this.lblReloadSmtp.Name = "lblReloadSmtp";
            this.lblReloadSmtp.Size = new System.Drawing.Size(58, 23);
            this.lblReloadSmtp.TabIndex = 14;
            this.lblReloadSmtp.Text = "Reload";
            this.lblReloadSmtp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblReloadSmtp.Click += new System.EventHandler(this.lblReloadSmtp_Click);
            // 
            // lblLogfileHttpd
            // 
            this.lblLogfileHttpd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogfileHttpd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblLogfileHttpd.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblLogfileHttpd.Location = new System.Drawing.Point(251, 51);
            this.lblLogfileHttpd.Name = "lblLogfileHttpd";
            this.lblLogfileHttpd.Size = new System.Drawing.Size(58, 23);
            this.lblLogfileHttpd.TabIndex = 13;
            this.lblLogfileHttpd.Text = "Log file";
            this.lblLogfileHttpd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLogfileHttpd.Click += new System.EventHandler(this.lblLogfileHttpd_Click);
            // 
            // lblReloadHttpd
            // 
            this.lblReloadHttpd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReloadHttpd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblReloadHttpd.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblReloadHttpd.Location = new System.Drawing.Point(187, 51);
            this.lblReloadHttpd.Name = "lblReloadHttpd";
            this.lblReloadHttpd.Size = new System.Drawing.Size(58, 23);
            this.lblReloadHttpd.TabIndex = 12;
            this.lblReloadHttpd.Text = "Reload";
            this.lblReloadHttpd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblReloadHttpd.Click += new System.EventHandler(this.lblReloadHttpd_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "SMTP Server";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictStatusSmtp
            // 
            this.pictStatusSmtp.BackColor = System.Drawing.Color.SlateGray;
            this.pictStatusSmtp.Location = new System.Drawing.Point(131, 97);
            this.pictStatusSmtp.Name = "pictStatusSmtp";
            this.pictStatusSmtp.Size = new System.Drawing.Size(27, 23);
            this.pictStatusSmtp.TabIndex = 10;
            this.pictStatusSmtp.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "Web Server";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictStatusHttpd
            // 
            this.pictStatusHttpd.BackColor = System.Drawing.Color.SlateGray;
            this.pictStatusHttpd.Location = new System.Drawing.Point(131, 51);
            this.pictStatusHttpd.Name = "pictStatusHttpd";
            this.pictStatusHttpd.Size = new System.Drawing.Size(27, 23);
            this.pictStatusHttpd.TabIndex = 8;
            this.pictStatusHttpd.TabStop = false;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = ((System.Drawing.Image) (resources.GetObject("pictureBoxIcon.Image")));
            this.pictureBoxIcon.Location = new System.Drawing.Point(22, 20);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(212, 87);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxIcon.TabIndex = 3;
            this.pictureBoxIcon.TabStop = false;
            // 
            // lblAbout
            // 
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblAbout.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblAbout.Location = new System.Drawing.Point(254, 20);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(100, 23);
            this.lblAbout.TabIndex = 10;
            this.lblAbout.Text = "&About";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // lblHostFile
            // 
            this.lblHostFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHostFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblHostFile.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblHostFile.Location = new System.Drawing.Point(254, 47);
            this.lblHostFile.Name = "lblHostFile";
            this.lblHostFile.Size = new System.Drawing.Size(100, 23);
            this.lblHostFile.TabIndex = 11;
            this.lblHostFile.Text = "&Hosts File";
            this.lblHostFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHostFile.Click += new System.EventHandler(this.lblHostFile_Click);
            // 
            // comboPhpVersion
            // 
            this.comboPhpVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPhpVersion.FormattingEnabled = true;
            this.comboPhpVersion.Location = new System.Drawing.Point(188, 132);
            this.comboPhpVersion.Name = "comboPhpVersion";
            this.comboPhpVersion.Size = new System.Drawing.Size(107, 23);
            this.comboPhpVersion.TabIndex = 12;
            this.comboPhpVersion.SelectedIndexChanged +=
                new System.EventHandler(this.comboPhpVersion_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label9.Location = new System.Drawing.Point(22, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 23);
            this.label9.TabIndex = 13;
            this.label9.Text = "Switch PHP Version";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSettings
            // 
            this.lblSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblSettings.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSettings.Location = new System.Drawing.Point(254, 76);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(100, 23);
            this.lblSettings.TabIndex = 14;
            this.lblSettings.Text = "&Preferences";
            this.lblSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSettings.Click += new System.EventHandler(this.lblSettings_Click);
            // 
            // lblPhpIni
            // 
            this.lblPhpIni.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPhpIni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblPhpIni.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPhpIni.Location = new System.Drawing.Point(303, 132);
            this.lblPhpIni.Name = "lblPhpIni";
            this.lblPhpIni.Size = new System.Drawing.Size(52, 23);
            this.lblPhpIni.TabIndex = 15;
            this.lblPhpIni.Text = "php.ini";
            this.lblPhpIni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPhpIni.Click += new System.EventHandler(this.lblPhpIni_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 427);
            this.Controls.Add(this.lblPhpIni);
            this.Controls.Add(this.lblSettings);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboPhpVersion);
            this.Controls.Add(this.lblHostFile);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTerminal);
            this.Controls.Add(this.btnServices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.pictStatusSmtp)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictStatusHttpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnTerminal;
        private System.Windows.Forms.Button btnServices;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboPhpVersion;
        private System.Windows.Forms.Label lblLogfileSmtp;
        private System.Windows.Forms.Label lblReloadSmtp;
        private System.Windows.Forms.Label lblLogfileHttpd;
        private System.Windows.Forms.Label lblReloadHttpd;
        private System.Windows.Forms.Label lblHostFile;
        private System.Windows.Forms.PictureBox pictStatusSmtp;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.PictureBox pictStatusHttpd;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label lblPhpIni;
    }
}
