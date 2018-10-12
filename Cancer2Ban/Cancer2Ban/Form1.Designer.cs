namespace Cancer2Ban
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.metroToggle1 = new MetroFramework.Controls.MetroToggle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroButton_Apply = new MetroFramework.Controls.MetroButton();
            this.metroButton_Edit = new MetroFramework.Controls.MetroButton();
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.metroTextBox_APIKEY = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.metroCheckBox2 = new MetroFramework.Controls.MetroCheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_BanDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.metroButton_APPLYGLOBAL = new MetroFramework.Controls.MetroButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_AttemptObserve = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_BanAttempts = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BanDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AttemptObserve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BanAttempts)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(27, 270);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1095, 112);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // metroToggle1
            // 
            this.metroToggle1.AutoSize = true;
            this.metroToggle1.DisplayStatus = false;
            this.metroToggle1.Location = new System.Drawing.Point(321, 70);
            this.metroToggle1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroToggle1.Name = "metroToggle1";
            this.metroToggle1.Size = new System.Drawing.Size(50, 15);
            this.metroToggle1.TabIndex = 1;
            this.metroToggle1.Text = "Aus";
            this.metroToggle1.UseSelectable = true;
            this.metroToggle1.CheckedChanged += new System.EventHandler(this.metroToggle1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 11);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auto-Report bans on AbuseIPDB.com";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroButton_Apply);
            this.groupBox1.Controls.Add(this.metroButton_Edit);
            this.groupBox1.Controls.Add(this.metroCheckBox1);
            this.groupBox1.Controls.Add(this.metroTextBox_APIKEY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.metroCheckBox2);
            this.groupBox1.Location = new System.Drawing.Point(26, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 76);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AbuseIPDB - Reporter";
            this.groupBox1.Visible = false;
            // 
            // metroButton_Apply
            // 
            this.metroButton_Apply.Enabled = false;
            this.metroButton_Apply.Location = new System.Drawing.Point(181, 44);
            this.metroButton_Apply.Name = "metroButton_Apply";
            this.metroButton_Apply.Size = new System.Drawing.Size(95, 23);
            this.metroButton_Apply.TabIndex = 4;
            this.metroButton_Apply.Text = "APPLY";
            this.metroButton_Apply.UseSelectable = true;
            this.metroButton_Apply.Click += new System.EventHandler(this.metroButton_Apply_Click);
            // 
            // metroButton_Edit
            // 
            this.metroButton_Edit.Location = new System.Drawing.Point(80, 44);
            this.metroButton_Edit.Name = "metroButton_Edit";
            this.metroButton_Edit.Size = new System.Drawing.Size(95, 23);
            this.metroButton_Edit.TabIndex = 4;
            this.metroButton_Edit.Text = "EDIT";
            this.metroButton_Edit.UseSelectable = true;
            this.metroButton_Edit.Click += new System.EventHandler(this.metroButton_Edit_Click);
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.Enabled = false;
            this.metroCheckBox1.Location = new System.Drawing.Point(313, 46);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(93, 15);
            this.metroCheckBox1.TabIndex = 5;
            this.metroCheckBox1.Text = "Save API-KEY";
            this.metroCheckBox1.UseSelectable = true;
            this.metroCheckBox1.CheckedChanged += new System.EventHandler(this.metroCheckBox1_CheckedChanged);
            // 
            // metroTextBox_APIKEY
            // 
            // 
            // 
            // 
            this.metroTextBox_APIKEY.CustomButton.Image = null;
            this.metroTextBox_APIKEY.CustomButton.Location = new System.Drawing.Point(304, 1);
            this.metroTextBox_APIKEY.CustomButton.Name = "";
            this.metroTextBox_APIKEY.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox_APIKEY.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_APIKEY.CustomButton.TabIndex = 1;
            this.metroTextBox_APIKEY.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_APIKEY.CustomButton.UseSelectable = true;
            this.metroTextBox_APIKEY.CustomButton.Visible = false;
            this.metroTextBox_APIKEY.Lines = new string[0];
            this.metroTextBox_APIKEY.Location = new System.Drawing.Point(80, 17);
            this.metroTextBox_APIKEY.MaxLength = 32767;
            this.metroTextBox_APIKEY.Name = "metroTextBox_APIKEY";
            this.metroTextBox_APIKEY.PasswordChar = '*';
            this.metroTextBox_APIKEY.PromptText = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            this.metroTextBox_APIKEY.ReadOnly = true;
            this.metroTextBox_APIKEY.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_APIKEY.SelectedText = "";
            this.metroTextBox_APIKEY.SelectionLength = 0;
            this.metroTextBox_APIKEY.SelectionStart = 0;
            this.metroTextBox_APIKEY.ShortcutsEnabled = true;
            this.metroTextBox_APIKEY.Size = new System.Drawing.Size(326, 23);
            this.metroTextBox_APIKEY.TabIndex = 4;
            this.metroTextBox_APIKEY.UseSelectable = true;
            this.metroTextBox_APIKEY.WaterMark = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            this.metroTextBox_APIKEY.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_APIKEY.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 11);
            this.label2.TabIndex = 4;
            this.label2.Text = "API-KEY: ";
            // 
            // metroCheckBox2
            // 
            this.metroCheckBox2.AutoSize = true;
            this.metroCheckBox2.Location = new System.Drawing.Point(415, 23);
            this.metroCheckBox2.Name = "metroCheckBox2";
            this.metroCheckBox2.Size = new System.Drawing.Size(72, 15);
            this.metroCheckBox2.TabIndex = 6;
            this.metroCheckBox2.Text = "show key";
            this.metroCheckBox2.UseSelectable = true;
            this.metroCheckBox2.CheckedChanged += new System.EventHandler(this.metroCheckBox2_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown_BanDuration);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.metroButton_APPLYGLOBAL);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDown_AttemptObserve);
            this.groupBox2.Controls.Add(this.numericUpDown_BanAttempts);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(572, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 132);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Global Settings";
            // 
            // numericUpDown_BanDuration
            // 
            this.numericUpDown_BanDuration.Location = new System.Drawing.Point(301, 69);
            this.numericUpDown_BanDuration.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.numericUpDown_BanDuration.Name = "numericUpDown_BanDuration";
            this.numericUpDown_BanDuration.Size = new System.Drawing.Size(103, 18);
            this.numericUpDown_BanDuration.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 11);
            this.label5.TabIndex = 5;
            this.label5.Text = "Duration of ban (in minutes)";
            // 
            // metroButton_APPLYGLOBAL
            // 
            this.metroButton_APPLYGLOBAL.Location = new System.Drawing.Point(329, 103);
            this.metroButton_APPLYGLOBAL.Name = "metroButton_APPLYGLOBAL";
            this.metroButton_APPLYGLOBAL.Size = new System.Drawing.Size(75, 23);
            this.metroButton_APPLYGLOBAL.TabIndex = 4;
            this.metroButton_APPLYGLOBAL.Text = "APPLY";
            this.metroButton_APPLYGLOBAL.UseSelectable = true;
            this.metroButton_APPLYGLOBAL.Click += new System.EventHandler(this.metroButton_APPLYGLOBAL_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 11);
            this.label4.TabIndex = 3;
            this.label4.Text = "Observe # minutes for same attempt";
            // 
            // numericUpDown_AttemptObserve
            // 
            this.numericUpDown_AttemptObserve.Location = new System.Drawing.Point(301, 47);
            this.numericUpDown_AttemptObserve.Name = "numericUpDown_AttemptObserve";
            this.numericUpDown_AttemptObserve.Size = new System.Drawing.Size(103, 18);
            this.numericUpDown_AttemptObserve.TabIndex = 2;
            // 
            // numericUpDown_BanAttempts
            // 
            this.numericUpDown_BanAttempts.Location = new System.Drawing.Point(301, 25);
            this.numericUpDown_BanAttempts.Name = "numericUpDown_BanAttempts";
            this.numericUpDown_BanAttempts.Size = new System.Drawing.Size(103, 18);
            this.numericUpDown_BanAttempts.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 11);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ban after # attempts: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(476, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 57);
            this.button1.TabIndex = 5;
            this.button1.Text = "START GUARD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 387);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroToggle1);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 17);
            this.Resizable = false;
            this.Text = "Cancer2Ban";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BanDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AttemptObserve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BanAttempts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Controls.MetroButton metroButton_Apply;
        private MetroFramework.Controls.MetroButton metroButton_Edit;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroButton metroButton_APPLYGLOBAL;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public MetroFramework.Controls.MetroTextBox metroTextBox_APIKEY;
        public System.Windows.Forms.NumericUpDown numericUpDown_AttemptObserve;
        public System.Windows.Forms.NumericUpDown numericUpDown_BanAttempts;
        public System.Windows.Forms.NumericUpDown numericUpDown_BanDuration;
        private System.Windows.Forms.Label label5;
        public MetroFramework.Controls.MetroToggle metroToggle1;
    }
}

