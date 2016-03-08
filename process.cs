using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Threading;



namespace File_Generation_System
{
     
    public class process
    {
        public StreamReader sr;
        public DataSet ds;
        public string nextLine;
        public string cardNumber;
        public string licenseNumber;
        public string requestorCode;
        public string citationNumber;
        public string regExpireDate;
        public string yearModel;
        public string make;
        public string roCity;
        public string roZip;
        public string roCountyCode;
        public string paperIssueDate;
        public string oldPlate;
        public int match;
        public XmlDataDocument doc;
        public int rejectCount = 0;
        public int collectedCount = 0;
        public int removedByDMVCount = 0;
        public int addedCount = 0;
        public string fileCode;
        public string roName;
        public string violationDate;
        public string  penaltyAmount;
        public string typeActionCode;
        public string rejectCode;
        public string dispCode;
        public int totalRejectAmount;
        public string datePaid;
        public string removedDate;
        public string removalCode;
        public String docketNumber;
        public String vin;
        public String roNameAndAddressSourceIndicator;
        public String roNameOrAddress;
        public String additionalRoNameOrAddress;
        public String recordConditionCode;
        public String additionalRoAddress;
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
        public int updatedrecords = 0;
        public String recordConditionCodeDate;
        public string oldplate;
        public string roState = "Ca";
        public int totalRecords = 0;
        public int totalNoAddressRecords = 0;
        public string recordCode;
        public int totalCollectedAmount = 0;
        public string collectedAmount;
        public System.Data.DataTable unmatchedDataTable;
        public DataRow rwn;
        public DataSet newData;
        public string asOfDate;
        public string requestorCodeFromConfig;
        public bool existingCite = false;
        public string checkReq;
        public static List<string> processRpt = new List<string>();
        public static string process_file;
        public bool runningConversionFile = false;
        public static Thread vrInquiryProcessingThread;
        public static Thread newThread;
        public string oldcite;
        

        public void spawnVr_Monthly()
        {
            newThread = new Thread(new ThreadStart(process_Vr_Monthly_File));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();     
             


        }
        

