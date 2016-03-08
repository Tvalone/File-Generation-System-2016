using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace File_Generation_System
{
    public partial class frmDLInquiry : Form
    {
        string requestorCode;
        string epnReportType;
        DataSet cf = new DataSet();
        string filepath;
        int dlCount = 0;
        int epnCount = 0;

        string epnPath;
        string dlPath;
        string governmentEPN;
        string mcdEPN;
        string odmvEPN;
        
        public frmDLInquiry()
        {
            InitializeComponent();
            cf.ReadXml(configure.cfdb);

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = configure.currentRequestorCode.ToString();
                filepath = cfr["drive_files_saved_in"].ToString();
                governmentEPN = cfr["government_epn"].ToString();
                mcdEPN = cfr["mcd_epn"].ToString();
                odmvEPN = cfr["other_dmv_epn"].ToString();

            }
            //Create DateTimePicker and set fifteen years back, then format into readable areas
            dtpBirthdate.Value = DateTime.Now.AddYears(-15);
            dtpBirthdate.CustomFormat = "MMM " + " " + " dd " + " " + " yyyy";
            //cboEPNReportType.SelectedIndex = 0;
            //string dir = @"C:\Temp\Transfer";
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
            //}
   
            
        }
        
        


        private void btnExit_Click(object sender, EventArgs e)
        {
            if (txtDriversLicense.Text.Length == 8)
            {
                MessageBox.Show("Please save data before exiting", "Save");
                txtDriversLicense.Focus();
            }
            else
            {
                this.Close();
                if (epnCount > 0 && dlCount == 0)
                {
                    MessageBox.Show("Total Records written=" + epnCount, "EPN File Count");

                    string copyfile = filepath + @"\" + "epn.txt";
                    File.Delete(copyfile);
                    File.Copy(epnPath, copyfile);
                    //File.Delete(epnPath);

                    MessageBox.Show("Copying file to " + copyfile, "File Copy");
                    DialogResult answer =
                        MessageBox.Show("Would you like to Send File Now?", "Send File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (answer == DialogResult.Yes)
                    {
                        sendReceive send = new sendReceive();
                        send.Send('D');
                    }
                }

                if (dlCount > 0 && epnCount == 0)
                {
                    MessageBox.Show("Total Records written=" + dlCount, "DL Inquiry File ");
                    string copyfile = filepath + @"\" + "dlinquiry.txt";
                    File.Delete(copyfile);
                    File.Copy(dlPath, copyfile);
                    //File.Delete(dlPath);
                    MessageBox.Show("Copying file to " + copyfile, "File Copy");
                    DialogResult answer =
                        MessageBox.Show("Would you like to Send File Now?", "Send File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (answer == DialogResult.Yes)
                    {
                        sendReceive send = new sendReceive();
                        send.Send('D');
                    }
                }

                if (epnCount > 0 && dlCount > 0)
                {
                    MessageBox.Show("Total Records written= " + epnCount, "EPN File ");

                    string copyfile = filepath + @"\" + "epn.txt";
                    File.Delete(copyfile);
                    File.Copy(epnPath, copyfile);
                    //File.Delete(epnPath);
                    MessageBox.Show("Copying file to " + copyfile + "You must Click Send Files to the DMV to Send", "File Copy");

                    MessageBox.Show("Total Records written= " + dlCount, "DL Inquiry File ");
                    string copyfile1 = filepath + @"\" + "dlinquiry.txt";
                    File.Delete(copyfile1);
                    File.Copy(dlPath, copyfile1);
                    //File.Delete(dlPath);

                    MessageBox.Show("Copying file to " + copyfile1 + "You must Click Send Files to the DMV to Send", "File Copy");
                    DialogResult answer =
                    MessageBox.Show("Would you like to Send File Now?", "Send File",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (answer == DialogResult.Yes)
                    {
                        sendReceive send = new sendReceive();
                        send.Send('D');


                    }

                }
            }
        }




        #region EPN-Soundex Choice
        //Set checkboxes to behave as radio buttons
        private void ckbEPNModify_CheckedChanged(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
                panel2.Visible = false;
            else
                panel2.Visible = true;
            if (ckbSSInquiry.Checked == true && ckbEPNModify.Checked == true)
                ckbSSInquiry.Checked = false;
            ckbDriversLicenseInquiry.Checked = false;
        }





        private void ckbSSInquiry_CheckedChanged(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
                panel1.Visible = false;
            else
                panel1.Visible = true;
            if (ckbEPNModify.Checked == true && ckbSSInquiry.Checked == true)
                ckbEPNModify.Checked = false;
                ckbDriversLicenseInquiry.Checked = false;
            if (ckbSSInquiry.Checked == true)
                txtDriversLicense.ReadOnly = true;
            else
                txtDriversLicense.ReadOnly = false;
        }
        #endregion




        #region Reset Values
        //Clear all data entry areas and reset DateTimePicker to initial value
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDriversLicense.Text = "";
            //txtEndUserRequestorCode.Text = "";
            txtFirstName.Text = "";
            txtMiddleInitial.Text = "";
            txtLastName.Text = "";
            txtMiscInfo.Text = "";
            //txtRequestorCode.Text = "";
            dtpBirthdate.Value = DateTime.Now.AddYears(-15);
            ckbSSInquiry.Checked = false;
            ckbEPNModify.Checked = false;
            //cboEPNReportType.SelectedIndex = 0;
            cboEPNTransType.SelectedIndex = -1;
        }
        #endregion




        #region Process Record
        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                if (ckbEPNModify.Checked == true)
                {
                    if (IsValidEPN())
                    {
                        //MessageBox.Show("All EPN fields validated","EPN Validation");
                        WriteEPN();
                        epnCount = epnCount + 1;
                        
                    }                       
                    
                }
                if (ckbSSInquiry.Checked == true)
                {
                    if (IsValidSS())
                    {
                        //MessageBox.Show("All Soundex fields validated","Validation Passed");
                        //Set DateTimePicker to correct format for record and load into string, then return to initial formatting
                        dtpBirthdate.CustomFormat = "MMddyy";
                        string birthDate = dtpBirthdate.Text;
                        dtpBirthdate.CustomFormat = "MMM " + " " + " dd " + " " + " yyyy";
                        WriteSS(birthDate);
                        dlCount = dlCount + 1;
                    }
                }
                if (ckbSSInquiry.Checked == false && ckbEPNModify.Checked == false)
                {
                    if (IsValidDLInquiry())
                    {
                        //MessageBox.Show("All DL Inquiry fields validated","DL Inquiry Validation");
                        WriteDLInquiry();
                        dlCount = dlCount + 1;
                        
                    }
                }
            }
        }
        #endregion




        #region Validation Calls
        public bool IsValid()
        {
            if (txtLastName.Text != "")
            {
                return
                    //Validation.IsValidReqCode(txtEndUserRequestorCode) &&
                    Validation.IsPresent(txtLastName, "Drivers Last Name") &&
                    Validation.IsWord(txtLastName); 
                    //Validation.IsPresent(txtRequestorCode, "Requestor Number") &&
                    //Validation.IsMinLength(txtRequestorCode, "Requestor Number", 5) &&
                    //Validation.IsValidReqCode(txtRequestorCode);
            }
            return
                Validation.IsPresent(txtLastName, "Drivers Last Name") &&
                Validation.IsWord(txtLastName);
                //Validation.IsPresent(txtRequestorCode, "Requestor Number") &&
                //Validation.IsMinLength(txtRequestorCode, "Requestor Number", 5) && 
                //Validation.IsValidReqCode(txtRequestorCode);

        }





        private bool IsValidEPN()
        {
            return
                Validation.IsPresent(txtDriversLicense, "Drivers License") &&
                Validation.IsMinLength(txtDriversLicense, "Drivers License", 8) &&
                Validation.IsValidDL(txtDriversLicense) &&
                Validation.IsSet(cboEPNTransType, "EPN Transaction Type");
        }
        




        private bool IsValidSS()
        {
            if (txtMiddleInitial.Text != "")
            {
                return
                    Validation.IsWord(txtMiddleInitial) &&
                    Validation.IsSixteen(dtpBirthdate) &&
                    Validation.IsPresent(txtFirstName, "Drivers First Name") &&
                    Validation.IsWord(txtFirstName);
            }
            return
                Validation.IsSixteen(dtpBirthdate) &&
                Validation.IsPresent(txtFirstName, "Drivers First Name") &&
                Validation.IsWord(txtFirstName);

        }




        private bool IsValidDLInquiry()
        {
            return
                Validation.IsPresent(txtDriversLicense, "Drivers License") &&
                Validation.IsMinLength(txtDriversLicense, "Drivers License", 8) &&
                Validation.IsValidDL(txtDriversLicense) &&
                Validation.IsValidDLInqName(txtLastName, txtFirstName);
        }

        #endregion



        #region Create Record
        private void WriteEPN()
        {
            string driversLicense = txtDriversLicense.Text.ToString();
            string driverName = txtLastName.Text.ToString() + " " + txtFirstName.Text.ToString();
            //string requestorCode = txtRequestorCode.Text.ToString();
            string epnTranType = cboEPNTransType.Text.ToString();

            
           epnReportType = "35";
             

            
            if (mcdEPN == "True")
            {
                epnReportType = "07";

            }
            if (odmvEPN == "True")
            {
                epnReportType = "37";
            }


            //Add code here to respond to 
            //string epnReportType = cboEPNReportType.Text.ToString();
            string miscInfo = txtMiscInfo.Text.ToString().PadRight(21);
            DateTime currDate = DateTime.Now;
            //string dir = @"C:\Temp\Transfer\";
            string dir = filepath;
            epnPath = dir + @"\" + "EPN_" + String.Format("{0:MMddyy}", currDate) + ".txt";
            if (File.Exists(epnPath))
            {
                //DialogResult answer =
                //    MessageBox.Show("File already exists.  Do you want to append data?", "Append Record",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //if (answer == DialogResult.Yes)
                //{
                    StreamWriter recordOut = new StreamWriter(
                        new FileStream(epnPath, FileMode.Append, FileAccess.Write));
                    recordOut.WriteLine(driversLicense + "1" + driverName.Substring(0, 3) + epnTranType.Substring(0, 1) + "9999" + epnReportType
                        + "042" + requestorCode + miscInfo.Substring(0,21));
                    recordOut.Close();
                     
                    
               // }
            }
            else
            {
                StreamWriter recordOut = new StreamWriter(
                    new FileStream(epnPath, FileMode.Create, FileAccess.Write));
                recordOut.WriteLine(driversLicense + "1" + driverName.Substring(0, 3) + epnTranType.Substring(0, 1) + "9999" + epnReportType
                    + "042" + requestorCode + miscInfo.Substring(0,21));
                recordOut.Close();
                 
            }

             
            
             
            //clear fields after successful record
            txtDriversLicense.Text = "";
            txtFirstName.Text = "";
            txtMiddleInitial.Text = "";
            txtLastName.Text = "";
            txtMiscInfo.Text = "";
            dtpBirthdate.Value = DateTime.Now.AddYears(-15);
        }




        private void WriteDLInquiry()
        {
            string driverName = txtLastName.Text.ToString() + " " + txtFirstName.Text.ToString();
            string driversLicense = txtDriversLicense.Text.ToString();
            //string requestorCode = txtRequestorCode.Text.ToString();
            string miscInfo = txtMiscInfo.Text.ToString().PadRight(20);
            //string endUserReqCode = txtEndUserRequestorCode.Text.ToString().PadLeft(5);
            string blank = "                                       ";
            DateTime currDate = DateTime.Now;
            //string dir = @"C:\Temp\Transfer\";
            string dir = filepath;
            dlPath = dir + @"\" + "DLInq_" + String.Format("{0:MMddyy}", currDate) + ".txt";
            if (File.Exists(dlPath))
            {
                //DialogResult answer =
                //    MessageBox.Show("File already exists.  Do you want to append data?", "Append Record",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //if (answer == DialogResult.Yes)
                //{
                    StreamWriter recordOut = new StreamWriter(
                        new FileStream(dlPath, FileMode.Append, FileAccess.Write));

                    recordOut.WriteLine(driversLicense + driverName.Substring(0,3) + requestorCode + miscInfo.Substring(0,20) + "     " + //endUserReqCode
                         blank);
                    recordOut.Close();
                     
                    
               // }
            }
            else
            {
                StreamWriter recordOut = new StreamWriter(
                    new FileStream(dlPath, FileMode.Create, FileAccess.Write));

                recordOut.WriteLine(driversLicense + driverName.Substring(0, 3) + requestorCode + miscInfo.Substring(0, 20) +  "     " + //endUserReqCode
                     blank);
                recordOut.Close();
                 
            }
             
           

            //clear fields after successful record
            txtDriversLicense.Text = "";
            txtFirstName.Text = "";
            txtMiddleInitial.Text = "";
            txtLastName.Text = "";
            txtMiscInfo.Text = "";
            dtpBirthdate.Value = DateTime.Now.AddYears(-15);
        }




        private void WriteSS(string birthDate)
        {
            string blank1 = "         ";
            //string requestorCode = txtRequestorCode.Text.ToString();
            string driverName = txtLastName.Text.ToString() + " " + txtFirstName.Text.ToString() + " " + txtMiddleInitial.Text.ToString();
            string blank2 = "           ";
            //string endUserReqCode = txtEndUserRequestorCode.Text.ToString().PadLeft(5);
            string miscInfo = txtMiscInfo.Text.ToString().PadRight(20);
            driverName = driverName.PadRight(22);
            DateTime currDate = DateTime.Now;
            //string dir = @"C:\Temp\Transfer\";
            string dir = filepath;
            dlPath = dir + @"\" + "DLInq_" + String.Format("{0:MMddyy}", currDate) + ".txt";
            if (File.Exists(dlPath))
            {
                //DialogResult answer =
                //    MessageBox.Show("File already exists.  Do you want to append data?", "Append Record",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //if (answer == DialogResult.Yes)
                //{
                    StreamWriter recordOut = new StreamWriter(new FileStream(dlPath, FileMode.Append, FileAccess.Write));
                    recordOut.WriteLine("SS" + blank1 + requestorCode + driverName.Substring(0,22) + birthDate + blank2 + "     " //+ endUserReqCode
                        + miscInfo.Substring(0,20));
                    recordOut.Close();
                     
                //}
            }
            else
            {
                StreamWriter recordOut = new StreamWriter(new FileStream(dlPath, FileMode.Create, FileAccess.Write));
                recordOut.WriteLine("SS" + blank1 + requestorCode + driverName.Substring(0, 22) + birthDate + blank2 + "     " //+ endUserReqCode
                    + miscInfo.Substring(0, 20));
                recordOut.Close();
                 
            }
            //clear fields after successful record
            txtDriversLicense.Text = "";
            txtFirstName.Text = "";
            txtMiddleInitial.Text = "";
            txtLastName.Text = "";
            txtMiscInfo.Text = "";
            dtpBirthdate.Value = DateTime.Now.AddYears(-15); 
        }





        #endregion

        private void frmDLInquiry_Load(object sender, EventArgs e)
        {
            //This should just actually say hey configure class just
            //give me the current requestor code. 

           
            currentRequestorCodelbl.Text = configure.currentRequestorCode;
             
        }

        private void ckbDriversLicenseInquiry_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDriversLicenseInquiry.Checked)
            {
                ckbEPNModify.Checked = false;
                ckbSSInquiry.Checked = false;
            }
        }

         

    }
}