namespace File_Generation_System
{
    partial class progressFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(progressFrm));
            this.SuspendLayout();
            // 
            // progressFrm
            // 
            this.ClientSize = new System.Drawing.Size(615, 610);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "progressFrm";
            this.Load += new System.EventHandler(this.progressFrm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource processingReport_DataTableBindingSource;
       // private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;




    }
}