        public void process_Vr_Monthly_File()
        {

          

             

            OpenFileDialog vrmpf = new OpenFileDialog();

            vrmpf.Title = "Select VR Monthly Parking file to process";

            vrmpf.Filter = "VR Monthly Parking File (VRMTHY_OUT*.TXT|*.TXT";

            if (vrmpf.ShowDialog() == DialogResult.OK)
            {

                process_file = vrmpf.FileName;

                FileStream fileStream = new FileStream(process_file, FileMode.Open, FileAccess.Read);

                sr = new StreamReader(fileStream);

                //Create Dataset to hold vrdatabase.xml

                 ds = new DataSet();

                //Read XML document into dataset

                ds.ReadXml(configure.vrdb);

                

                //Create dataset for config.xml read and then 

                DataSet cf = new DataSet();

                cf.ReadXml(configure.cfdb);

                foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
                {

                    requestorCodeFromConfig = configure.currentRequestorCode;
                     
                }

                //Using the indexOf we look for parkviod, if it is not -1 then we know we are
                //processing a list of outstanding cites.

                //Obviously outstanding citation process run after some cites have been paid does not contain
                // cites paid or removed and as for adds we really don't do anything with them (that ok?) 

                

                //merge the two tables before newdata aquires any data then we will avoid any issues with newdata contain
                //ing any existing and new cites

                //this.ds.Merge(newData);

                newData = new DataSet();


                newData = ds.Clone();

                int pcf = process_file.IndexOf("parkviod.txt");

                if (pcf != -1)
                {

                    


                    //Create user_information aka citation as primary key to prevent dups due to logic limit

                    DataColumn[] pk = new DataColumn[1];

                    pk[0] = newData.Tables["inquiry_data"].Columns["user_information"];

                    newData.Tables["inquiry_data"].PrimaryKey = pk;
                    runningConversionFile = true;
                   

                     
                }
                

                
                while ((nextLine = sr.ReadLine()) != null)
                {

                    
                     

                    if (runningConversionFile)
                    {
                        checkReq = nextLine.Substring(0,5);
                        cardNumber = "5";
                    } else{
                        checkReq = nextLine.Substring(1, 5);
                        cardNumber = nextLine.Substring(0, 1);
                    }

                     


                    if (checkReq == requestorCodeFromConfig)
                    {
                        

                         

                        if (cardNumber == "1")
                        {

                            requestorCode = nextLine.Substring(1, 5);
                            citationNumber = nextLine.Substring(6, 15).Trim();
                            fileCode = nextLine.Substring(21, 1);
                            licenseNumber = nextLine.Substring(22, 7);
                            roName = nextLine.Substring(29, 30);
                            make = nextLine.Substring(114, 12);
                            violationDate = nextLine.Substring(126, 6);
                            penaltyAmount = nextLine.Substring(132, 3);
                            typeActionCode = nextLine.Substring(135, 1);
                            //Maps to rejection_code on vrdatabase
                            rejectCode = nextLine.Substring(136, 1);
                            dispCode = nextLine.Substring(137, 1);


                            rejectCount = rejectCount + 1;

                            totalRejectAmount = totalRejectAmount + Int32.Parse(penaltyAmount);

                            //pass citation number and the number 1, not the license

                            existingCite = false;
                            updateXmlDatabase(nextLine.Substring(6, 15), "1");
                            //So we ran thru DB and we did not match so update newdataset
                            if (existingCite == false)
                            {
                                updateNewDataSet("1");
                            }


                        }
                        if (cardNumber == "2")
                        {
                            citationNumber = nextLine.Substring(6, 15).Trim();
                            //date paid maps to database element date_paid
                            datePaid = nextLine.Substring(122, 6);
                            licenseNumber = nextLine.Substring(21, 7);
                            asOfDate = nextLine.Substring(113, 6);
                            roName = nextLine.Substring(28, 5);
                            collectedAmount = nextLine.Substring(119, 3);
                            penaltyAmount = collectedAmount;
                            collectedCount = collectedCount + 1;
                            totalCollectedAmount = totalCollectedAmount + Int32.Parse(collectedAmount);
                            existingCite = false;
                            updateXmlDatabase(nextLine.Substring(6, 15), "2");
                            //So we ran thru DB and we did not match so update newdataset
                            if (existingCite == false)
                            {
                                updateNewDataSet("2");
                            }
                        }

                        if (cardNumber == "3")
                        {
                            citationNumber = nextLine.Substring(6, 15).Trim();
                            licenseNumber = nextLine.Substring(21, 7);
                            roName = nextLine.Substring(28, 16);
                            roNameOrAddress = nextLine.Substring(58, 22);
                            roCity = nextLine.Substring(88, 13);
                            make = nextLine.Substring(113, 12);
                            asOfDate = nextLine.Substring(125, 6);
                            penaltyAmount = nextLine.Substring(129, 3);
                            removedDate = nextLine.Substring(134, 6);
                            //maps to removal_date in vrdatabase
                            removalCode = nextLine.Substring(140, 1);
                            removedByDMVCount = removedByDMVCount + 1;
                            existingCite = false;
                            updateXmlDatabase(nextLine.Substring(6, 15), "3");
                            //So we ran thru DB and we did not match so update newdataset
                            if (existingCite == false)
                            {
                                updateNewDataSet("3");
                            }

                        }

                        if (cardNumber == "4")
                        {
                            citationNumber = nextLine.Substring(6, 15).Trim();
                            licenseNumber = nextLine.Substring(21, 7);
                            roName = nextLine.Substring(28, 16);
                            roNameOrAddress = nextLine.Substring(58, 22);
                            make = nextLine.Substring(113, 12);
                            asOfDate = nextLine.Substring(125, 6);
                            penaltyAmount = nextLine.Substring(131, 3);
                            addedCount = addedCount + 1;
                            recordCode = nextLine.Substring(140, 1);
                            existingCite = false;
                            updateXmlDatabase(nextLine.Substring(6, 15), "4");

                            //So we ran thru DB and we did not match so update newdataset
                            if (existingCite == false)
                            {
                                updateNewDataSet("4");
                            }
                        }

                        

                        if (runningConversionFile)
                        {
                            citationNumber = nextLine.Substring(5, 15).Trim();
                            licenseNumber = nextLine.Substring(20, 7);
                            roName = nextLine.Substring(27, 5);
                            make = nextLine.Substring(32, 4);

                            asOfDate = nextLine.Substring(39, 2) + nextLine.Substring(41, 2) + nextLine.Substring(37, 2);
                            penaltyAmount = nextLine.Substring(43, 3);
                            typeActionCode = "A";
                            existingCite = false;
                            //updateXmlDatabase(nextLine.Substring(5, 15), "5");
                            loadDataBase();
                        }


                        if (oldPlate != licenseNumber)
                        {

                            //writeOutXml();
                        }

                        //when the old plate != new plate write out XML

                        //oldPlate = licenseSwitch(licenseNumber);
                    }//ends if config req == file req 
                    else
                    {
                        MessageBox.Show("Config Req not equal to file requestor", "Check Configuration");
                        MessageBox.Show("Configuration Requestor Code=" + requestorCodeFromConfig.ToString());
                        MessageBox.Show("Requestor Code from File = " + checkReq);
                        return;
                    }


                } //ends while

                //Commented the merge down here because existing table ds and new table newdata contained
                //the same cit numbers as the loop for updating existing and new is loose enought to allow an update 
                //to ds on an existing cite and then add it to newdata on a no match, so instead will copy ds first to
                //newdata with a merge and then hope for the best. 
                //03/24/10 i commented all of the else logic that put citiations that are not in FGS
                //in as i just cannot tighten up the logic yet.I left that code in for the card 5 that is the
                //made up code that allows for the loading of all outstanding citations. 

                //6-17-10 commented out the if surrounding this. 
                //if (pcf != -1)
               // {
                    this.ds.Merge(newData);
                //This code is to replace the report viewer with a xls file with data from the database
                //customers use it to import data to external systems. 

                    StreamWriter wr = new StreamWriter(configure.filepath + "mthly.xls");
                    
                    var whatisit = "";
                      try
                    {
                        for (int i = 0; i < ds.Tables["inquiry_data"].Columns.Count; i++)
                        {
                            wr.Write(ds.Tables["inquiry_data"].Columns[i].ToString().ToUpper() + "\t");
                        }
                        wr.WriteLine();
                        for (int i = 0; i < (ds.Tables["inquiry_data"].Rows.Count); i++)
                        {
                            
                            for (int j = 0; j < 50 ; j++)
                                if (ds.Tables["inquiry_data"].Rows[i][j] != null)
                                {
                                    whatisit = Convert.ToString(ds.Tables["inquiry_data"].Rows[i][j]);
                                    wr.Write(Convert.ToString(ds.Tables["inquiry_data"].Rows[i][j]) + "\t");

                                }
                                    wr.WriteLine();
                                    //wr.Write("\t");
                                 
                        }
                        //wr.WriteLine();

                        wr.Close();
                    }
                     catch (Exception ex)
                     {
                         MessageBox.Show("Error  " + ex.ToString());
                        throw ex;
                     }
               //  }
                this.ds.AcceptChanges();
                this.ds.WriteXml(configure.vrdb);
                

                
            }// ends if


            MessageBox.Show("Total rejected=" + rejectCount,"Citations Rejected");
            MessageBox.Show("Total amount rejected=" + "$" + totalRejectAmount.ToString(),"Dollar Amount Rejected");
            MessageBox.Show("Total added=" + addedCount,"Total Added to DMV");
            MessageBox.Show("Total count of records collected=" + collectedCount,"Total records collected");
            MessageBox.Show("Monetary Amount collected " + "$ " + totalCollectedAmount, "Dollar Amount Collected");
            MessageBox.Show("Total removed=" + removedByDMVCount,"Removed Count");

            sr.Close();





            MessageBox.Show("An Excel Spreadsheet mthly.xls has been created ", "Excel File Available");
            
           
             

             
        } //ends process monthly 
         
   

