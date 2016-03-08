namespace File_Generation_System
{
    partial class print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(print));
            this.label1 = new System.Windows.Forms.Label();
            this.printersList = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.createParkingLettersBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitPrintingFunctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printfile2Btn = new System.Windows.Forms.Button();
            this.createFinalParkingLetterBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Printers";
            // 
            // printersList
            // 
            this.printersList.FormattingEnabled = true;
            this.printersList.Location = new System.Drawing.Point(125, 22);
            this.printersList.Name = "printersList";
            this.printersList.Size = new System.Drawing.Size(172, 21);
            this.printersList.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 115);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(220, 20);
            this.textBox1.TabIndex = 4;
            // 
            // createParkingLettersBtn
            // 
            this.createParkingLettersBtn.Location = new System.Drawing.Point(16, 156);
            this.createParkingLettersBtn.Name = "createParkingLettersBtn";
            this.createParkingLettersBtn.Size = new System.Drawing.Size(100, 34);
            this.createParkingLettersBtn.TabIndex = 6;
            this.createParkingLettersBtn.Text = "Create First Parking letter";
            this.createParkingLettersBtn.UseVisualStyleBackColor = true;
            this.createParkingLettersBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "File to Print";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(339, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitPrintingFunctionsToolStripMenuItem});
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.functionsToolStripMenuItem.Text = "Functions";
            // 
            // exitPrintingFunctionsToolStripMenuItem
            // 
            this.exitPrintingFunctionsToolStripMenuItem.Name = "exitPrintingFunctionsToolStripMenuItem";
            this.exitPrintingFunctionsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitPrintingFunctionsToolStripMenuItem.Text = "Exit Printing Functions";
            this.exitPrintingFunctionsToolStripMenuItem.Click += new System.EventHandler(this.exitPrintingFunctionsToolStripMenuItem_Click);
            // 
            // printfile2Btn
            // 
            this.printfile2Btn.Location = new System.Drawing.Point(140, 156);
            this.printfile2Btn.Name = "printfile2Btn";
            this.printfile2Btn.Size = new System.Drawing.Size(96, 34);
            this.printfile2Btn.TabIndex = 9;
            this.printfile2Btn.Text = "Print File ";
            this.printfile2Btn.UseVisualStyleBackColor = true;
            this.printfile2Btn.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // createFinalParkingLetterBtn
            // 
            this.createFinalParkingLetterBtn.Location = new System.Drawing.Point(16, 196);
            this.createFinalParkingLetterBtn.Name = "createFinalParkingLetterBtn";
            this.createFinalParkingLetterBtn.Size = new System.Drawing.Size(100, 34);
            this.createFinalParkingLetterBtn.TabIndex = 10;
            this.createFinalParkingLetterBtn.Text = "Create Final Parking Letter";
            this.createFinalParkingLetterBtn.UseVisualStyleBackColor = true;
            this.createFinalParkingLetterBtn.Click += new System.EventHandler(this.createFinalParkingLetterBtn_Click);
            // 
            // print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 301);
            this.Controls.Add(this.createFinalParkingLetterBtn);
            this.Controls.Add(this.printfile2Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.createParkingLettersBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.printersList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "print";
            this.Text = "Parking Print";
            this.Load += new System.EventHandler(this.print_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox printersList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button createParkingLettersBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitPrintingFunctionsToolStripMenuItem;
        private System.Windows.Forms.Button printfile2Btn;
        private System.Windows.Forms.Button createFinalParkingLetterBtn;
    }
}