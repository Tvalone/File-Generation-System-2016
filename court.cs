using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;


 



namespace File_Generation_System
{
    
    public partial class courtAbstract : Form
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
        public bool Bachaschars = false;
        public DataSet cf = new DataSet();
        public string filepath;
        public string courtPath;
        //fullSectViolatedLine to hold all abstracts
        public string fullSectViolatedLine;
        public List<string> courtAbstractLine = new List<string>();
        public string courtWriteLine;
        public List<string> dispCodes = new List<string>();
        public List<string> probCodes = new List<string>();
        public string lastName;
        public string firstName;
        public string middleName;
        public string suffix;
        public List<ListBox> dispListOfListBoxes = new List<ListBox>();
        public List<TextBox> sectViolList = new List<TextBox>();
        bool A40001;
        bool A40001A;
        bool A40001B;
         
        

        public int match;
    
        public courtAbstract()
        {
            InitializeComponent();

            cf.ReadXml(configure.cfdb);

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = configure.currentRequestorCode.ToString();

                filepath = cfr["drive_files_saved_in"].ToString();
            }
        }

        private void exitTransferScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transfer_Load(object sender, EventArgs e)
        {

            label3.Text = configure.currentRequestorCode.ToString();
            transactionLstBx.SelectedItem = "D10";
            outOfStateLstBx.SelectedItem = -1;
            amendedCodeLstBx.SelectedItem = -1;
            dispCodeLstBx1.SelectedIndex = -1;

            suffixLstBx.SelectedItem = 0;

            


        }

        private void vRInquiriesToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //Create Dataset
            ds = new DataSet();

            //Read XML document into dataset
            ds.ReadXml(configure.vrdb);

            //This allows for editing
            ds.EnforceConstraints = false;

            //Create new XML document
            doc = new XmlDataDocument(ds);

            //Create a nodelist with just Licenses         
            XmlNodeList nodeLst = doc.SelectNodes("//vrdatabase/vr_record/inquiry_data/license_number");

            int nodeTotal = nodeLst.Count;

            MessageBox.Show("node total=" + nodeTotal);


            //Create Stream reader eventually point to real file
            FileStream fileStream = new FileStream(@"U:\1201out.txt",FileMode.Open,FileAccess.Read);
            sr = new StreamReader(fileStream);
            
                        
            
            //Declare a counter
            int totalRecords = 0;

            //create the declaration if not created do this on first run or whenever file
            // does not exist
            //XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", null, null);


            
            while ((nextLine = sr.ReadLine()) != null)
            {

                cardNumber = nextLine.Substring(nextLine.Length - 1, 1);
                licenseNumber = nextLine.Substring(0, 7);
                

                    if (cardNumber == "1")  
                    {
                        if (nextLine.Substring(19, 1) == "V")
                        {
                            vin = nextLine.Substring(27, 17);
                            updateXmlDatabase(vin, "1", 1);
                        }
                        


                       updateXmlDatabase(nextLine.Substring(0, 7), "1");
                       
                         
                    }
                    if (cardNumber == "2")  
                    {

                       
                        
                        regExpireDate = nextLine.Substring(8, 6);
                        yearModel     = nextLine.Substring(15, 2);
                        make          = nextLine.Substring(18, 5);
                        vin           = nextLine.Substring(24, 30);
                        roCity        = nextLine.Substring(55, 13);
                        roZip         = nextLine.Substring(69, 5);
                        roCountyCode  = nextLine.Substring(75, 2);

                         
                        updateXmlDatabase(vin, "2", 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "2" );
                    }
                    if (cardNumber == "3")
                    {
                        
                        paperIssueDate = nextLine.Substring(8, 6);
                        roNameAndAddressSourceIndicator = nextLine.Substring(14, 1);
                        roName = nextLine.Substring(15, 30);
                        roNameOrAddress = nextLine.Substring(46, 30);

                        updateXmlDatabase(vin, "3", 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "3" );
                    }
                    if (cardNumber == "4")
                    {
                         
                        additionalRoNameOrAddress  = nextLine.Substring(8, 30);
                        additionalRoAddress =  nextLine.Substring(31, 30);

                        updateXmlDatabase(nextLine.Substring(0, 7), "4" ); 
                    }
                    if (cardNumber == "5")
                    {
                         
                        recordConditionCode     = nextLine.Substring(8, 2);
                        recordConditionCodeDate = nextLine.Substring(11, 6);
                        recordRemarks           = nextLine.Substring(18, 30);

                        updateXmlDatabase(nextLine.Substring(0, 7), "5") ;
                         
                    }

                    if (cardNumber == "6")
                    {
                    
                        errorCode     = nextLine.Substring(8, 2);
                        errorMessage  = nextLine.Substring(13, 45);
                        errorDate     = nextLine.Substring(59, 6);

                        updateXmlDatabase(nextLine.Substring(0, 7), "6");
                        
                    }
                    if (cardNumber == "7")
                    {
                        
                        nrlDate          = nextLine.Substring(7, 6);
                        nrlTransferDate  = nextLine.Substring(14, 15);
                        nrlNameOrMessage = nextLine.Substring(30, 30);
                        nrlRecordId      = nextLine.Substring(65, 1);

                        updateXmlDatabase(nextLine.Substring(0, 7), "7");
                         
                    }
                    if (cardNumber == "8")
                    {
                         
                        nrlAddressMessage = nextLine.Substring(8, 30);
                        nrlCityStateMessage = nextLine.Substring(39, 25);
                        
                        updateXmlDatabase(nextLine.Substring(0, 7), "8");                       
                         
                    }


                 
                 if (oldplate != licenseNumber)
                 {


                     totalRecords = totalRecords + 1;
 

                     writeOutXml();
                 }
                //when the old plate != new plate write out XML

                 oldplate = licenseSwitch(licenseNumber);
                              

            } //ends while
            MessageBox.Show("Total Records read =" + totalRecords);
        }
        private string licenseSwitch(string newplate)
        {

            oldplate = newplate;
             
            return oldplate;
        }

        private void updateXmlDatabase(String LicenseNumber,String cardNumber)
        {

            foreach (DataRow rw in ds.Tables["inquiry_data"].Rows)
            {

                

                  
                match = string.Compare(rw[0].ToString(),LicenseNumber.Trim());


                if (match == 0 && cardNumber == "2")
                {

                    rw.BeginEdit();
                    rw["reg_expire_date"] = regExpireDate;
                    rw["make"] = make;
                    rw["vin"] = vin;
                    rw["ro_city"] = roCity;
                    rw["ro_zip"] = roZip;
                    rw["ro_county_code"] = roCountyCode;
                    rw["year_model"] = yearModel;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;

                }

                                

                if (match == 0 && cardNumber == "3") 
                    
                {
                        
                        rw.BeginEdit();
                        rw["paper_issue_date"] = paperIssueDate;
                        rw["ro_nameaddress_source_indicator"] = roNameAndAddressSourceIndicator;
                        rw["ro_name"] = roName;
                        rw["ro_name_or_address"] = roNameOrAddress;
                        MessageBox.Show("=" + roNameOrAddress);
                        rw.EndEdit(); 
                        ds.AcceptChanges();
                        ds.WriteXml(configure.vrdb);
                        match = 1;
                        break; 
                   
                }

                if (match == 0 && cardNumber == "4")
                {
                    
                    rw.BeginEdit();
                    rw["additional_ro_name_or_address"] = additionalRoNameOrAddress;
                    rw["additional_ro_address"] = additionalRoAddress;
                     
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;  

                }

                if (match == 0 && cardNumber == "5")
                {
                   
                    rw.BeginEdit();
                    rw["record_condition_code"] = recordConditionCode;
                    rw["record_condition_code_date"] = recordConditionCodeDate;
                    rw["record_remarks"] = recordRemarks; 
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break; 

                       

                }

                if (match == 0 && cardNumber == "6")
                {
                   
                    rw.BeginEdit();
                    rw["error_code"] = errorCode;
                    rw["error_message"] = errorMessage;
                    rw["error_date"] = errorDate; 
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break; 

                       

                }

                if (match == 0 && cardNumber == "7")
                {

                    rw.BeginEdit();
                    rw["nrl_date"] = nrlDate;
                    rw["nrl_transfer_date"] = nrlTransferDate;
                    rw["nrl_name_or_message"] = nrlNameOrMessage;
                    rw["nrl_record_id"] = nrlRecordId ;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break; 
                    

                }

                if (match == 0 && cardNumber == "8")
                {

                    rw.BeginEdit();
                    rw["nrl_address_message"] = nrlAddressMessage;
                    rw["nrlCityStateMessage"] = nrlCityStateMessage;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break; 
     


                }
                
                       
                 
            }

            

        }

        private void updateXmlDatabase(String vin, String cardNumber, int nothing)
        {

            foreach (DataRow rw in ds.Tables["inquiry_data"].Rows)
            {



                int matcha = string.Compare(rw["vin"].ToString().Trim(), vin.Trim());

                if (matcha == 0 && cardNumber == "1")
                {

                     
                    rw.BeginEdit();
                    rw["license_number"] = licenseNumber;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;

                }

                if (matcha == 0 && cardNumber == "2")
                {

               
                    rw.BeginEdit();
                    rw["license_number"] = licenseNumber;
                    rw["reg_expire_date"] = regExpireDate;
                    rw["make"] = make;
                    rw["vin"] = vin;
                    rw["ro_city"] = roCity;
                    rw["ro_zip"] = roZip;
                    rw["ro_county_code"] = roCountyCode;
                    rw["year_model"] = yearModel;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;

                }

                if (matcha == 0 && cardNumber == "3")
                {

                    rw.BeginEdit();
                    rw["paper_issue_date"] = paperIssueDate;
                    rw["ro_nameaddress_source_indicator"] = roNameAndAddressSourceIndicator;
                    rw["ro_name"] = roName;
                    rw["ro_name_or_address"] = roNameOrAddress;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;

                }

                if (matcha == 0 && cardNumber == "4")
                {

                    rw.BeginEdit();
                    rw["additional_ro_name_or_address"] = additionalRoNameOrAddress;
                    rw["additional_ro_address"] = additionalRoAddress;

                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;

                }

                if (matcha == 0 && cardNumber == "5")
                {

                    rw.BeginEdit();
                    rw["record_condition_code"] = recordConditionCode;
                    rw["record_condition_code_date"] = recordConditionCodeDate;
                    rw["record_remarks"] = recordRemarks;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;



                }

                if (matcha == 0 && cardNumber == "6")
                {

                    rw.BeginEdit();
                    rw["error_code"] = errorCode;
                    rw["error_message"] = errorMessage;
                    rw["error_date"] = errorDate;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;



                }

                if (matcha == 0 && cardNumber == "7")
                {

                    rw.BeginEdit();
                    rw["nrl_date"] = nrlDate;
                    rw["nrl_transfer_date"] = nrlTransferDate;
                    rw["nrl_name_or_message"] = nrlNameOrMessage;
                    rw["nrl_record_id"] = nrlRecordId;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;


                }

                if (matcha == 0 && cardNumber == "8")
                {

                    rw.BeginEdit();
                    rw["nrl_address_message"] = nrlAddressMessage;
                    rw["nrlCityStateMessage"] = nrlCityStateMessage;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    match = 1;
                    break;


                }



            }



        }

           private void writeOutXml()
          {

              XmlTextWriter tr = new XmlTextWriter(configure.vrdb, null);
              tr.Formatting = Formatting.Indented;
              doc.WriteContentTo(tr);
              tr.Close();
          }

        private void vRInquiriesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            writeDmvFile wdf = new writeDmvFile();
            this.write_DMV_file();
        }
        public void write_DMV_file()
        {
            //Create Dataset for vrdatabase
            DataSet da = new DataSet();
            //Create Dataset for config file
            DataSet cf = new DataSet();
            //Read XML document into dataset for vrdatabase (da)
            //and config file (cf)
            da.ReadXml(configure.vrdb);
            cf.ReadXml(configure.cfdb);



            //Create Stream reader eventually point to real file
            FileStream vrInqFileStream = new FileStream("dmvinq.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sa = new StreamWriter(vrInqFileStream);

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = configure.currentRequestorCode.ToString();
            }

            foreach (DataRow ra in da.Tables["inquiry_data"].Rows)
            {
                if (ra["ro_city"].ToString().Length == 0 && ra["error_message"].ToString().Length == 0) 
                {
                     
                    string vrinqline = requestorCode + "1" + ra["as_of_date"].ToString() + ra["file_code"].ToString() + ra["license_number"].ToString() +
                    "                                   " + ra["user_information"].ToString();
                    sa.WriteLine(vrinqline);
              

                    
                }
                
            }
            sa.Close();
        }

        private void vRMonthlyFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //This code reads in the monthly parking report file

                //Create Dataset
                ds = new DataSet();

                //Read XML document into dataset
                ds.ReadXml(configure.vrdb);

                //This allows for editing
                ds.EnforceConstraints = false;

                //Create new XML document
                doc = new XmlDataDocument(ds);

                //Create a nodelist with just Licenses         
                XmlNodeList nodeLst = doc.SelectNodes("//vrdatabase/vr_record/inquiry_data/license_number");

                int nodeTotal = nodeLst.Count;

                MessageBox.Show("node total=" + nodeTotal);


                //Create Stream reader eventually point to real file
                FileStream fileStream = new FileStream("vrmthyout.txt", FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fileStream);



                //Declare a counter
                int totalRecords = 0;

                //create the declaration if not created do this on first run or whenever file
                // does not exist
                //XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", null, null);



                while ((nextLine = sr.ReadLine()) != null)
                {

                    cardNumber = nextLine.Substring(nextLine.Length - 1, 1);
                    licenseNumber = nextLine.Substring(0, 7);


                    if (cardNumber == "1")
                    {
                        updateXmlDatabase(nextLine.Substring(0, 7), "1");

                    }
                    if (cardNumber == "2")
                    {

                        regExpireDate = nextLine.Substring(8, 6);
                        yearModel = nextLine.Substring(15, 2);
                        make = nextLine.Substring(18, 5);
                        vin = nextLine.Substring(24, 30);
                        roCity = nextLine.Substring(55, 13);
                        roZip = nextLine.Substring(69, 5);
                        roCountyCode = nextLine.Substring(75, 2);
                        updateXmlDatabase(vin, "2", 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "2");
                    }
                    if (cardNumber == "3")
                    {

                        paperIssueDate = nextLine.Substring(8, 6);
                        roNameAndAddressSourceIndicator = nextLine.Substring(14, 1);
                        roName = nextLine.Substring(15, 30);
                        roNameOrAddress = nextLine.Substring(46, 30);

                        updateXmlDatabase(nextLine.Substring(0, 7), "3");
                    }
                    if (cardNumber == "4")
                    {

                        additionalRoNameOrAddress = nextLine.Substring(8, 30);
                        additionalRoAddress = nextLine.Substring(31, 30);

                        updateXmlDatabase(nextLine.Substring(0, 7), "4");
                    }
                    if (cardNumber == "5")
                    {

                        recordConditionCode = nextLine.Substring(8, 2);
                        recordConditionCodeDate = nextLine.Substring(11, 6);
                        recordRemarks = nextLine.Substring(18, 30);

                        updateXmlDatabase(nextLine.Substring(0, 7), "5");

                    }

                    if (cardNumber == "6")
                    {

                        errorCode = nextLine.Substring(8, 2);
                        errorMessage = nextLine.Substring(13, 45);
                        errorDate = nextLine.Substring(59, 6);

                        updateXmlDatabase(nextLine.Substring(0, 7), "6");

                    }
                    if (cardNumber == "7")
                    {

                        nrlDate = nextLine.Substring(7, 6);
                        nrlTransferDate = nextLine.Substring(14, 15);
                        nrlNameOrMessage = nextLine.Substring(30, 30);
                        nrlRecordId = nextLine.Substring(65, 1);

                        updateXmlDatabase(nextLine.Substring(0, 7), "7");

                    }
                    if (cardNumber == "8")
                    {

                        nrlAddressMessage = nextLine.Substring(8, 30);
                        nrlCityStateMessage = nextLine.Substring(39, 25);

                        updateXmlDatabase(nextLine.Substring(0, 7), "8");

                    }



                    if (oldplate != licenseNumber)
                    {


                        totalRecords = totalRecords + 1;


                        writeOutXml();
                    }
                    //when the old plate != new plate write out XML

                    oldplate = licenseSwitch(licenseNumber);


                } //ends while
                MessageBox.Show("Total Records read =" + totalRecords);
            }

        }

        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sendEPNTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 1)
            {
                sectvioTxtBx1.Visible = true;
                sectvioTxtBx2.Visible = false;
                sectViolatedlbl2.Visible = false;
                sectvioTxtBx3.Visible = false;
                sectViolatedlbl3.Visible = false;
                sectvioTxtBx4.Visible = false;
                sectViolatedlbl4.Visible = false;
                sectvioTxtBx5.Visible = false;
                sectViolatedlbl5.Visible = false;
                sectvioTxtBx6.Visible = false;
                sectViolatedlbl6.Visible = false;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }

            if (numericUpDown1.Value == 2)
            {
                sectvioTxtBx1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = false;
                sectViolatedlbl3.Visible = false;
                sectvioTxtBx4.Visible = false;
                sectViolatedlbl4.Visible = false;
                sectvioTxtBx5.Visible = false;
                sectViolatedlbl5.Visible = false;
                sectvioTxtBx6.Visible = false;
                sectViolatedlbl6.Visible = false;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }
            if (numericUpDown1.Value == 3)
            {
                sectvioTxtBx1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = true;
                sectViolatedlbl3.Visible = true;
                sectvioTxtBx4.Visible = false; 
                sectViolatedlbl4.Visible = false;
                sectvioTxtBx5.Visible = false;
                sectViolatedlbl5.Visible = false;
                sectvioTxtBx6.Visible = false;
                sectViolatedlbl6.Visible = false;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }

            if (numericUpDown1.Value == 4)
            {
                sectvioTxtBx1.Visible = true;
                sectViolatedlbl1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = true;
                sectViolatedlbl3.Visible = true;
                sectvioTxtBx4.Visible = true;
                sectViolatedlbl4.Visible = true;
                sectvioTxtBx5.Visible = false;
                sectViolatedlbl5.Visible = false;
                sectvioTxtBx6.Visible = false;
                sectViolatedlbl6.Visible = false;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }
            if (numericUpDown1.Value == 5)
            {
                sectvioTxtBx1.Visible = true;
                sectViolatedlbl1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = true;
                sectViolatedlbl3.Visible = true;
                sectvioTxtBx4.Visible = true;
                sectViolatedlbl4.Visible = true;
                sectvioTxtBx5.Visible = true;
                sectViolatedlbl5.Visible = true;
                sectvioTxtBx6.Visible = false;
                sectViolatedlbl6.Visible = false;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }

            if (numericUpDown1.Value == 6)
            {
                sectvioTxtBx1.Visible = true;
                sectViolatedlbl1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = true;
                sectViolatedlbl3.Visible = true;
                sectvioTxtBx4.Visible = true;
                sectViolatedlbl4.Visible = true;
                sectvioTxtBx5.Visible = true;
                sectViolatedlbl5.Visible = true;
                sectvioTxtBx6.Visible = true;
                sectViolatedlbl6.Visible = true;
                sectvioTxtBx7.Visible = false;
                sectViolatedlbl7.Visible = false;
            }
            if (numericUpDown1.Value == 7)
            {
                sectvioTxtBx1.Visible = true;
                sectViolatedlbl1.Visible = true;
                sectvioTxtBx2.Visible = true;
                sectViolatedlbl2.Visible = true;
                sectvioTxtBx3.Visible = true;
                sectViolatedlbl3.Visible = true;
                sectvioTxtBx4.Visible = true;
                sectViolatedlbl4.Visible = true;
                sectvioTxtBx5.Visible = true;
                sectViolatedlbl5.Visible = true;
                sectvioTxtBx6.Visible = true;
                sectViolatedlbl6.Visible = true;
                sectvioTxtBx7.Visible = true;
                sectViolatedlbl7.Visible = true;
            }
        }

        private void amendedCodeLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            //validateSectionsViolated(sender);
        }

        private bool IsValidD10()
        {
            return
            Validation.IsValidDL(driversLicenseTxtBx);
        }

        private void validateSectionsViolated(Object sender)
        {
            //General clear

            ((TextBox)sender).BackColor = Color.White;
            errorLstBx.Items.Clear();
            errorLstBx.Visible = false;

            if (((TextBox)sender).Text.Length == 0 && (((TextBox)sender).Name != "termCourtSuspensionTxtBx"))
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Section Violdated Cannot be blank");
            }
             

            //Dont exactly know what i was trying to do here 
           

            //if (((TextBox)sender).Text.Length == 2)
            //{
            //    string zero = "0";
            //    string hold = (((TextBox)sender).Text.Substring(0,2));
            //    ((TextBox)sender).Text = zero + hold;
                              
                 
            //}

            //if (((TextBox)sender).Text.Length == 1)
            //{
            //    string zero = "00";
            //    string hold = (((TextBox)sender).Text.Substring(0, 1));
            //    ((TextBox)sender).Text = zero + hold;


            //}


            if   (((TextBox)sender).Text.Length == 3 &&  (((TextBox)sender).Name == "termCourtSuspensionTxtBx" || (((TextBox)sender).Name =="jailTxtBx")))
            {


                ((TextBox)sender).BackColor = Color.White;
                errorLstBx.Items.Clear();
                errorLstBx.Visible = false;

                //sch1 if WMY is found then check the next two bytes to insure they are numbers
                //checking done by trying to convert the second and third byte of the field

                int sch1 = (((TextBox)sender).Text.Substring(0, 1).IndexOfAny("WMY-".ToCharArray()));

                if (sch1 == 0)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        
                        char c = Convert.ToChar((((TextBox)sender).Text.Substring(i,1))); 
                                                  
                        if (char.IsNumber(c) == false) 
                        {
                            ((TextBox)sender).Focus();
                            ((TextBox)sender).BackColor = Color.Red;
                             errorLstBx.Visible = true;
                             errorLstBx.Items.Add("Term Court must have a valid number after W M or Y");
                        }

                        
                            
                    }
                    if ((((TextBox)sender).Text.Substring(0, 1) == "W"))
                    {
                        int weekTst = Convert.ToInt16((((TextBox)sender).Text.Substring(1, 2)));
                        if ((weekTst < 01) || (weekTst > 52))
                        {
                            ((TextBox)sender).Focus();
                            ((TextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("If Jail Term is in weeks, it must be between 01 and 52");

                        }
                    }

                        if ((((TextBox)sender).Text.Substring(0, 1) == "M"))
                    {
                        int mthTst = Convert.ToInt16((((TextBox)sender).Text.Substring(1, 2)));
                        if ((mthTst < 01) || (mthTst > 12))
                        {
                            ((TextBox)sender).Focus();
                            ((TextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("If Jail Term is in months, it must be between 01 and 12");

                        }
                         
                    }
                        
                        if ((((TextBox)sender).Text.Substring(0, 1) == "Y"))
                    {
                        int yrTst = Convert.ToInt16((((TextBox)sender).Text.Substring(1, 2)));
                        if ((yrTst < 01) || (yrTst > 99))
                        {
                            ((TextBox)sender).Focus();
                            ((TextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("If Jail Term is in years, it must be between 01 and 99");

                        }
                         
                    }
                    
                }

                //Turn off Term Date of Court Suspension

                termCourtSuspensionDateMskTxtBx.Enabled = false;
                 
            } 

            
            
            //if (((TextBox)sender).Visible == true && (((TextBox)sender).Text.Length >= 4))

            if (((TextBox)sender).Visible == true && (((TextBox)sender).Name.Substring(0,7) == "sectvio") && (((TextBox)sender).Text.Length != 00))

            {
                int ssc = (((TextBox)sender).Text.Substring(0,1).IndexOfAny("ABCDEFGHJKLMNOPRSTVY-".ToCharArray()));

                if (ssc == -1)
                {
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Statute codes must start with A,B,C,D,E,F,G,H,J,K,L,M,N,O,P,R,S,T,V,Y,-");
                }
                


            }

            //Per DMV JAG area manual that this was coded from is invalid, and sections can be under 4 bytes

            //if (((TextBox)sender).Text.Length < 4 && (((TextBox)sender).Name.Substring(0, 7) == "sectvio"))
            //{
            //    ((TextBox)sender).Focus();
            //    ((TextBox)sender).BackColor = Color.Red;
            //    errorLstBx.Visible = true;
            //    errorLstBx.Items.Add("Section Violated must be at least 4 bytes long ");
            //}

            //Per the abstract manual two sections must have decimal points

            if (((TextBox)sender).Text == "AB28071") 
            {
                 ((TextBox)sender).Text = "AB2807.1"    ;
                 
            }

            //Per the abstract manual two sections must have decimal points
            if (((TextBox)sender).Text == "A40004")
            {
                ((TextBox)sender).Text = "A4000.4";

            }
            //Add textboxes to a list of texboxes so when its owner responsablitiy 
            //the correct cite is entered

            sectViolList.Add((((TextBox)sender)));

            


        }

        private void checkSectviolated(Object sender)
        {
            
            errorLstBx.Items.Clear();

            if (sectViolList.Count == 0 & ownerResponsibilityChkBx.Checked == true)
            {
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Owner Responsiblity Violations must report Section A40001,1A,1B");
            }
            
                

            foreach (TextBox sv in sectViolList)
            {
                if (sv.Text.Contains("A40001"))
                {
                   A40001 = true; 
                }
                if (sv.Text.Contains("A40001A"))
                {
                    A40001A = true;
                }
                if (sv.Text.Contains("A40001B"))
                {
                    A40001B = true;
                }
               
                 

                if (A40001 == false & A40001A == false & A40001B == false)
                {
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Owner Responsiblity Violations must report Section 40001,1A,1B");
                }
            }

        }


        private void validateBac(Object sender)
        {


            Bachaschars = false;

            if (((TextBox)sender).Name == "bacTxtBx" & ((TextBox)sender).Text.Length== 3)
            {
                int bactst1 = ((TextBox)sender).Text.Substring(0,1).IndexOfAny("AB".ToCharArray());
                if (bactst1 == -1)
                {
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Blood Alcohol codes must start with A or B");
                }
                if (bactst1 != -1)
                {
                    ((TextBox)sender).BackColor = Color.White;
                    errorLstBx.Items.Clear();
                }


                for (int i = 1; i < ((TextBox)sender).Text.Length; i++)
                {
                    string test = ((TextBox)sender).Text.ToString().Substring(i, 1);
                    char testc = Convert.ToChar(test);

                    if (char.IsDigit(testc) == false)
                    {
                        Bachaschars = true;
                    }
                }

                if (Bachaschars == true)
                {
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Blood Alcohol codes be numeric in bytes two thru three");
                }
                if (Bachaschars == false && bactst1 != -1)
                {
                    ((TextBox)sender).BackColor = Color.White;
                    errorLstBx.Items.Clear();
                }
            }

            if (((TextBox)sender).Name == "bacTxtBx" & ((TextBox)sender).Text.Length < 3 & ((TextBox)sender).Text.Length > 0)
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Blood Alcohol codes be three bytes long");
            }

        }


        private void validateDriversLicense(Object sender)
        {

            if (((TextBox)sender).Text.Trim().Length == 0)
            {
                ((TextBox)sender).Text = "A0000000";
            }

            if (((TextBox)sender).Text.Trim().Length  < 8)
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Drivers License must be 8 bytes long");
            }
            else
            {
               ((TextBox)sender).BackColor = Color.White;
                errorLstBx.Items.Clear();
                errorLstBx.Visible = false;
            }

            if (((TextBox)sender).Text.Trim().Length == 8)
            {

                int dlPrefix = ((TextBox)sender).Text.Substring(0, 1).IndexOfAny("ILOQT".ToCharArray());
                 
                if (dlPrefix == 0)
                {
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Drivers Licenses cannot start with I L O Q or T");

                }

                int dlPrefix1 = ((TextBox)sender).Text.Substring(0, 1).IndexOfAny("0123456789".ToCharArray());

                if (dlPrefix1 == 0)
                {
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Drivers Licenses cannot start with a number");

                }

            }




        }

        private void validateBirthDate(object sender)
        {

            System.DateTime currentDate = System.DateTime.Now;

         

            //Length of 4 means nothing is entered in the box

            if ((((MaskedTextBox)sender).Text.Trim().Length > 1) & (((MaskedTextBox)sender).Text.Trim().Length < 8))
            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Date must be fully entered");
            }
            else
            {
                ((MaskedTextBox)sender).BackColor = Color.White;
                errorLstBx.Items.Clear();
                errorLstBx.Visible = false;

            }

            //if 8 bytes means date fully entered promt and literals exluded in property

            if (((MaskedTextBox)sender).Text.Length == 8)
            {
                try
                {

                     
                    

                        string mm = (((MaskedTextBox)sender).Text.Substring(0, 2));
                        string dd = (((MaskedTextBox)sender).Text.Substring(2, 2));
                        string year = (((MaskedTextBox)sender).Text.Substring(4, 4));

                        int monthc = Convert.ToInt32(mm);
                        int dayc = Convert.ToInt32(dd);
                        int yearc = Convert.ToInt32(year);

                        bool leapyear = System.DateTime.IsLeapYear(yearc);

                        //Converts the Date from the masked text box to a date object

                        DateTime dt = Convert.ToDateTime(mm + "/" + dd + "/" + year);
                         
                            
                        //compares the current date to the converted date from the amended text box

                        int greaterThanCurrentDate = currentDate.CompareTo(dt);


                        errorLstBx.Items.Clear();



                        if ((monthc < 01) || (monthc > 12))
                        {
                            ((MaskedTextBox)sender).Focus();
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("Month must be 01- 12");


                        }



                        //calculates the days in the month for the year and month from masked text box

                        int daysinmonth = System.DateTime.DaysInMonth(yearc, monthc);



                        if ((dayc < 01) || (dayc > daysinmonth))
                        {
                            ((MaskedTextBox)sender).Focus();
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("Day must be between 01 and" + daysinmonth);
                        }
                        else
                        {
                            ((MaskedTextBox)sender).BackColor = Color.White;
                        }



                        if (greaterThanCurrentDate == -1)
                        {
                            ((MaskedTextBox)sender).Focus();
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("Date cannot be a date in the future");

                        }

                         //commented this out, if you are in the birthdate box, then you have not reached the
                         // viodate or convict date boxes
                        ////Leaving the conviction date box one assumes the violation date has already been filled

                        //if (((MaskedTextBox)sender).Name == "convictionDateMskTxtBx")
                        //{



                        //    DateTime ct = Convert.ToDateTime(convictionDateMskTxtBx.Text.Substring(0, 2) + "/" + convictionDateMskTxtBx.Text.Substring(2, 2) + "/" + convictionDateMskTxtBx.Text.Substring(4, 4));
                             
                        //    DateTime vt = Convert.ToDateTime(violationDateMskTxtBx.Text.ToString().Substring(0,2) + "/" + violationDateMskTxtBx.Text.Substring(2,2) + "/" + violationDateMskTxtBx.Text.Substring(4,4));

                        //    int compareConvitDateToVioDate = ct.CompareTo(vt);


                        //}



                    
                }
                catch (FormatException ex)
                {
                    ((MaskedTextBox)sender).Focus();
                    ((MaskedTextBox)sender).BackColor = Color.Red;
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("Incorrect date Entered - Correct" + ex);
                }
            }


        }
        public void checkTab(Object sender)
        {
            errorLstBx.Items.Clear();
            errorLstBx.Visible = false;
            courtWriteBtn.Enabled = true;
            ((TabControl)sender).BackColor = Color.Black;

            if (dispListOfListBoxes.Count == 0)

            //if (((TabControl)sender).SelectedTab.Text.Length == 0)
            {
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("At least one Disposition Code must be selected");
                ((TabControl)sender).Focus();
                ((TabControl)sender).BackColor = Color.Red;
                courtWriteBtn.Enabled = false;

            }
        }


        public void collectDispCodes(Object sender)

             
        {
            if (((ListBox)sender).SelectedIndex != -1)
            {
                if (((ListBox)sender).SelectedItem != null & ((ListBox)sender).SelectedItem.ToString() != "AF-Traffic school dismisal")
                {
                    dispCodes.Add(((ListBox)sender).SelectedItem.ToString().Substring(0, 1));
                    dispListOfListBoxes.Add(((ListBox)sender));
                    //(((ListBox)sender)).Enabled = false;
                }

                if (((ListBox)sender).SelectedItem != null & ((ListBox)sender).SelectedItem.ToString() == "AF-Traffic school dismisal")
                {

                    dispCodes.Add("AF");
                    dispListOfListBoxes.Add(((ListBox)sender));
                    //(((ListBox)sender)).Enabled = false;
                }



            }                  

             
        }


        public void disableDispCodeListBox(Object sender)
        {
            (((ListBox)sender)).Enabled = false;
        }



        public void collectProbCodes(Object sender)
        {


            if (((ListBox)sender).SelectedIndex > 0)
            {
                probCodes.Add(((ListBox)sender).SelectedItem.ToString().Substring(0, 1));
                dispListOfListBoxes.Add(((ListBox)sender));
                (((ListBox)sender)).Enabled = false;
            }
        }

        public void nameValidatons(Object sender)
        {
              ((TextBox)sender).BackColor = Color.White;
               errorLstBx.Items.Clear();

           if  (((TextBox)sender).Text.Trim().Length == 0)  
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("First Name/Last Name cannot be blank");
            }
              
        }

        public void DateValidations(Object sender)
        {

            ((MaskedTextBox)sender).BackColor = Color.White;
            errorLstBx.Items.Clear();

            System.DateTime currentDate = System.DateTime.Now;
 
            //MaskedTextBox TextMaskFormat must not be Include literals 

            if (((MaskedTextBox)sender).Text.ToString().Trim().Length != 8)
            {
               ((MaskedTextBox)sender).Focus();
               ((MaskedTextBox)sender).BackColor = Color.Red;
               errorLstBx.Visible = true;
               errorLstBx.Items.Add("Date must be fully entered");   
            }

            //So a full date mm/dd/yyyy was entered but have to put this in a try catch
            //to account for bad dates put in 

            try
            {

                if (((MaskedTextBox)sender).Text.Length == 8)
                {

                    string mm = (((MaskedTextBox)sender).Text.Substring(0, 2));
                    string dd = (((MaskedTextBox)sender).Text.Substring(2, 2));
                    string year = (((MaskedTextBox)sender).Text.Substring(4, 4));

                    int monthc = Convert.ToInt32(mm);
                    int dayc = Convert.ToInt32(dd);
                    int yearc = Convert.ToInt32(year);

                    bool leapyear = System.DateTime.IsLeapYear(yearc);

                    //Converts the Date from the masked text box to a date object since omitting the / in the properties
                    //need to put that back in. 

                    DateTime dt = Convert.ToDateTime(mm + "/" + dd + "/" + year);


                    //compares the current date to the converted date from the amended text box

                    int greaterThanCurrentDate = currentDate.CompareTo(dt);


                    errorLstBx.Items.Clear();



                    if ((monthc < 01) || (monthc > 12))
                    {
                        ((MaskedTextBox)sender).Focus();
                        ((MaskedTextBox)sender).BackColor = Color.Red;
                        errorLstBx.Visible = true;
                        errorLstBx.Items.Add("Month must be 01- 12");


                    }



                    //calculates the days in the month for the year and month from masked text box

                    int daysinmonth = System.DateTime.DaysInMonth(yearc, monthc);



                    if ((dayc < 01) || (dayc > daysinmonth))
                    {
                        ((MaskedTextBox)sender).Focus();
                        ((MaskedTextBox)sender).BackColor = Color.Red;
                        errorLstBx.Visible = true;
                        errorLstBx.Items.Add("Day must be between 01 and" + daysinmonth);
                    }
                    else
                    {
                        ((MaskedTextBox)sender).BackColor = Color.White;
                    }



                    if (greaterThanCurrentDate == -1)
                    {
                        ((MaskedTextBox)sender).Focus();
                        ((MaskedTextBox)sender).BackColor = Color.Red;
                        errorLstBx.Visible = true;
                        errorLstBx.Items.Add("Date cannot be a date in the future");

                    }

                    //Leaving the conviction date box one assumes the violation date has already been filled

                    if (((MaskedTextBox)sender).Name == "convictionDateMskTxtBx")
                    {




                        DateTime ct = Convert.ToDateTime(convictionDateMskTxtBx.Text.Substring(0, 2) + "/" + convictionDateMskTxtBx.Text.Substring(2, 2) + "/" + convictionDateMskTxtBx.Text.Substring(4, 4));
                        DateTime vt = Convert.ToDateTime(violationDateMskTxtBx.Text.ToString().Substring(0, 2) + "/" + violationDateMskTxtBx.Text.ToString().Substring(2, 2) + "/" + violationDateMskTxtBx.Text.ToString().Substring(4, 4));

                        int compareConvitDateToVioDate = ct.CompareTo(vt);

                        if (compareConvitDateToVioDate == -1)
                        {
                            ((MaskedTextBox)sender).Focus();
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("Conviction date cannot be before violation date");
                        }

                    }

                    //Leaving the Term Date of Court Suspension date box one assumes the violation date has already been filled

                    if (((MaskedTextBox)sender).Name == "termCourtSuspensionDateMskTxtBx")
                    {


                        ((MaskedTextBox)sender).BackColor = Color.White;
                         errorLstBx.Items.Clear();



                        if (convictionDateMskTxtBx.Text.Length == 8)
                        {

                            DateTime tc = Convert.ToDateTime(termCourtSuspensionDateMskTxtBx.Text.Substring(0, 2) + "/" + termCourtSuspensionDateMskTxtBx.Text.Substring(2, 2) + "/" + termCourtSuspensionDateMskTxtBx.Text.Substring(4, 4));

                            DateTime ct = Convert.ToDateTime(convictionDateMskTxtBx.Text.Substring(0, 2) + "/" + convictionDateMskTxtBx.Text.Substring(2, 2) + "/" + convictionDateMskTxtBx.Text.Substring(4, 4));
                            int compareTermDateToConvictDate = tc.CompareTo(ct);



                            if (compareTermDateToConvictDate == -1)
                            {
                                ((MaskedTextBox)sender).Focus();
                                ((MaskedTextBox)sender).BackColor = Color.Red;
                                errorLstBx.Visible = true;
                                errorLstBx.Items.Add("Termination Date cannot be before conviction date");
                            }

                        }
                        if (convictionDateMskTxtBx.Text.Length < 8)
                        {
                            //((MaskedTextBox)sender).Focus();
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("Conviction date must be entered");
                             
                        }

                        if (termCourtSuspensionTxtBx.Text.Trim().Length > 0)
                        {
                            ((MaskedTextBox)sender).BackColor = Color.Red;
                            errorLstBx.Visible = true;
                            errorLstBx.Items.Add("You cannot have a Term AND a Term Date");
                        }

                    }

                }
            }
            catch (FormatException ex)
            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Incorrect date Entered - Correct");
            }

            catch (Exception ea)
            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
                errorLstBx.Visible = true;
                errorLstBx.Items.Add("Incorrect date Entered - Correct" + ea);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {

                string transCode = transactionLstBx.SelectedItem.ToString().Substring(0, 3);
                string driversLicense = driversLicenseTxtBx.Text.ToString().PadRight(8,' ');
                string soundexCode = "1";

                int lastNameLength = driversLastNameTxtBx.Text.Trim().Length;
                int middleNameLength = driversMiddleNameTxtBx.Text.Trim().Length;
                int firstNameLength = driversFirstNameTxtBx.Text.Trim().Length;
                 



                int totalBytesAvailable = 36;

                //Name formatting each box is 20 bytes long
                if (lastNameLength > 0 && lastNameLength <= totalBytesAvailable)
                {
                    lastName = driversLastNameTxtBx.Text.Trim().Substring(0, lastNameLength);
                    totalBytesAvailable = totalBytesAvailable - lastNameLength;
                }
                if (firstNameLength > 0 && firstNameLength <= totalBytesAvailable)
                {
                    firstName = driversFirstNameTxtBx.Text.Trim().Substring(0, firstNameLength);
                    totalBytesAvailable = totalBytesAvailable - firstNameLength;
                }
                if (middleNameLength > 0 && middleNameLength <= totalBytesAvailable)
                {

                    middleName = driversMiddleNameTxtBx.Text.Trim().Substring(0, middleNameLength);
                    totalBytesAvailable = totalBytesAvailable - middleNameLength;
                }
                //Prod problem 5.6.15 retaining middlename.
                if (middleNameLength == 0)
                {
                    middleName = "";
                }

                if (suffixLstBx.SelectedItems.Count > 0 && suffixLstBx.SelectedItem.ToString().Trim().Length <= totalBytesAvailable)
                {
                    suffix = suffixLstBx.SelectedItem.ToString().Trim().ToUpper();
                }
                 
                if (suffixLstBx.SelectedItems.Count == 0)
                {
                    suffix = "";
                } 


                string totalName = lastName + " " + firstName + " " + middleName + " " + suffix;

                if (totalName.Length > 36)
                {
                    totalName = totalName.Substring(0, 36);
                }
                totalName = totalName.ToString().PadRight(36, ' ');
                 
                //Address formatting Street

                string address = addressTxtBx.Text.Trim().PadRight(36, ' ');

                //City

                string city = cityTxtBx.Text.Trim();


                if (outOfStateLstBx.SelectedItems.Count > 0)
                {
                    city = city + " " + outOfStateLstBx.SelectedItem.ToString().Trim().Substring(0, 2);

                }

                //Birth date should be validated, as we validate the date before the user leaves the box if
                //length is equal to 10 

                string birthDate;

                if (birthDateMskTxtBx.Text.Trim().Length == 8)
                {
                    birthDate = birthDateMskTxtBx.Text.Trim();
                    birthDate = birthDate.Substring(0, 4) + birthDate.Substring(6, 2);
                }
                else
                {
                    birthDate = "      ";
                }

                //Docket Number formatting

                string docNumber = docketNumberTxtBx.Text.Trim();
                docNumber = docNumber.PadRight(7);

                //Viodate formatting has to be there no need to check length
                string vioDate = violationDateMskTxtBx.Text.Trim();
                if (vioDate.Length != 8)
                {
                    throw new System.Exception("Violation Date Empty");
                     
                }
                vioDate = vioDate.Substring(0, 4) + vioDate.Substring(6, 2);

                //Section Violated formatting

                fullSectViolatedLine = sectvioTxtBx1.Text.Trim().PadRight(7,' ') + sectvioTxtBx2.Text.Trim().PadRight(7,' ') +
                                       sectvioTxtBx3.Text.Trim().PadRight(7, ' ') + sectvioTxtBx4.Text.Trim().PadRight(7, ' ') + sectvioTxtBx5.Text.Trim().PadRight(7, ' ') +
                                       sectvioTxtBx6.Text.Trim().PadRight(7, ' ') + sectvioTxtBx7.Text.Trim().PadRight(7, ' ');


                fullSectViolatedLine = fullSectViolatedLine.PadRight(56);

                //amendedCode Formatting

                string amendedCode = "";
                amendedCode = amendedCode.PadRight(1, ' ');

                if (amendedCodeLstBx.SelectedItems.Count > 0)
                {
                    amendedCode = amendedCodeLstBx.SelectedItem.ToString().Substring(0,1);
                }
                //Amended Date properties excludes literals and prompts 

                string amendedDate = amendedDateMskTxtBx.Text.Trim().Replace("/", "").PadRight(6);
                if (amendedDate.Length == 8)
                {
                    amendedDate = amendedDate.Substring(0, 4) + amendedDate.Substring(6, 2);
                }
                //Vehicle License Number formatting
                string vehicleLicenseNumber = vehicleLicenseNumberTxtBx.Text.Trim().PadRight(7);

                //Convict date formatting
                string convictDate = convictionDateMskTxtBx.Text.Trim().Replace("/", "");
                if (transCode != "D12")
                {
                    convictDate = convictDate.Substring(0, 4) + convictDate.Substring(6, 2);
                }

                string dispostion = "";
                dispostion.PadRight(9, ' ');

                if (dispCodeLstBx1.SelectedItems.Count == 0 && transCode == "D10")
                {
                    errorLstBx.Visible = true;
                    errorLstBx.Items.Add("At least one disposition must be selected");
                    dispCodeLstBx1.Focus();
                    dispCodeLstBx1.BackColor = Color.Red;


                }

                if (dispCodeLstBx1.SelectedItems.Count > 0)
                {
                    foreach (String dc in dispCodes)
                    {

                        dispostion = dispostion + dc;
                    }

                    dispCodes.Clear();
                }

                string bac = bacTxtBx.Text.Trim().PadRight(3, ' ');
                string jail = jailTxtBx.Text.Trim().PadRight(3, ' ');
                string termOfCourtSuspension = termCourtSuspensionTxtBx.Text.Trim().PadRight(3, ' ');


                string termDateOfCourtSuspension = "";
                 

                if (termCourtSuspensionDateMskTxtBx.Text.Trim().Length > 0)
                {
                    termDateOfCourtSuspension = termCourtSuspensionDateMskTxtBx.Text.Trim().Replace("/", "");
                    termDateOfCourtSuspension = termDateOfCourtSuspension.Substring(0, 4) + termDateOfCourtSuspension.Substring(6, 2);
                }

                if (termCourtSuspensionDateMskTxtBx.Text.Trim().Length == 0)
                {
                    termDateOfCourtSuspension.PadRight(6, ' ');
                }

                //Format Aka name

                string akaName = akaNameTxtBx.Text.Trim().PadRight(36);

                string probationCondition = "";
                probationCondition.PadRight(10); 

                if (probcode1.SelectedIndex >  0)
                {

                    foreach (String pc in probCodes)
                    {

                        probationCondition = probationCondition + pc;
                    }
                     
                }
                if (transCode == "D10")
                {
                      courtWriteLine = transCode + driversLicense + soundexCode + totalName +
                                            address + city.PadRight(13, ' ') + birthDate + docNumber + vioDate + fullSectViolatedLine +
                                            amendedCode + amendedDate + vehicleLicenseNumber + convictDate.PadRight(6, ' ') + dispostion.PadRight(9, ' ') +
                                            bac + jail + requestorCode + ' ' + termOfCourtSuspension.PadRight(3, ' ') + termDateOfCourtSuspension.PadRight(6, ' ') +
                                            akaName.PadRight(36, ' ') + probationCondition.PadRight(10, ' ') + "                              ";
                       
                      
 
                }
                if (transCode == "D12")
                {
                    courtWriteLine = transCode + driversLicense + soundexCode + totalName +
                                     address + city.PadRight(13, ' ') + birthDate + docNumber + vioDate + fullSectViolatedLine +
                                     "       " + vehicleLicenseNumber + "                     " + requestorCode + "          " + akaName.PadRight(36, ' ');
                                                             
                                             

                      courtWriteLine.PadRight(258, ' ');
                      
                }

                if (transCode == "D13")
                {
                    courtWriteLine = transCode + driversLicense + soundexCode + totalName +
                                     address + city.PadRight(13, ' ') + birthDate + docNumber + vioDate + fullSectViolatedLine +
                                     "       " + vehicleLicenseNumber +  convictDate + "               " + requestorCode + "          " + akaName.PadRight(36, ' ');



                    courtWriteLine.PadRight(258, ' ');
                    
                }

                if (transCode == "D14")
                {
                    courtWriteLine = transCode + driversLicense + soundexCode + totalName +
                                     address + city.PadRight(13, ' ') + birthDate + docNumber + vioDate + fullSectViolatedLine +
                                     "       " + vehicleLicenseNumber + convictDate + "               " + requestorCode + "          " + akaName.PadRight(36, ' ');



                    courtWriteLine.PadRight(258, ' ');
                     
                }
                DateTime currDate = DateTime.Now;

                string dir = filepath;
                courtPath = dir + @"\" + "COURT_" + String.Format("{0:MMddyy}", currDate) + ".txt";

                //Add to list

                courtAbstractLine.Add(courtWriteLine);

                //Clear all fields

                ClearForm.clearForm(this);

                numericUpDown1.Value = 1;

                transactionLstBx.Focus();

                //Since the listboxes in the tabcontrol don't respond to the clear form, 
                //seeing if this will work

                foreach (ListBox dcl in dispListOfListBoxes)
                {

                    dcl.Enabled = true;
                }

                //this works 
                //this.dispCodeLstBx1.Enabled = true;


                //Enable exit create file  file button 

                exitCreateFileBtn.Enabled = true;

                //Disable write button

                courtWriteBtn.Enabled = false;
             



            


            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                courtWriteBtn.Enabled = false;
            }
        }
             
     
      

        private void driversLicenseNameTxtBx_Leave(object sender, EventArgs e)
        {
            nameValidatons(sender);
           
        }

        private void transactionLstBx_SelectedValueChanged(object sender, EventArgs e)
        {


            if (transactionLstBx.SelectedIndex == -1)
            {
                transactionLstBx.SelectedIndex = 0;
            }

                if (transactionLstBx.SelectedItem.ToString() == "D10")
                {
                    panel1.Visible = true;
                    convictionDateLbl.Visible =true;
                    convictionDateMskTxtBx.Visible = true;
                    akaNameTxtBx.ReadOnly = false;
                    firstDispCodeTab.Visible = true;

                }


                if (transactionLstBx.SelectedItem.ToString() == "D12")
                {
                    panel1.Visible = false;
                    convictionDateLbl.Visible = false;
                    convictionDateMskTxtBx.Visible = false;
                    akaNameTxtBx.ReadOnly = true;
                    firstDispCodeTab.Visible = false;


                }


                if (transactionLstBx.SelectedItem.ToString() == "D13")
                {
                    panel1.Visible = false;
                    convictionDateLbl.Visible = true;
                    convictionDateMskTxtBx.Visible = true;
                    akaNameTxtBx.ReadOnly = true;
                    firstDispCodeTab.Visible = false;

                }

                if (transactionLstBx.SelectedItem.ToString() == "D14")
                {
                    panel1.Visible = false;
                    convictionDateLbl.Visible = true;
                    convictionDateMskTxtBx.Visible = true;
                    firstDispCodeTab.Visible = false;

                }
            
        }

        private void transactionLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void outOfStateLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outOfStateLstBx.SelectedIndex > 0)
            {
                
                driversLicenseTxtBx.Text = "A0000000";
            }
        }

        private void driversFirstNameTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {

            string squote = "'";
            char csquote = Convert.ToChar(squote);
           

            //Try to cover all special chars, unfortunatly enum cannot
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '-' || e.KeyChar == ' ' || e.KeyChar == csquote
               || e.KeyChar == ',' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '1' || e.KeyChar == '2'
               || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8'
               || e.KeyChar == '9' || e.KeyChar == '0')
                e.Handled = true;
        }

        private void driversMiddleNameTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            string squote = "'";
            char csquote = Convert.ToChar(squote);
           

            //Try to cover all special chars, unfortunatly enum cannot
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '-' || e.KeyChar == ' ' || e.KeyChar == csquote
                || e.KeyChar == ',' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '1' || e.KeyChar == '2'
                || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8'
                || e.KeyChar == '9' || e.KeyChar == '0')
                e.Handled = true;
        

        }

        private void driversLastNameTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void driversLastNameTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            string squote = "'";
            char csquote = Convert.ToChar(squote);
            

            //Try to cover all special chars, unfortunatly enum cannot
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '-' || e.KeyChar == ' ' || e.KeyChar == csquote
                || e.KeyChar == ',' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '1' || e.KeyChar == '2'
                || e.KeyChar == '3' || e.KeyChar == '4' || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8'
                || e.KeyChar == '9' || e.KeyChar == '0')
                e.Handled = true;
        
        }

        private void addressTxtBx_Leave(object sender, EventArgs e)
        {
            // This code block below can only be used IF you isolate the street address
            // ie 1234 sesame street, so you would have to assume that if the address is
            // has three dimensions to it numbers, street name and descriptor (Way place) 
            // then force the abrreviation on the descriptor only 

            //string[] streetNameArray =   addressTxtBx.Text.Split(' ');

            //if (streetNameArray.Length == 4)
            //{

            //    if ((streetNameArray[1].ToString() == "NAVAL") & (streetNameArray[2].ToString() == "AIR") & (streetNameArray[3].ToString() == "STATION"))
            //    {
            //        streetNameArray[1] = "NAS";

            //        addressTxtBx.Text = streetNameArray[0].ToString() + " " + streetNameArray[1].ToString();

            //    }
            //}

                //streetNameArray[3].Text = streetNameArray[3].ToString().tex             addressTxtBx.Text.Replace("NAVAL AIR BASE", "NAB");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NAVAL AIR STATION", "NAS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("PACIFIC COAST HIGHWAY", "PAC CST HWY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ADMINISTRATION", "ADMIN");  
                //addressTxtBx.Text=   addressTxtBx.Text.Replace("AIR FORCE BASE", "AFB");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("AIR FORCE STATION", "AFS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ANNEX", "ANX");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ARCADE", "ARC");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("AVENUE", "AVE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BARRACKS", "BRKS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BASE", "BS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BATTALION", "BN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BATTERY", "BN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BEACH", "BCH");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BOULEVARD", "BLVD");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BOX", "BX");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BRANCH(ES)", "BR(S)");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BRANCH", "BR"); 
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BRIDGE", "BRG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BROOK", "BRK");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BUILDING(S)", "BLDG(S)");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BUILDING", "BLDG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("BUREAU", "BUR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CAMP", "CP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CANYON", "CYN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CAPE", "CP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CARE OF", "C/O");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CAUSEWAY", "CSWY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CENTER", "CTR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CIRCLE", "CIR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CITY", "CY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COAST", "CST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COLLEGE", "CLG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COMPANY", "CO");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COMPANIES", "CO(S)");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CORNER", "COR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CORNERS", "CORS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CORPORATION", "CORP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COURSE", "CRSE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COURT", "CT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COURTS", "CTS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("COVE", "CV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CREEK", "CRK");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("CROSSING", "XING");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("DAM", "DM");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("DEPARTMENT", "DEPT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("DIVIDE", "DV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("DIVISION", "DIV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("DRIVE", "DR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("EAST", "E");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ESTATE", "EST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("EXCHANGE", "EXCH");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("EXPRESSWAY", "EXPY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("EXTENSION", "EXT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FIELD", "FLD");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FIELDS", "FLDS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FIRST", "1ST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FLIGHT", "FLT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FLOOR", "FLR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FOREST", "FRST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FORT", "FT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FOUNTAIN", "FTN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("FREEWAY", "FWY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GARDENS", "GDNS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GATEWAY", "GTWY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GENERAL DELIVERY", "GEN DEL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GROUNDS", "GRDS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GROUP", "GRP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("GROVE", "GRV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HARBOR", "HBR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HEIGHTS", "HTS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HIGHWAY", "HWY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HILL", "HL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HILLS", "HLS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HOME", "HM");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HOSPITAL", "HOSP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("HOUSE", "HSE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("INCORPORATED", "INC");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("INLET", "INLT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ISLAND", "IS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ISLANDS", "ISS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("JUNCTION", "JCT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("LAKE", "LK");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("LAKES", "LKS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("LIMITED", "LTD");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("LODGE", "LDG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MAINTENANCE", "MAINT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MANOR", "MNR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MARINE", "MAR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MARINE CORPS BASE", "MCB");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MILL", "ML");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MILLS", "MLS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MISSLE", "MSLE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MISSION", "MSN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MOBILE HOME PARK", "MHP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MOTOR", "MTR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("MOTORS", "MTRS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NATIONAL", "NATL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NAVY", "NAV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NAVAL", "NAV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NORTHWEST", "NW");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NORTHEAST", "NE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("NORTH", "N");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("PARKWAY", "PKY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("PLACE", "PL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("PLAZA", "PLZ");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("POINT OF EMBARKATION", "POE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("POINT", "PT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("PORT", "PRT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("POST OFFICE BOX", "PO BOX");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("POST OFFICE DRAWER", "PO DRW");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RANCH", "RNCH");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RANCHO", "RNCH");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RESERVE", "POE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("REST", "RST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RIDGE", "RDG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RIVER", "RIV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RIVERS", "RIVS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ROAD", "RD");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ROADS", "RDS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ROOM", "RM");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("ROUTE", "RT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RURAL FREE DELIVERY", "RFD");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("RUAL ROUTE", "RRT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SAINT", "ST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SANITARIUM", "SNTRM");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SAVING", "SAV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SAVINGS", "SAV");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SERVICE", "SER");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SERVICES", "SERS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SECTION", "SECT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SOUTHWEST", "SW");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SOUTHERN CALIFORNIA", "S CALIF");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SOUTHEAST", "SE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SOUTH", "S");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SPACE", "SP");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SPRING", "SPG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SPRINGS", "SPGS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SQUARE", "SQ");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("STATION", "STA");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("STREET", "ST");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SUITE", "STE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("SUMMIT", "SMT");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TERMINAL", "TERM");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRACE", "TRCE");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRACK", "TRAK");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRAIL", "TRL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRAILS", "TRLS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRAILER", "TRLR");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRAILERS", "TRLRS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TRAINING", "TRNG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("TUNNEL", "TUNL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("UNITED STATES AIR FORCE", "USAF");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("UNITED STATES NAVY", "USN");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("UNITED STATES MARINE CORPS", "USMC");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("UNITED STATES", "US");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VALLEY", "VLY");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VETERANS", "VETS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VETERAN", "VET");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VIEW", "VW");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VILLAGE", "VLG");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VILLA", "VL");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("VISTA", "VIS");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("WALK", "WK");
                //addressTxtBx.Text = addressTxtBx.Text.Replace("WEST", "W");


            //}
         
        }

        private void addressTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '!' || e.KeyChar == '@'   || e.KeyChar == '-' || e.KeyChar == '&' || e.KeyChar == '*'
                || e.KeyChar == '(' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == ')'
                || e.KeyChar == '_' || e.KeyChar == '{' || e.KeyChar == '}' || e.KeyChar == '[' || e.KeyChar == ']')
                e.Handled = true;
        }

        private void cityTxtBx_Leave(object sender, EventArgs e)
        {


             

            if (cityTxtBx.Text.Length > 13)
            {

                string[] cityNameArray = cityTxtBx.Text.Split(' ');


                if (cityNameArray.Length == 4)
                {

                    //For X AIR FORCE BASE
                    if ((cityNameArray[1].ToString() == "AIR") & (cityNameArray[2].ToString() == "FORCE"))
                    {

                        cityNameArray[1] = "A";
                        cityNameArray[2] = "F";
                        cityNameArray[3] = "B";

                        cityTxtBx.Text = cityNameArray[0].ToString() +  " " + cityNameArray[1].ToString()
                             + cityNameArray[2].ToString() +  cityNameArray[3].ToString();

                    }

                    //For EL TORO MARINE CORPS 

                    if ((cityNameArray[0].ToString() == "EL") & (cityNameArray[1].ToString() == "TORO"))
                    {

                        
                        cityNameArray[2] = "MAS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[2].ToString(); 

                    }

                    //For KINGS CANYON NATIONAL PARK

                    if ((cityNameArray[0].ToString() == "KINGS") & (cityNameArray[1].ToString() == "CANYON"))
                    {


                        cityNameArray[0] = "KS";
                        cityNameArray[1] = "CYN";
                        cityNameArray[2] = "NAT";
                        cityNameArray[3] = "PK";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[2].ToString() + " " + cityNameArray[3].ToString();

                    }

                    //For SAN JUAN HOT SPRINGS

                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "JUAN") & (cityNameArray[2].ToString() == "HOT"))
                    {


                        cityNameArray[0] = "SN";
                         
                        cityNameArray[2] = "H";
                        cityNameArray[3] = "SPG";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[2].ToString() + " " + cityNameArray[3].ToString();

                    }

                    //For Lake of the Woods

                    if ((cityNameArray[0].ToString() == "LAKE") & (cityNameArray[3].ToString() == "WOODS"))
                    {


                        cityNameArray[0] = "LK";
                        


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             +  " " + cityNameArray[3].ToString();

                    }

                    //For San Luis Rey Downs

                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "LUIS") & (cityNameArray[2].ToString() == "REY")
                        & (cityNameArray[3].ToString() == "DOWNS"))
                    {


                        cityNameArray[0] = "SN";
                        cityNameArray[1] = "LU";
                        cityNameArray[2] = "RE";
                        cityNameArray[3] = "DWNS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[2].ToString() + " " + cityNameArray[3].ToString();

                    }

                    //For South San Jose Hills

                    if ((cityNameArray[0].ToString() == "SOUTH") & (cityNameArray[1].ToString() == "SAN") & (cityNameArray[2].ToString() == "JOSE")
                        & (cityNameArray[3].ToString() == "HILLS"))
                    {


                        cityNameArray[0] = "S";
                        cityNameArray[1] = "SN";
                        
                        cityNameArray[3] = "HLS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[2].ToString() + " " + cityNameArray[3].ToString();

                    }

                    //For Princeton By The Sea

                    if ((cityNameArray[0].ToString() == "PRINCETON") & (cityNameArray[3].ToString() == "SEA"))
                    {


                        cityNameArray[0] = "PRNCTN";



                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString()
                             + " " + cityNameArray[3].ToString();

                    }
                }

                //Cities with three names ************************

                if (cityNameArray.Length == 3)
                {

                    //For AGUA CALIENTE SPRINGS
                    if ((cityNameArray[0].ToString() == "AGUA") & (cityNameArray[1].ToString() == "CALIENTE")) 
                    {

                        cityNameArray[1] = "CLNT";
                        cityNameArray[2] = "SPG";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Italian Swiss Colony
                    if ((cityNameArray[0].ToString() == "ITALIAN") & (cityNameArray[1].ToString() == "SWISS"))
                    {

                        cityNameArray[0] = "ITAL";
                        cityNameArray[1] = "SWS";
                        cityNameArray[2] = "COLN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Little Morongo Heights
                    if ((cityNameArray[0].ToString() == "LITTLE") & (cityNameArray[1].ToString() == "MORONGO"))
                    {

                        cityNameArray[0] = "LTL";
                        cityNameArray[1] = "MRNGO";
                        cityNameArray[2] = "HTS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For San Joaquin City
                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "JOAQUIN") & (cityNameArray[2].ToString() == "CITY"))
                    {

                        cityNameArray[0] = "SN";
                         
                        cityNameArray[2] = "CY";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SAN JUAN BAUTISTA
                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "JUAN") & (cityNameArray[2].ToString() == "BAUTISTA"))
                    {

                        cityNameArray[0] = "SN";
                        cityNameArray[1] = "JUN";
                        cityNameArray[2] = "BATSTA";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SAN LUIS OBISPO
                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "LUIS") & (cityNameArray[2].ToString() == "OBISPO"))
                    {

                        cityNameArray[0] = "SN";
                        
                        cityNameArray[2] = "OBSPO";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Mark West Springs
                    if ((cityNameArray[0].ToString() == "MARK") & (cityNameArray[2].ToString() == "SPRINGS"))
                    {

                        cityNameArray[1] = "W";
                        cityNameArray[2] = "SPGS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Marina Del Ray
                    if ((cityNameArray[0].ToString() == "MARINA") & (cityNameArray[2].ToString() == "REY"))
                    {

                        cityNameArray[0] = "MRNA";
                         

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Murrieta Hot Springs  
                    if ((cityNameArray[0].ToString() == "MURRIETA") & (cityNameArray[2].ToString() == "SPRINGS"))
                    {

                        cityNameArray[0] = "MURIETA";



                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For FETTER HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "FETTER") & (cityNameArray[1].ToString() == "HOT"))
                    {

                        cityNameArray[1] = "HT";
                        cityNameArray[2] = "SPG";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For KEOUGHS HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "KEOUGHS") & (cityNameArray[1].ToString() == "HOT"))
                    {

                        cityNameArray[0] = "KEOUGH";
                        cityNameArray[1] = "H";
                        cityNameArray[2] = "SPGS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For FALL RIVER MILLS
                    if ((cityNameArray[0].ToString() == "FALL") & (cityNameArray[1].ToString() == "RIVER"))
                    {

                        cityNameArray[0] = "FL";
                        cityNameArray[1] = "RIV";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }


                    //For CALIFORNIA HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "CALIFORNIA") & (cityNameArray[1].ToString() == "HOT"))
                    {

                        cityNameArray[0] = "CALIF";
                        cityNameArray[2] = "SPG";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For BERKELEY RECREATION CAMP
                    if ((cityNameArray[0].ToString() == "BERKELEY") & (cityNameArray[1].ToString() == "RECREATION"))
                    {

                        cityNameArray[0] = "BRKLEY";
                        cityNameArray[1] = "REC";
                        cityNameArray[2] = "CP";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For BIG LAGOON PARK
                    if ((cityNameArray[0].ToString() == "BIG") & (cityNameArray[1].ToString() == "LAGOON"))
                    {

                        cityNameArray[0] = "BG";
                        cityNameArray[1] = "LAGON";
                        

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For CALAVERAS BIG TREES 
                    if ((cityNameArray[0].ToString() == "CALAVERAS") & (cityNameArray[1].ToString() == "BIG"))
                    {

                        cityNameArray[0] = "CAL";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For OAKLAND RECREATION CAMP 
                    if ((cityNameArray[0].ToString() == "OAKLAND") & (cityNameArray[1].ToString() == "RECREATION"))
                    {

                        cityNameArray[0] = "OAKLND";
                        cityNameArray[1] = "REC";
                        cityNameArray[2] = "CP";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For AUGUST SCHOOL AREA
                    if ((cityNameArray[0].ToString() == "AUGUST") & (cityNameArray[1].ToString() == "SCHOOL"))
                    {

                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For DESERT VIEW HIGHLANDS
                    if (((cityNameArray[0].ToString() == "DESERT") & (cityNameArray[1].ToString() == "VIEW") & (cityNameArray[2].ToString() == "HIGHLANDS")))
                    {

                        cityNameArray[0] = "DSRT";
                        cityNameArray[1] = "VW";
                        cityNameArray[2] = "HGLDS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For LOS TRANCOS WOOD
                    if (((cityNameArray[0].ToString() == "LOS") & (cityNameArray[1].ToString() == "TRANCOS") & (cityNameArray[2].ToString() == "WOOD")))
                    {

                        cityNameArray[0] = "LS";
                        cityNameArray[1] = "TRNCOS";
                        cityNameArray[2] = "WDS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //AIRPORT SAN FRANCISCO
                    if (((cityNameArray[0].ToString() == "AIRPORT") & (cityNameArray[1].ToString() == "SAN") & (cityNameArray[2].ToString() == "FRANCISCO")))
                    {

                        cityNameArray[0] = "ARPRT";
                        cityNameArray[1] = "SF";
                        


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For DESERT HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "DESERT") & (cityNameArray[1].ToString() == "HOT"))
                    {
                        cityNameArray[1] = "H";
                        cityNameArray[2] = "SPGS";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For EL DORADO HILLS
                    if ((cityNameArray[0].ToString() == "EL") & (cityNameArray[1].ToString() == "DORADO"))
                    {
                        cityNameArray[2] = "HLS";
                        


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For EL ENCANTO HEIGHTS
                    if ((cityNameArray[0].ToString() == "EL") & (cityNameArray[1].ToString() == "ENCANTO"))
                    {
                        cityNameArray[2] = "HT";



                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For MONTEREY BAY ACADEMY
                    if ((cityNameArray[0].ToString() == "MONTEREY") & (cityNameArray[2].ToString() == "ACADEMY"))
                    {
                        cityNameArray[0] = "MONT";
                        cityNameArray[2] = "ACAD";



                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For NEW PINE CREEK
                    if ((cityNameArray[0].ToString() == "NEW") & (cityNameArray[2].ToString() == "CREEK"))
                    {
                        cityNameArray[0] = "NW";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For NICHOLLS WARM SPRINGS
                    if ((cityNameArray[0].ToString() == "NICHOLLS") & (cityNameArray[1].ToString() == "WARM"))
                    {
                        cityNameArray[0] = "NCHLS";
                        cityNameArray[1] = "WRM";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For PALOS VERDES ESTATES
                    if ((cityNameArray[0].ToString() == "PALOS") & (cityNameArray[2].ToString() == "ESTATES"))
                    {
                        cityNameArray[0] = "PLS";
                        cityNameArray[1] = "VRDS";
                        cityNameArray[2] = "EST";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For Point Reyes Station
                    if ((cityNameArray[0].ToString() == "POINT") & (cityNameArray[2].ToString() == "STATION"))
                    {
                        cityNameArray[0] = "PT";
                        
                        cityNameArray[2] = "STA";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For PALOS VERDES PENINSULA
                    if ((cityNameArray[0].ToString() == "PALOS") & (cityNameArray[2].ToString() == "PENINSULA"))
                    {
                        cityNameArray[0] = "PLS";
                        cityNameArray[1] = "VRDS";
                        cityNameArray[2] = "PNSL";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For PRESIDIO SAN FRANCISCO
                    if ((cityNameArray[0].ToString() == "PRESIDIO") & (cityNameArray[2].ToString() == "FRANCISCO"))
                    {
                        
                        cityNameArray[1] = "SF";
                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For PRESIDIO OF MONTEREY
                    if ((cityNameArray[0].ToString() == "PRESIDIO") & (cityNameArray[2].ToString() == "MONTEREY"))
                    {
                         
                        cityNameArray[1] = "MONT";
                         
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For RANCHO SANTA CLARITA
                    if ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[1].ToString() == "SANTA") & (cityNameArray[2].ToString() == "CLARITA"))
                    {

                        cityNameArray[0] = "RNCH";
                        cityNameArray[1] = "SN";
                        cityNameArray[2] = "CLRTA";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For RANCHO PALOS VERDES
                    if ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[2].ToString() == "VERDES"))
                    {
                        cityNameArray[1] = "PALO";
                        cityNameArray[2] = "VRD";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For ROLLING HILLS ESTATE
                    if ((cityNameArray[0].ToString() == "ROLLING") & (cityNameArray[1].ToString() == "HILLS"))
                    {
                        cityNameArray[0] = "RLLNG";
                        cityNameArray[1] = "HL";
                        cityNameArray[2] = "EST";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SALTON SEA BEACH
                    if ((cityNameArray[0].ToString() == "SALTON") & (cityNameArray[1].ToString() == "SEA")
                        & (cityNameArray[2].ToString() == "BEACH"))
                    {
                        cityNameArray[0] = "SALTN";
                        
                        cityNameArray[2] = "BCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SAN JUAN CAPISTRANO
                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "JUAN")
                        & (cityNameArray[2].ToString() == "CAPISTRANO"))
                    {
                        cityNameArray[0] = "SN";
                        cityNameArray[1] = "JUN";
                        cityNameArray[2] = "CPSTRN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SHARP ARMY DEPOT
                    if ((cityNameArray[0].ToString() == "SHARP") & (cityNameArray[1].ToString() == "ARMY")
                        & (cityNameArray[2].ToString() == "DEPOT"))
                    {
                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SOUTH SAN FRANCISCO
                    if ((cityNameArray[0].ToString() == "SOUTH") & (cityNameArray[1].ToString() == "SAN")
                        & (cityNameArray[2].ToString() == "FRANCISCO"))
                    {

                        cityNameArray[0] = "S";
                        
                        cityNameArray[2] = "FRAN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SO SAN FRANCISCO
                    if ((cityNameArray[0].ToString() == "SO") & (cityNameArray[1].ToString() == "SAN")
                        & (cityNameArray[2].ToString() == "FRANCISCO"))
                    {

                        cityNameArray[0] = "S";
                        
                        cityNameArray[2] = "FRAN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SOUTH SAN GABRIEL
                    if ((cityNameArray[0].ToString() == "SOUTH") & (cityNameArray[1].ToString() == "SAN")
                        & (cityNameArray[2].ToString() == "GABRIEL"))
                    {

                        cityNameArray[0] = "S";
                        cityNameArray[1] = "SN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }


                    //For SEQUOIA NATIONAL PARK
                    if ((cityNameArray[0].ToString() == "SEQUOIA") & (cityNameArray[1].ToString() == "NATIONAL")
                        & (cityNameArray[2].ToString() == "PARK"))
                    {
                        cityNameArray[0] = "SQUOIA";
                        cityNameArray[1] = "NAT";
                        cityNameArray[2] = "PK";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SUNSET WHITNEY RANCH
                    if ((cityNameArray[0].ToString() == "SUNSET") & (cityNameArray[1].ToString() == "WHITNEY")
                        & (cityNameArray[2].ToString() == "RANCH"))
                    {
                        cityNameArray[0] = "S";
                        cityNameArray[1] = "WHITNY";
                        cityNameArray[2] = "RNCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For TASSAJARA HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "TASSAJARA") & (cityNameArray[1].ToString() == "HOT")
                        & (cityNameArray[2].ToString() == "SPRINGS"))
                    {
                        cityNameArray[0] = "TSAJARA";
                         

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For TIERRA DEL SOL
                    if ((cityNameArray[0].ToString() == "TIERRA") & (cityNameArray[1].ToString() == "DEL")
                        & (cityNameArray[2].ToString() == "SOL"))
                    {
                        cityNameArray[0] = "TIERA";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For TWAIN HARTE VALLEY
                    if ((cityNameArray[0].ToString() == "TWAIN") & (cityNameArray[1].ToString() == "HARTE")
                        & (cityNameArray[2].ToString() == "VALLEY"))
                    {
                        cityNameArray[0] = "TWAN";
                        cityNameArray[1] = "HART";
                        cityNameArray[2] = "VLY";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For WHITMORE HOT SPRINGS
                    if ((cityNameArray[0].ToString() == "WHITMORE") & (cityNameArray[1].ToString() == "HOT")
                        & (cityNameArray[2].ToString() == "SPRINGS"))
                    {
                        cityNameArray[0] = "WHTMORE";
                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For YERBA BUENA ISLAND

                    if ((cityNameArray[0].ToString() == "YERBA") & (cityNameArray[1].ToString() == "BUENA")
                        & (cityNameArray[2].ToString() == "ISLAND"))
                    {
                        cityNameArray[1] = "BUNA";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }


                    //For YOSMITE NATIONAL PARK

                    if ((cityNameArray[0].ToString() == "YOSMITE") & (cityNameArray[1].ToString() == "NATIONAL")
                        & (cityNameArray[2].ToString() == "PARK"))
                    {
                        cityNameArray[0] = "YSMITE";
                        cityNameArray[1] = "NAT";


                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }
                    //For ROUGH AND READY
                    if ((cityNameArray[0].ToString() == "ROUGH") & (cityNameArray[2].ToString() == "READY"))
                    {
                         

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[2].ToString();

                    }


                     

                    //For x x LAKE
                    if (cityNameArray[2].ToString() == "LAKE")
                    {

                        cityNameArray[2] = "LK";
                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For EAST X X 
                    if (cityNameArray[0].ToString() == "EAST")
                    {

                        cityNameArray[0] = "E";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X LAKE X
                    if (cityNameArray[1].ToString() == "LAKE" & (cityNameArray[2].ToString() != "TAHOE"))
                    {

                        cityNameArray[1] = "LK";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X VALLEY X
                    if (cityNameArray[1].ToString() == "VALLEY")
                    {

                        cityNameArray[1] = "VLY";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X  X JUNCTION
                    if (cityNameArray[2].ToString() == "JUNCTION")
                    {

                        cityNameArray[2] = "JCT";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X  X PARK
                    if ((cityNameArray[2].ToString() == "PARK") && (cityNameArray[1].ToString() != "MENLO"))
                    {

                        cityNameArray[2] = "PK";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X  X HEIGHTS
                    if (cityNameArray[2].ToString() == "HEIGHTS")
                    {

                        cityNameArray[2] = "HTS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }


                    //For X X SPRINGS 
                    if (cityNameArray[2].ToString() == "SPRINGS")
                    {

                        cityNameArray[2] = "SPGS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X HOT X 
                    if (cityNameArray[1].ToString() == "HOT")
                    {

                        cityNameArray[1] = "H";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For CITY X X  
                    if (cityNameArray[0].ToString() == "CITY")
                    {

                        cityNameArray[0] = "CY";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    

                    //For CORONA DEL X 
                    if ((cityNameArray[0].ToString() == "CORONA") & (cityNameArray[1].ToString() == "DEL"))
                    {

                        cityNameArray[0] = "CRONA";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X X BEACH  
                    if (cityNameArray[2].ToString() == "BEACH")
                    {

                        cityNameArray[2] = "BCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X X HILLS  
                    if (cityNameArray[2].ToString() == "HILLS")
                    {

                        cityNameArray[2] = "HLS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X X RANCHOS  
                    if (cityNameArray[2].ToString() == "RANCHOS")
                    {

                        cityNameArray[2] = "RNCHOS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For RANCHO X X  
                    if (cityNameArray[0].ToString() == "RANCHO")
                    {

                        cityNameArray[0] = "RNCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SAN X X  
                    if (cityNameArray[0].ToString() == "SAN")
                    {

                        cityNameArray[0] = "SN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X X VILLAGE  
                    if (cityNameArray[2].ToString() == "VILLAGE")
                    {

                        cityNameArray[2] = "VLG";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X X VALLEY  
                    if (cityNameArray[2].ToString() == "VALLEY")
                    {

                        cityNameArray[2] = "VLY";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }
                    //For Mission X X    
                    if (cityNameArray[0].ToString() == "MISSION")
                    {

                        cityNameArray[0] = "MSN";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For NORTH X X    
                    if (cityNameArray[0].ToString() == "NORTH")
                    {

                        cityNameArray[0] = "N";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For SOUTH X X    
                    if (cityNameArray[0].ToString() == "SOUTH")
                    {

                        cityNameArray[0] = "S";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For X ROAD X    
                    if (cityNameArray[1].ToString() == "ROAD")
                    {

                        cityNameArray[1] = "RD";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }

                    //For WEST X X    
                    if (cityNameArray[0].ToString() == "WEST")
                    {

                        cityNameArray[0] = "W";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString() + " " + cityNameArray[2].ToString();

                    }
                }

                //Two names for city  ******************

                if (cityNameArray.Length == 2)
                {

                    //For BRENTWOOD PARK
                    if ((cityNameArray[0].ToString() == "BRENTWOOD") & (cityNameArray[1].ToString() == "PARK"))
                    {

                        cityNameArray[0] = "BRENTWD";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For CAPISTRANO BEACH
                    if ((cityNameArray[0].ToString() == "CAPISTRANO") & (cityNameArray[1].ToString() == "BEACH"))
                    {

                        cityNameArray[0] = "CAPISTRAN";
                        cityNameArray[1] = "BCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For MONTGOMERY CREEK
                    if ((cityNameArray[0].ToString() == "MONTGOMERY") & (cityNameArray[1].ToString() == "CREEK"))
                    {

                        cityNameArray[0] = "MONTGOMRY";
                        cityNameArray[1] = "CRK";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For Marshall Station
                    if ((cityNameArray[0].ToString() == "MARSHALL") & (cityNameArray[1].ToString() == "STATION"))
                    {

                        cityNameArray[1] = "STA";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For PORTUGUESE BEND
                    if ((cityNameArray[0].ToString() == "PORTUGUESE") & (cityNameArray[1].ToString() == "BEND"))
                    {
                                            
                        cityNameArray[0] = "PORTUGESE";
                        cityNameArray[1] = "BND";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }


                   

                    //For Pacific Palisades

                    if ((cityNameArray[0].ToString() == "PACIFIC") & (cityNameArray[1].ToString() == "PALISADES"))
                    {

                        cityNameArray[0] = "PCFIC";
                        cityNameArray[1] = "PALSADS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For RANCHO RINCONADA

                    if ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[1].ToString() == "RINCONADA"))
                    {


                        cityNameArray[0] = "RNCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For RANCHO CALIFORNIA

                    if ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[1].ToString() == "CALIFORNIA"))
                    {


                        cityNameArray[1] = "CALIF";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For RANCHO CORDOVA

                    if  ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[1].ToString() == "CORDOVA"))
                    {


                        cityNameArray[0] = "RNCHO";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For SCOTTYS CASTLE

                    if ((cityNameArray[0].ToString() == "SCOTTYS") & (cityNameArray[1].ToString() == "CASTLE"))
                    {


                        cityNameArray[0] = "SCOTYS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For SHASTA RETREAT

                    if ((cityNameArray[0].ToString() == "SHASTA") & (cityNameArray[1].ToString() == "RETREAT"))
                    {


                        cityNameArray[1] = "RETRET";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For STRAWBERRY VALLEY

                    if ((cityNameArray[0].ToString() == "STRAWBERRY") & (cityNameArray[1].ToString() == "VALLEY"))
                    {


                        cityNameArray[0] = "STRAWBRRY";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For SUMMERHOME PARK

                    if ((cityNameArray[0].ToString() == "SUMMERHOME") & (cityNameArray[1].ToString() == "PARK"))
                    {


                        cityNameArray[0] = "SUMERHME";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    

                    //For RICHARDSON GROVE OR SPRINGS

                    if ((cityNameArray[0].ToString() == "RICHARDSON") & (cityNameArray[1].ToString() == "GROVE" || cityNameArray[1].ToString() == "SPRINGS")) 
                    {


                        cityNameArray[0] = "RCHARDSN";

                        if (cityNameArray[1] == "GROVE")
                        {
                            cityNameArray[1] = "GRV";
                        }

                        if (cityNameArray[1] == "SPRINGS")
                        {
                            cityNameArray[1] = "SPGS";
                        }




                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For SAN BERNARDINO

                    if ((cityNameArray[0].ToString() == "SAN") & (cityNameArray[1].ToString() == "BERNARDINO") || cityNameArray[1].ToString() == "BUENAVENTURA") 
                    {


                        cityNameArray[0] = "SN";
                        if (cityNameArray[1].ToString() == "BERNARDINO")
                        {
                            cityNameArray[1] = "BERNRDNO";
                        }
                        if (cityNameArray[1].ToString() == "BUENAVENTURA")
                        {
                            cityNameArray[1] = "BUENAVENT";
                        }

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For TAHOE PARADISE

                    if ((cityNameArray[0].ToString() == "TAHOE") & (cityNameArray[1].ToString() == "PARADISE"))
                    {


                        cityNameArray[1] = "PARADSE";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For TUSTIN FOOTHILLS

                    if ((cityNameArray[0].ToString() == "TUSTIN") & (cityNameArray[1].ToString() == "FOOTHILLS"))
                    {


                        cityNameArray[1] = "FOOTHL";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For TWENTYNINE PALMS

                    if ((cityNameArray[0].ToString() == "TWENTYNINE") & (cityNameArray[1].ToString() == "PALMS"))
                    {


                        cityNameArray[0] = "TWENTYNIN";
                        cityNameArray[1] = "PLM";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For WHISPERING PINES

                    if ((cityNameArray[0].ToString() == "WHISPERING") & (cityNameArray[1].ToString() == "PINES"))
                    {


                        cityNameArray[0] = "WHSPRNG";
                        
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For WOODLAND HILLS

                    if ((cityNameArray[0].ToString() == "WOODLAND") & (cityNameArray[1].ToString() == "HILLS"))
                    {


                        cityNameArray[1] = "HLS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For YOSEMITE FORKS

                    if ((cityNameArray[0].ToString() == "YOSEMITE") & (cityNameArray[1].ToString() == "FORKS"))
                    {


                        cityNameArray[1] = "FRKS";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For CP X 
                    if (cityNameArray[0].ToString() == "CAMP")
                    {

                        cityNameArray[0] = "CP";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X POINT
                    if (cityNameArray[1].ToString() == "POINT")
                    {

                        cityNameArray[1] = "PT";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X ISLAND
                    if (cityNameArray[1].ToString() == "ISLAND")
                    {

                        cityNameArray[1] = "IS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For RANCHO X

                    if ((cityNameArray[0].ToString() == "RANCHO") & (cityNameArray[1].ToString() != "CALIF"))
                    {

                        if (cityNameArray[1] == "CUCAMONGA")
                        {
                            cityNameArray[1] = "CUCAMNGA";
                        }


                        cityNameArray[0] = "RNCH";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For SOUTH X

                    if ((cityNameArray[0].ToString() == "SOUTH"))
                    {


                        cityNameArray[0] = "S";

                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    

                    
                    

                    //For X Village
                    if (cityNameArray[1].ToString() == "VILLAGE")
                    {

                        cityNameArray[1] = "VLG";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();
                        
                    }

                    //For X Grove
                    if (cityNameArray[1].ToString() == "GROVE")
                    {

                        cityNameArray[1] = "GR";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X Estates
                    if (cityNameArray[1].ToString() == "ESTATES")
                    {

                        cityNameArray[1] = "EST";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X Wells
                    if (cityNameArray[1].ToString() == "WELLS")
                    {

                        cityNameArray[1] = "WLS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X Bluff
                    if (cityNameArray[1].ToString() == "BLUFF")
                    {

                        cityNameArray[1] = "BLF";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    

                    //For X VALLEY  
                    if (cityNameArray[1].ToString() == "VALLEY")
                    {

                        cityNameArray[1] = "VLY";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X RANCH  
                    if (cityNameArray[1].ToString() == "RANCH")
                    {

                        cityNameArray[1] = "RNCH";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }
                    //For X PARK
                    if ((cityNameArray[0].ToString() != "BRENTWD" && (cityNameArray[0].ToString() !="SUMERHME") & (cityNameArray[1].ToString() == "PARK")))
                    {

                        cityNameArray[1] = "PK";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X LODGE
                    if (cityNameArray[1].ToString() == "LODGE")
                    {

                        cityNameArray[1] = "LDG";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X JUNCTION
                    if (cityNameArray[1].ToString() == "JUNCTION")
                    {

                        cityNameArray[1] = "JCT";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }
                    //For X SUMIT
                    if (cityNameArray[1].ToString() == "SUMIT")
                    {

                        cityNameArray[1] = "SMT";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    // MOUNT X
                    if (cityNameArray[0].ToString() == "MOUNT")
                    {

                        cityNameArray[0] = "MT";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    // NORTH X
                    if (cityNameArray[0].ToString() == "NORTH")
                    {

                        cityNameArray[0] = "N";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }


                    // Santa X
                    if (cityNameArray[0].ToString() == "SANTA")
                    {

                        cityNameArray[0] = "SN";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    // WEST X
                    if (cityNameArray[0].ToString() == "WEST")
                    {

                        cityNameArray[0] = "W";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }
                    //For X Acres
                    if (cityNameArray[1].ToString() == "ACRES")
                    {

                        cityNameArray[1] = "ACRE";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X BEACH
                    if (cityNameArray[1].ToString() == "BEACH")
                    {
                        if (cityNameArray[0].ToString().Length == 10)
                        {


                            cityNameArray[1] = "BH";
                            cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();
                        }
                        else
                        {
                            cityNameArray[1] = "BCH";
                            cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();
                        }

                    }

                    //For X CENTER
                    if (cityNameArray[1].ToString() == "CENTER")
                    {

                        cityNameArray[1] = "CTR";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X CITY
                    if (cityNameArray[1].ToString() == "CITY")
                    {

                        cityNameArray[1] = "CY";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X LAKE
                    if (cityNameArray[1].ToString() == "LAKE")
                    {

                        cityNameArray[1] = "LK";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X MEADOWS
                    if (cityNameArray[1].ToString() == "MEADOWS")
                    {

                        cityNameArray[1] = "MDWS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For LAKE X
                    if (cityNameArray[0].ToString() == "LAKE")
                    {

                        cityNameArray[0] = "LK";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }


                     

                    //For EAST X 
                    if (cityNameArray[0].ToString() == "EAST")
                    {

                        cityNameArray[0] = "E";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X HEIGHTS
                    if ((cityNameArray[1].ToString() == "HEIGHTS") & (cityNameArray[0] != "E"))
                    {

                        cityNameArray[1] = "HTS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For AIRPORT X
                    if (cityNameArray[0].ToString() == "AIRPORT") 
                    {

                        cityNameArray[0] = "ARPRT";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    

                    //For X OAKS
                    if (cityNameArray[1].ToString() == "OAKS")
                    {

                        cityNameArray[1] = "OAK";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }
                    //For X MILL
                    if (cityNameArray[1].ToString() == "MILL")
                    {

                        cityNameArray[1] = "ML";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X MILLS
                    if (cityNameArray[1].ToString() == "MILLS")
                    {

                        cityNameArray[1] = "MLS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X MOUNTAIN
                    if (cityNameArray[1].ToString() == "MOUNTAIN")
                    {

                        cityNameArray[1] = "MTN";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    

                    //For X SPRINGS
                    if (cityNameArray[1].ToString() == "SPRINGS")
                    {

                        cityNameArray[1] = "SPGS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X TIBERON
                    if (cityNameArray[1].ToString() == "TIBERON")
                    {

                        cityNameArray[1] = "TIB";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X TERRANCE
                    if (cityNameArray[1].ToString() == "TERRANCE")
                    {

                        cityNameArray[1] = "TER";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X CORNER
                    if (cityNameArray[1].ToString() == "CORNER")
                    {

                        cityNameArray[1] = "COR";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X GARDENS
                    if (cityNameArray[1].ToString() == "GARDENS")
                    {

                        cityNameArray[1] = "GDNS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For CALIFORNIA X 
                    if (cityNameArray[0].ToString() == "CALIFORNIA")
                    {

                        cityNameArray[0] = "CALIF";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For CAMP X 
                    if (cityNameArray[0].ToString() == "CAMP")
                    {

                        cityNameArray[0] = "CP";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For CANYON X 
                        if (cityNameArray[0].ToString() == "CANYON")
                    {

                        cityNameArray[0] = "CYN";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For x CANYON  
                    if (cityNameArray[1].ToString() == "CANYON")
                    {

                        cityNameArray[1] = "CYN";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For x RANCHOS  
                    if (cityNameArray[1].ToString() == "RANCHOS")
                    {

                        cityNameArray[1] = "RNCHOS";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X LANDING
                    if (cityNameArray[1].ToString() == "LANDING")
                    {

                        cityNameArray[1] = "LNDG";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                    //For X PINES
                    if ((cityNameArray[1].ToString() == "PINES") & (cityNameArray[0].ToString() != "WHSPRNG"))
                    {

                        cityNameArray[1] = "PNES";
                        cityTxtBx.Text = cityNameArray[0].ToString() + " " + cityNameArray[1].ToString();

                    }

                     
                }

                if (cityNameArray.Length == 1)
                {
                    if (cityNameArray[0].ToString() == "CARDIFFBYTHESEA")
                    {
                        cityNameArray[0] = "CARDIFF";
                        cityTxtBx.Text = cityNameArray[0].ToString();

                    }

                    if (cityNameArray[0].ToString() == "FREDERICKSBURG")
                    {
                        cityNameArray[0] = "FREDERICKBURG";
                        cityTxtBx.Text = cityNameArray[0].ToString();

                    }

                    if (cityNameArray[0].ToString() == "HOLLYWOODBYTHESEA")
                    {
                        cityNameArray[0] = "HOLYWD BY SEA";
                        cityTxtBx.Text = cityNameArray[0].ToString();

                    }

                }
                if (cityTxtBx.Text.Length > 13)
                {
                    cityTxtBx.Text = cityTxtBx.Text.Substring(0, 13);
                }
            }
 
 
 
            
            
        }

        private void birthDateMskTxtBx_Validating(object sender, CancelEventArgs e)
        {
             

                
        }

        private void addressTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void driversFirstNameTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void ownerResponsibilityChkBx_CheckedChanged(object sender, EventArgs e)
        {
            //Birthdate is zeros for owner responsibility and
            //Sections Violated must be 40001,1A,1B

            if (ownerResponsibilityChkBx.Checked == true)
            {
                birthDateMskTxtBx.Text = "00/00/0000";
                driversLicenseTxtBx.Text = "A0000000"; 
                //this checks to see that at least 40001,1A,1B are entered. 
                checkSectviolated(sender);
                
            }
            else
            {
                birthDateMskTxtBx.Text = birthDateMskTxtBx.Text;
            }

            if (ownerResponsibilityChkBx.Checked == false)
            {
                errorLstBx.Visible = false;
                errorLstBx.Items.Clear();
            }
        }

        private void sectvioTxtBx1_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx2_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void driversLicenseTxtBx_Leave(object sender, EventArgs e)
        {
            validateDriversLicense(sender);
            
        }

        private void sectvioTxtBx1_TextChanged(object sender, EventArgs e)
        {

        }

        private void amendedDateMskTxtBx_Leave(object sender, EventArgs e)
        {
            DateValidations(sender);
            courtWriteBtn.Enabled = true;
        }

        private void convictionDateMskTxtBx_Leave(object sender, EventArgs e)
        {
            DateValidations(sender);

            if (transactionLstBx.SelectedItem.ToString() == "D13" || transactionLstBx.SelectedItem.ToString() == "D14")
            {
                courtWriteBtn.Enabled = true;
            }

            

             
        }

        private void violationDateMskTxtBx_Leave(object sender, EventArgs e)
        {
            DateValidations(sender);

            if (transactionLstBx.SelectedItem.ToString() == "D12")
            {
                courtWriteBtn.Enabled = true;
            }
        }

        private void termCourtSuspensionTxtBx_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void bacTxtBx_Leave(object sender, EventArgs e)
        {
            validateBac(sender);
        }

        private void violationDateMskTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StreamWriter recordOut;

            //This block just checks for a file existence, if there append, if not create
            if (File.Exists(courtPath))
            {

                  recordOut = new StreamWriter(
                  new FileStream(courtPath, FileMode.Append, FileAccess.Write));
                

            }
            else
            {
                    recordOut = new StreamWriter(
                    new FileStream(courtPath, FileMode.Create, FileAccess.Write));
                 
            }

            foreach (String cls in courtAbstractLine)
            {
                 
                //MessageBox.Show("length=" + cls.Length);

                
                //recordOut.WriteLine(cls.PadRight(298,' '));
                recordOut.WriteLine(cls);
                //this was causing an error in production.
               
                

            }

            recordOut.Close();

            MessageBox.Show("Court Abstract records Written=" + courtAbstractLine.Count, "Court Abstracts");
            //MessageBox.Show("Court Abstact file Written to " + courtPath);
            string copyfile = filepath + @"\" + "court.txt";
            File.Delete(copyfile);
            File.Copy(courtPath, copyfile);
            //File.Encrypt(copyfile);
            MessageBox.Show("File copied to:" + copyfile, "File Copy Send to DMV");
            //commented out close, so customer can use send file button
            this.Close();

        }

        private void birthDateMskTxtBx_Leave(object sender, EventArgs e)
        {
            validateBirthDate(sender);
        }

        private void dispositionLstBx_Leave(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex != -1)
            {
                collectDispCodes(sender);
            }
             
            
        }

        private void dispoNumberUpDwn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dispositionLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void firstDispCodeTab_Leave(object sender, EventArgs e)
        {
            checkTab(sender);
            
             
            
             
        }

        private void dispCodeLstBx2_Leave(object sender, EventArgs e)
        {
             
        }

        private void driversLastNameTxtBx_Leave(object sender, EventArgs e)
        {
            nameValidatons(sender);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void amendedDateMskTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void termCourtSuspensionDateMskTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void termCourtSuspensionDateMskTxtBx_Leave(object sender, EventArgs e)
        {
            if (termCourtSuspensionTxtBx.Text.Trim().Length == 0)
            {
                DateValidations(sender);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void firstProbCodeTabPage_Leave(object sender, EventArgs e)
        {
           
        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            collectProbCodes(sender);
        }

        private void driversLicenseTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%'
                || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')'
                || e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == '~' || e.KeyChar == '`' || e.KeyChar == '{'
                || e.KeyChar == '}' || e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '|' || e.KeyChar == ':'
                || e.KeyChar == ';' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == ',' || e.KeyChar == '.'
                || e.KeyChar == '?' || e.KeyChar == ' ')
                e.Handled = true;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
               

                bool success;

                Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
                success = ftp.UnlockComponent("T09262010FTP_erJHxsUc9Z8s");

                ftp.Hostname = "sftp.dts.ca.gov";
                ftp.Username = "dmv-ddt-test2";
                //ftp.Password = "Frankie@1";
                ftp.AuthSsl = true;
                ftp.AuthTls = true;
                ftp.Ssl = false;
                //ftp.Port = 443;
                ftp.Port = 2121;
                ftp.Passive = true;
                ftp.ConnectTimeout = 20000;
                Chilkat.Cert cert = new Chilkat.Cert();
                success = cert.LoadByCommonName("dmv-ddt-test2");
                ftp.SetSslClientCert(cert);
                success = ftp.Connect();
                //Get info about file.
 
                FileInfo courtFile = new FileInfo(filepath + @"\" + "court.txt");
                


                if (courtFile.Exists == true)
                {
                    if (success != true)
                    {
                        MessageBox.Show(ftp.LastErrorText);
                        return;
                    }

                    success = ftp.ChangeRemoteDir("/DMVData/SendToDMV");

                    if (success != true)
                    {
                        MessageBox.Show(ftp.LastErrorText);
                        return;
                    }
                    success = ftp.PutFile(filepath + @"\" + "court.txt", "court.txt");
                     

                    if (success != true)
                    {
                        MessageBox.Show(ftp.LastErrorText);
                        return;
                    }

                    if (success == true)
                    {
                        MessageBox.Show("Court File Sent Succesfully with byte count of  " + ftp.AsyncBytesSent);

                        courtFile.Delete();
                    }


                }

                if (courtFile.Exists == false)
                {
                    MessageBox.Show("You must enter data and create file!", "Court File Does Not Exist");
                }

                 
            }
            catch (UriFormatException f)
            {
                Console.WriteLine("Invalid URL" + f);
            }
            catch (IOException f)
            {
                Console.WriteLine("Could not connect to URL");
            }

           


        }

        private void sendFTP(string remoteFileName,string UserName,string Password,string localFile,int BufferSize)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(remoteFileName);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.Credentials = new NetworkCredential(UserName, Password);
            ftpRequest.ReadWriteTimeout = 10000;
            ftpRequest.Timeout = 20000;
            ftpRequest.KeepAlive = true;
            ftpRequest.Proxy = null;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            Stream requestStream = null;

            try
            {

                using (MemoryStream fileStream = new MemoryStream(1))
                {
                    byte[] buffer = new byte[BufferSize];

                    int readCount = fileStream.Read(buffer, 0, BufferSize);
                    int bytesSentCounter = 0;

                    while (readCount > 0)
                    {
                        requestStream.Write(buffer, 0, readCount);
                        bytesSentCounter += readCount;

                        readCount = fileStream.Read(buffer, 0, BufferSize);
                        System.Threading.Thread.Sleep(100);

                    }
                }

                requestStream.Close();
                requestStream = null;

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                FtpStatusCode code = response.StatusCode;
                string description = response.StatusDescription;
                response.Close();

                //MessageBox.Show("Upload file result : status code {0}, status description {1}", code, description);

                if (code == FtpStatusCode.ClosingData)
                {
                    //MessageBox.Show.Information("File {0} uploaded successfully", localFileName);
                }
                else
                {
                    //MessageBox.Show.Error("Uploading file {0} did not succeed. Status code is {1}, description  {2}", localFileName, code, description);
                }

            }
            catch (WebException ex)
            {

                if (requestStream != null)
                    requestStream.Close();

                ftpRequest.Abort();

                FtpStatusCode code = ((FtpWebResponse)ex.Response).StatusCode;
                string description = ((FtpWebResponse)ex.Response).StatusDescription;

                //logger.Error("A connection to the ftp server could not be established. Status code: {0}, description: {1} Exception: {2}. Retrying...", code, description, ex.ToString());
            }
        }

        private void jailTxtBx_TextChanged(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void amendedCodeLstBx_DoubleClick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.ListBox)sender).SelectedIndex = -1;
        }

        private void amendedCodeLstBx_Leave(object sender, EventArgs e)
        {
            amendedDateMskTxtBx.Enabled = true;
            courtWriteBtn.Enabled = false;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearForm.clearForm(this);

            foreach (ListBox dcl in dispListOfListBoxes)
            {

                dcl.Enabled = true;
                errorLstBx.ClearSelected();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.ListBox)sender).SelectedIndex = -1;
        }

        private void cityTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            string squote = "'";
            char csquote = Convert.ToChar(squote);


            //Try to cover all special chars, unfortunatly enum cannot
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '-' ||  e.KeyChar == csquote
                || e.KeyChar == ',' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^')
                e.Handled = true;
        }

        private void getFilesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                

                bool success;

                Chilkat.Ftp2 ftp = new Chilkat.Ftp2();

                success = ftp.UnlockComponent("T09262010FTP_erJHxsUc9Z8s");

                ftp.Hostname = "sftp.dts.ca.gov";
                ftp.Username = "dmv-ddt-test2";
                //ftp.Password = "Frankie@1";
                ftp.AuthSsl = true;
                ftp.AuthTls = true;
                ftp.Ssl = false;
                //ftp.Port = 443;
                ftp.Port = 2121;
                ftp.Passive = true;
                ftp.ConnectTimeout = 20000;
                ftp.SetTypeAscii();
                Chilkat.Cert cert = new Chilkat.Cert();
                success = cert.LoadByCommonName("dmv-ddt-test2");
                ftp.SetSslClientCert(cert);
                success = ftp.Connect();

                if (success != true)
                {
                    MessageBox.Show(ftp.LastErrorText);
                    return;
                }

                success = ftp.ChangeRemoteDir("/DMVData/FromDMV");
                 
                if (success != true)
                {
                    MessageBox.Show(ftp.LastErrorText);
                    return;
                }

                //This gets the number of files in the current directory

                 
                int getAllFiles = ftp.MGetFiles("*.*", filepath);

                if (getAllFiles != 0)
                {
                    for (int i = 0; i < getAllFiles; i++)
                    {
                        MessageBox.Show("Downloading File " + ftp.GetFilename(i) + "File Size" + ftp.GetSize(i), "File Download");
                        MessageBox.Show("Deleting files from your SFT Account", "Deleting Files");
                        ftp.DeleteMatching("*.*");
                    }
                }

                if (getAllFiles == 0)
                {
                    MessageBox.Show("There are no files in your SFT account", "SFT Account Empty");
                }

                          
                 

                if (success != true)
                {
                    MessageBox.Show(ftp.LastErrorText);
                    return;
                }


                 




            }
            catch (UriFormatException f)
            {
                Console.WriteLine("Invalid URL" + f);
            }
            catch (IOException f)
            {
                Console.WriteLine("Could not connect to URL");
            }
        }

        private void jailTxtBx_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void sectvioTxtBx4_TextChanged(object sender, EventArgs e)
        {

        }

        private void sectvioTxtBx3_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx4_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx5_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx6_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void sectvioTxtBx7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void sectvioTxtBx7_Leave(object sender, EventArgs e)
        {
            validateSectionsViolated(sender);
        }

        private void ownerResponsibilityChkBx_Leave(object sender, EventArgs e)
        {
            checkSectviolated(sender);
        }
              
        }//ends tool strip
        
    } //ends public partial class

  //ends namespace
 

            
 