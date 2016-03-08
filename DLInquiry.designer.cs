namespace File_Generation_System
{
    partial class frmDLInquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDLInquiry));
            this.lblEnterDL = new System.Windows.Forms.Label();
            this.txtDriversLicense = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblMiscInfo = new System.Windows.Forms.Label();
            this.txtMiscInfo = new System.Windows.Forms.TextBox();
            this.btnWriteFile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.ckbEPNModify = new System.Windows.Forms.CheckBox();
            this.ckbSSInquiry = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpBirthdate = new System.Windows.Forms.DateTimePicker();
            this.lblBirthdate = new System.Windows.Forms.Label();
            this.txtMiddleInitial = new System.Windows.Forms.TextBox();
            this.lblMiddleInitial = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboEPNTransType = new System.Windows.Forms.ComboBox();
            this.lblEPNTranType = new System.Windows.Forms.Label();
            this.ckbDriversLicenseInquiry = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentRequestorCodelbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEnterDL
            // 
            this.lblEnterDL.AutoSize = true;
            this.lblEnterDL.Location = new System.Drawing.Point(13, 13);
            this.lblEnterDL.Name = "lblEnterDL";
            this.lblEnterDL.Size = new System.Drawing.Size(111, 13);
            this.lblEnterDL.TabIndex = 0;
            this.lblEnterDL.Text = "Enter Drivers License:";
            // 
            // txtDriversLicense
            // 
            this.txtDriversLicense.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDriversLicense.Location = new System.Drawing.Point(13, 30);
            this.txtDriversLicense.MaxLength = 8;
            this.txtDriversLicense.Name = "txtDriversLicense";
            this.txtDriversLicense.Size = new System.Drawing.Size(74, 20);
            this.txtDriversLicense.TabIndex = 0;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(13, 57);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(97, 13);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Drivers Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLastName.Location = new System.Drawing.Point(13, 74);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(155, 20);
            this.txtLastName.TabIndex = 1;
            // 
            // lblMiscInfo
            // 
            this.lblMiscInfo.AutoSize = true;
            this.lblMiscInfo.Location = new System.Drawing.Point(13, 145);
            this.lblMiscInfo.Name = "lblMiscInfo";
            this.lblMiscInfo.Size = new System.Drawing.Size(132, 13);
            this.lblMiscInfo.TabIndex = 6;
            this.lblMiscInfo.Text = "Miscellaneous Information:";
            // 
            // txtMiscInfo
            // 
            this.txtMiscInfo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMiscInfo.Location = new System.Drawing.Point(13, 162);
            this.txtMiscInfo.MaxLength = 22;
            this.txtMiscInfo.Name = "txtMiscInfo";
            this.txtMiscInfo.Size = new System.Drawing.Size(155, 20);
            this.txtMiscInfo.TabIndex = 4;
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(12, 281);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(75, 23);
            this.btnWriteFile.TabIndex = 8;
            this.btnWriteFile.Text = "Save";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(93, 281);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit & Create File";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFirstName.Location = new System.Drawing.Point(174, 74);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(147, 20);
            this.txtFirstName.TabIndex = 2;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(171, 57);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(60, 13);
            this.lblFirstName.TabIndex = 13;
            this.lblFirstName.Text = "First Name:";
            // 
            // ckbEPNModify
            // 
            this.ckbEPNModify.AutoSize = true;
            this.ckbEPNModify.Location = new System.Drawing.Point(16, 235);
            this.ckbEPNModify.Name = "ckbEPNModify";
            this.ckbEPNModify.Size = new System.Drawing.Size(159, 17);
            this.ckbEPNModify.TabIndex = 6;
            this.ckbEPNModify.Text = "EPN Add, Delete or Change";
            this.ckbEPNModify.UseVisualStyleBackColor = true;
            this.ckbEPNModify.CheckedChanged += new System.EventHandler(this.ckbEPNModify_CheckedChanged);
            // 
            // ckbSSInquiry
            // 
            this.ckbSSInquiry.AutoSize = true;
            this.ckbSSInquiry.Location = new System.Drawing.Point(171, 258);
            this.ckbSSInquiry.Name = "ckbSSInquiry";
            this.ckbSSInquiry.Size = new System.Drawing.Size(139, 17);
            this.ckbSSInquiry.TabIndex = 7;
            this.ckbSSInquiry.Text = "Soundex Search Inquiry";
            this.ckbSSInquiry.UseVisualStyleBackColor = true;
            this.ckbSSInquiry.CheckedChanged += new System.EventHandler(this.ckbSSInquiry_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(223, 281);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Reset Fields";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpBirthdate);
            this.panel1.Controls.Add(this.lblBirthdate);
            this.panel1.Controls.Add(this.txtMiddleInitial);
            this.panel1.Controls.Add(this.lblMiddleInitial);
            this.panel1.Location = new System.Drawing.Point(321, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 133);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            // 
            // dtpBirthdate
            // 
            this.dtpBirthdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthdate.Location = new System.Drawing.Point(63, 105);
            this.dtpBirthdate.Name = "dtpBirthdate";
            this.dtpBirthdate.Size = new System.Drawing.Size(122, 20);
            this.dtpBirthdate.TabIndex = 6;
            // 
            // lblBirthdate
            // 
            this.lblBirthdate.AutoSize = true;
            this.lblBirthdate.Location = new System.Drawing.Point(4, 108);
            this.lblBirthdate.Name = "lblBirthdate";
            this.lblBirthdate.Size = new System.Drawing.Size(52, 13);
            this.lblBirthdate.TabIndex = 2;
            this.lblBirthdate.Text = "Birthdate:";
            // 
            // txtMiddleInitial
            // 
            this.txtMiddleInitial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMiddleInitial.Location = new System.Drawing.Point(4, 61);
            this.txtMiddleInitial.MaxLength = 1;
            this.txtMiddleInitial.Name = "txtMiddleInitial";
            this.txtMiddleInitial.Size = new System.Drawing.Size(52, 20);
            this.txtMiddleInitial.TabIndex = 1;
            // 
            // lblMiddleInitial
            // 
            this.lblMiddleInitial.AutoSize = true;
            this.lblMiddleInitial.Location = new System.Drawing.Point(4, 44);
            this.lblMiddleInitial.Name = "lblMiddleInitial";
            this.lblMiddleInitial.Size = new System.Drawing.Size(68, 13);
            this.lblMiddleInitial.TabIndex = 0;
            this.lblMiddleInitial.Text = "Middle Initial:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboEPNTransType);
            this.panel2.Controls.Add(this.lblEPNTranType);
            this.panel2.Location = new System.Drawing.Point(321, 188);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 101);
            this.panel2.TabIndex = 15;
            this.panel2.Visible = false;
            // 
            // cboEPNTransType
            // 
            this.cboEPNTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEPNTransType.FormattingEnabled = true;
            this.cboEPNTransType.Items.AddRange(new object[] {
            "ADD",
            "DELETE",
            "CHANGE"});
            this.cboEPNTransType.Location = new System.Drawing.Point(4, 27);
            this.cboEPNTransType.Name = "cboEPNTransType";
            this.cboEPNTransType.Size = new System.Drawing.Size(68, 21);
            this.cboEPNTransType.TabIndex = 1;
            // 
            // lblEPNTranType
            // 
            this.lblEPNTranType.AutoSize = true;
            this.lblEPNTranType.Location = new System.Drawing.Point(3, 11);
            this.lblEPNTranType.Name = "lblEPNTranType";
            this.lblEPNTranType.Size = new System.Drawing.Size(140, 13);
            this.lblEPNTranType.TabIndex = 0;
            this.lblEPNTranType.Text = "Select EPN transaction type";
            // 
            // ckbDriversLicenseInquiry
            // 
            this.ckbDriversLicenseInquiry.AutoSize = true;
            this.ckbDriversLicenseInquiry.Location = new System.Drawing.Point(16, 215);
            this.ckbDriversLicenseInquiry.Name = "ckbDriversLicenseInquiry";
            this.ckbDriversLicenseInquiry.Size = new System.Drawing.Size(137, 17);
            this.ckbDriversLicenseInquiry.TabIndex = 16;
            this.ckbDriversLicenseInquiry.Text = "Drivers License Record";
            this.ckbDriversLicenseInquiry.UseVisualStyleBackColor = true;
            this.ckbDriversLicenseInquiry.CheckedChanged += new System.EventHandler(this.ckbDriversLicenseInquiry_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Requestor Code Being Used";
            // 
            // currentRequestorCodelbl
            // 
            this.currentRequestorCodelbl.AutoSize = true;
            this.currentRequestorCodelbl.Location = new System.Drawing.Point(16, 125);
            this.currentRequestorCodelbl.Name = "currentRequestorCodelbl";
            this.currentRequestorCodelbl.Size = new System.Drawing.Size(10, 13);
            this.currentRequestorCodelbl.TabIndex = 18;
            this.currentRequestorCodelbl.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Not for use by EPN Accounts";
            // 
            // frmDLInquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 325);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentRequestorCodelbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckbDriversLicenseInquiry);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.ckbSSInquiry);
            this.Controls.Add(this.ckbEPNModify);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnWriteFile);
            this.Controls.Add(this.txtMiscInfo);
            this.Controls.Add(this.lblMiscInfo);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtDriversLicense);
            this.Controls.Add(this.lblEnterDL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDLInquiry";
            this.Text = "Drivers License Inquiry";
            this.Load += new System.EventHandler(this.frmDLInquiry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnterDL;
        private System.Windows.Forms.TextBox txtDriversLicense;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblMiscInfo;
        private System.Windows.Forms.TextBox txtMiscInfo;
        private System.Windows.Forms.Button btnWriteFile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.CheckBox ckbEPNModify;
        private System.Windows.Forms.CheckBox ckbSSInquiry;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMiddleInitial;
        private System.Windows.Forms.Label lblMiddleInitial;
        private System.Windows.Forms.Label lblBirthdate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboEPNTransType;
        private System.Windows.Forms.Label lblEPNTranType;
        private System.Windows.Forms.DateTimePicker dtpBirthdate;
        private System.Windows.Forms.CheckBox ckbDriversLicenseInquiry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentRequestorCodelbl;
        private System.Windows.Forms.Label label2;
    }
}

