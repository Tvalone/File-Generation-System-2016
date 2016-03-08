namespace File_Generation_System
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.transactionMenusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureFGSForUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLTranactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLInquiriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ePNAddsDeletesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDLFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveDLFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestDriversListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courtAbstractsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vRTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterVRInquiriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterVRHoldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFunctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vRInquiriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vRHoldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vRInquiresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vRMonthlyParkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterViewRecordsInDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRecordsInDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveFilesFromDMVToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFilesToDMVToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutFGSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.errorlstBx = new System.Windows.Forms.ListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.progressLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLbl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dmvWatchForFiles = new System.IO.FileSystemWatcher();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dmvWatchForFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionMenusToolStripMenuItem,
            this.transactionMenuToolStripMenuItem,
            this.aboutFGSToolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(415, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // transactionMenusToolStripMenuItem
            // 
            this.transactionMenusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureFGSForUseToolStripMenuItem,
            this.exitFGSToolStripMenuItem});
            this.transactionMenusToolStripMenuItem.Name = "transactionMenusToolStripMenuItem";
            this.transactionMenusToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.transactionMenusToolStripMenuItem.Text = "Utilities";
            this.transactionMenusToolStripMenuItem.Click += new System.EventHandler(this.transactionMenusToolStripMenuItem_Click);
            // 
            // configureFGSForUseToolStripMenuItem
            // 
            this.configureFGSForUseToolStripMenuItem.Name = "configureFGSForUseToolStripMenuItem";
            this.configureFGSForUseToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.configureFGSForUseToolStripMenuItem.Text = "Configure FGS for use";
            this.configureFGSForUseToolStripMenuItem.Click += new System.EventHandler(this.configureFGSForUseToolStripMenuItem_Click);
            // 
            // exitFGSToolStripMenuItem
            // 
            this.exitFGSToolStripMenuItem.Name = "exitFGSToolStripMenuItem";
            this.exitFGSToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exitFGSToolStripMenuItem.Text = "Exit FGS";
            this.exitFGSToolStripMenuItem.Click += new System.EventHandler(this.exitFGSToolStripMenuItem_Click);
            // 
            // transactionMenuToolStripMenuItem
            // 
            this.transactionMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dLTranactionsToolStripMenuItem,
            this.vRTransactionsToolStripMenuItem});
            this.transactionMenuToolStripMenuItem.Name = "transactionMenuToolStripMenuItem";
            this.transactionMenuToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.transactionMenuToolStripMenuItem.Text = "Transaction Menu";
            this.transactionMenuToolStripMenuItem.Click += new System.EventHandler(this.transactionMenuToolStripMenuItem_Click);
            // 
            // dLTranactionsToolStripMenuItem
            // 
            this.dLTranactionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dLInquiriesToolStripMenuItem,
            this.ePNAddsDeletesToolStripMenuItem,
            this.sendDLFilesToolStripMenuItem,
            this.receiveDLFilesToolStripMenuItem,
            this.requestDriversListToolStripMenuItem,
            this.courtAbstractsToolStripMenuItem});
            this.dLTranactionsToolStripMenuItem.Name = "dLTranactionsToolStripMenuItem";
            this.dLTranactionsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.dLTranactionsToolStripMenuItem.Text = "DL Transactions";
            this.dLTranactionsToolStripMenuItem.Click += new System.EventHandler(this.dLTranactionsToolStripMenuItem_Click);
            // 
            // dLInquiriesToolStripMenuItem
            // 
            this.dLInquiriesToolStripMenuItem.Name = "dLInquiriesToolStripMenuItem";
            this.dLInquiriesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.dLInquiriesToolStripMenuItem.Text = "DL Inquiries/EPN";
            this.dLInquiriesToolStripMenuItem.Click += new System.EventHandler(this.dLInquiriesToolStripMenuItem_Click);
            // 
            // ePNAddsDeletesToolStripMenuItem
            // 
            this.ePNAddsDeletesToolStripMenuItem.Name = "ePNAddsDeletesToolStripMenuItem";
            this.ePNAddsDeletesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ePNAddsDeletesToolStripMenuItem.Text = "View Files";
            this.ePNAddsDeletesToolStripMenuItem.Click += new System.EventHandler(this.ePNAddsDeletesToolStripMenuItem_Click);
            // 
            // sendDLFilesToolStripMenuItem
            // 
            this.sendDLFilesToolStripMenuItem.Name = "sendDLFilesToolStripMenuItem";
            this.sendDLFilesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.sendDLFilesToolStripMenuItem.Text = "Send Files to The DMV";
            this.sendDLFilesToolStripMenuItem.Click += new System.EventHandler(this.sendDLFilesToolStripMenuItem_Click);
            // 
            // receiveDLFilesToolStripMenuItem
            // 
            this.receiveDLFilesToolStripMenuItem.Name = "receiveDLFilesToolStripMenuItem";
            this.receiveDLFilesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.receiveDLFilesToolStripMenuItem.Text = "Receive Files from The DMV";
            this.receiveDLFilesToolStripMenuItem.Click += new System.EventHandler(this.receiveDLFilesToolStripMenuItem_Click);
            // 
            // requestDriversListToolStripMenuItem
            // 
            this.requestDriversListToolStripMenuItem.Name = "requestDriversListToolStripMenuItem";
            this.requestDriversListToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.requestDriversListToolStripMenuItem.Text = "Request Drivers List";
            this.requestDriversListToolStripMenuItem.Click += new System.EventHandler(this.requestDriversListToolStripMenuItem_Click);
            // 
            // courtAbstractsToolStripMenuItem
            // 
            this.courtAbstractsToolStripMenuItem.Name = "courtAbstractsToolStripMenuItem";
            this.courtAbstractsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.courtAbstractsToolStripMenuItem.Text = "Court Abstracts";
            this.courtAbstractsToolStripMenuItem.Click += new System.EventHandler(this.courtAbstractsToolStripMenuItem_Click);
            // 
            // vRTransactionsToolStripMenuItem
            // 
            this.vRTransactionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterVRInquiriesToolStripMenuItem,
            this.enterVRHoldsToolStripMenuItem,
            this.printFunctionsToolStripMenuItem,
            this.createToolStripMenuItem,
            this.processToolStripMenuItem,
            this.filterViewRecordsInDatabaseToolStripMenuItem,
            this.viewRecordsInDatabaseToolStripMenuItem,
            this.receiveFilesFromDMVToolStripMenuItem1,
            this.sendFilesToDMVToolStripMenuItem1});
            this.vRTransactionsToolStripMenuItem.Name = "vRTransactionsToolStripMenuItem";
            this.vRTransactionsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.vRTransactionsToolStripMenuItem.Text = "VR Transactions";
            this.vRTransactionsToolStripMenuItem.Click += new System.EventHandler(this.vRTransactionsToolStripMenuItem_Click);
            // 
            // enterVRInquiriesToolStripMenuItem
            // 
            this.enterVRInquiriesToolStripMenuItem.Name = "enterVRInquiriesToolStripMenuItem";
            this.enterVRInquiriesToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.enterVRInquiriesToolStripMenuItem.Text = "Enter VR Inquiries";
            this.enterVRInquiriesToolStripMenuItem.Click += new System.EventHandler(this.enterVRInquiriesToolStripMenuItem_Click);
            // 
            // enterVRHoldsToolStripMenuItem
            // 
            this.enterVRHoldsToolStripMenuItem.Name = "enterVRHoldsToolStripMenuItem";
            this.enterVRHoldsToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.enterVRHoldsToolStripMenuItem.Text = "Enter VR Holds";
            this.enterVRHoldsToolStripMenuItem.Click += new System.EventHandler(this.enterVRHoldsToolStripMenuItem_Click);
            // 
            // printFunctionsToolStripMenuItem
            // 
            this.printFunctionsToolStripMenuItem.Name = "printFunctionsToolStripMenuItem";
            this.printFunctionsToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.printFunctionsToolStripMenuItem.Text = "Parking Print Functions";
            this.printFunctionsToolStripMenuItem.Click += new System.EventHandler(this.printFunctionsToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vRInquiriesToolStripMenuItem,
            this.vRHoldsToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.createToolStripMenuItem.Text = "Create Files For DMV ";
            // 
            // vRInquiriesToolStripMenuItem
            // 
            this.vRInquiriesToolStripMenuItem.Name = "vRInquiriesToolStripMenuItem";
            this.vRInquiriesToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.vRInquiriesToolStripMenuItem.Text = "VR Inquiries";
            this.vRInquiriesToolStripMenuItem.Click += new System.EventHandler(this.vRInquiriesToolStripMenuItem_Click);
            // 
            // vRHoldsToolStripMenuItem
            // 
            this.vRHoldsToolStripMenuItem.Name = "vRHoldsToolStripMenuItem";
            this.vRHoldsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.vRHoldsToolStripMenuItem.Text = "VR Holds";
            this.vRHoldsToolStripMenuItem.Click += new System.EventHandler(this.vRHoldsToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vRInquiresToolStripMenuItem,
            this.vRMonthlyParkingToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.processToolStripMenuItem.Text = "Process Files From DMV";
            this.processToolStripMenuItem.Click += new System.EventHandler(this.processToolStripMenuItem_Click);
            // 
            // vRInquiresToolStripMenuItem
            // 
            this.vRInquiresToolStripMenuItem.Name = "vRInquiresToolStripMenuItem";
            this.vRInquiresToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.vRInquiresToolStripMenuItem.Text = "VR Inquires";
            this.vRInquiresToolStripMenuItem.Click += new System.EventHandler(this.vRInquiresToolStripMenuItem_Click);
            // 
            // vRMonthlyParkingToolStripMenuItem
            // 
            this.vRMonthlyParkingToolStripMenuItem.Name = "vRMonthlyParkingToolStripMenuItem";
            this.vRMonthlyParkingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.vRMonthlyParkingToolStripMenuItem.Text = "VR Monthly Parking";
            this.vRMonthlyParkingToolStripMenuItem.Click += new System.EventHandler(this.vRMonthlyParkingToolStripMenuItem_Click);
            // 
            // filterViewRecordsInDatabaseToolStripMenuItem
            // 
            this.filterViewRecordsInDatabaseToolStripMenuItem.Name = "filterViewRecordsInDatabaseToolStripMenuItem";
            this.filterViewRecordsInDatabaseToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.filterViewRecordsInDatabaseToolStripMenuItem.Text = "Filter View Records in Database";
            this.filterViewRecordsInDatabaseToolStripMenuItem.Click += new System.EventHandler(this.filterViewRecordsInDatabaseToolStripMenuItem_Click);
            // 
            // viewRecordsInDatabaseToolStripMenuItem
            // 
            this.viewRecordsInDatabaseToolStripMenuItem.Name = "viewRecordsInDatabaseToolStripMenuItem";
            this.viewRecordsInDatabaseToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.viewRecordsInDatabaseToolStripMenuItem.Text = "Modify Records in Database";
            this.viewRecordsInDatabaseToolStripMenuItem.Click += new System.EventHandler(this.viewRecordsInDatabaseToolStripMenuItem_Click);
            // 
            // receiveFilesFromDMVToolStripMenuItem1
            // 
            this.receiveFilesFromDMVToolStripMenuItem1.Name = "receiveFilesFromDMVToolStripMenuItem1";
            this.receiveFilesFromDMVToolStripMenuItem1.Size = new System.Drawing.Size(237, 22);
            this.receiveFilesFromDMVToolStripMenuItem1.Text = "Receive Files From DMV";
            this.receiveFilesFromDMVToolStripMenuItem1.Click += new System.EventHandler(this.receiveFilesFromDMVToolStripMenuItem1_Click);
            // 
            // sendFilesToDMVToolStripMenuItem1
            // 
            this.sendFilesToDMVToolStripMenuItem1.Name = "sendFilesToDMVToolStripMenuItem1";
            this.sendFilesToDMVToolStripMenuItem1.Size = new System.Drawing.Size(237, 22);
            this.sendFilesToDMVToolStripMenuItem1.Text = "Send Files to DMV";
            this.sendFilesToDMVToolStripMenuItem1.Click += new System.EventHandler(this.sendFilesToDMVToolStripMenuItem1_Click);
            // 
            // aboutFGSToolStripMenuItem1
            // 
            this.aboutFGSToolStripMenuItem1.Name = "aboutFGSToolStripMenuItem1";
            this.aboutFGSToolStripMenuItem1.Size = new System.Drawing.Size(75, 20);
            this.aboutFGSToolStripMenuItem1.Text = "About FGS";
            this.aboutFGSToolStripMenuItem1.Click += new System.EventHandler(this.aboutFGSToolStripMenuItem1_Click);
            // 
            // errorlstBx
            // 
            this.errorlstBx.FormattingEnabled = true;
            this.errorlstBx.Location = new System.Drawing.Point(44, 81);
            this.errorlstBx.Name = "errorlstBx";
            this.errorlstBx.Size = new System.Drawing.Size(291, 43);
            this.errorlstBx.TabIndex = 3;
            this.errorlstBx.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(266, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(137, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Link to Secure File Transfer";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // progressLbl
            // 
            this.progressLbl.Name = "progressLbl";
            this.progressLbl.Size = new System.Drawing.Size(10, 17);
            this.progressLbl.Text = " ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressLbl,
            this.progressLabel2,
            this.progressLbl2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 244);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(415, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // progressLabel2
            // 
            this.progressLabel2.Name = "progressLabel2";
            this.progressLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // progressLbl2
            // 
            this.progressLbl2.Name = "progressLbl2";
            this.progressLbl2.Size = new System.Drawing.Size(10, 17);
            this.progressLbl2.Text = " ";
            // 
            // dmvWatchForFiles
            // 
            this.dmvWatchForFiles.EnableRaisingEvents = true;
            this.dmvWatchForFiles.Filter = "*.txt";
            this.dmvWatchForFiles.SynchronizingObject = this;
            this.dmvWatchForFiles.Renamed += new System.IO.RenamedEventHandler(this.dmvWatchForFiles_Renamed);
            this.dmvWatchForFiles.Deleted += new System.IO.FileSystemEventHandler(this.dmvWatchForFiles_Deleted);
            this.dmvWatchForFiles.Created += new System.IO.FileSystemEventHandler(this.dmvWatchForFiles_Created);
            this.dmvWatchForFiles.Changed += new System.IO.FileSystemEventHandler(this.dmvWatchForFiles_Changed);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 266);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.errorlstBx);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "File Generation System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.EnabledChanged += new System.EventHandler(this.Form1_EnabledChanged);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Enter += new System.EventHandler(this.Form1_Enter);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dmvWatchForFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem transactionMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLTranactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLInquiriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ePNAddsDeletesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vRTransactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterVRInquiriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterVRHoldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionMenusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureFGSForUseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printFunctionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vRInquiriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vRHoldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vRInquiresToolStripMenuItem;
        public System.Windows.Forms.ListBox errorlstBx;
        private System.Windows.Forms.ToolStripMenuItem vRMonthlyParkingToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolStripMenuItem viewRecordsInDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterViewRecordsInDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courtAbstractsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveFilesFromDMVToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendFilesToDMVToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendDLFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveDLFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel progressLbl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel2;
        private System.Windows.Forms.ToolStripStatusLabel progressLbl2;
        private System.IO.FileSystemWatcher dmvWatchForFiles;
        private System.Windows.Forms.ToolStripMenuItem requestDriversListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutFGSToolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

