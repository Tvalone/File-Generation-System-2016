using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
 
using System.Data.Common;
 
 
 

 
namespace File_Generation_System
{

     

    public partial class config_fgs : Form
    {
        
        public DataSet ds;
        public bool errorflag = false;
        public static bool govEPN = false;
        public static bool mcdEPN;
        public static bool internalDMV;
        public List<char> keyedRequestorCode = new List<char>();
        
        public config_fgs()
       
        {
            InitializeComponent();

            
  

            ds = new DataSet();

            ds.ReadXml(configure.cfdb);

            
            driveFilesSavedInTxtBx.DataBindings.Add("Text", ds, "fgs_config.drive_files_saved_in");
            driveLetterImageSavedInTxtBx.DataBindings.Add("Text", ds, "fgs_config.drive_letter_image_saved_in");
            requestorCodeTxtBx.DataBindings.Add("Text", ds, "fgs_config.requestor_code");
            requestorCodeTxtBx1.DataBindings.Add("Text", ds, "fgs_config.requestor_code1");
            requestorCodeTxtBx2.DataBindings.Add("Text", ds, "fgs_config.requestor_code2");
            //requestorCityTxtBx.DataBindings.Add("Text", ds, "fgs_config.requestor_code");
            vrInquiryChkBx.DataBindings.Add("Checked", ds, "fgs_config.vr_inquiry",true);
            dlInquiriesChkBx.DataBindings.Add("Checked", ds, "fgs_config.dl_inquiry", true);
            vrTaxDelChkBx.DataBindings.Add("Checked", ds, "fgs_config.vr_taxholds", true);
            requestorAgencyNameTxtBx.DataBindings.Add("Text", ds, "fgs_config.name");
            requestorStreetAddressTxtBx.DataBindings.Add("Text", ds, "fgs_config.street_address");
            requestorCityTxtBx.DataBindings.Add("Text", ds, "fgs_config.city");
            requestorStateTxtBx.DataBindings.Add("Text", ds, "fgs_config.state");
            requestorZipTxtBx.DataBindings.Add("Text", ds, "fgs_config.zip");
            requestorEmailTxtBx.DataBindings.Add("Text", ds, "fgs_config.email");
            requestorWebAddress.DataBindings.Add("Text", ds, "fgs_config.web_address");
            requestPhoneNumberTxtBx.DataBindings.Add("Text", ds, "fgs_config.phone");
            //epncheckbox.DataBindings.Add("Checked", ds, "fgs_config.government_epn");
            mcdEPNChkBx.DataBindings.Add("Checked", ds, "fgs_config.mcd_epn");
            internalDMVEPN.DataBindings.Add("Checked", ds, "fgs_config.other_dmv_epn");
            SFTUseridTxtBx.DataBindings.Add("Text", ds, "fgs_config.userid");
            validateRequestorCode.DataBindings.Add("Text", ds, "fgs_config.validated_requestor_code");
            validateRequestorCode1.DataBindings.Add("Text", ds, "fgs_config.validated_requestor_code1");
            validateRequestorCode2.DataBindings.Add("Text", ds, "fgs_config.validated_requestor_code2");
            hostNameTxtBx.DataBindings.Add("Text", ds, "fgs_config.hostname");
            useThisRequestorCodechkbx.DataBindings.Add("Checked", ds, "fgs_config.use_this_requestor_code",true);
            useThisRequestorCodechkbx1.DataBindings.Add("Checked", ds, "fgs_config.use_this_requestor_code1",true); 
            useThisRequestorCodechkbx2.DataBindings.Add("Checked", ds, "fgs_config.use_this_requestor_code2",true); 
      
             
 
            
        }

        private void config_fgs_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vrdataDataSet.configuration_data' table. You can move, or remove it, as needed.
           // this.configuration_dataTableAdapter.Fill(this.vrdataDataSet.configuration_data);
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            ds.RejectChanges();
            this.Close();
            
        }

