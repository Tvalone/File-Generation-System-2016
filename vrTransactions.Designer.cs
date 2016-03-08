namespace File_Generation_System
{
    partial class vrinquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vrinquiry));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitVRTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelYearTxtBx = new System.Windows.Forms.MaskedTextBox();
            this.vehicleLicenseNumberlbl = new System.Windows.Forms.Label();
            this.modelYearlbl = new System.Windows.Forms.Label();
            this.selectFileCodelbl = new System.Windows.Forms.Label();
            this.fileCodeListBox = new System.Windows.Forms.ListBox();
            this.vinlbl = new System.Windows.Forms.Label();
            this.makeOfCarlbl = new System.Windows.Forms.Label();
            this.asOfDatelbl = new System.Windows.Forms.Label();
            this.citationNumberlbl = new System.Windows.Forms.Label();
            this.vinRichTxtBx = new System.Windows.Forms.RichTextBox();
            this.makeRchTxtBx = new System.Windows.Forms.RichTextBox();
            this.citationNumberRchTxtBx = new System.Windows.Forms.RichTextBox();
            this.addVrInquiry = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.messageLbl = new System.Windows.Forms.Label();
            this.inquiryErrorlstbx = new System.Windows.Forms.ListBox();
            this.platesWithOwnerchkbx = new System.Windows.Forms.CheckBox();
            this.docketNumberTxtBx = new System.Windows.Forms.TextBox();
            this.docketNumberLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.secViol1RchTxtBx = new System.Windows.Forms.RichTextBox();
            this.secViol2RchTxtBx = new System.Windows.Forms.RichTextBox();
            this.secViol3RchTxtBx = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.locationRchTxtBx = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.colorRchTxtBx = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dueDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.penaltyDueRchTxtBx = new System.Windows.Forms.MaskedTextBox();
            this.inquiryTrackBar = new System.Windows.Forms.TrackBar();
            this.vehicleLicenseNumbertxtbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.currentRequestorCodelbl = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inquiryTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitVRTransactionsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitVRTransactionsToolStripMenuItem
            // 
            this.exitVRTransactionsToolStripMenuItem.Name = "exitVRTransactionsToolStripMenuItem";
            this.exitVRTransactionsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitVRTransactionsToolStripMenuItem.Text = "Exit VR Transactions";
            this.exitVRTransactionsToolStripMenuItem.Click += new System.EventHandler(this.exitVRTransactionsToolStripMenuItem_Click);
            // 
            // modelYearTxtBx
            // 
            this.modelYearTxtBx.Location = new System.Drawing.Point(150, 62);
            this.modelYearTxtBx.Mask = " 99";
            this.modelYearTxtBx.Name = "modelYearTxtBx";
            this.modelYearTxtBx.Size = new System.Drawing.Size(100, 20);
            this.modelYearTxtBx.TabIndex = 2;
            this.modelYearTxtBx.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.modelYearTxtBx_MaskInputRejected);
            // 
            // vehicleLicenseNumberlbl
            // 
            this.vehicleLicenseNumberlbl.AutoSize = true;
            this.vehicleLicenseNumberlbl.Location = new System.Drawing.Point(13, 37);
            this.vehicleLicenseNumberlbl.Name = "vehicleLicenseNumberlbl";
            this.vehicleLicenseNumberlbl.Size = new System.Drawing.Size(122, 13);
            this.vehicleLicenseNumberlbl.TabIndex = 3;
            this.vehicleLicenseNumberlbl.Text = "Vehicle License Number";
            // 
            // modelYearlbl
            // 
            this.modelYearlbl.AutoSize = true;
            this.modelYearlbl.Location = new System.Drawing.Point(147, 37);
            this.modelYearlbl.Name = "modelYearlbl";
            this.modelYearlbl.Size = new System.Drawing.Size(61, 13);
            this.modelYearlbl.TabIndex = 4;
            this.modelYearlbl.Text = "Model Year";
            // 
            // selectFileCodelbl
            // 
            this.selectFileCodelbl.AutoSize = true;
            this.selectFileCodelbl.Location = new System.Drawing.Point(266, 37);
            this.selectFileCodelbl.Name = "selectFileCodelbl";
            this.selectFileCodelbl.Size = new System.Drawing.Size(84, 13);
            this.selectFileCodelbl.TabIndex = 5;
            this.selectFileCodelbl.Text = "Select File Code";
            // 
            // fileCodeListBox
            // 
            this.fileCodeListBox.FormattingEnabled = true;
            this.fileCodeListBox.Items.AddRange(new object[] {
            "A - Automotive",
            "B - Vessel",
            "C - Commercial",
            "E - Exempt",
            "F - Off Highway Vehicle",
            "H - Amateur Radio",
            "I - Apportioned Vehicle",
            "L - Environmental License Plate",
            "M - Motorcycle",
            "P - Prorate Commercial Trailer",
            "S - Special Interest Plate",
            "T - All Trailer license plates",
            "V - VIN",
            "X - Taxi or Limo",
            "Y - Miscellaneous",
            "Z - Special Equiptment(Construction, Farm)"});
            this.fileCodeListBox.Location = new System.Drawing.Point(256, 62);
            this.fileCodeListBox.Name = "fileCodeListBox";
            this.fileCodeListBox.Size = new System.Drawing.Size(110, 30);
            this.fileCodeListBox.TabIndex = 3;
            this.fileCodeListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // vinlbl
            // 
            this.vinlbl.AutoSize = true;
            this.vinlbl.Location = new System.Drawing.Point(10, 121);
            this.vinlbl.Name = "vinlbl";
            this.vinlbl.Size = new System.Drawing.Size(49, 13);
            this.vinlbl.TabIndex = 7;
            this.vinlbl.Text = "VIN/HIN";
            // 
            // makeOfCarlbl
            // 
            this.makeOfCarlbl.AutoSize = true;
            this.makeOfCarlbl.Location = new System.Drawing.Point(128, 121);
            this.makeOfCarlbl.Name = "makeOfCarlbl";
            this.makeOfCarlbl.Size = new System.Drawing.Size(65, 13);
            this.makeOfCarlbl.TabIndex = 8;
            this.makeOfCarlbl.Text = "Make of Car";
            // 
            // asOfDatelbl
            // 
            this.asOfDatelbl.AutoSize = true;
            this.asOfDatelbl.Location = new System.Drawing.Point(266, 121);
            this.asOfDatelbl.Name = "asOfDatelbl";
            this.asOfDatelbl.Size = new System.Drawing.Size(57, 13);
            this.asOfDatelbl.TabIndex = 9;
            this.asOfDatelbl.Text = "As of Date";
            // 
            // citationNumberlbl
            // 
            this.citationNumberlbl.AutoSize = true;
            this.citationNumberlbl.Location = new System.Drawing.Point(10, 197);
            this.citationNumberlbl.Name = "citationNumberlbl";
            this.citationNumberlbl.Size = new System.Drawing.Size(206, 13);
            this.citationNumberlbl.TabIndex = 10;
            this.citationNumberlbl.Text = "Citation Number/User Information/Parcel#";
            // 
            // vinRichTxtBx
            // 
            this.vinRichTxtBx.Location = new System.Drawing.Point(13, 149);
            this.vinRichTxtBx.MaxLength = 30;
            this.vinRichTxtBx.Name = "vinRichTxtBx";
            this.vinRichTxtBx.Size = new System.Drawing.Size(112, 26);
            this.vinRichTxtBx.TabIndex = 8;
            this.vinRichTxtBx.Text = "";
            // 
            // makeRchTxtBx
            // 
            this.makeRchTxtBx.Location = new System.Drawing.Point(131, 149);
            this.makeRchTxtBx.MaxLength = 3;
            this.makeRchTxtBx.Name = "makeRchTxtBx";
            this.makeRchTxtBx.Size = new System.Drawing.Size(67, 26);
            this.makeRchTxtBx.TabIndex = 9;
            this.makeRchTxtBx.Text = "";
            // 
            // citationNumberRchTxtBx
            // 
            this.citationNumberRchTxtBx.Location = new System.Drawing.Point(13, 225);
            this.citationNumberRchTxtBx.MaxLength = 15;
            this.citationNumberRchTxtBx.Multiline = false;
            this.citationNumberRchTxtBx.Name = "citationNumberRchTxtBx";
            this.citationNumberRchTxtBx.Size = new System.Drawing.Size(237, 26);
            this.citationNumberRchTxtBx.TabIndex = 13;
            this.citationNumberRchTxtBx.Text = "";
            // 
            // addVrInquiry
            // 
            this.addVrInquiry.Location = new System.Drawing.Point(516, 357);
            this.addVrInquiry.Name = "addVrInquiry";
            this.addVrInquiry.Size = new System.Drawing.Size(75, 23);
            this.addVrInquiry.TabIndex = 16;
            this.addVrInquiry.Text = "Add";
            this.addVrInquiry.UseVisualStyleBackColor = true;
            this.addVrInquiry.Click += new System.EventHandler(this.addVrInquiry_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = " MM/dd/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(244, 155);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 20);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new System.DateTime(2008, 12, 5, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(10, 303);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(10, 13);
            this.messageLbl.TabIndex = 11;
            this.messageLbl.Text = " ";
            // 
            // inquiryErrorlstbx
            // 
            this.inquiryErrorlstbx.FormattingEnabled = true;
            this.inquiryErrorlstbx.Location = new System.Drawing.Point(16, 303);
            this.inquiryErrorlstbx.Name = "inquiryErrorlstbx";
            this.inquiryErrorlstbx.Size = new System.Drawing.Size(340, 95);
            this.inquiryErrorlstbx.TabIndex = 12;
            this.inquiryErrorlstbx.Visible = false;
            // 
            // platesWithOwnerchkbx
            // 
            this.platesWithOwnerchkbx.AutoSize = true;
            this.platesWithOwnerchkbx.Location = new System.Drawing.Point(13, 88);
            this.platesWithOwnerchkbx.Name = "platesWithOwnerchkbx";
            this.platesWithOwnerchkbx.Size = new System.Drawing.Size(109, 17);
            this.platesWithOwnerchkbx.TabIndex = 7;
            this.platesWithOwnerchkbx.Text = "Plates with owner";
            this.platesWithOwnerchkbx.UseVisualStyleBackColor = true;
            // 
            // docketNumberTxtBx
            // 
            this.docketNumberTxtBx.Location = new System.Drawing.Point(362, 360);
            this.docketNumberTxtBx.Name = "docketNumberTxtBx";
            this.docketNumberTxtBx.ReadOnly = true;
            this.docketNumberTxtBx.Size = new System.Drawing.Size(122, 20);
            this.docketNumberTxtBx.TabIndex = 14;
            // 
            // docketNumberLbl
            // 
            this.docketNumberLbl.AutoSize = true;
            this.docketNumberLbl.Location = new System.Drawing.Point(362, 341);
            this.docketNumberLbl.Name = "docketNumberLbl";
            this.docketNumberLbl.Size = new System.Drawing.Size(82, 13);
            this.docketNumberLbl.TabIndex = 15;
            this.docketNumberLbl.Text = "Docket Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Sections  Violated";
            // 
            // secViol1RchTxtBx
            // 
            this.secViol1RchTxtBx.Location = new System.Drawing.Point(372, 72);
            this.secViol1RchTxtBx.MaxLength = 7;
            this.secViol1RchTxtBx.Name = "secViol1RchTxtBx";
            this.secViol1RchTxtBx.Size = new System.Drawing.Size(67, 26);
            this.secViol1RchTxtBx.TabIndex = 4;
            this.secViol1RchTxtBx.Text = " ";
            this.secViol1RchTxtBx.WordWrap = false;
            // 
            // secViol2RchTxtBx
            // 
            this.secViol2RchTxtBx.Location = new System.Drawing.Point(455, 72);
            this.secViol2RchTxtBx.MaxLength = 7;
            this.secViol2RchTxtBx.Name = "secViol2RchTxtBx";
            this.secViol2RchTxtBx.Size = new System.Drawing.Size(77, 26);
            this.secViol2RchTxtBx.TabIndex = 5;
            this.secViol2RchTxtBx.Text = " ";
            this.secViol2RchTxtBx.WordWrap = false;
            // 
            // secViol3RchTxtBx
            // 
            this.secViol3RchTxtBx.Location = new System.Drawing.Point(546, 72);
            this.secViol3RchTxtBx.MaxLength = 7;
            this.secViol3RchTxtBx.Name = "secViol3RchTxtBx";
            this.secViol3RchTxtBx.Size = new System.Drawing.Size(67, 26);
            this.secViol3RchTxtBx.TabIndex = 6;
            this.secViol3RchTxtBx.Text = " ";
            this.secViol3RchTxtBx.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(378, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Location";
            // 
            // locationRchTxtBx
            // 
            this.locationRchTxtBx.Location = new System.Drawing.Point(372, 149);
            this.locationRchTxtBx.MaxLength = 30;
            this.locationRchTxtBx.Name = "locationRchTxtBx";
            this.locationRchTxtBx.Size = new System.Drawing.Size(82, 26);
            this.locationRchTxtBx.TabIndex = 11;
            this.locationRchTxtBx.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(470, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Color of Vehicle";
            // 
            // colorRchTxtBx
            // 
            this.colorRchTxtBx.Location = new System.Drawing.Point(473, 149);
            this.colorRchTxtBx.MaxLength = 30;
            this.colorRchTxtBx.Name = "colorRchTxtBx";
            this.colorRchTxtBx.Size = new System.Drawing.Size(82, 26);
            this.colorRchTxtBx.TabIndex = 12;
            this.colorRchTxtBx.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Penalty Due";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(481, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Due Date";
            // 
            // dueDateTimePicker
            // 
            this.dueDateTimePicker.Checked = false;
            this.dueDateTimePicker.CustomFormat = " MM/dd/yyyy";
            this.dueDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dueDateTimePicker.Location = new System.Drawing.Point(473, 225);
            this.dueDateTimePicker.Name = "dueDateTimePicker";
            this.dueDateTimePicker.ShowCheckBox = true;
            this.dueDateTimePicker.Size = new System.Drawing.Size(111, 20);
            this.dueDateTimePicker.TabIndex = 15;
            this.dueDateTimePicker.Value = new System.DateTime(2008, 12, 5, 0, 0, 0, 0);
            // 
            // penaltyDueRchTxtBx
            // 
            this.penaltyDueRchTxtBx.Location = new System.Drawing.Point(354, 226);
            this.penaltyDueRchTxtBx.Mask = "$000 ";
            this.penaltyDueRchTxtBx.Name = "penaltyDueRchTxtBx";
            this.penaltyDueRchTxtBx.Size = new System.Drawing.Size(100, 20);
            this.penaltyDueRchTxtBx.TabIndex = 14;
            this.penaltyDueRchTxtBx.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.penaltyDueRchTxtBx_MaskInputRejected);
            // 
            // inquiryTrackBar
            // 
            this.inquiryTrackBar.Location = new System.Drawing.Point(380, 271);
            this.inquiryTrackBar.Name = "inquiryTrackBar";
            this.inquiryTrackBar.Size = new System.Drawing.Size(104, 45);
            this.inquiryTrackBar.TabIndex = 31;
            // 
            // vehicleLicenseNumbertxtbx
            // 
            this.vehicleLicenseNumbertxtbx.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.vehicleLicenseNumbertxtbx.Location = new System.Drawing.Point(16, 62);
            this.vehicleLicenseNumbertxtbx.Name = "vehicleLicenseNumbertxtbx";
            this.vehicleLicenseNumbertxtbx.Size = new System.Drawing.Size(109, 20);
            this.vehicleLicenseNumbertxtbx.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Requestor Code Being Used";
            // 
            // currentRequestorCodelbl
            // 
            this.currentRequestorCodelbl.AutoSize = true;
            this.currentRequestorCodelbl.Location = new System.Drawing.Point(16, 271);
            this.currentRequestorCodelbl.Name = "currentRequestorCodelbl";
            this.currentRequestorCodelbl.Size = new System.Drawing.Size(10, 13);
            this.currentRequestorCodelbl.TabIndex = 34;
            this.currentRequestorCodelbl.Text = " ";
            // 
            // vrinquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 413);
            this.Controls.Add(this.currentRequestorCodelbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vehicleLicenseNumbertxtbx);
            this.Controls.Add(this.inquiryTrackBar);
            this.Controls.Add(this.penaltyDueRchTxtBx);
            this.Controls.Add(this.dueDateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorRchTxtBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.locationRchTxtBx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.secViol3RchTxtBx);
            this.Controls.Add(this.secViol2RchTxtBx);
            this.Controls.Add(this.secViol1RchTxtBx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.docketNumberLbl);
            this.Controls.Add(this.docketNumberTxtBx);
            this.Controls.Add(this.platesWithOwnerchkbx);
            this.Controls.Add(this.inquiryErrorlstbx);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.addVrInquiry);
            this.Controls.Add(this.citationNumberRchTxtBx);
            this.Controls.Add(this.makeRchTxtBx);
            this.Controls.Add(this.vinRichTxtBx);
            this.Controls.Add(this.citationNumberlbl);
            this.Controls.Add(this.asOfDatelbl);
            this.Controls.Add(this.makeOfCarlbl);
            this.Controls.Add(this.vinlbl);
            this.Controls.Add(this.fileCodeListBox);
            this.Controls.Add(this.selectFileCodelbl);
            this.Controls.Add(this.modelYearlbl);
            this.Controls.Add(this.vehicleLicenseNumberlbl);
            this.Controls.Add(this.modelYearTxtBx);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "vrinquiry";
            this.Text = "VR Inquiry/Citation Input";
            this.Load += new System.EventHandler(this.vrinquiry_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inquiryTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitVRTransactionsToolStripMenuItem;
        private System.Windows.Forms.MaskedTextBox modelYearTxtBx;
        private System.Windows.Forms.Label vehicleLicenseNumberlbl;
        private System.Windows.Forms.Label modelYearlbl;
        private System.Windows.Forms.Label selectFileCodelbl;
        private System.Windows.Forms.ListBox fileCodeListBox;
        private System.Windows.Forms.Label vinlbl;
        private System.Windows.Forms.Label makeOfCarlbl;
        private System.Windows.Forms.Label asOfDatelbl;
        private System.Windows.Forms.Label citationNumberlbl;
        private System.Windows.Forms.RichTextBox vinRichTxtBx;
        private System.Windows.Forms.RichTextBox makeRchTxtBx;
        private System.Windows.Forms.RichTextBox citationNumberRchTxtBx;
        private System.Windows.Forms.Button addVrInquiry;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.ListBox inquiryErrorlstbx;
        private System.Windows.Forms.CheckBox platesWithOwnerchkbx;
        private System.Windows.Forms.TextBox docketNumberTxtBx;
        private System.Windows.Forms.Label docketNumberLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox secViol1RchTxtBx;
        private System.Windows.Forms.RichTextBox secViol2RchTxtBx;
        private System.Windows.Forms.RichTextBox secViol3RchTxtBx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox locationRchTxtBx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox colorRchTxtBx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dueDateTimePicker;
        private System.Windows.Forms.MaskedTextBox penaltyDueRchTxtBx;
        private System.Windows.Forms.TrackBar inquiryTrackBar;
        private System.Windows.Forms.TextBox vehicleLicenseNumbertxtbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label currentRequestorCodelbl;
    }
}