        //private string licenseSwitch(string newplate)
        //{

        //    oldPlate = newplate;
             
        //    return oldPlate;
        //}

        

        private void updateXmlDatabase(String citationNumber,String cardNumber)
        {

            citationNumber = citationNumber.Trim();

            foreach (DataRow rw in ds.Tables["inquiry_data"].Rows)
            {
   
                match = string.Compare(rw["user_information"].ToString(),citationNumber.Trim());


                if (match == 0 && cardNumber == "1")
                {

                    rw.BeginEdit();
                    rw["rejection_code"] = rejectCode.Trim();
                    string rc = lookupRejectionMessage(rejectCode);
                    rw["rejection_message"] = rc;
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    existingCite = true;

                }   
               

                
                 

                if (match == 0 && cardNumber == "2")
                {
         
                    rw.BeginEdit();
                    rw["date_paid"] = datePaid.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                     
                    existingCite = true;
                    

                }

                
                                    
                              

                if (match == 0 && cardNumber == "3") 
                    
                {
                        
                        rw.BeginEdit();
                        string rcm = lookupRemovalCode(removalCode);
                        rw["removal_code"] = removalCode.Trim();
                        rw["removal_message"] = rcm.Trim();
                        rw["removed_date"] = removedDate.Trim();
                        rw.EndEdit(); 
                        ds.AcceptChanges();
                        ds.WriteXml(configure.vrdb);
                        existingCite = true;
                        
                        
                   
                }

                

                if (match == 0 && cardNumber == "4")
                {
                    
                    rw.BeginEdit();
                    rw["record_code"] = recordCode.Trim();
                    rw["penalty_amount"] = penaltyAmount.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    existingCite = true;
                   

                }
               

                   
                 
 
                 
            }//ends foreach 
            

            }//ends updatexml

