using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Threading;

namespace File_Generation_System
{
        
    public partial class Form1 : Form

    {
        public string nextLine;
        public string cardNumber;
        public string licenseNumber;
        public string oLicenseNumber = null;
        public StreamReader sr;
        public XmlDataDocument doc;
        public string oldplate;
        public XmlElement newVrRecord;
        public XmlElement newInquiryData;
        public DataSet cf;
        public DataSet ds;
        public String regExpireDate;
        public String yearModel;
        public String vin;
        public String make;
        public String roCity;
        public String roZip;
        public String roCountyCode;
        public String paperIssueDate;
        public String roNameAndAddressSourceIndicator;
        public String roName;
        public String roNameOrAddress;
        public String additionalRoNameOrAddress;
        public String additionalRoAddress;
        public String recordConditionCode;
        public String recordConditionCodeDate;
        public String recordRemarks;
        public String errorCode;
        public String errorMessage;
        public String errorDate;
        public String nrlDate;
        public String nrlTransferDate;
        public String nrlNameOrMessage;
        public String nrlRecordId;
        public String nrlAddressMessage;
        public String nrlCityStateMessage;
        public String requestorCode;
        public int match;
        public int updatedrecords = 0;
        public String docketNumber;
        public ProgressBar pbv;
        public long s1;
         

        public Form1()
        {
            InitializeComponent();

            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //The beginnings of the filewatch code

            configure.read_xml_files();
            configure.getInformationFromConfig();

            dmvWatchForFiles.Path = configure.filepath;
             
            if (configure.requestorCode == "99999")
            {
                MessageBox.Show("You Must Configure FGS", "Going to Configure Screen");
                Form form = new config_fgs();
                form.ShowDialog(); // show form modally
            }


        }

        
        private void transactionMenusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void exitFGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configureFGSForUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new config_fgs();
            form.ShowDialog(); // show form modally


        }