        private static void InstallCertificate(string certFileName)
        {

            // The path to the certificate.
            string Certificate = certFileName;

            
            //cert.Import(certFileName);

             

             
            
            try
            {
                FileInfo info = new FileInfo(certFileName);

                X509Certificate2 cert = new X509Certificate2(certFileName);

                 
                X509Store store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);

                store.Open(OpenFlags.ReadWrite);
                store.Add(cert);
                store.Close();

     
                 
 
            }
            catch (Exception ct)
            {
                MessageBox.Show("Certificate is not in Drive files saved in" + ct);
               
            }

        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void passwordExireDateTxtBx_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void useridTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void driveLetterImageSavedInTxtBx_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void requestorCodeTxtBx_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void requestorCodeTxtBx_Click(object sender, EventArgs e)
        {
           
        }

        private void requestorCodeTxtBx_Leave(object sender, EventArgs e)
        {
            bool isGood = Validation.IsValidReqCode(((TextBox)sender)); 

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
             //Although it says button1 it acutally is updating the fgsconfig xml
            // file. If the error flag is false go ahead and update

           
                //Get folder selected to save files to. 
                string dr = driveFilesSavedInTxtBx.Text;
                
             
                try
                {
                    DirectoryInfo myDir = new DirectoryInfo(dr);
                    

                    //check to see if it exists  

                    if (myDir.Exists)  
                    {
                        //Check to see if the Customer has write access to the folder
                        //returns False if they cannot write. 
                        var canWrite = hasWriteAccessToFolder(dr);
                        if (canWrite)
                        {
                            this.errorflag = false;
                            errorlstbx.Items.Clear();
                            errorlstbx.Visible = false;
                        }
                        if (!canWrite)
                        {
                            this.errorflag = true;
                            errorlstbx.Visible = true;
                            errorlstbx.Items.Add("You do not have write access to this folder");
                            driveFilesSavedInTxtBx.Focus();
                        }


                        //ds.AcceptChanges();
                        //ds.WriteXml(configure.cfdb);
                    }

                    if (myDir.Exists == false)
                    {

                        throw new Exception();
                    }
                }
                catch (DriveNotFoundException fx)
                {
                    this.errorflag = true;
                    errorlstbx.Visible = true;
                    errorlstbx.Items.Add("Directory entered does not exist");
                    driveFilesSavedInTxtBx.Focus();


                }
                catch (Exception Ex)
                {
                    this.errorflag = true;
                    errorlstbx.Visible = true;
                    errorlstbx.Items.Add("This is not a Directory" + Ex);
                    driveFilesSavedInTxtBx.Focus();

                }

                 

            if (vrInquiryChkBx.Checked & driveLetterImageSavedInTxtBx.Text.Length < 5)
                {
                    this.errorflag = true;
                    errorlstbx.Visible = true;
                    errorlstbx.Items.Add("VR processes require selecting an image file for processing");
                    driveLetterImageSavedInTxtBx.Focus();
                }

            if (!useThisRequestorCodechkbx.Checked & !useThisRequestorCodechkbx1.Checked & !useThisRequestorCodechkbx2.Checked)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("One Requestor Code Must be Selected");
                 
            }