        //This method will just load up the newdata table then the merge will happen with the blank
        // Vrdatabase 

        private void loadDataBase()
        {
            try
            {




                this.rwn = this.newData.Tables["inquiry_data"].NewRow();
                 
                this.rwn.BeginEdit();
                this.rwn["user_information"] = citationNumber;
                this.rwn["license_number"] = licenseNumber;
                this.rwn["ro_name"] = roName;
                this.rwn["make"] = make;
                this.rwn["as_of_date"] = asOfDate;
                this.rwn["penalty_amount"] = penaltyAmount;
                this.rwn.EndEdit();
                this.newData.Tables["inquiry_data"].Rows.Add(rwn);
                this.newData.AcceptChanges();

            }
            catch (ConstraintException ex)
            {
                MessageBox.Show("Error" + ex);
            }
        
        }

        private void updateNewDataSet(String cardNumber)
        {
            try
            {




                if (cardNumber == "1")
                {

                    try
                    {
                        this.rwn = this.newData.Tables["inquiry_data"].NewRow();

                        this.rwn.BeginEdit();
                        this.rwn["user_information"] = citationNumber;
                        this.rwn["file_code"] = fileCode;
                        this.rwn["license_number"] = licenseNumber;
                        this.rwn["ro_name"] = roName;
                        this.rwn["make"] = make;
                        this.rwn["as_of_date"] = asOfDate;
                        this.rwn["type_action_code"] = typeActionCode;
                        this.rwn["penalty_amount"] = penaltyAmount;
                        this.rwn["rejection_code"] = rejectCode.Trim();
                        string rc = lookupRejectionMessage(rejectCode);
                        this.rwn["rejection_message"] = rc;
                        this.rwn.EndEdit();

                        this.newData.Tables["inquiry_data"].Rows.Add(rwn);
                        this.newData.AcceptChanges();




                    }
                    catch (ConstraintException ex)
                    {

                    }


                }


                if (cardNumber == "2")
                {

                    try
                    {
                        this.rwn = this.newData.Tables["inquiry_data"].NewRow();

                        this.rwn.BeginEdit();
                        this.rwn["user_information"] = citationNumber;

                        this.rwn["license_number"] = licenseNumber;
                        this.rwn["date_paid"] = datePaid;
                        this.rwn["as_of_date"] = asOfDate;
                        this.rwn["ro_name"] = roName;
                        this.rwn["type_action_code"] = typeActionCode;
                        this.rwn["penalty_amount"] = penaltyAmount;
                        this.rwn.EndEdit();

                        this.newData.Tables["inquiry_data"].Rows.Add(rwn);
                        this.newData.AcceptChanges();




                    }
                    catch (ConstraintException ex)
                    {

                    }


                }

                 if (cardNumber == "3" )
                {

                    try
                    {


                        removedDate = nextLine.Substring(134, 6);
                        //maps to removal_date in vrdatabase
                        removalCode = nextLine.Substring(140, 1);
                        this.rwn = this.newData.Tables["inquiry_data"].NewRow();

                        this.rwn.BeginEdit();
                        this.rwn["user_information"] = citationNumber;
                        this.rwn["license_number"] = licenseNumber;
                        this.rwn["ro_name"] = roName;
                        this.rwn["ro_name_or_address"] = roNameOrAddress;
                        this.rwn["ro_city"] = roCity;
                        this.rwn["make"] = make;
                        this.rwn["as_of_date"] = asOfDate;
                        this.rwn["penalty_amount"] = penaltyAmount;

                        string rcm = lookupRemovalCode(removalCode);
                        this.rwn["removal_code"] = removalCode.Trim();
                        this.rwn["removal_message"] = rcm.Trim();
                        this.rwn["removed_date"] = removedDate.Trim();


                        this.rwn["penalty_amount"] = penaltyAmount;
                        this.rwn.EndEdit();

                        this.newData.Tables["inquiry_data"].Rows.Add(rwn);
                        this.newData.AcceptChanges();



                    }
                    catch (ConstraintException ex)
                    {

                    }


                }

                else if (cardNumber == "4")
                {

                    try
                    {




                        this.rwn = this.newData.Tables["inquiry_data"].NewRow();

                        this.rwn.BeginEdit();
                        this.rwn["user_information"] = citationNumber;
                        this.rwn["license_number"] = licenseNumber;
                        this.rwn["ro_name"] = roName;
                        this.rwn["ro_name_or_address"] = roNameOrAddress;
                        this.rwn["ro_city"] = roCity;
                        this.rwn["make"] = make;
                        this.rwn["as_of_date"] = asOfDate;
                        this.rwn["penalty_amount"] = penaltyAmount;

                        string rcm = lookupRemovalCode(removalCode);
                        this.rwn["removal_code"] = removalCode.Trim();
                        this.rwn["removal_message"] = rcm.Trim();
                        this.rwn["removed_date"] = removedDate.Trim();


                        this.rwn["penalty_amount"] = penaltyAmount;
                        this.rwn["record_code"] = recordCode.Trim();
                        this.rwn.EndEdit();

                        this.newData.Tables["inquiry_data"].Rows.Add(rwn);
                        this.newData.AcceptChanges();



                    }
                    catch (ConstraintException ex)
                    {

                    }
                }
            }
            catch (ConstraintException ex)
            {

            }
        }

