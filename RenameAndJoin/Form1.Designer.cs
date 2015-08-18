namespace RenameAndJoin
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOldPCName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewPCName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDomainUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDomainPassword = new System.Windows.Forms.TextBox();
            this.buttonDomain = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxIPAddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxRenamePC = new System.Windows.Forms.CheckBox();
            this.checkBoxAdmin = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current PC Name";
            // 
            // textBoxOldPCName
            // 
            this.textBoxOldPCName.Location = new System.Drawing.Point(12, 31);
            this.textBoxOldPCName.Name = "textBoxOldPCName";
            this.textBoxOldPCName.ReadOnly = true;
            this.textBoxOldPCName.Size = new System.Drawing.Size(100, 20);
            this.textBoxOldPCName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New PC Name";
            // 
            // textBoxNewPCName
            // 
            this.textBoxNewPCName.Enabled = false;
            this.textBoxNewPCName.Location = new System.Drawing.Point(130, 76);
            this.textBoxNewPCName.Name = "textBoxNewPCName";
            this.textBoxNewPCName.Size = new System.Drawing.Size(100, 20);
            this.textBoxNewPCName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username";
            // 
            // textBoxDomainUser
            // 
            this.textBoxDomainUser.Enabled = false;
            this.textBoxDomainUser.Location = new System.Drawing.Point(265, 76);
            this.textBoxDomainUser.Name = "textBoxDomainUser";
            this.textBoxDomainUser.Size = new System.Drawing.Size(100, 20);
            this.textBoxDomainUser.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // textBoxDomainPassword
            // 
            this.textBoxDomainPassword.Enabled = false;
            this.textBoxDomainPassword.Location = new System.Drawing.Point(396, 76);
            this.textBoxDomainPassword.Name = "textBoxDomainPassword";
            this.textBoxDomainPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxDomainPassword.TabIndex = 7;
            // 
            // buttonDomain
            // 
            this.buttonDomain.Location = new System.Drawing.Point(396, 117);
            this.buttonDomain.Name = "buttonDomain";
            this.buttonDomain.Size = new System.Drawing.Size(100, 23);
            this.buttonDomain.TabIndex = 9;
            this.buttonDomain.Text = "Go!";
            this.buttonDomain.UseVisualStyleBackColor = true;
            this.buttonDomain.Click += new System.EventHandler(this.buttonDomain_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(12, 146);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(484, 122);
            this.textBoxOutput.TabIndex = 10;
            this.textBoxOutput.TabStop = false;
            this.textBoxOutput.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "IP Address";
            // 
            // textBoxIPAddr
            // 
            this.textBoxIPAddr.Location = new System.Drawing.Point(130, 31);
            this.textBoxIPAddr.Name = "textBoxIPAddr";
            this.textBoxIPAddr.ReadOnly = true;
            this.textBoxIPAddr.Size = new System.Drawing.Size(100, 20);
            this.textBoxIPAddr.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 26);
            this.label6.TabIndex = 13;
            this.label6.Text = "PC name should match the sticker on the PC\r\nFor example:  ABC12345";
            // 
            // checkBoxRenamePC
            // 
            this.checkBoxRenamePC.AutoSize = true;
            this.checkBoxRenamePC.Location = new System.Drawing.Point(12, 76);
            this.checkBoxRenamePC.Name = "checkBoxRenamePC";
            this.checkBoxRenamePC.Size = new System.Drawing.Size(89, 17);
            this.checkBoxRenamePC.TabIndex = 15;
            this.checkBoxRenamePC.Text = "Rename PC?";
            this.checkBoxRenamePC.UseVisualStyleBackColor = true;
            this.checkBoxRenamePC.CheckedChanged += new System.EventHandler(this.checkBoxRenamePC_CheckedChanged);
            // 
            // checkBoxAdmin
            // 
            this.checkBoxAdmin.AutoSize = true;
            this.checkBoxAdmin.Location = new System.Drawing.Point(265, 31);
            this.checkBoxAdmin.Name = "checkBoxAdmin";
            this.checkBoxAdmin.Size = new System.Drawing.Size(101, 17);
            this.checkBoxAdmin.TabIndex = 16;
            this.checkBoxAdmin.Text = "Admin Use Only";
            this.checkBoxAdmin.UseVisualStyleBackColor = true;
            this.checkBoxAdmin.CheckedChanged += new System.EventHandler(this.checkBoxAdmin_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 280);
            this.Controls.Add(this.checkBoxAdmin);
            this.Controls.Add(this.checkBoxRenamePC);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxIPAddr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonDomain);
            this.Controls.Add(this.textBoxDomainPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDomainUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNewPCName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOldPCName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Join and Rename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOldPCName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNewPCName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDomainUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDomainPassword;
        private System.Windows.Forms.Button buttonDomain;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxIPAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxRenamePC;
		private System.Windows.Forms.CheckBox checkBoxAdmin;
    }
}