        private void enterVRInquiriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new vrinquiry();
            form.ShowDialog(); // show form modally
        }

        private void enterVRHoldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new vrholds();
            form.ShowDialog(); // show form modally
             
            
        }

        private void transferMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new courtAbstract();
            form.ShowDialog();
        }

        private void printParkingDemandLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new print();
            form.ShowDialog(); // show form modally
        }

        private void dLInquiriesToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Form dlInquiry = new frmDLInquiry();
            dlInquiry.ShowDialog();
        }

        private void ePNAddsDeletesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form dl414 = new frmTranslateText();
            dl414.ShowDialog();
        }

        private void printFunctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new print();
            form.ShowDialog(); // show form modally
        }
        
        private void vRInquiresToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Create an object

            process vrfile = new process();


            //Update Label of status strip
            progressLbl.Text = "Processing Vr Inquiry File From DMV";
                   
            
            

            //Create spawn thread - comment out until thread issue resolved
             vrfile.spawnVr_Inquiry();
           // vrfile.process_Vr_Inquiry_File();
          

          //  Form.ActiveForm.ControlBox = false;

             spawn_progressBar();
             
        }
        public void spawn_progressBar()
        {
            Thread progressBarThread = new Thread(new ThreadStart(progressBar));
            
            progressBarThread.Start();

            
        }

        private void progressBar()
        {
            


           while (process.newThread.IsAlive  == true)
            {
                Thread.Sleep(1000); 

            } 




            if (process.newThread.IsAlive == false)
            {

                this.progressLbl.Text = "Processing Complete";
                Thread.Sleep(2000);
                
                
            }
            this.progressLbl.Visible = false;
        }

        private void vRInquiriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeDmvFile wif = new writeDmvFile();
            wif.write_DMV_file();
             
           
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
             configure.read_xml_files();

             configure.validate_req(errorlstBx, dLTranactionsToolStripMenuItem);

              

             
        }

        private void Form1_Enter(object sender, EventArgs e)
        {
            if (configure.requestorCode == "99999")
            {
                MessageBox.Show("You Must Configure FGS", "Going to Configure Screen");
                Form form = new config_fgs();
                form.ShowDialog(); // show form modally
            } 
        }

        private void transactionMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            configure.validate_req(errorlstBx, dLTranactionsToolStripMenuItem);
            configure.validate_req(errorlstBx, vRTransactionsToolStripMenuItem);

            if (configure.requestorCode == "99999")
            {
                MessageBox.Show("You Must Configure FGS", "Going to Configure Screen");
                Form form = new config_fgs();
                form.ShowDialog(); // show form modally
            }
        }

        private void dLTranactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            configure.validate_req(errorlstBx, dLTranactionsToolStripMenuItem);
             
        }

        private void Form1_EnabledChanged(object sender, EventArgs e)
        {
             
        }

        private void vRTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vRHoldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Parking holds file to the DMV
            //Call the static getinfo if vesseltaxhold is equal to true
            //then call overloaded write for vessels otherwise call regular
            //holds file 

            configure.getInformationFromConfig();

            

            if (configure.vesselTaxHold == "True")
            {
                writeDmvFile vhf = new writeDmvFile();
                vhf.write_DMV_Vessel_file();
                 
            }
            else
            {
                writeDmvFile chf = new writeDmvFile();
                chf.write_DMV_Holds_file();

                
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebBrowser wb = new WebBrowser();
            wb.Navigate("https://" + configure.hostname, true);
        }

        private void vRHoldsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
             
            
        }

        private void viewRecordsInDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form vfm = new modifyFrm();
            vfm.ShowDialog(); // show form modally
        }

        private void vRMonthlyParkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            
            
             this.progressLbl.Text = "Starting Monthly Parking Processing";
             process vrm = new process();

            //vrm.process_Vr_Monthly_File();


             vrm.spawnVr_Monthly();

            spawn_progressBarMonthly();
             
        }
        public void spawn_progressBarMonthly()
        {


            Thread newThread = new Thread(new ThreadStart(progressBarMonthly));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();     

                        
        }

             
                     
         

        private void progressBarMonthly()
        {
             
            while (process.newThread.IsAlive == true)
            {
                Thread.Sleep(1000);
                this.progressLbl.Text = "                        ";
                this.progressLbl.Text = "Processing Monthly File";

            }




            if (process.newThread.IsAlive == false)
            {

                this.progressLbl.Text = "Processing Complete";
                Thread.Sleep(2000);


            }
            this.progressLbl.Visible = false;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbv.Value = 100;
        }

        private void filterViewRecordsInDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form vfr = new exceptionFrm();
            vfr.Text = "Filter View VR Records";
            vfr.ShowDialog(); // show form modally
        }

        private void courtAbstractsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form courtAbstract = new courtAbstract();
            courtAbstract.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
             
             
        }

        private void receiveFilesFromDMVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
             
            
         
        }

        private void receiveFilesFromDMVToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            sendReceive sr = new sendReceive();
            sr.receive();

            //try
            //{


            //    configure.getInformationFromConfig();

            //    bool success;

            //    Chilkat.Ftp2 ftp = new Chilkat.Ftp2();

            //    success = ftp.UnlockComponent("DMVCAGFTP_rk6c67zZ4XnK");

            //    ftp.Hostname = "sft.ca.gov";
            //    ftp.Username = configure.userid;
            //    string certName = "dmv-ddt-" + configure.userid + "-x509";
            //    ftp.AuthSsl = true;
            //    ftp.AuthTls = true;
            //    ftp.Ssl = false;
            //    //ftp.Port = 443;
            //    ftp.Port = 2121;
            //    ftp.Passive = true;
            //    ftp.ConnectTimeout = 20000;
            //    ftp.SetTypeAscii();
            //    Chilkat.Cert cert = new Chilkat.Cert();
            //    success = cert.LoadByCommonName(certName);

            //    if (success == true)
            //    {
            //        ftp.SetSslClientCert(cert);

            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show("Certificate not installed", "Certificate Error");
            //        return;
            //    }

                 
            //    success = ftp.Connect();

            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }

            //    success = ftp.ChangeRemoteDir("/DMVData/FromDMV");

            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }

            //    //This gets the number of files in the current directory


            //    int getAllFiles = ftp.MGetFiles("*.*", configure.filepath);

            //    if (getAllFiles != 0)
            //    {
            //        for (int i = 0; i < getAllFiles; i++)
            //        {
            //            MessageBox.Show("Downloading File " + ftp.GetFilename(i) + "File Size " + ftp.GetSize(i), "File Download");
            //            MessageBox.Show("Deleting files from your SFT Account", "Deleting Files");
            //            ftp.DeleteMatching("*.*");
            //        }
            //    }

            //    if (getAllFiles == 0)
            //    {
            //        MessageBox.Show("There are no files in your SFT account", "SFT Account Empty");
            //    }

            //    if (getAllFiles < 0)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }





            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }







            //}
            //catch (UriFormatException f)
            //{
            //    Console.WriteLine("Invalid URL" + f);
            //}
            //catch (IOException f)
            //{
            //    Console.WriteLine("Could not connect to URL" + f.ToString());
            //}
        }

        private void sendFilesToDMVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sendReceive vrSend = new sendReceive();

            vrSend.Send('V');

            //try
            //{

            //    configure.getInformationFromConfig();

            //    bool successUnlock;
            //    bool successConnect;
            //    bool successChangeDir;
            //    bool successPutFile;
            //    bool successLoadCert;
                
            //    Chilkat.Ftp2 ftp = new Chilkat.Ftp2();

            //    successUnlock = ftp.UnlockComponent("DMVCAGFTP_rk6c67zZ4XnK");

            //    ftp.Hostname = "sft.ca.gov";
            //    ftp.Username = configure.userid;
            //    string certName = "dmv-ddt-" + configure.userid + "-x509";
                 
            //    ftp.AuthSsl = true;
            //    ftp.AuthTls = true;
            //    ftp.Ssl = false;
            //    //port 443 is for https 
            //    // ftp.Port = 443;
            //    ftp.Port = 2121;
            //    ftp.Passive = true;
            //    ftp.ConnectTimeout = 20000;
            //    Chilkat.Cert cert = new Chilkat.Cert();

            //    var success = cert.LoadByCommonName(certName);
                

            //    if (success == true)
            //    {
            //        ftp.SetSslClientCert(cert);

            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show("Certificate not installed", "Certificate Error");
            //        MessageBox.Show("certname=" + certName);
            //        MessageBox.Show("username=" + configure.userid);
            //        return;
            //    }

            //    success = ftp.Connect();

            //    //If connect is not successful 

            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }

            //    successChangeDir = ftp.ChangeRemoteDir("/DMVData/SendToDMV");
                 
            //    if (successChangeDir != true)
            //    {
            //        successChangeDir = ftp.ChangeRemoteDir("/dmv-ddt-router/SendToDMV");

            //    }

            //    if (successChangeDir != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }


            //    if (successChangeDir == true)
            //    {
            //        //Get info about file. 


            //        FileInfo vrinquiryFile = new FileInfo(configure.filepath + @"\" + "vrinquiry.txt");
            //        FileInfo vrholdFile = new FileInfo(configure.filepath + @"\" + "parkhold.txt");
            //        FileInfo veseltaxFile = new FileInfo(configure.filepath + @"\" + "veseltax.txt");



            //        if ((vrinquiryFile.Exists == true) && (vrinquiryFile.Length > 0))
            //        {



            //            successPutFile = ftp.PutFile(configure.filepath + @"\" + "vrinquiry.txt", "vrinquiry.txt");


            //            if (successPutFile != true)
            //            {
            //                MessageBox.Show(ftp.LastErrorText);
            //                return;
            //            }

            //            if (successPutFile == true)
            //            {
                            
            //                MessageBox.Show("Vr Inquiry File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent,"VR Inquiry File Sent");
            //                vrinquiryFile.Delete();
            //            }


            //        }

            //        if (vrinquiryFile.Exists == false)
            //        {
            //            MessageBox.Show("Vehicle Inquiry File Empty File Not Sent","File Not Sent");
            //        }

            //        if ((vrholdFile.Exists == true) && (vrholdFile.Length > 0))
            //        {
                         

                         
            //            successPutFile = ftp.PutFile(configure.filepath + @"\" + "parkhold.txt", "parkhold.txt");


            //            if (successPutFile != true)
            //            {
            //                MessageBox.Show(ftp.LastErrorText);
            //                return;
            //            }

            //            if (successPutFile == true)
            //            {
            //                MessageBox.Show("VR Holds File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent,"VR Hold File Sent");

            //                vrholdFile.Delete();
            //            }


            //        }

            //        if ((vrholdFile.Exists == true) && (vrholdFile.Length == 0))
            //        {
                        
            //            MessageBox.Show("VR Holds File Empty","File Not Sent");
                         
            //        }
                     
                    
                     
            //    }
            //}
            //catch (UriFormatException f)
            //{
            //    Console.WriteLine("Invalid URL" + f);
            //}
            //catch (IOException f)
            //{
            //    Console.WriteLine("Could not connect to URL" + f.ToString());
            //}

             
            

        }

        private void sendDLFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendReceive send = new sendReceive();
            send.Send('D');
            
            //try
            //{

            //    configure.getInformationFromConfig();

            //    bool success;
               
                
            //    Chilkat.Ftp2 ftp = new Chilkat.Ftp2();

            //    //Need uplock code again. 
            //    success = ftp.UnlockComponent("DMVCAGFTP_rk6c67zZ4XnK");
            //    //success = ftp.UnlockComponent("asdfasdccd");
            //    ftp.Hostname = "sft.ca.gov";
            //    ftp.Username = configure.userid;
            //    string certname = "dmv-ddt-" + configure.userid + "-x509"; 
            //    ftp.AuthSsl = true;
            //    ftp.AuthTls = true;
            //    ftp.Ssl = false;
            //    //ftp.Port = 443;
            //    ftp.Port = 2121;
            //    ftp.Passive = true;
            //    //Added 5.30.13 on major rebuild. 
            //    ftp.UseEpsv = true;
            //    ftp.ConnectTimeout = 20000;
                
            //    Chilkat.Cert cert = new Chilkat.Cert();
                
            //    success = cert.LoadByCommonName(certname);
                
                
            //    if (success == true)
            //    {
            //        ftp.SetSslClientCert(cert);
                   
            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show("Certificate not installed","Certificate Error");
            //        return;
            //    }

            //    var isExpired = cert.Expired;

            //    if (isExpired == true)
            //    {
            //        MessageBox.Show("Certificate is Expired");
            //        return;
            //    }

            //    success = ftp.Connect();

            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }

            //    //Now try and change the directory

            //    success = ftp.ChangeRemoteDir("/DMVData/SendToDMV");

            //    if (success != true)
            //    {
            //        success = ftp.ChangeRemoteDir("/dmv-ddt-router/SendToDMV");
            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }

            //    //Get info about file. 

            //    configure.getInformationFromConfig();

            //    FileInfo dlinquiryFile = new FileInfo(configure.filepath + @"\" + "dlinquiry.txt");
            //    FileInfo epnFile       = new FileInfo(configure.filepath + @"\" + "epn.txt");
            //    FileInfo courtFile     = new FileInfo(configure.filepath + @"\" + "court.txt");
            //    FileInfo dlistFile         = new FileInfo(configure.filepath + @"\" + "dlist.txt");



            //    if ((dlinquiryFile.Exists == true) && (dlinquiryFile.Length > 0))
            //    {
                     

                   

            //        success = ftp.PutFile(configure.filepath + @"\" + "dlinquiry.txt", "dlinquiry.txt");


            //        if (success != true)
            //        {
            //            MessageBox.Show(ftp.LastErrorText);
            //            return;
            //        }

            //        if (success == true)
            //        {
            //            MessageBox.Show("DL Inquiry File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent,"DL Inquiry File Sent");

            //            dlinquiryFile.Delete();
            //        }


            //    }

            //    if (dlinquiryFile.Exists == false)
            //    {
            //        MessageBox.Show("Drivers License Inquiry File Empty", "File Not Sent");
            //    }

            //    if ((epnFile.Exists == true) && (epnFile.Length > 0))
            //    {
                     

                     
            //        success = ftp.PutFile(configure.filepath + @"\" + "epn.txt", "epn.txt");


            //        if (success != true)
            //        {
            //            MessageBox.Show(ftp.LastErrorText);
            //            return;
            //        }

            //        if (success == true)
            //        {
            //            MessageBox.Show("EPN File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent,"EPN File Sent");

            //            epnFile.Delete();
            //        }


            //    }

            //    if  (epnFile.Exists == false) 
            //    {
            //        MessageBox.Show("EPN File Empty", "File Not Sent");
            //    }

            //    if ((courtFile.Exists == true) && (courtFile.Length > 0))
            //    {
                    
            //        success = ftp.PutFile(configure.filepath + @"\" + "court.txt", "court.txt");


            //        if (success != true)
            //        {
            //            MessageBox.Show(ftp.LastErrorText);
            //            return;
            //        }

            //        if (success == true)
            //        {
            //            MessageBox.Show("Court File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent,"Court Abstract File Sent");

            //            courtFile.Delete();
            //        }


            //    }

            //    if (courtFile.Exists == false)
            //    {
            //        MessageBox.Show("Court File Empty", "File Not Sent");
            //    }
            //    if ((dlistFile.Exists == true) && (dlistFile.Length > 0))
            //    {

            //        success = ftp.PutFile(configure.filepath + @"\" + "dlist.txt", "dlist.txt");


            //        if (success != true)
            //        {
            //            MessageBox.Show(ftp.LastErrorText);
            //            return;
            //        }

            //        if (success == true)
            //        {
            //            MessageBox.Show("Drivers List File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent, "Drivers List File");

            //            dlistFile.Delete();
            //        }


            //    }

            //    if (dlistFile.Exists == false)
            //    {
            //        MessageBox.Show("Drivers List File Empty", "File Not Sent");
            //    }
            //}
            //catch (UriFormatException f)
            //{
            //    Console.WriteLine("Invalid URL" + f);
            //}
            //catch (IOException f)
            //{
            //    Console.WriteLine("Could not connect to URL" + f.ToString());
            //}

           
        }

        private void receiveDLFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendReceive sr = new sendReceive();
            sr.receive();
            statusStrip1.Text = "Downloading Files";
            //try
            //{

            //    configure.getInformationFromConfig();

            //    bool success;
            //    bool isExpired;
            //    Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
                
            //    success = ftp.UnlockComponent("DMVCAGFTP_rk6c67zZ4XnK");

            //    ftp.Hostname = "sft.ca.gov";
            //    ftp.Username = configure.userid;

            //    string certName = "dmv-ddt-" + configure.userid + "-x509";
                
            //    //AuthSsl and AuthTls true for both https and sftp
            //    ftp.AuthSsl = true;
            //    ftp.AuthTls = true;

            //    //ftp.ssl = false for sftp true for https

            //    ftp.Ssl = false;

            //    //port 443 for https 2121 for sftp
            //    ftp.Port = 2121;
                
            //    //passive true for sftp and false for httsp
            //    ftp.Passive = true;
            //    ftp.UseEpsv = true;
            //    ftp.ConnectTimeout = 20000;
            //    ftp.SetTypeAscii();
            //    Chilkat.Cert cert = new Chilkat.Cert();

            //    success = cert.LoadByCommonName(certName);

            //    if (success == true)
            //    {
            //        ftp.SetSslClientCert(cert);

            //        //Check to see if end-user has access to private key

            //        var hasPrivKey = false;

            //        hasPrivKey = cert.HasPrivateKey();

            //        if (hasPrivKey == false)
            //        {
            //            MessageBox.Show("Certificate is Installed but user cannot access private key", "User must install certificate");
            //        }

            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show("Certificate not installed", "Certificate Error");
            //        return;
            //    }

            //    isExpired = cert.Expired;

            //    if (isExpired == true)
            //    {
            //        MessageBox.Show("Certificate is Expired");
            //        return;
            //    }

            //    if (isExpired == false)
            //    {
            //        //test for upcoming expiration of cert
            //        //Get todays Date 
            //        DateTime Today = DateTime.Today;
                    
            //        //Certificates expiration date 
            //        DateTime expireDate = cert.ValidTo;
            //        //Get the diff between today and expire, must wrap inside 
            //        //the certificate not being expired and it being installed
                     
            //        var Days = (expireDate - Today).Days;
            //        //If Certificates have 30's or less, start nagging 
            //        if (Days <= 30)
            //        {

            //            MessageBox.Show("Certificate will expire in " + Days + " Days" ,"Get New Cert From DMV");
            //        }

            //    }

            //    //Connection 

            //    success = ftp.Connect();

            //    if (success != true)
            //    {
            //        MessageBox.Show("Connection Error" + ftp.LastErrorText);
            //    } 
                 
                


            //    //change the directory
                 
            //    success = ftp.ChangeRemoteDir("/DMVData/FromDMV");

            //    if (success != true)
            //    {
            //        MessageBox.Show("2" + ftp.LastErrorText);
            //        success = ftp.ChangeRemoteDir("/dmv-ddt-router/FromDMV");
            //    }

            //    if (success != true)
            //    {
            //        MessageBox.Show("2" + ftp.LastErrorText);
            //        return;
            //    }

            //    //This gets the number of files in the current directory


            //    int getAllFiles = ftp.MGetFiles("*", configure.filepath);

            //    if (getAllFiles > 0)
            //    {
            //        for (int i = 0; i < getAllFiles; i++)
            //        {
            //            MessageBox.Show("Downloading File " + ftp.GetFilename(i) + "File Size" + ftp.GetSize(i), "File Download");
            //            MessageBox.Show("Deleting files from your SFT Account", "Deleting Files");
            //            ftp.DeleteMatching("*.*");
            //        }
            //    }

            //    if (getAllFiles == 0)
            //    {
            //        MessageBox.Show("There are no files in your SFT account", "SFT Account Empty");
            //    }

            //    if (getAllFiles < 0)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }




            //    if (success != true)
            //    {
            //        MessageBox.Show(ftp.LastErrorText);
            //        return;
            //    }







            //}
            //catch (UriFormatException f)
            //{
            //    Console.WriteLine("Invalid URL" + f);
            //}
            //catch (IOException f)
            //{
            //    Console.WriteLine(f + "Could not connect to URL");
            //}


            // //The below code works for SSH but the big problem is that you cannot
            //// set the transfer type, it just comes down in binary, so that is a big
            //// problem. 
            ////  Important: It is helpful to send the contents of the
            //////  sftp.LastErrorText property when requesting support.

            ////Chilkat.SFtp sftp = new Chilkat.SFtp();
            
            //////  Any string automatically begins a fully-functional 30-day trial.
            ////bool success1;
            ////success1 = sftp.UnlockComponent("Anything for 30-day trial");
            ////if (success1!= true)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  Set some timeouts, in milliseconds:
            ////sftp.ConnectTimeoutMs = 5000;
            ////sftp.IdleTimeoutMs = 10000;

            //////  Connect to the SSH server.
            //////  The standard SSH port = 22
            //////  The hostname may be a hostname or IP address.
            ////int port;
            ////string hostname;
            ////hostname = "sftp.dts.ca.gov";
            ////port = 22;
            ////success1 = sftp.Connect(hostname, port);
            ////if (success1 != true)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  Authenticate with the SSH server.  Chilkat SFTP supports
            //////  both password-based authenication as well as public-key
            //////  authentication.  This example uses password authenication.
            //////success1 = sftp.AuthenticatePw("dmv-ddt-test2", "Frankie@1");
            ////Chilkat.SshKey key = new Chilkat.SshKey();
            ////string privKey;
            ////key.Password = "dmv1";
            ////privKey = key.LoadText(@"C:\Documents and Settings\mvtwv\Desktop\dmv-ddt-test2.ppk");
            ////if (privKey == null)
            ////{
            ////    MessageBox.Show(key.LastErrorText);
            ////    return;
            ////}
           
            ////success1 = key.FromOpenSshPrivateKey(privKey);
           
            ////if (success1 != true)
            ////{
            ////    MessageBox.Show(key.LastErrorText);
            ////    return;
            ////}

            //////  Authenticate with the SSH server.  Chilkat SFTP supports
            //////  both password-based authenication as well as public-key
            //////  authentication.
           
            ////success1 = sftp.AuthenticatePk("dmv-ddt-test2", key);
            ////if (success1 != true)
            ////{
            ////    MessageBox.Show("boop" + sftp.LastErrorText);
            ////    return;
            ////}

            
            ////MessageBox.Show("Public-Key Authentication Successful!");


            ////if (success1 != true)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  After authenticating, the SFTP subsystem must be initialized:
            ////success1 = sftp.InitializeSftp();
            ////if (success1 != true)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  Open a directory on the server...
            //////  Paths starting with a slash are "absolute", and are relative
            //////  to the root of the file system. Names starting with any other
            //////  character are relative to the user's default directory (home directory).
            //////  A path component of ".." refers to the parent directory,
            //////  and "." refers to the current directory.
            
            ////string handle;
            ////string dirPath;
            ////dirPath = "/DMVData/FromDMV";
            ////handle = sftp.OpenDir(dirPath);
            ////sftp.FilenameCharset = "ASCII";
            ////if (handle == null)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  Download the directory listing:
            ////Chilkat.SFtpDir dirListing = null;
            ////dirListing = sftp.ReadDir(handle);
            ////if (dirListing == null)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            //////  Iterate over the files.
            ////int j;
            ////int n;
            ////n = dirListing.NumFilesAndDirs;
            ////if (n == 0)
            ////{
            ////    MessageBox.Show( "No entries found in this directory.");
            ////}
            ////else
            ////{
            ////    for (j = 0; j <= n - 1; j++)
            ////    {
            ////        Chilkat.SFtpFile fileObj = null;
            ////        fileObj = dirListing.GetFileObject(j);
            ////        if (fileObj.FileType == "regular")
            ////        {
            ////            //textBox1.Text += fileObj.Filename + "\r\n";

            ////            //  Does this filename match the desired pattern?
            ////            //  Write code here to determine if it's a match or not.

            ////            //  Assuming it's a match, you would download the file
            ////            //  like this:
            ////            string remoteFilePath;
            ////            remoteFilePath = dirPath + "/";
                         
            ////            remoteFilePath = remoteFilePath + fileObj.Filename;
            ////            string localFilePath;
            ////            localFilePath = configure.filepath + @"\" + fileObj.Filename;
                        
            ////            success1 = sftp.DownloadFileByName(remoteFilePath, localFilePath);
            ////        }
            ////        if (success1 != true)
            ////        {
            ////            MessageBox.Show("Goof" + sftp.LastErrorText);
            ////            return;
            ////        }

            ////    }

            ////}

            //////  Close the directory
            ////success1 = sftp.CloseHandle(handle);
            ////if (success1 != true)
            ////{
            ////    MessageBox.Show(sftp.LastErrorText);
            ////    return;
            ////}

            ////MessageBox.Show("Success.");

        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            
        }

        private void dmvWatchForFiles_Created(object sender, FileSystemEventArgs e)
        {

            //Check for vr inquiry file - Remind User to send  

            if (e.Name == "vrinquiry.txt")
            {
                DialogResult dlgResult = MessageBox.Show("A VR Inquiry File has been created!", "Be sure to Send!");

                
            }

            //Check for parking holds file - Remind User to send  

            if (e.Name == "parkhold.txt")
            {
                DialogResult dlgResult = MessageBox.Show("A VR Parking Holds File has been created!", "Be sure to Send!");

            }

            //Check for dlinquiry file - Remind User to send  

            if (e.Name == "dlinquiry.txt")
            {
                DialogResult dlgResult = MessageBox.Show("A Drivers License Inquiry File has been created!", "Be sure to Send!");
               
            }

            //Employee Pull Notice file - Remind User to send  
            if (e.Name == "epn.txt")
            {
                DialogResult dlgResult = MessageBox.Show("A Employee Pull Notice File has been created!", "Be sure to Send!");

            }
        }


            // Thread cleanThread = new Thread(delegate()
            //{
            //    cleanFile.ReadFile(e.FullPath.ToString());
            //});
            //cleanThread.Start();

            //while (cleanThread.IsAlive == true)
            //{
            //    Thread.Sleep(1000);


            //}

            //if (cleanThread.IsAlive == false)
            //{
            //    Thread dlConThread = new Thread(delegate()
            //    {
            //        dlConversion dl = new dlConversion();
            //        dl.Path = path;
            //        dl.dl414Conversion(path, e.FullPath.ToString());
            //    });
            //    dlConThread.Start();
            //    //workDone = true;

       // }

        private void dmvWatchForFiles_Changed(object sender, FileSystemEventArgs e)
        {
             
        }

        private void dmvWatchForFiles_Deleted(object sender, FileSystemEventArgs e)
        {
             
        }

        private void dmvWatchForFiles_Renamed(object sender, RenamedEventArgs e)
        {
            
        }

        private void requestDriversListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Create file
            DateTime currDate = DateTime.Now;
            var dlistPath = configure.filepath + @"\" + "DLIST_" + String.Format("{0:MMddyy}", currDate) + ".txt";
            StreamWriter recordOut = new StreamWriter(
                    new FileStream(dlistPath, FileMode.Create, FileAccess.Write));
            recordOut.WriteLine(configure.currentRequestorCode);
            recordOut.Close();
            MessageBox.Show("Drivers List file created, BE SURE TO SEND", "Drivers List Requested");

            string copyfile = configure.filepath + @"\" + "dlist.txt";
            File.Delete(copyfile);
            File.Copy(dlistPath, copyfile);
        }

        private void aboutFGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new AboutFGS();
            about.ShowDialog();
             
        }

        private void aboutFGSToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Form aboutFGS = new AboutBox2();
            aboutFGS.ShowDialog();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            process vrm = new process();

            vrm.process_Vr_Monthly_File();

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
                  
           


        //}
        
         

}        
  
}