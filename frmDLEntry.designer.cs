namespace File_Generation_System
{
    partial class frmTranslateText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTranslateText));
            this.lblEnterFile = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.dl_records = new File_Generation_System.dl_records();
            this.crDL4141 = new File_Generation_System.crDL414();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLbl = new System.Windows.Forms.Label();
            this.dlFileWatcher = new System.IO.FileSystemWatcher();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dl_records)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlFileWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEnterFile
            // 
            this.lblEnterFile.AutoSize = true;
            this.lblEnterFile.Location = new System.Drawing.Point(13, 13);
            this.lblEnterFile.Name = "lblEnterFile";
            this.lblEnterFile.Size = new System.Drawing.Size(157, 13);
            this.lblEnterFile.TabIndex = 0;
            this.lblEnterFile.Text = "Browse to select report filename";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(91, 31);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(460, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(10, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(557, 28);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(83, 23);
            this.btnTranslate.TabIndex = 3;
            this.btnTranslate.Text = "Create Report";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // dl_records
            // 
            this.dl_records.DataSetName = "dl_records";
            this.dl_records.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(26, 277);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(126, 21);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressLbl
            // 
            this.progressLbl.AutoSize = true;
            this.progressLbl.Location = new System.Drawing.Point(23, 249);
            this.progressLbl.Name = "progressLbl";
            this.progressLbl.Size = new System.Drawing.Size(138, 13);
            this.progressLbl.TabIndex = 5;
            this.progressLbl.Text = "DL414 Conversion progress";
            this.progressLbl.Visible = false;
            // 
            // dlFileWatcher
            // 
            this.dlFileWatcher.EnableRaisingEvents = true;
            this.dlFileWatcher.SynchronizingObject = this;
            this.dlFileWatcher.Renamed += new System.IO.RenamedEventHandler(this.dlFileWatcher_Renamed);
            this.dlFileWatcher.Deleted += new System.IO.FileSystemEventHandler(this.dlFileWatcher_Deleted);
            this.dlFileWatcher.Created += new System.IO.FileSystemEventHandler(this.dlFileWatcher_Created);
            this.dlFileWatcher.Changed += new System.IO.FileSystemEventHandler(this.dlFileWatcher_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Files you can view ";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(19, 112);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(179, 73);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "DLINQ_OUT = Drivers Records\nEPN_OUT = Add/Delete Error File\nEPN_LIST = Drivers Li" +
                "st\n\n";
            // 
            // frmTranslateText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 333);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressLbl);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblEnterFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTranslateText";
            this.Text = "View Files";
            this.Load += new System.EventHandler(this.frmTranslateText_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dl_records)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlFileWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnterFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnTranslate;
        private dl_records dl_records;
        private crDL414 crDL4141;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressLbl;
        private System.IO.FileSystemWatcher dlFileWatcher;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;

         

    }
}