            if (useThisRequestorCodechkbx.Checked & useThisRequestorCodechkbx1.Checked & useThisRequestorCodechkbx2.Checked)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("ONLY One Requestor Code Must be Selected");

            }
            if (useThisRequestorCodechkbx.Checked & useThisRequestorCodechkbx1.Checked)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("ONLY One Requestor Code Must be Selected");
            }

            if (useThisRequestorCodechkbx.Checked & useThisRequestorCodechkbx2.Checked)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("ONLY One Requestor Code Must be Selected");
            }
            if (useThisRequestorCodechkbx1.Checked  & useThisRequestorCodechkbx2.Checked)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("ONLY One Requestor Code Must be Selected");

            }
            if (requestorCodeTxtBx.Text != validateRequestorCode.Text)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("Requestor/Validated Requestor Code No Match");
                validateRequestorCode.Focus();
            }

            if (SFTUseridTxtBx.Text.Trim().Length == 0)
            {
                this.errorflag = true;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("SFT Userid Cannot Be Blank");
                SFTUseridTxtBx.Focus();
            }

            

            //Cert checks first load the cert, then see if the private key is available  
 
            Chilkat.Cert cert = new Chilkat.Cert();
            bool success;
            var Username = SFTUseridTxtBx.Text.Trim();
            //Changed for DMV SFT Server
            //string certName = "dmv-ddt-" + Username + "-x509";
            string certName = Username.Trim() + "-x509" ;
            success = cert.LoadByCommonName(certName);
            if (success == false) {
                MessageBox.Show("Certificate Not Installed","Could not Find Certificate - please install");
            }
            else {
                bool hasPrvKey;
                hasPrvKey = cert.HasPrivateKey();
                if (hasPrvKey == true) {
                }
                else {
                    MessageBox.Show("Certificate installed by Admin, please have end user install certificate","Cert Error 01");
                }
            } 

            //Check to see if customer has access to Secure Server and port 2121 is open 
            

            System.Net.Sockets.TcpClient client = new TcpClient();
            try
            {
              client.Connect(hostNameTxtBx.Text, 2121);
              var portOk = client.Connected;
              if (portOk)
              {
                  //MessageBox.Show("Port 2121 is open","Certificates can be used!");
                   
              }
             

            } catch (SocketException sk)
            {
                //6.10.15 changed to false to allow for customer to still config
                MessageBox.Show(sk.Message, "Open port 2121/Or DNS Error");
                errorflag = false;
                errorlstbx.Visible = true;
                errorlstbx.Items.Add("Port2121 not open or Customer DNS cannot resolve host");
            }
            finally
            {
                client.Close();
            }

            
                    

            
            if (errorflag == false)
            {

                try
                {
                    

                    ds.AcceptChanges();
                    ds.WriteXml(configure.cfdb);
                    MessageBox.Show("Configuration File Updated", "Configuration Updated");
                    configure.getInformationFromConfig();
                     
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update Config file", "Error");
                    MessageBox.Show("Exception= " + ex);

                }
                 

            }
            
             
        }
        private bool hasWriteAccessToFolder(string folderPath)
        {
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        private void configueFGSMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

            private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void driveFilesSavedInTxtBx_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void driveFilesSavedInTxtBx_Click(object sender, EventArgs e)
        {
 
             if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
             {
                 driveFilesSavedInTxtBx.Text = folderBrowserDialog1.SelectedPath.ToString();
             }

        }

        private void driveLetterImageSavedInTxtBx_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofs = new OpenFileDialog();
            ofs.Filter = "image files (*.jpg)|*.jpg";
            if (ofs.ShowDialog() == DialogResult.OK)
            {
                driveLetterImageSavedInTxtBx.Text = ofs.FileName;

                driveLetterImageSavedInTxtBx.Refresh();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void vrInquiryChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (vrInquiryChkBx.Checked == true)
            {
                vrTaxDelChkBx.Visible = true;
                requestorContactPnl.Visible = true;
                driveLetterImageSavedInLbl.Visible = true;
                driveLetterImageSavedInTxtBx.Visible = true;
                //epncheckbox.Visible = false;
            }
            else
            {
                vrTaxDelChkBx.Visible = false;
                //epncheckbox.Visible = true;
            }

            if ((vrInquiryChkBx.Checked == true) && (dlInquiriesChkBx.Checked == true))
            {
                vrTaxDelChkBx.Visible = true;
                requestorContactPnl.Visible = true;
                driveLetterImageSavedInLbl.Visible = true;
                driveLetterImageSavedInTxtBx.Visible = true;
                //epncheckbox.Visible = true;
            }

            if ((vrInquiryChkBx.Checked == false) && (dlInquiriesChkBx.Checked == false))
            {
                vrTaxDelChkBx.Visible = false;
                requestorContactPnl.Visible = false;
                driveLetterImageSavedInLbl.Visible = false;
                driveLetterImageSavedInTxtBx.Visible = false;
                //epncheckbox.Visible = false;
            }

            if ((vrInquiryChkBx.Checked == false) && (dlInquiriesChkBx.Checked == true))
            {
                vrTaxDelChkBx.Visible = false;
                requestorContactPnl.Visible = false;
                driveLetterImageSavedInLbl.Visible = false;
                driveLetterImageSavedInTxtBx.Visible = false;
                //epncheckbox.Visible = true;
            }
        }

        private void vrTaxDelChkBx_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dlInquiriesChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if ((dlInquiriesChkBx.Checked == true) && (vrInquiryChkBx.Checked == false))
            {
                requestorContactPnl.Visible = false;
                //epncheckbox.Visible = true;
                driveLetterImageSavedInLbl.Visible = false;
                driveLetterImageSavedInTxtBx.Visible = false;
            }

            if ((dlInquiriesChkBx.Checked == false && vrInquiryChkBx.Checked == true))
            {
                requestorContactPnl.Visible = true;
                //epncheckbox.Visible = false;
            }

            if ((dlInquiriesChkBx.Checked == false && vrInquiryChkBx.Checked == false))
            {
                requestorContactPnl.Visible = false;
                //epncheckbox.Visible = false;
            }

            if ((dlInquiriesChkBx.Checked == true && vrInquiryChkBx.Checked == true))
            {
                requestorContactPnl.Visible = true;
                //epncheckbox.Visible = true;
            }
        }

        private void epncheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //if (epncheckbox.Checked == true)
            //{
                
            //   warningLbl.Text =  "If you are NOT a government agency,leave this box unchecked";
            //}

            //if (epncheckbox.Checked == false)
            //{

            //    warningLbl.Text = null;
            //}
             

        }

        private void mcdEPNChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (mcdEPNChkBx.Checked == true)
            {
                mcdEPN = true;
            }

            if (mcdEPNChkBx.Checked == false)
            {
                mcdEPN = false;
            }

        }

        private void internalDMVEPN_CheckedChanged(object sender, EventArgs e)
        {
            if (internalDMVEPN.Checked == true)
            {
                internalDMV = true;
            }

            if (internalDMVEPN.Checked == false)
            {
                internalDMV = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            configure.getInformationFromConfig();
           // installCertificate(configure.filepath + "\\" + "dmv-ddt-test2.ppk");
            //InstallCertificate(@"C:\Program Files\Microsoft Visual Studio 8\Common7\Tools\Bin\dmvtest1.cer");
        }

        private void requestorCodeTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%'
                || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')'
                || e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == '~' || e.KeyChar == '`' || e.KeyChar == '{'
                || e.KeyChar == '}' || e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '|' || e.KeyChar == ':'
                || e.KeyChar == ';' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == ',' || e.KeyChar == '.'
                || e.KeyChar == '?' || e.KeyChar == ' ')
                e.Handled = true;


            keyedRequestorCode.Add(e.KeyChar); 
            

        }

        private void requestorCode_Popup(object sender, PopupEventArgs e)
        {
             

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void validateRequestorCode_Leave(object sender, EventArgs e)
        {
            if (requestorCodeTxtBx.Text != validateRequestorCode.Text)
            {
             
                MessageBox.Show("Requestor code/Validation Requestor Code", "No Match");
                validateRequestorCode.Focus();
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Form mrc = new multipleRequestorCodes();
            mrc.ShowDialog();

        }

        private void requestorCodeTxtBx1_Leave(object sender, EventArgs e)
        {
            bool isGood = Validation.IsValidReqCode(((TextBox)sender));
        }

        private void requestorCodeTxtBx2_TextChanged(object sender, EventArgs e)
        {

        }

        private void requestorCodeTxtBx2_Leave(object sender, EventArgs e)
        {
            bool isGood = Validation.IsValidReqCode(((TextBox)sender));
        }

        private void useThisRequestorCodechkbx_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void useThisRequestorCodechkbx1_CheckedChanged(object sender, EventArgs e)
        {

             
        }

        private void useThisRequestorCodechkbx2_CheckedChanged(object sender, EventArgs e)
        {

             
        }

        private void validateRequestorCode1_Leave(object sender, EventArgs e)
        {
            if (requestorCodeTxtBx1.Text != validateRequestorCode1.Text)
            {
                MessageBox.Show("Requestor code/Validation Requestor Code", "No Match");
                validateRequestorCode.Focus();
            }
        }

        private void validateRequestorCode2_Leave(object sender, EventArgs e)
        {
            if (requestorCodeTxtBx2.Text != validateRequestorCode2.Text)
            {
                MessageBox.Show("Requestor code/Validation Requestor Code", "No Match");
                validateRequestorCode.Focus();
            }
        }
    }
}