using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace File_Generation_System
{
    

    public partial class vrinquiry : Form 
        
    {

         
        public bool errorflag;
        public string docket;
        public DataSet ds;
         
       

        public vrinquiry()
        {
            InitializeComponent();
            //Create standard dataset

             ds = new DataSet();

            //Read XML document into dataset

            configure.read_xml_files();
             
            ds.ReadXml(configure.vrdb);

            

            

        }

        private void exitVRTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

         

        private void addVrInquiry_Click(object sender, EventArgs e)
        
        {
            try
            {


                this.errorflag = false;
                inquiryErrorlstbx.Items.Clear();
                messageLbl.Text = "";
                int count =  ds.Tables["inquiry_data"].Rows.Count;

                try
                {
                    DataColumn[] pk = new DataColumn[1];

                    pk[0] = ds.Tables["inquiry_data"].Columns["user_information"];

                    ds.Tables["inquiry_data"].PrimaryKey = pk;
                }
                catch (Exception ex)
                {
                    inquiryErrorlstbx.Visible = true;
                    inquiryErrorlstbx.Items.Add(ex + "Duplicate Citation Numbers Not Allowed");

                }

                string citeTest = citationNumberRchTxtBx.Text.Trim();

                if (citeTest.Length == 0)
                {
                    inquiryErrorlstbx.Visible = true;
                    inquiryErrorlstbx.Items.Add("Citation Number must be entered");
                    citationNumberRchTxtBx.Focus();
                    errorflag = true;

                }

                if (penaltyDueRchTxtBx.Text.Trim().Length != 4)
                {

                    inquiryErrorlstbx.Visible = true;
                    inquiryErrorlstbx.Items.Add("Penalty Due must be three bytes long");
                    penaltyDueRchTxtBx.Focus();
                    errorflag = true;
                }


                if (fileCodeListBox.Text.Length == 0)
                {
                    inquiryErrorlstbx.Visible = true;
                    inquiryErrorlstbx.Items.Add("File code must be selected");
                    fileCodeListBox.Focus();
                    errorflag = true;


                }

                //Vessel Inquiry Validations 
                //5.27.15 added check for nulls as this was preventing error display also changed & or to && and 

                if (fileCodeListBox.SelectedItem != null && vehicleLicenseNumbertxtbx.Text != "")
                {
                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "B" & vehicleLicenseNumbertxtbx.Text.Substring(0, 2) == "CF")
                    {
                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("For inquiries on Vessels do not include the CF prefix");
                        vehicleLicenseNumbertxtbx.Focus();
                        errorflag = true;
                    }

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "B" & vehicleLicenseNumbertxtbx.Text.Length != 6)
                    {

                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("Vessel Inquiries must be 6 bytes long");
                        vehicleLicenseNumbertxtbx.Focus();
                        errorflag = true;
                    }

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "B" & vehicleLicenseNumbertxtbx.Text.Length == 6)
                    {
                        int t1;
                        int t2;
                        //Check entry, if it is nnnncc or ccnnnn then its good otherwise complain. 

                        t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 4).IndexOfAny("0123456789".ToCharArray());
                        t2 = vehicleLicenseNumbertxtbx.Text.Substring(4, 2).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

                        if (t1 == -1 || t2 == -1)
                        {
                            t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 2).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                            t2 = vehicleLicenseNumbertxtbx.Text.Substring(2, 4).IndexOfAny("0123456789".ToCharArray());
                        }

                        if (t1 == -1 || t2 == -1)
                        {
                            inquiryErrorlstbx.Visible = true;
                            inquiryErrorlstbx.Items.Add("Vessel Inquiries format must be 1234AB or AB1234 ");
                            vehicleLicenseNumbertxtbx.Focus();
                            errorflag = true;
                        }


                    }

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "A" & vehicleLicenseNumbertxtbx.Text.Length == 6)
                    {
                        int t1;
                        int t2;
                        //Check Entry 6 byte Auto AAANNN or NNNAAA 
                        t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 3).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                        t2 = vehicleLicenseNumbertxtbx.Text.Substring(3, 3).IndexOfAny("0123456789".ToCharArray());


                        if (t1 == -1 || t2 == -1)
                        {
                            t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 3).IndexOfAny("0123456789".ToCharArray());
                            t2 = vehicleLicenseNumbertxtbx.Text.Substring(3, 3).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

                        }

                        if (t1 == -1 || t2 == -1)
                        {
                            inquiryErrorlstbx.Visible = true;
                            inquiryErrorlstbx.Items.Add("Auto Inquiries must be AAANNN or NNNAAA (A=alpha,N=numeric)");
                            vehicleLicenseNumbertxtbx.Focus();
                            errorflag = true;
                        }


                    }

                    //Six byte Commerial Plate configuration check

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "C" & vehicleLicenseNumbertxtbx.Text.Length == 6)
                    {
                        int t1;
                        int t2;

                        //Check Entry 6 byte Commercial ANNNNN or NNNNNA 
                        t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 1).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                        t2 = vehicleLicenseNumbertxtbx.Text.Substring(1, 5).IndexOfAny("0123456789".ToCharArray());


                        if (t1 == -1 || t2 == -1)
                        {
                            t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 5).IndexOfAny("0123456789".ToCharArray());
                            t2 = vehicleLicenseNumbertxtbx.Text.Substring(5, 1).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

                        }

                        if (t1 == -1 || t2 == -1)
                        {
                            inquiryErrorlstbx.Visible = true;
                            inquiryErrorlstbx.Items.Add("Commercial Inquiries must be ANNNNN or NNNNNA (A=alpha,N=numeric)");
                            vehicleLicenseNumbertxtbx.Focus();
                            errorflag = true;
                        }


                    }

                    //Seven byte checks Automotive and Commercial


                    //Automotive Seven Byte plate configuration check

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "A" & vehicleLicenseNumbertxtbx.Text.Length == 7)
                    {
                        int t1;
                        int t2;
                        int t3;
                        //Check Entry 7 byte Auto NAAANNN  

                        t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 1).IndexOfAny("0123456789".ToCharArray());
                        t2 = vehicleLicenseNumbertxtbx.Text.Substring(1, 3).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                        t3 = vehicleLicenseNumbertxtbx.Text.Substring(4, 3).IndexOfAny("0123456789".ToCharArray());




                        if (t1 == -1 || t2 == -1 || t3 == -1)
                        {
                            inquiryErrorlstbx.Visible = true;
                            inquiryErrorlstbx.Items.Add("Auto Inquiries must be NAAANNN (A=alpha,N=numeric)");
                            vehicleLicenseNumbertxtbx.Focus();
                            errorflag = true;
                        }


                    }

                    //Commercial Plate Configuration Check

                    if (fileCodeListBox.SelectedItem.ToString().Substring(0, 1) == "C" & vehicleLicenseNumbertxtbx.Text.Length == 7)
                    {
                        int t1;
                        int t2;
                        int t3;
                        int t4;
                        int t5;
                        int t6;
                        //Check Entry 7 byte Commercial NANNNNN

                        t1 = vehicleLicenseNumbertxtbx.Text.Substring(0, 1).IndexOfAny("0123456789".ToCharArray());
                        t2 = vehicleLicenseNumbertxtbx.Text.Substring(1, 1).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                        t3 = vehicleLicenseNumbertxtbx.Text.Substring(2, 5).IndexOfAny("0123456789".ToCharArray());

                        t4 = vehicleLicenseNumbertxtbx.Text.Substring(0, 4).IndexOfAny("0123456789".ToCharArray());
                        t5 = vehicleLicenseNumbertxtbx.Text.Substring(5, 1).IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                        t6 = vehicleLicenseNumbertxtbx.Text.Substring(6, 1).IndexOfAny("0123456789".ToCharArray());

                        //Commercial Inquiries NANNNNN in this check we just omit checking byte 2 and 6
                        //Reverse Commercial NNNNNAN
                        if (t1 == -1 || t3 == -1 || t4 == -1 || t5 == -1)
                        {
                            inquiryErrorlstbx.Visible = true;
                            inquiryErrorlstbx.Items.Add("Commercial Inquiries must be NANNNNN");
                            inquiryErrorlstbx.Items.Add("Reverse Commercial must be NNNNNAN (A=alpha,N=numeric)");
                            vehicleLicenseNumbertxtbx.Focus();
                            errorflag = true;
                        }


                    }
                }

                if (vehicleLicenseNumbertxtbx.Text.Length == 0 & vinRichTxtBx.Text.Length == 0)
                {
                    inquiryErrorlstbx.Visible = true;
                    inquiryErrorlstbx.Items.Add("vehicle license number or VIN must be entered");
                    this.vehicleLicenseNumbertxtbx.Focus();
                    errorflag = true;


                }

                // if (vehicleLicenseNumbertxtbx.Text.Substring(   



                if (fileCodeListBox.Text.Length > 0)
                {
                    if (fileCodeListBox.Text.Substring(0, 1) == "V" && vinRichTxtBx.Text.Trim().Length == 0)
                    {
                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("If file code is equal to V, then VIN field cannot be blank");
                        vinRichTxtBx.Focus();
                        this.errorflag = true;
                    }
                    //Needs to change, should check a plates with owner check box or something
                    if (platesWithOwnerchkbx.Checked == true & vinRichTxtBx.Text.Length < 3)
                    {
                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("Plates with owner require license number and last three bytes of VIN");
                        vinRichTxtBx.Focus();
                        this.errorflag = true;
                    }

                    if (this.dateTimePicker1.Checked)
                    {


                        modelYearTxtBx.Text = "    ";
                    }
                    //Enum would work well here. 
                    if (fileCodeListBox.Text.Substring(0, 1) == "A" & vehicleLicenseNumbertxtbx.Text.Length == 0)
                    {
                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("If file code is equal to A, then License field cannot be blank");
                        vinRichTxtBx.Focus();
                        this.errorflag = true;
                    }

                    vehicleLicenseNumbertxtbx.Focus();

                }



                if (this.dateTimePicker1.Checked == false)
                {
                    if (this.modelYearTxtBx.Text.Length == 1 & this.makeRchTxtBx.Text.Length == 1)
                    {
                        inquiryErrorlstbx.Visible = true;
                        inquiryErrorlstbx.Items.Add("If As of Date is not used Model Year or Make must be used");
                    }
                }

                


                //Trying to insure citation is unique
                if (errorflag == false )
                {
                    inquiryErrorlstbx.Visible = false;
                    XmlDocument doc = new XmlDocument();

                    doc.Load(configure.vrdb);

                    XmlElement newVrRecord = doc.CreateElement("vr_record");

                    //Get date and time and make it a string
                    //DateTime today = DateTime.Now;
                    //string vrtime = today.ToString();

                    XmlElement newInquiryData = doc.CreateElement("inquiry_data");
                    newVrRecord.AppendChild(newInquiryData);

                    XmlElement newLicenseNumber = doc.CreateElement("license_number");
                    newLicenseNumber.InnerText = vehicleLicenseNumbertxtbx.Text.ToUpper().Replace(" ", "");
                    newInquiryData.AppendChild(newLicenseNumber);

                    XmlElement regExpireDate = doc.CreateElement("reg_expire_date");
                    regExpireDate.InnerText = null;
                    newInquiryData.AppendChild(regExpireDate);


                    XmlElement make = doc.CreateElement("make");
                    make.InnerText = makeRchTxtBx.Text.ToUpper();
                    newInquiryData.AppendChild(make);

                    XmlElement vin = doc.CreateElement("vin");
                    vin.InnerText = vinRichTxtBx.Text.ToUpper();
                    newInquiryData.AppendChild(vin);

                    XmlElement roCity = doc.CreateElement("ro_city");
                    roCity.InnerText = null;
                    newInquiryData.AppendChild(roCity);

                    XmlElement roZip = doc.CreateElement("ro_zip");
                    roZip.InnerText = null;
                    newInquiryData.AppendChild(roZip);

                    XmlElement roCountyCode = doc.CreateElement("ro_county_code");
                    roCountyCode.InnerText = null;
                    newInquiryData.AppendChild(roCountyCode);


                    XmlElement newAsOfDate = doc.CreateElement("as_of_date");
                    if (dateTimePicker1.Checked)
                    {

                        String fasofdate = dateTimePicker1.Text.ToString().Trim();
                        fasofdate = fasofdate.Replace("/", "");
                        fasofdate = fasofdate.Substring(0, 4) + fasofdate.Substring(6, 2);
                        newAsOfDate.InnerText = fasofdate;
                        newInquiryData.AppendChild(newAsOfDate);
                    }




                    if (dateTimePicker1.Checked.Equals(false))
                    {

                        //changed to send nothing if no date is chosen improves hits. 


                        newAsOfDate.InnerText = "      ";
                        newInquiryData.AppendChild(newAsOfDate);
                    }

                    //Used in VR inquiries and Parking Holds
                    XmlElement newFileCode = doc.CreateElement("file_code");
                    newFileCode.InnerText = fileCodeListBox.Text.Substring(0, 1);
                    newInquiryData.AppendChild(newFileCode);



                    XmlElement newModelYear = doc.CreateElement("year_model");
                    newModelYear.InnerText = modelYearTxtBx.Text;
                    newInquiryData.AppendChild(newModelYear);


                    XmlElement newUserInformation = doc.CreateElement("user_information");
                    newUserInformation.InnerText = citationNumberRchTxtBx.Text.Trim();
                    newInquiryData.AppendChild(newUserInformation);


                    XmlElement paperIssueDate = doc.CreateElement("paper_issue_date");
                    paperIssueDate.InnerText = null;
                    newInquiryData.AppendChild(paperIssueDate);

                    XmlElement roNameAndAddressSourceIndicator = doc.CreateElement("ro_nameaddress_source_indicator");
                    roNameAndAddressSourceIndicator.InnerText = null;
                    newInquiryData.AppendChild(roNameAndAddressSourceIndicator);

                    XmlElement roName = doc.CreateElement("ro_name");
                    roName.InnerText = null;
                    newInquiryData.AppendChild(roName);

                    XmlElement roNameOrAddress = doc.CreateElement("ro_name_or_address");
                    roNameOrAddress.InnerText = null; ;
                    newInquiryData.AppendChild(roNameOrAddress);

                    XmlElement additionalRoNameOrAddress = doc.CreateElement("additional_ro_name_or_address");
                    additionalRoNameOrAddress.InnerText = null;
                    newInquiryData.AppendChild(additionalRoNameOrAddress);

                    XmlElement additionalRoAddress = doc.CreateElement("additional_ro_address");
                    additionalRoAddress.InnerText = null;
                    newInquiryData.AppendChild(additionalRoAddress);

                    XmlElement recordConditionCode = doc.CreateElement("record_condition_code");
                    recordConditionCode.InnerText = null;
                    newInquiryData.AppendChild(recordConditionCode);

                    XmlElement recordConditionCodeDate = doc.CreateElement("record_condition_code_date");
                    recordConditionCodeDate.InnerText = null;
                    newInquiryData.AppendChild(recordConditionCodeDate);

                    XmlElement recordRemarks = doc.CreateElement("record_remarks");
                    recordRemarks.InnerText = null;
                    newInquiryData.AppendChild(recordRemarks);

                    XmlElement errorCode = doc.CreateElement("error_code");
                    errorCode.InnerText = null;
                    newInquiryData.AppendChild(errorCode);

                    XmlElement errorMessage = doc.CreateElement("error_message");
                    errorMessage.InnerText = null;
                    newInquiryData.AppendChild(errorMessage);

                    XmlElement errorDate = doc.CreateElement("error_date");
                    errorDate.InnerText = null;
                    newInquiryData.AppendChild(errorDate);

                    XmlElement holdRecordWritten = doc.CreateElement("hold_record_written");
                    holdRecordWritten.InnerText = null;
                    newInquiryData.AppendChild(holdRecordWritten);

                    XmlElement nrlTransferDate = doc.CreateElement("nrl_transfer_date");
                    nrlTransferDate.InnerText = null;
                    newInquiryData.AppendChild(nrlTransferDate);

                    XmlElement nrlNameOrMessage = doc.CreateElement("nrl_name_or_message");
                    nrlNameOrMessage.InnerText = null;
                    newInquiryData.AppendChild(nrlNameOrMessage);

                    XmlElement nrlRecordId = doc.CreateElement("nrl_record_Id");
                    nrlRecordId.InnerText = null;
                    newInquiryData.AppendChild(nrlRecordId);

                    XmlElement nrlAddressMessage = doc.CreateElement("nrl_address_message");
                    nrlAddressMessage.InnerText = null;
                    newInquiryData.AppendChild(nrlAddressMessage);

                    XmlElement nrlCityStateMessage = doc.CreateElement("nrl_city_state_message");
                    nrlCityStateMessage.InnerText = null;
                    newInquiryData.AppendChild(nrlCityStateMessage);

                    //Start of Parking Holds elements
                    XmlElement newTypeActionCode = doc.CreateElement("type_action_code");
                    newTypeActionCode.InnerText = null;
                    newInquiryData.AppendChild(newTypeActionCode);

                    XmlElement newDispositionCode = doc.CreateElement("dispostion_code");
                    newDispositionCode.InnerText = null;
                    newInquiryData.AppendChild(newDispositionCode);

                    ////Violation Date (which should be as of date)                                                           
                    //DateTime date1 = DateTime.Now;
                    //string fdate1 = date1.ToString("MM/dd/yyyy");
                    //XmlElement newViolationDate = doc.CreateElement("violation_date");
                    //newViolationDate.InnerText = fdate1;
                    //newInquiryData.AppendChild(newViolationDate);

                    ////Violation Time 

                    //XmlElement newViolationTime = doc.CreateElement("violation_time");
                    //newViolationTime.InnerText = fdate1;
                    //newInquiryData.AppendChild(newViolationTime);

                    //This needs to show up on the VR inquiry screen as well
                    XmlElement newPenaltyAmount = doc.CreateElement("penalty_amount");
                    newPenaltyAmount.InnerText = penaltyDueRchTxtBx.Text.Replace("$", "");
                    newInquiryData.AppendChild(newPenaltyAmount);


                    XmlElement newNoticeDate = doc.CreateElement("notice_date");
                    newNoticeDate.InnerText = null;
                    newInquiryData.AppendChild(newNoticeDate);

                    XmlElement newCitationNumber = doc.CreateElement("citation_number");
                    newCitationNumber.InnerText = null;
                    newInquiryData.AppendChild(newCitationNumber);

                    //Start of Monthly Parking file elements
                    XmlElement newRecordIdentifier = doc.CreateElement("record_identifier");
                    newRecordIdentifier.InnerText = null;
                    newInquiryData.AppendChild(newRecordIdentifier);

                    XmlElement newRejectionCode = doc.CreateElement("rejection_code");
                    newRejectionCode.InnerText = null;
                    newInquiryData.AppendChild(newRejectionCode);

                    XmlElement newRejectionMessage = doc.CreateElement("rejection_message");
                    newRejectionMessage.InnerText = null;
                    newInquiryData.AppendChild(newRejectionMessage);

                    XmlElement newDatePaid = doc.CreateElement("date_paid");
                    newDatePaid.InnerText = null;
                    newInquiryData.AppendChild(newDatePaid);

                    XmlElement newRemovedDate = doc.CreateElement("removed_date");
                    newRemovedDate.InnerText = null;
                    newInquiryData.AppendChild(newRemovedDate);

                    XmlElement newRemovalCode = doc.CreateElement("removal_code");
                    newRemovalCode.InnerText = null;
                    newInquiryData.AppendChild(newRemovalCode);

                    XmlElement newRemovalMessage = doc.CreateElement("removal_message");
                    newRemovalMessage.InnerText = null;
                    newInquiryData.AppendChild(newRemovalMessage);

                    //poplulated when Record Indicator is 4 indicating a succesful add
                    XmlElement newRecordCode = doc.CreateElement("record_code");
                    newRecordCode.InnerText = null;
                    newInquiryData.AppendChild(newRecordCode);

                    //Docket number provides a unique number for each record
                    XmlElement newDocketNumber = doc.CreateElement("docket_number");
                    newDocketNumber.InnerText = docketNumberTxtBx.Text;
                    newInquiryData.AppendChild(newDocketNumber);

                    //Viol1 first violation  
                    XmlElement newViol1 = doc.CreateElement("viol1_section");
                    newViol1.InnerText = secViol1RchTxtBx.Text;
                    newInquiryData.AppendChild(newViol1);

                    //Viol2 second violation  
                    XmlElement newViol2 = doc.CreateElement("viol2_section");
                    newViol2.InnerText = secViol2RchTxtBx.Text;
                    newInquiryData.AppendChild(newViol2);

                    //Viol3 third violation  
                    XmlElement newViol3 = doc.CreateElement("viol3_section");
                    newViol3.InnerText = secViol3RchTxtBx.Text;
                    newInquiryData.AppendChild(newViol3);

                    //Color of Vehicle  
                    XmlElement newCarColor = doc.CreateElement("car_color");
                    newCarColor.InnerText = colorRchTxtBx.Text;
                    newInquiryData.AppendChild(newCarColor);

                    //Date Courtesy mailed updated in the print process.   
                    XmlElement newDateCourtesyMailed = doc.CreateElement("cletter_mailed");
                    newDateCourtesyMailed.InnerText = null;
                    newInquiryData.AppendChild(newDateCourtesyMailed);

                    //Location violation occurred.   
                    XmlElement newLocation = doc.CreateElement("location");
                    newLocation.InnerText = locationRchTxtBx.Text;
                    newInquiryData.AppendChild(newLocation);


                    ////Initial Penalty Due   
                    //XmlElement newInitialPenaltyDue = doc.CreateElement("penalty_due");
                    //newInitialPenaltyDue.InnerText = penaltyDueRchTxtBx.Text;
                    //newInquiryData.AppendChild(newInitialPenaltyDue);


                    XmlElement newDueDate = doc.CreateElement("due_date");
                    if (dueDateTimePicker.Checked)
                    {

                        String fduedate = dueDateTimePicker.Text.ToString().Trim();
                        newDueDate.InnerText = fduedate;
                        newInquiryData.AppendChild(newDueDate);
                    }

                    if (dueDateTimePicker.Checked.Equals(false))
                    {
                        //Since no asofdate was selected get today
                        DateTime date = DateTime.Now;
                        //create a string to hold the fomatted date
                        string fdate = date.ToString("MM/dd/yyyy");

                        newDueDate.InnerText = fdate;
                        newInquiryData.AppendChild(newDueDate);
                    }

                    doc.DocumentElement.AppendChild(newInquiryData);

                    XmlTextWriter tr = new XmlTextWriter(configure.vrdb, null);
                    tr.Formatting = Formatting.Indented;
                    doc.WriteContentTo(tr);
                    tr.Close();

                    ClearForm.clearForm(this);


                    docket = DateTime.Now.ToString();

                    docket = docket.Replace("/", "");

                    docket = docket.Replace(":", "");

                    docket = docket.Replace(" ", "");


                    docketNumberTxtBx.Text = docket;

                    platesWithOwnerchkbx.Checked = false;




                }
            }
            catch (Exception Ex)
            {
               var nothing = Ex; 
            }


        }

        private void asofDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            

        }

        private void vrinquiry_Load(object sender, EventArgs e)
        {


            currentRequestorCodelbl.Text = configure.currentRequestorCode;
             
            docket = DateTime.Now.ToString();
            docket = docket.Replace("/", "");
            docket = docket.Replace(":", "");
            docket = docket.Replace(" ", "");
            docketNumberTxtBx.Text = docket;
         
            
           


         }

        


        private void vehicleLicenseNumbertxtbx_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked)
            {
                messageLbl.Text = "If an as of date is selected, then model year will not be used";
            }
        }

        private void vehicleLicenseNumbertxtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Try to cover all special chars, unfortunatly enum cannot
            if (e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#')
                e.Handled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void vehicleLicenseNumbertxtbx_TextChanged_1(object sender, EventArgs e)
        {
             
        }

        private void vehicleLicenseNumbertxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void penaltyDueRchTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void modelYearTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

         
    }
}