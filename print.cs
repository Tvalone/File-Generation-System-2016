using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;

namespace File_Generation_System
{

    public partial class print : Form
    {

        private Font verdana08Font;
        private StreamReader reader;
        private string name;
        private string street;
        private string city;
        private string zip;
        private string email;
        private string webaddress;
        private string phone;
        private string pimage;
        public Image curImage = null;
        public string curFileName = null;
        string filepath;
        string filesSavedIn;
        int letterCount = 0;
        string filename;
        private string fPenaltyAmount;
        private string dateMailed;
        private string rstate;
        bool additionalRoNameOrAddressHasNumbers = false;
        
        
        

        public print()
        {
            InitializeComponent();

            //Create Dataset for config file

            DataSet cf = new DataSet();
            cf.ReadXml(configure.cfdb);
            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {
                 
                filepath = cfr["drive_letter_image_saved_in"].ToString();
                filesSavedIn = cfr["drive_files_saved_in"].ToString();
            }
             

            dateMailed = DateTime.Today.Date.ToString();
            dateMailed = dateMailed.ToString().Substring(0, 10); 
        }

        private void print_Load(object sender, EventArgs e)
        {
            // See if any printers are installed
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Printer not found!");
                return;
            }
            // Get all available printers and add them to the
            // combo box
            foreach (String printer in
              PrinterSettings.InstalledPrinters)
            {
                printersList.Items.Add(printer.ToString());
            }

        }

          

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object
            OpenFileDialog fdlg = new OpenFileDialog();
            // Set its properties
            fdlg.Title = "Parking file letters";
            fdlg.InitialDirectory = filesSavedIn;
            //fdlg.InitialDirectory = @"c:\Documents and Settings\mvtwv\My Documents\Visual Studio 2005\Projects\File Generation System\File Generation System\Bin\Debug";
            fdlg.Filter =
            "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            // Show dialog and set the selected file name
            // as the text of the text box
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)

            

        {

    

            // Get the file name

            filename = textBox1.Text.ToString();

            string printername = printersList.Text.ToString();

            // Check if it's not empty
            
            if (filename.Equals(string.Empty))
            {
                MessageBox.Show("Enter a valid file name","Select File Name");
                textBox1.Focus();
                return;
            }

            if (!File.Exists(filename))
            {
                MessageBox.Show("Enter a file that exists","Enter Valid File");
                textBox1.Focus();
                return;
            }

             

            // Check if it's not empty
            if (printername.Equals(string.Empty))
            {
                MessageBox.Show("Select a printer");
                printersList.Focus();
                return;
            }
            // Create a StreamReader object
            reader = new StreamReader(filename);
            // Create a Verdana font with size 10
            verdana08Font = new Font("Verdana", 08);

            // Create a PrintDocument object
            PrintDocument pd = new PrintDocument();

            pd.PrinterSettings.PrinterName = printersList.SelectedItem.ToString();
                   

            // Add PrintPage event handler
            pd.PrintPage += new PrintPageEventHandler
              (this.PrintTextFileHandler);
            // Call Print method
            pd.Print();
            // Close the reader
            if (reader != null)
                reader.Close();
        }

        private void PrintTextFileHandler(object sender,PrintPageEventArgs ppeArgs)
            
        {


            
            curFileName = filepath;

            if (curFileName.Length <  5 )
            {
                MessageBox.Show("Select logo for letters","Update Configuration File");
            }
            else
            {

            curImage = Image.FromFile(curFileName);
            //// Get the Graphics object
             Graphics g = ppeArgs.Graphics;
                 

             
             g.DrawImage(curImage, 650, 0, curImage.Width, curImage.Height);
             
            

            //trFont.Dispose();
            

            
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
           
            // Read margins from PrintPageEventArgs
            float leftMargin = ppeArgs.MarginBounds.Left;
            leftMargin = leftMargin - 10;
            float topMargin = ppeArgs.MarginBounds.Top;
            string line = null;
            // Calculate the lines per page on the basis of
            // the height of the page and the height of
            // the font
            linesPerPage = ppeArgs.MarginBounds.Height /
              verdana08Font.GetHeight(g);
            // Now read lines one by one, using StreamReader
            while (count < linesPerPage &&
              ((line = reader.ReadLine()) != null))
            {
                
                // Calculate the starting position
                yPos = topMargin + (count *
                  verdana08Font.GetHeight(g));
                // Draw text
                g.DrawString(line, verdana08Font, Brushes.Black,
                  leftMargin, yPos, new StringFormat());
                // Move to next line
                count++;
            }
            // If PrintPageEventArgs has more pages
            // to print
            if (line != null)
                ppeArgs.HasMorePages = true;
            else
                ppeArgs.HasMorePages = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //This process xml file into dataset if ro_name.length       
            //is greater than zero 

            //Create Dataset
            DataSet ds = new DataSet();
            DataSet fc = new DataSet();
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
           
            string printfile = System.IO.Path.Combine(filesSavedIn, "parkingletter.txt");
            FileStream fs1 = new FileStream(printfile, FileMode.Create); 
            StreamWriter sa = new StreamWriter(fs1);

            letterCount = 0;
            //Read XML document into dataset
            ds.ReadXml(configure.vrdb);

            //Read XML document into dataset
            fc.ReadXml(configure.cfdb);

            foreach (DataRow fr in fc.Tables["fgs_config"].Rows)
            {
                 name       =    fr["name"].ToString().Trim();
                 street     =    fr["street_address"].ToString().Trim();
                 city       =    fr["city"].ToString().Trim();
                 zip        =    fr["zip"].ToString().Trim();
                 rstate     =    fr["state"].ToString().Trim(); 
                 pimage     =    fr["drive_letter_image_saved_in"].ToString().Trim();
                 email      =    fr["email"].ToString().Trim();
                 webaddress =    fr["web_address"].ToString().Trim();
                 phone      =    fr["phone"].ToString().Trim();
                   
            }

            

            //If there is a registered owner name then write out a letter

            foreach (DataRow ra in ds.Tables["inquiry_data"].Rows)
            {
                if (ra["ro_name"].ToString().Length > 0 & ra["date_paid"].ToString().Length == 0 & ra["rejection_code"].ToString().Length == 0 & ra["ro_name_or_address"].ToString().Length > 0 & ra["removal_code"].ToString().Length == 0 & ra["cletter_mailed"].ToString().Trim().Length == 0)  
                {

                    letterCount = letterCount + 1;

                    
                    //Write name and address 
 
                    //Trying to move address down so when you fold letter it shows up in
                    // window. Need to minus 3 from loop down below
                    sa.WriteLine("COURTESY NOTICE PARKING/VEHICLE REGISTRATION VIOLATION ");
                    sa.WriteLine("______________________________________________________ ");
                    sa.WriteLine("DOCKET NO: " + ra["docket_number"].ToString().Trim());
                    sa.WriteLine("Due Date " + ra["due_date"].ToString());
                    sa.WriteLine(" ");

                    string checkOR = "OR";
                    string checkBackSlash = "/";
                    string leadZero = "0";
                    //string checknum = "0123456789";

                     
                    //if ro_name_or_address contains an address not a name then IndexOf returns -1
                    // this Assumes that ro_name contains the name and ro_address containts the address

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) != 0 & ra["ro_name"].ToString().IndexOf(checkBackSlash) == -1)
                    {

                        sa.WriteLine(ra["ro_name"].ToString().Trim());
                        sa.WriteLine(ra["ro_name_or_address"].ToString().Trim());
                    }

                    //if ro_name_or_address contains another name then IndexOf will not be equal to -1 so it containts another name
                    //So  additional_ro_name_or_address must contain the address if it does not contain an or

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) == 0 & ra["additional_ro_name_or_address"].ToString().IndexOf(checkOR) != 0)
                    {

                        additionalRoNameOrAddressHasNumbers = false;
                         
                         //12/15/09 mod  assumes that numbers found in addronameoraddress means its an an address                      
                        sa.WriteLine(ra["ro_name"].ToString().Trim() + " " + ra["ro_name_or_address"].ToString());

                        for (int i = 0; i < ra["additional_ro_name_or_address"].ToString().Length; i++)
                        {
                            string test = ra["additional_ro_name_or_address"].ToString().Substring(i, 1);
                            char testc = Convert.ToChar(test);

                            if (char.IsDigit(testc) == true)
                            {
                                additionalRoNameOrAddressHasNumbers = true;
                            }
                        }

                        if (additionalRoNameOrAddressHasNumbers == true)
                        {

                            sa.WriteLine(ra["additional_ro_name_or_address"].ToString().Trim());
                        }
                        else
                        {
                            sa.WriteLine(ra["additional_ro_address"].ToString().Trim());
                        }
                    }

                    //If ro_name_or_address contains a name and addtional_ro_name_or_address contains a name then concat them and write out
                    // additional_ro_address as address

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) == 0 & ra["additional_ro_name_or_address"].ToString().IndexOf(checkOR) == 0 ) 
                    {
                         sa.WriteLine(ra["ro_name"].ToString().Trim() + " " + ra["ro_name_or_address"].ToString().Trim() + " " + ra["additional_ro_name_or_address"].ToString().Trim());
                         sa.WriteLine(ra["additional_ro_address"].ToString().Trim()); 
                         
                    }
                     
                    //Create a string with foward slash then convert it to a char array for indexofany
                    // If the last byte on the ro_name contains a / then ro_name_or_address contains another name
                 

                    
                    if (ra["ro_name"].ToString().IndexOf(checkBackSlash)  != -1) 
                    {
                         
                        if (ra["ro_name"].ToString().Trim().Length  - 1 == ra["ro_name"].ToString().IndexOf(checkBackSlash))
                        {
                           string fixname = ra["ro_name"].ToString().Trim().Remove(ra["ro_name"].ToString().Trim().Length - 1);
                          
                           sa.WriteLine(fixname + " " + ra["ro_name_or_address"].ToString().Trim());
                           sa.WriteLine(ra["additional_ro_name_or_address"].ToString().Trim()); 
                        }
                    }

                    if (ra["penalty_amount"].ToString().IndexOf(leadZero) == 1)
                    {
                        fPenaltyAmount = ra["penalty_amount"].ToString().Replace('0', ' ');
                    }
                    else
                    {
                        fPenaltyAmount = ra["penalty_amount"].ToString();
                    }

                        
                    sa.WriteLine(ra["ro_city"].ToString() + " " + " CA " + ra["ro_zip"].ToString());

                    sa.WriteLine();
                    sa.WriteLine();
                    sa.WriteLine();
                    sa.WriteLine();

                    //Write text of letter
                    sa.WriteLine(ra["ro_name"].ToString().Trim() + "," +" On " + ra["as_of_date"].ToString() + " you received a parking ticket");
                    sa.WriteLine("citation number " + ra["user_information"].ToString() + " for your" + " " + ra["year_model"].ToString().Trim() + " " + ra["make"].ToString().Trim() + " " + "license number" + " " + ra["license_number"].ToString().Trim() + ".");
                    //sa.WriteLine("Please remit payment in the amount of " + fPenaltyAmount + ".00" + " to: ");
                    sa.WriteLine("Please remit payment in the amount of " + fPenaltyAmount +  " to: ");
                    sa.WriteLine(" ");
                    sa.WriteLine(name);
                    sa.WriteLine(street);
                    sa.WriteLine(city +  " " + rstate  + " " + zip);
                    sa.WriteLine(" ");
                    sa.WriteLine("Inquiries can be made to email address" + " " + email);
                    sa.WriteLine("or to" + " " + webaddress + ".Phone Inquiries can be made by calling  " + phone + ".");
                    sa.WriteLine("           Detach and return this portion with payment or correspondence           ");
                    sa.WriteLine("---------------------------------------------------------------------------------- ");
                    sa.WriteLine("PLEASE READ CAREFULLY.");
                    sa.WriteLine("                                                                                   ");
                    sa.WriteLine("The records of this office indicate that a parking citation and/or registration citation was issued to");
                    sa.WriteLine("the above-referenced vehicle");
                    sa.WriteLine(" ");
                    sa.WriteLine("BY THE DUE DATE ON THIS NOTICE,YOU MUST PAY THE TOTAL AMOUNT INDICATED or take one of the other ");
                    sa.WriteLine("actions listed below. The penalty may be sent through the mail by detaching and mailing the top");
                    sa.WriteLine("portion of this Notice with your MONEY ORDER or PERSONAL CHECK made payable to the agency listed");
                    sa.WriteLine("above. If previously paid, please disregard this notice.");
                    sa.WriteLine(" ");
                    sa.WriteLine("As the registered owner, you may:");
                    sa.WriteLine(" ");
                    sa.WriteLine("1)  Pay the PENALTY AMOUNT as indicated before the DUE DATE without further late fees or penalty applied.");
                    sa.WriteLine(" ");
                    sa.WriteLine("    REGISTRATION/EQUIPTMENT VIOLATIONS Sections 5200, 5204(a), 38010,38020: Pay the total PENALTY AMOUNT");
                    sa.WriteLine("    as indicated on the citation within 21 days of receipt of the citation; OR provide PROOF OF CORRECTION");
                    sa.WriteLine("    (PC) and pay the fix-it transaction fee of $10 within 21 days of the date of citation. PROOF OF ");
                    sa.WriteLine("    CORRECTION means either having the citation signed off by a law enforcement agency or the DMV and");
                    sa.WriteLine("    submitting the signed original citation to the Parking Agent with your $10 fee; Or by providing a copy");
                    sa.WriteLine("    of your current registration, if your have a registration violation, to the Parking Agent with your $10 fee.");
                    sa.WriteLine("                                 -OR-                                                                         ");
                    sa.WriteLine("2) Contest the citation by requesting an INITIAL review within 21 days from the issuance of the citation.");
                    sa.WriteLine(" ");
                    sa.WriteLine("3)  If the vehicle described above was sold or transferred prior to the date of the violation(s), you must");
                    sa.WriteLine("    fill out the enclosed AFFIDAVIT OF NONLIABILITY and immediately return to the address above - Sec.40208 V.C.");
                    sa.WriteLine("    or 40209 V.C.                                                                                               ");
                    sa.WriteLine(" ");
                    sa.WriteLine("THE PARKING/REGISTRATION PENALTY IS DUE WITHIN 21 DAYS OF ISSUANCE, BUT AN ADDITIONAL 21-DAY GRACE       ");
                    sa.WriteLine("PERIOD IS GRANTED BEFORE THE VIOLATION IS REPORTED TO THE DEPARTMENT OF MOTOR VEHICLES AS A FAILURE");
                    sa.WriteLine("TO PAY ADDITIONAL ADMINISTRATIVE FEES WILL BE APPLIED!                                      ");
                    sa.WriteLine(" ");
                    sa.WriteLine("A $25 LATE FEE WILL BE ADDED 14-DAYS AFTER THE DUE DATE ON THE NOTICE IF YOU HAVE NOT TAKEN ONE OF THE");
                    sa.WriteLine("ABOVE ACTIONS.          ");
                    sa.WriteLine(" ");
                    sa.WriteLine("DATE NOTICE MAILED: " + dateMailed );  
                    // if more lines are added you must decrement 37 
                    // 3/18/09 added three lines above decrement 32 now 
                    for (int counter = 0; counter < 08; counter++)
                    {
                        sa.WriteLine();
                    }

                     ra["cletter_mailed"] = DateTime.Today.Date.ToString();

                }

                
            }
            sa.Close();
            ds.AcceptChanges();
            ds.WriteXml(configure.vrdb);
            MessageBox.Show("Parking Courtesy letters=" + letterCount, "Total Letters Created");
            textBox1.Text =  printfile;
            createParkingLettersBtn.Enabled = false;
             
        }

        private void exitPrintingFunctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reportDocument1_InitReport(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            // Get the file name

            filename = textBox1.Text.ToString();

            string printername = printersList.Text.ToString();

            // Check if it's not empty
            
            if (filename.Equals(string.Empty))
            {
                MessageBox.Show("Enter a valid file name","Select File Name");
                textBox1.Focus();
                return;
            }

            if (!File.Exists(filename))
            {
                MessageBox.Show("Enter a file that exists","Enter Valid File");
                textBox1.Focus();
                return;
            }

             

            // Check if it's not empty
            if (printername.Equals(string.Empty))
            {
                MessageBox.Show("Select a printer");
                printersList.Focus();
                return;
            }
            // Create a StreamReader object
            reader = new StreamReader(filename);
            // Create a Verdana font with size 08
            verdana08Font = new Font("Verdana", 08);

            // Create a PrintDocument object
            PrintDocument pd = new PrintDocument();

            pd.PrinterSettings.PrinterName = printersList.SelectedItem.ToString();
                   

            // Add PrintPage event handler
            pd.PrintPage += new PrintPageEventHandler
              (this.PrintTextFileHandler);
            // Call Print method
            pd.Print();
            // Close the reader
            if (reader != null)
                reader.Close();
        
            //filename = textBox1.Text.ToString();
            //TextPrintDocument tpd = new TextPrintDocument(filename,filepath);
            //tpd.Font = verdana08Font;
          
            //tpd.Print();
        }

        private void createFinalParkingLetterBtn_Click(object sender, EventArgs e)
        {
            //This process xml file into dataset if ro_name.length       
            //is greater than zero 

            //Create Dataset
            DataSet ds = new DataSet();
            DataSet fc = new DataSet();
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
          
            string printfile = System.IO.Path.Combine(filesSavedIn, "parkingfinal.txt");
            FileStream fs = new FileStream(printfile, FileMode.Create);
            StreamWriter sa = new StreamWriter(fs);

            letterCount = 0;

            //Read XML document into dataset
            ds.ReadXml(configure.vrdb);

            //Read XML document into dataset
            fc.ReadXml(configure.cfdb);

            foreach (DataRow fr in fc.Tables["fgs_config"].Rows)
            {
                name = fr["name"].ToString().Trim();
                street = fr["street_address"].ToString().Trim();
                city = fr["city"].ToString().Trim();
                rstate = fr["state"].ToString().Trim(); 
                zip = fr["zip"].ToString().Trim();
                pimage = fr["drive_letter_image_saved_in"].ToString().Trim();
                email = fr["email"].ToString().Trim();
                webaddress = fr["web_address"].ToString().Trim();
                phone = fr["phone"].ToString().Trim();

            }



            //If there is a registered owner name then write out a letter

            foreach (DataRow ra in ds.Tables["inquiry_data"].Rows)
            {
                if (ra["ro_name"].ToString().Length > 0 & ra["date_paid"].ToString().Length == 0 & ra["rejection_code"].ToString().Length == 0 & ra["ro_name_or_address"].ToString().Length > 0 & ra["removal_code"].ToString().Length == 0)
                {

                    letterCount = letterCount + 1;


                    //Write name and address 

                    //Trying to move address down so when you fold letter it shows up in
                    // window. Need to minus 3 from loop down below
                    sa.WriteLine("NOTICE OF DELINQUENT PARKING/REGISTRATION VIOLATION  ");
                    sa.WriteLine("______________________________________________________ ");
                    sa.WriteLine("DOCKET NO: " + ra["docket_number"].ToString().Trim());
                    sa.WriteLine("Due Date " + ra["due_date"].ToString());
                    sa.WriteLine(" ");

                    string checkOR = "OR";
                    string checkBackSlash = "/";
                    string leadZero = "0";

                    //if ro_name_or_address contains an address not a name then IndexOf returns -1
                    // this Assumes that ro_name contains the name and ro_address containts the address

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) != 0 & ra["ro_name"].ToString().IndexOf(checkBackSlash) == -1)
                    {

                        sa.WriteLine(ra["ro_name"].ToString().Trim());
                        sa.WriteLine(ra["ro_name_or_address"].ToString().Trim());
                    }

                    //if ro_name_or_address contains another name then IndexOf will not be equal to -1 so it containts another name
                    //So  additional_ro_name_or_address must contain the address if it does not contain an or

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) == 0 & ra["additional_ro_name_or_address"].ToString().IndexOf(checkOR) != 0)
                    {

                        sa.WriteLine(ra["ro_name"].ToString().Trim() + " " + ra["ro_name_or_address"].ToString());
                        sa.WriteLine(ra["additional_ro_name_or_address"].ToString().Trim());
                    }

                    //If ro_name_or_address contains a name and addtional_ro_name_or_address contains a name then concat them and write out
                    // additional_ro_address as address

                    if (ra["ro_name_or_address"].ToString().IndexOf(checkOR) == 0 & ra["additional_ro_name_or_address"].ToString().IndexOf(checkOR) == 0)
                    {
                        sa.WriteLine(ra["ro_name"].ToString().Trim() + " " + ra["ro_name_or_address"].ToString().Trim() + " " + ra["additional_ro_name_or_address"].ToString().Trim());
                        sa.WriteLine(ra["additional_ro_address"].ToString().Trim());

                    }

                    //Create a string with foward slash then convert it to a char array for indexofany
                    // If the last byte on the ro_name contains a / then ro_name_or_address contains another name



                    if (ra["ro_name"].ToString().IndexOf(checkBackSlash) != -1)
                    {

                        if (ra["ro_name"].ToString().Trim().Length - 1 == ra["ro_name"].ToString().IndexOf(checkBackSlash))
                        {
                            string fixname = ra["ro_name"].ToString().Trim().Remove(ra["ro_name"].ToString().Trim().Length - 1);

                            sa.WriteLine(fixname + " " + ra["ro_name_or_address"].ToString().Trim());
                            sa.WriteLine(ra["additional_ro_name_or_address"].ToString().Trim());
                        }
                    }


                    if (ra["penalty_amount"].ToString().IndexOf(leadZero) == 1)
                    {
                        fPenaltyAmount = ra["penalty_amount"].ToString().Replace('0', ' ');
                    }
                    else
                    {
                        fPenaltyAmount = ra["penalty_amount"].ToString();
                    }

                        

                    sa.WriteLine(ra["ro_city"].ToString() + " " + " CA " + ra["ro_zip"].ToString());

                    sa.WriteLine();
                    sa.WriteLine();
                    sa.WriteLine();
                    sa.WriteLine();

                    //Write text of letter
                    sa.WriteLine(ra["ro_name"].ToString().Trim() + "," + " On " + ra["as_of_date"].ToString() + " you received a parking ticket");
                    sa.WriteLine("citation number " + ra["user_information"].ToString() + " for your" + " " + ra["year_model"].ToString().Trim() + " " + ra["make"].ToString().Trim() + " " + "license number" + " " + ra["license_number"].ToString().Trim() + ".");
                    //sa.WriteLine("Please remit payment in the amount of " + fPenaltyAmount + ".00" + " to: ");
                    sa.WriteLine("Please remit payment in the amount of " + fPenaltyAmount +  " to: ");
                    sa.WriteLine(" ");
                    sa.WriteLine(name);
                    sa.WriteLine(street);
                    sa.WriteLine(city + " " + rstate + " " + zip);
                    sa.WriteLine(" ");
                    sa.WriteLine("Inquiries can be made to email address" + " " + email);
                    sa.WriteLine("or to" + " " + webaddress + ".Phone Inquiries can be made by calling  " + phone + ".");
                    sa.WriteLine("           Detach and return this portion with payment or correspondence           ");
                    sa.WriteLine("---------------------------------------------------------------------------------- ");
                    sa.WriteLine("PLEASE READ CAREFULLY.");
                    sa.WriteLine("The records of this office indicate that a parking citation and/or registration citation(s)");
                    sa.WriteLine("listed above have not been satisfied. Failure to pay the penalties due (by mail or in person)");
                    sa.WriteLine("prior to the DUE DATE indicated above will result in increased penalties AND may result ");
                    sa.WriteLine("in witholding of your vehicle registration by the Deparment of Motor Vehicles.           ");
                    sa.WriteLine(" ");
                    sa.WriteLine("BY THE DUE DATE ON THIS NOTICE, YOU MUST PAY THE TOTAL AMOUNT INDICATED OR TAKE ONE OF THE");
                    sa.WriteLine("OTHER ACTIONS LISTED BELOW. The parking penalty may be sent through the mail by detaching ");
                    sa.WriteLine("and mailing the top portion of this notice with your MONEY ORDER OR PERSONAL CHECK made");
                    sa.WriteLine("payable to the agency listed above. If previously paid, please disregard this notice.");
                    sa.WriteLine(" ");
                    sa.WriteLine("As the registered owner, you may:");
                    sa.WriteLine("1) Registration/Equiptment Violation sections 5200,52049(a),39010,28020: Pay the total ");
                    sa.WriteLine("   PENALTY AMOUNT as indicated on the citation within 14 calendar days after the mailing of the    ");
                    sa.WriteLine("   NOTICE OF DELINQUENT PARKING VIOLATION or provide PROOF OF CORRECTION (PC) and pay the fix-it  ");
                    sa.WriteLine("   transaction fee of $10 within 14 days after the mailing of the NOTICE OF DELINQUENT PARKING ");
                    sa.WriteLine("   VIOLATION. PROOF OF CORRECTION means either having the citation signed off by a law enforcement agency     ");
                    sa.WriteLine("   or the DMV and submitting the signed original citation to the Parking Agent with your $10 fee; or by          ");
                    sa.WriteLine("   Contest the citation by requesting an INITIAL review within 21 days from the issuance of the citation.");
                    sa.WriteLine("   provideing a copy of your current registration , if you have a registraton violation, to the Parking");
                    sa.WriteLine("   Agent, with your $10 fee.                         ");
                    sa.WriteLine(" ");
                    sa.WriteLine("2) All other Violations: Pay the PENALRY AMOUNT as indicated above within 14 days after mailing of the ");
                    sa.WriteLine("   NOTICE OF DELINQUENT PARKING VIOLATION.        ");
                    sa.WriteLine("                        -OR-               ");
                    sa.WriteLine("3) Contest the citation within 14 calendar days from the mailing of this NOTICE OF DELINQUENT PARKING VIOLATION");
                    sa.WriteLine("   by submitting a written statement to the Parking Agent describing why the citation should be dismissed");
                    sa.WriteLine("   and requesting an INITIAL REVIEW. if the time frames for requesting an Initial Review are exceeded,");
                    sa.WriteLine("   then the parking violation must be paid. THERE IS NOT APPEAL IF YOUR REQUEST FOR REVIEW IS LATE.");
                    sa.WriteLine("                         -OR-                                                           ");
                    sa.WriteLine("4) If the vehicle described above was sold or tranferred prior to the date of the violation(s), complete");
                    sa.WriteLine("   and file an AFFIDAVIT OF NONLIABILITY,which complies with Sec. 40208 V.C. or 40209 V.C.-");
                    sa.WriteLine("                         NOTICE                   ");
                    sa.WriteLine(" ");
                    sa.WriteLine("FAILURE TO PAY THE PENALTIES DUE PRIOR TO THE DUE DATE INDICATED ABOVE WILL RESULT IN  ");
                    sa.WriteLine("INCREASED ADMINSTRATIVE PENALTIES. THE RENEWAL OF THE VEHICLE REGISTRATION SHALL BE");
                    sa.WriteLine("CONTINGENT UPON COMPLIANCE WITH THIS NOTICE OF DELINQUENT PARKING VIOLATION! A $25 LATE");
                    sa.WriteLine("FEE WILL BE ADDED 14 DAYS AFTER THE MAILING DATE ON THIS NOTICE IF YOU HAVE NOT TAKEN");
                    sa.WriteLine("ONE OF THE ABOVE ACTIONS.              ");
                    sa.WriteLine("              ");
                    sa.WriteLine("DATE NOTICE MAILED " + dateMailed);
                    // if more lines are added you must decrement 37 
                    // 3/18/09 added three lines above decrement 32 now 
                    for (int counter = 0; counter < 02; counter++)
                    {
                        sa.WriteLine();
                    }



                }


            }
            sa.Close();

            MessageBox.Show("Delinquent Parking letters=" + letterCount, "Total Letters Created");
            textBox1.Text = printfile;
            createFinalParkingLetterBtn.Enabled = false;

        }




    }
}