        private string lookupRejectionMessage(string rejectCode )
        {
          
            switch(rejectCode)
            {

                case "1":
                    return "No Citations on File";
                case "2" :
                    return "File Code Invalid or Incompatible With License" ;
                case "3" :
                    return "Error In Type Action Code";
                case "4":
                    return "Unacceptable Citation Configuration";
                case "5":
                    return "Unacceptable Violation Date";
                case "6":
                    return "Unacceptable Penalty Amount";
                case "7":
                    return "Pending Condition On Vehicle - Inquire again";
                case "8":
                    return "Incomplete Release of Liability on File";
                case "9":
                    return "DOJ stop on file for Violation Date";
                case "A":
                    return "No Record for License Number Given";
                case "B":
                    return "No Match on Name";
                case "C":
                    return "No Match on Make";
                case "D":
                    return "No Match on Citation";
                case "E":
                    return "No Match on Court Code";
                case "F":
                    return "ELP Reservation of Rentention Record on File";
                case "G":
                    return "License Subplated";
                case "H":
                    return "Can't add Citation - 75 citations already on file";
                case "I":
                    return "Disp 5 or P set for citation";
                case "J":
                    return "Vehicle Junked or Title Surrendered";
                case "K":
                     return "Name Submitted on Citation is Prior R/O";
                case "L":
                    return "Citation # already on File";
                case "M":
                    return "Penalty Amount not present";
                case "N":
                    return "Invalid parking court code";
                case "O":
                    return "VEH subplated after viol date - R/O Info Change";
                case "P":
                    return "Disposition Date not Present";
                case "Q":
                    return "Last Trans Date is prior to input date";
                case "R":
                    return "Obsolete";
                case "S":
                    return "No history for license number on violation date";
                case "T":
                    return "Obsolete";
                case "X":
                    return "ELP surrendered/invalid/lost/stolen";
                case "Y":
                    return "Error in Dispostion Code";
                case "Z":
                    return "Obsolete";


            }

            return "no error message mate"; 
        }

