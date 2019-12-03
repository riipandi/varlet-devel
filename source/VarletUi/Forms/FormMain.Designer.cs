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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPhpVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbHttpd = new System.Windows.Forms.CheckBox();
            this.cbMailhog = new System.Windows.Forms.CheckBox();
            this.btnStartService = new System.Windows.Forms.Button();
            this.txtPortHttp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPortMailhog = new System.Windows.Forms.TextBox();
            this.cbHttpSSL = new System.Windows.Forms.CheckBox();
            this.btnPreference = new System.Windows.Forms.Button();
            this.pictStatusHttpd = new System.Windows.Forms.PictureBox();
            this.pictStatusMailhog = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictStatusHttpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictStatusMailhog)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VarletUi.Properties.Resources.varlet_2;
            this.pictureBox1.InitialImage = global::VarletUi.Properties.Resources.varlet_2;
            this.pictureBox1.Location = new System.Drawing.Point(25, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "HTTP Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(247, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mailhog SMTP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPreference);
            this.groupBox1.Controls.Add(this.cbHttpSSL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPortMailhog);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPortHttp);
            this.groupBox1.Controls.Add(this.btnStartService);
            this.groupBox1.Controls.Add(this.cbMailhog);
            this.groupBox1.Controls.Add(this.cbHttpd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbPhpVersion);
            this.groupBox1.Location = new System.Drawing.Point(25, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 260);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // cbPhpVersion
            // 
            this.cbPhpVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhpVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPhpVersion.FormattingEnabled = true;
            this.cbPhpVersion.Location = new System.Drawing.Point(193, 30);
            this.cbPhpVersion.Name = "cbPhpVersion";
            this.cbPhpVersion.Size = new System.Drawing.Size(132, 25);
            this.cbPhpVersion.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "PHP Version";
            // 
            // cbHttpd
            // 
            this.cbHttpd.AutoSize = true;
            this.cbHttpd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHttpd.Location = new System.Drawing.Point(22, 93);
            this.cbHttpd.Name = "cbHttpd";
            this.cbHttpd.Size = new System.Drawing.Size(139, 21);
            this.cbHttpd.TabIndex = 12;
            this.cbHttpd.Text = "Web Server Service";
            this.cbHttpd.UseVisualStyleBackColor = true;
            // 
            // cbMailhog
            // 
            this.cbMailhog.AutoSize = true;
            this.cbMailhog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMailhog.Location = new System.Drawing.Point(22, 131);
            this.cbMailhog.Name = "cbMailhog";
            this.cbMailhog.Size = new System.Drawing.Size(157, 21);
            this.cbMailhog.TabIndex = 13;
            this.cbMailhog.Text = "Mailhog SMTP Service";
            this.cbMailhog.UseVisualStyleBackColor = true;
            // 
            // btnStartService
            // 
            this.btnStartService.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartService.Location = new System.Drawing.Point(22, 186);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(143, 42);
            this.btnStartService.TabIndex = 14;
            this.btnStartService.Text = "Start &Services";
            this.btnStartService.UseVisualStyleBackColor = true;
            // 
            // txtPortHttp
            // 
            this.txtPortHttp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPortHttp.Location = new System.Drawing.Point(229, 90);
            this.txtPortHttp.Name = "txtPortHttp";
            this.txtPortHttp.Size = new System.Drawing.Size(54, 22);
            this.txtPortHttp.TabIndex = 15;
            this.txtPortHttp.Text = "80";
            this.txtPortHttp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(189, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(190, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Port:";
            // 
            // txtPortMailhog
            // 
            this.txtPortMailhog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPortMailhog.Location = new System.Drawing.Point(230, 130);
            this.txtPortMailhog.Name = "txtPortMailhog";
            this.txtPortMailhog.Size = new System.Drawing.Size(54, 22);
            this.txtPortMailhog.TabIndex = 17;
            this.txtPortMailhog.Text = "8025";
            this.txtPortMailhog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbHttpSSL
            // 
            this.cbHttpSSL.AutoSize = true;
            this.cbHttpSSL.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHttpSSL.Location = new System.Drawing.Point(291, 91);
            this.cbHttpSSL.Name = "cbHttpSSL";
            this.cbHttpSSL.Size = new System.Drawing.Size(47, 21);
            this.cbHttpSSL.TabIndex = 19;
            this.cbHttpSSL.Text = "SSL";
            this.cbHttpSSL.UseVisualStyleBackColor = true;
            // 
            // btnPreference
            // 
            this.btnPreference.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreference.Location = new System.Drawing.Point(188, 186);
            this.btnPreference.Name = "btnPreference";
            this.btnPreference.Size = new System.Drawing.Size(143, 42);
            this.btnPreference.TabIndex = 20;
            this.btnPreference.Text = "&Preferences";
            this.btnPreference.UseVisualStyleBackColor = true;
            this.btnPreference.Click += new System.EventHandler(this.btnPreference_Click);
            // 
            // pictStatusHttpd
            // 
            this.pictStatusHttpd.BackColor = System.Drawing.Color.Red;
            this.pictStatusHttpd.Location = new System.Drawing.Point(351, 35);
            this.pictStatusHttpd.Name = "pictStatusHttpd";
            this.pictStatusHttpd.Size = new System.Drawing.Size(30, 15);
            this.pictStatusHttpd.TabIndex = 7;
            this.pictStatusHttpd.TabStop = false;
            // 
            // pictStatusMailhog
            // 
            this.pictStatusMailhog.BackColor = System.Drawing.Color.LimeGreen;
            this.pictStatusMailhog.Location = new System.Drawing.Point(351, 63);
            this.pictStatusMailhog.Name = "pictStatusMailhog";
            this.pictStatusMailhog.Size = new System.Drawing.Size(30, 15);
            this.pictStatusMailhog.TabIndex = 8;
            this.pictStatusMailhog.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 389);
            this.Controls.Add(this.pictStatusMailhog);
            this.Controls.Add(this.pictStatusHttpd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictStatusHttpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictStatusMailhog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPhpVersion;
        private System.Windows.Forms.CheckBox cbMailhog;
        private System.Windows.Forms.CheckBox cbHttpd;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.TextBox txtPortHttp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPortMailhog;
        private System.Windows.Forms.CheckBox cbHttpSSL;
        private System.Windows.Forms.Button btnPreference;
        private System.Windows.Forms.PictureBox pictStatusHttpd;
        private System.Windows.Forms.PictureBox pictStatusMailhog;
    }
}