        private string lookupRemovalCode(string removalCode)
        {

            switch (removalCode)
            {

                case "T":
                    return "Transfers or uncorrectable";
                case "R":
                    return "Nonrenewal for 2 years,junked or surrendered title";
                case "C":
                    return "Parking agency receipts presented by applicant";
                case "D":
                    return "Deletion notices by parking agency";
                
            }

            return "";
        }



        public void spawnVr_Inquiry()
        {
            //vrInquiryProcessingThread = new Thread(new ThreadStart(process_Vr_Inquiry_File));
             
            //vrInquiryProcessingThread.Start(); 

            newThread = new Thread(new ThreadStart(process_Vr_Inquiry_File));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();     

           // ThreadStart mythread = new ThreadStart(process_Vr_Inquiry_File);
           // Thread mythreadStart = new Thread(mythread);
             
                      
            
        }
        

        
        
          public void process_Vr_Inquiry_File()
        {

            OpenFileDialog vif = new OpenFileDialog();
            vif.Title = "Select VR Inquiry file to process";
            vif.Filter = "vr inquiry files (vrinq_out*.txt)|*.txt";

            if (vif.ShowDialog() == DialogResult.OK)
            {

              
                process_file = vif.FileName;
                
                //Create Dataset
                ds = new DataSet();

                //Read XML document into dataset
                //Above file dialog seems to switch to the last drive not good
                ds.ReadXml(configure.vrdb);
               
                //This allows for editing

                ds.EnforceConstraints = false;

                try
                {

                    //Create Stream reader  

                    FileStream fileStream = new FileStream(process_file, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fileStream);

                     
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured opening file" + ex);
                    
                }

                while ((nextLine = sr.ReadLine()) != null)
                {

                    cardNumber = nextLine.Substring(nextLine.Length - 1, 1);
                    licenseNumber = nextLine.Substring(0, 7);

                    //Code to prevent opening of wrong source file

                    if (cardNumber != "1"  & totalRecords == 0)
                    {
                        MessageBox.Show("VR Inquiry Source File Incorrect", "Please Select the right file");
                        break;
                        
                    }

                    if (cardNumber == "1")
                    {

                        // We count the card 1's because they indicate a complete record
                        licenseNumber = nextLine.Substring(0, 7);
                        totalRecords = totalRecords + 1;
                         
                        docketNumber = nextLine.Substring(62, 15);
                        updateXmlDatabase(nextLine.Substring(0, 7), "1", "vrinquiry",docketNumber);
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

                        // Out of states card 2 holds the state and card 4 holds the city

                        if(roCountyCode == "60")
                        {
                            roCity = "";
                            roState = nextLine.Substring(55, 13);
                        }

                        //updateXmlDatabase(vin, "2", 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "2", "vrinquiry",docketNumber);


                    }
                    if (cardNumber == "3")
                    {

                        paperIssueDate = nextLine.Substring(8, 6);
                        roNameAndAddressSourceIndicator = nextLine.Substring(14, 1);
                        roName = nextLine.Substring(15, 30);
                        roNameOrAddress = nextLine.Substring(46, 30);

                        //updateXmlDatabase(vin, "3", 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "3", "vrinquiry",docketNumber);
                    }
                    if (cardNumber == "4")
                    {

                        additionalRoNameOrAddress = nextLine.Substring(8, 30);
                        additionalRoAddress = nextLine.Substring(31, 30);
                        if (roCountyCode == "60")
                        {
                          
                            roCity = nextLine.Substring(8, 20);
                            
                        }

                        updateXmlDatabase(nextLine.Substring(0, 7), "4", "vrinquiry",docketNumber);
                    }
                    if (cardNumber == "5")
                    {

                        recordConditionCode = nextLine.Substring(8, 2);
                        recordConditionCodeDate = nextLine.Substring(11, 6);
                        recordRemarks = nextLine.Substring(18, 30);
                        updateXmlDatabase(nextLine.Substring(0, 7), "5", "vrinquiry",docketNumber);

                    }

                    if (cardNumber == "6")
                    {

                        totalNoAddressRecords =  totalNoAddressRecords + 1;

                        errorCode = nextLine.Substring(8, 2);
                        errorMessage = nextLine.Substring(13, 45);
                        errorDate = nextLine.Substring(59, 6);
                        updateXmlDatabase(nextLine.Substring(0, 7), "6", "vrinquiry",docketNumber);

                    }
                    if (cardNumber == "7")
                    {

                        nrlDate = nextLine.Substring(7, 6);
                        nrlTransferDate = nextLine.Substring(14, 15);
                        nrlNameOrMessage = nextLine.Substring(30, 30);
                        nrlRecordId = nextLine.Substring(65, 1);
                        updateXmlDatabase(nextLine.Substring(0, 7), "7","vrinquiry",docketNumber);

                    }
                    if (cardNumber == "8")
                    {

                        nrlAddressMessage = nextLine.Substring(8, 30);
                        nrlCityStateMessage = nextLine.Substring(39, 25);
                        updateXmlDatabase(nextLine.Substring(0, 7), "8","vrinquiry",docketNumber);

                    }


                    //when the plate number switches, then get the new citenumber
                    //if (oldplate != licenseNumber)
                    //{

                         
                    //    oldcite = docketNumber;



                    //}

                    //when the old plate != new plate write out XML

                    //oldplate = licenseSwitch(licenseNumber);


                } //ends while

                MessageBox.Show("Total Records Read=" + totalRecords,"VR Inquiry Records");
                MessageBox.Show("Total Records in error=" + totalNoAddressRecords,"Records in Error");
                MessageBox.Show("Records Updated to the Database=" + updatedrecords,"Records Updated");
                 
                Form.ActiveForm.ControlBox = true;
                
                
            }
        }

         
        //This updateXmlDatabase updates for Vr Inquiry data only

        private void updateXmlDatabase(String LicenseNumber, String cardNumber,string vrinq,string cite)
        {
             
            foreach (DataRow rw in ds.Tables["inquiry_data"].Rows)
            {


                
                //change match to license with match to citation. 

                //match = string.Compare(rw[0].ToString(), LicenseNumber.Trim());

                match = string.Compare(rw[10].ToString(), cite.Trim());

                if (match == 0 && cardNumber == "1")
                {

                    rw.BeginEdit();
                    rw["license_number"] = licenseNumber.Trim().PadRight(7);
                     
                    rw.EndEdit();
                    ds.AcceptChanges();
                   
                    ds.WriteXml(configure.vrdb);

                    updatedrecords = updatedrecords + 1;
                    break;

                }

                if (match == 0 && cardNumber == "2")
                {

                    rw.BeginEdit();
                    rw["reg_expire_date"] = regExpireDate.Trim();
                    rw["make"] = make.Trim();
                    rw["vin"] = vin.Trim();
                    rw["ro_city"] = roCity.Trim();
                    rw["ro_zip"] = roZip.Trim();
                    rw["ro_county_code"] = roCountyCode.Trim();
                    rw["year_model"] = yearModel.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    
                    updatedrecords = updatedrecords + 1;
                    break;

                }



                if (match == 0 && cardNumber == "3")
                {

                    rw.BeginEdit();
                    rw["paper_issue_date"] = paperIssueDate.Trim();
                    rw["ro_nameaddress_source_indicator"] = roNameAndAddressSourceIndicator.Trim();
                    rw["ro_name"] = roName.Trim();
                    rw["ro_name_or_address"] = roNameOrAddress.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;
                      

                }

                if (match == 0 && cardNumber == "4")
                {

                    rw.BeginEdit();
                    if (roCountyCode == "60")
                    {
                        rw["ro_city"] = roCity.Trim();
                    }
                    rw["additional_ro_name_or_address"] = additionalRoNameOrAddress.Trim();
                    rw["additional_ro_address"] = additionalRoAddress.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;
                     

                }

                if (match == 0 && cardNumber == "5")
                {

                    
                    if (rw["record_condition_code"].ToString().Trim().Length == 0)
                    {
                    
                        rw.BeginEdit();
                        rw["record_condition_code"] = recordConditionCode.Trim();
                        rw["record_condition_code_date"] = recordConditionCodeDate.Trim();
                        rw["record_remarks"] = recordRemarks.Trim();
                        rw.EndEdit();
                        ds.AcceptChanges();
                        ds.WriteXml(configure.vrdb);
                        break;
                    }
                    
                   
                     



                }

                if (match == 0 && cardNumber == "6")
                {
                    

                    rw.BeginEdit();
                    rw["error_code"] = errorCode.Trim();
                    rw["error_message"] = errorMessage.Trim();
                    rw["error_date"] = errorDate.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;
                    



                }

                if (match == 0 && cardNumber == "7")
                {

                    rw.BeginEdit();
                    rw["hold_record_written"] = nrlDate.Trim();
                    rw["nrl_transfer_date"] = nrlTransferDate.Trim();
                    rw["nrl_name_or_message"] = nrlNameOrMessage.Trim();
                    rw["nrl_record_id"] = nrlRecordId.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;
                    

                }

                if (match == 0 && cardNumber == "8")
                {

                    rw.BeginEdit();
                    rw["nrl_address_message"] = nrlAddressMessage.Trim();
                    rw["nrl_City_State_Message"] = nrlCityStateMessage.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
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
                    rw["license_number"] = licenseNumber.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;


                }

                if (matcha == 0 && cardNumber == "2")
                {


                    rw.BeginEdit();
                    rw["license_number"] = licenseNumber.Trim();
                    rw["reg_expire_date"] = regExpireDate.Trim();
                    rw["make"] = make.Trim();
                    rw["vin"] = vin.Trim();
                    rw["ro_city"] = roCity.Trim();
                    rw["ro_zip"] = roZip.Trim();
                    rw["ro_county_code"] = roCountyCode.Trim();
                    rw["year_model"] = yearModel.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);

                    break;


                }

                if (matcha == 0 && cardNumber == "3")
                {

                    rw.BeginEdit();
                    rw["paper_issue_date"] = paperIssueDate.Trim();
                    rw["ro_nameaddress_source_indicator"] = roNameAndAddressSourceIndicator.Trim();
                    rw["ro_name"] = roName.Trim();
                    rw["ro_name_or_address"] = roNameOrAddress.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;


                }

                if (matcha == 0 && cardNumber == "4")
                {

                    rw.BeginEdit();
                    rw["additional_ro_name_or_address"] = additionalRoNameOrAddress.Trim();
                    rw["additional_ro_address"] = additionalRoAddress.Trim();

                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;


                }

                if (matcha == 0 && cardNumber == "5")
                {

                    if (rw["record_condition_code"].ToString().Trim().Length == 0)
                    {
                         
                        rw.BeginEdit();
                        rw["record_condition_code"] = recordConditionCode.Trim();
                        rw["record_condition_code_date"] = recordConditionCodeDate.Trim();
                        rw["record_remarks"] = recordRemarks.Trim();
                        rw.EndEdit();
                        ds.AcceptChanges();
                        ds.WriteXml(configure.vrdb);
                        break;
                    }



                }

                if (matcha == 0 && cardNumber == "6")
                {

                    rw.BeginEdit();
                    rw["error_code"] = errorCode.Trim();
                    rw["error_message"] = errorMessage.Trim();
                    rw["error_date"] = errorDate.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;


                }

                if (matcha == 0 && cardNumber == "7")
                {

                    rw.BeginEdit();
                    rw["hold_record_written"] = nrlDate.Trim();
                    rw["nrl_transfer_date"] = nrlTransferDate.Trim();
                    rw["nrl_name_or_message"] = nrlNameOrMessage.Trim();
                    rw["nrl_record_id"] = nrlRecordId.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);
                    break;



                }

                if (matcha == 0 && cardNumber == "8")
                {

                    rw.BeginEdit();
                    rw["nrl_address_message"] = nrlAddressMessage.Trim();
                    rw["nrlCityStateMessage"] = nrlCityStateMessage.Trim();
                    rw.EndEdit();
                    ds.AcceptChanges();
                    ds.WriteXml(configure.vrdb);

                    break;



                }

               
            } // Ends the For Each

             
            
        } // Ends Update by VIN
         
            
        

                
            
        } //end class
         
         
               

    } // ends namespace
//}
