using System;
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
using System.Threading;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace File_Generation_System
{
    public class writeDmvFile
    {
        string requestorCode;
        string filepath;
        string name;
        int count = 0;
        DateTime currDate = DateTime.Now;
        string noticeDate;
        //string vesselTaxHold;
        string typeActionCode;
        public static Thread writeVrHoldsThread;
        string fileCode;

        public void spawn_write_VR_Holds()
        {
            writeVrHoldsThread = new Thread(new ThreadStart(write_DMV_file)); 
            writeVrHoldsThread.Start();

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

                        

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = configure.currentRequestorCode;
                filepath = cfr["drive_files_saved_in"].ToString();
               
            }

            //dir is set to whatever is in the config file
            //if nothing is there, then make default dir1

            string dir = filepath;

            
            if (!Directory.Exists(dir))
            {
                string dir1 = @"C:\ToDMV\Transfer";
                Directory.CreateDirectory(dir1);
                dir = dir1; 
            }

            string vrfilename = dir + @"\" + String.Format("{0:MMddyy}", currDate) + "vrinquiry.txt"; 
            
            FileStream vrInqFileStream = new FileStream(vrfilename, FileMode.Create, FileAccess.Write);

            StreamWriter sa = new StreamWriter(vrInqFileStream);

            foreach (DataRow ra in da.Tables["inquiry_data"].Rows)
            {
                if (ra["ro_city"].ToString().Length == 0 && ra["error_message"].ToString().Length == 0 && ra["ro_name"].ToString().Length == 0)
                {

                    //License numbers less than 7 bytes must have a space in them
                    //except for ELP license numbers

                    string licenseNumber = ra["license_number"].ToString().Trim();

                    if ((licenseNumber.Length < 7) &&  (ra["file_code"].ToString() == "L"))
                    {
                        
                        licenseNumber = licenseNumber.PadRight(7, ' ');
                    }
                    if ((licenseNumber.Length < 7) && (ra["file_code"].ToString() == "S"))
                    {

                        licenseNumber = licenseNumber.PadRight(7, ' ');
                    }

                    if ((licenseNumber.Length < 7) && (ra["file_code"].ToString() == "A"))
                    {
                        licenseNumber = " " + licenseNumber.PadRight(6, ' ');
                    }

                     
                     

                    if (ra["file_code"].ToString() == "V")
                    {
                        licenseNumber = string.Empty;
                        licenseNumber = licenseNumber.PadRight(7, ' ');
                    }
                    //Added for Vessel file code B 4.23.15

                    if (ra["file_code"].ToString() == "B")
                    {
                        //licenseNumber = string.Empty;
                        licenseNumber = licenseNumber.PadLeft(7, ' ');
                    }

                    string vrinqline = requestorCode + "1" + ra["as_of_date"].ToString().PadRight(6,' ') + ra["file_code"].ToString() + licenseNumber +
                                       ra["vin"].ToString().PadRight(24) + "      " + ra["year_model"].ToString().PadRight(2) +
                                       ra["make"].ToString().PadRight(3) + ra["user_information"].ToString().PadRight(24);
                    sa.WriteLine(vrinqline);

                    count = count + 1;

                    


                }

            }
            
            sa.Close();
            
            MessageBox.Show("Total Records written=" + count,"VR Inquiry File");
            string copyfile = dir + @"\" + "vrinquiry.txt";
            File.Delete(copyfile);
            File.Copy(vrfilename, copyfile);
            
            MessageBox.Show("Copying file to "  + copyfile, "File Copy");

            if (count > 0)
            {

           DialogResult answer =
           MessageBox.Show("Would you like to Send File Now?", "Send File",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    sendReceive send = new sendReceive();
                    send.Send('V');

                }
            }
        }
        public void write_DMV_Holds_file()
        {

            
            //Create Dataset for vrdatabase

            DataSet da = new DataSet();

            //Create Dataset for config file

            DataSet cf = new DataSet();
            
            //Read XML document into dataset for vrdatabase (da)
            //and config file (cf)

            da.ReadXml(configure.vrdb);

            cf.ReadXml(configure.cfdb);

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = configure.currentRequestorCode;
                filepath = cfr["drive_files_saved_in"].ToString();
                
            }

            //dir is set to whatever is in the config file
            //if nothing is there, then make default dir1

            string dir = filepath;


            if (!Directory.Exists(dir))
            {
                string dir1 = @"C:\ToDMV\Transfer";
                Directory.CreateDirectory(dir1);
                dir = dir1;
            }

            string vrfilename = dir + @"\" + String.Format("{0:MMddyy}", currDate) + "vrholds.txt";

            FileStream vrInqFileStream = new FileStream(vrfilename, FileMode.Create, FileAccess.Write);

            StreamWriter sa = new StreamWriter(vrInqFileStream);

            foreach (DataRow ra in da.Tables["inquiry_data"].Rows)
            {

                            
                
                noticeDate = DateTime.Today.ToShortDateString().Replace("/","").Trim();
                 
                if (noticeDate.Length == 7)
                {
                    noticeDate = noticeDate.Substring(0, 3) + noticeDate.Substring(5, 2);
                    noticeDate = "0" + noticeDate;
                }

                if (noticeDate.Length == 8)
                {
                    noticeDate = noticeDate.Substring(0, 4) + noticeDate.Substring(6, 2);

                } 

                if (ra["type_action_code"].ToString().Length > 0 && ra["license_number"].ToString().Length > 0  && ra["hold_record_written"].ToString().Trim().Length == 0 &&
                    ra["date_paid"].ToString().Length == 0 && ra["make"].ToString().Trim().Length > 0)
                {
                     
                                      

                    //To account for names under 5 bytes for now just prevent writout from failing
                    //need to add putting the first name into file name 

                    if (ra["ro_name"].ToString().Trim().Length  < 5)
                    {
                        int nameLength = ra["ro_name"].ToString().Trim().Length;
                        
                        int namePad = (5 - nameLength);

                        name = ra["ro_name"].ToString().Substring(0, nameLength).ToUpper().PadRight(namePad);
                    }
                    else
                    {
                        name = ra["ro_name"].ToString().Substring(0, 5).ToUpper();
                    }

                    //added to insure as of date is clean

                    var fasofdate = ra["as_of_date"].ToString().Trim();

                    var dispCode = ra["dispostion_code"].ToString();

                    var typeActionCode = ra["type_action_code"].ToString();

                    //Just in case force A's to the only value they can have.

                    if ((typeActionCode == "A") || (typeActionCode == "C"))
                    {
                        dispCode = "0";
                    }

                    //Just encase the disp is blank force it to a 1
                    if ((dispCode.ToString().Trim().Length == 0) & (typeActionCode == "D"))
                    {
                        dispCode = "1";
                    }

                    if (fasofdate.Trim().Length == 0)
                    {
                        fasofdate = DateTime.Today.ToString("MMddyy");

                    }

                    //VIN inquiries force to A since the inquiry does not return actual value

                     fileCode = ra["file_code"].ToString().Trim();

                     if (fileCode  == "V") 
                     {
                         fileCode = "A";
                     }


                    string vrinqline = "R80" + typeActionCode + ra["file_code"].ToString() +
                                        ra["license_number"].ToString().PadRight(7) + name.PadRight(5) +
                                        ra["user_information"].ToString().PadRight(15) + dispCode +
                                        fasofdate + ra["make"].ToString().PadRight(12) + ra["penalty_amount"].ToString().Substring(0,3) + requestorCode +
                                        noticeDate;  
                                         
                    
                                        
                    sa.WriteLine(vrinqline);
                    ra["hold_record_written"] = DateTime.Today;
                    da.AcceptChanges();
                    da.WriteXml(configure.vrdb);
                    count = count + 1;

                }

            }

            

           
            sa.Close();
            if (count > 0)
            {
                MessageBox.Show("Total Records written=" + count + " ", "VR Holds File ");

                string copyfile = dir + @"\" + "parkhold.txt";
                File.Delete(copyfile);
                File.Copy(vrfilename, copyfile);

                MessageBox.Show("Copying file to " + copyfile, "File Copy");
                DialogResult answer =
                MessageBox.Show("Would you like to Send File Now?", "Send File",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    sendReceive send = new sendReceive();
                    send.Send('V');

                }



            }

            if (count == 0)
            {
                MessageBox.Show("No Records are eligible to be held", "Records Not Eligible");
            }

        }

        //write out tax delinquent vessels

        public void write_DMV_Vessel_file()
        {
            //Create Dataset for vrdatabase

            DataSet da = new DataSet();


            //Read XML document into dataset for vrdatabase (da)
            //and config file (cf)

            da.ReadXml(configure.vrdb);

            
        

            string dir = configure.filepath;


            if (!Directory.Exists(dir))
            {
                string dir1 = @"C:\ToDMV\Transfer";
                Directory.CreateDirectory(dir1);
                dir = dir1;
            }

            string vrfilename = dir + @"\" + String.Format("{0:MMddyy}", currDate) + "veseltax.txt";

            FileStream vrInqFileStream = new FileStream(vrfilename, FileMode.Create, FileAccess.Write);

            StreamWriter sa = new StreamWriter(vrInqFileStream);

            

            foreach (DataRow ra in da.Tables["inquiry_data"].Rows)
            {


                noticeDate = DateTime.Today.ToShortDateString().Replace("/", "").Trim();

                if (noticeDate.Length == 7)
                {
                    noticeDate = noticeDate.Substring(0, 3) + noticeDate.Substring(3, 4);
                    noticeDate = "0" + noticeDate;
                }

                if (noticeDate.Length == 8)
                {
                    noticeDate = noticeDate.Substring(0, 4) + noticeDate.Substring(4, 4);

                } 






                if (ra["type_action_code"].ToString().Length > 0 && ra["license_number"].ToString().Length > 0 && ra["hold_record_written"].ToString().Trim().Length == 0)
                {



                    //To account for names under 5 bytes for now just prevent writout from failing
                    //need to add putting the first name into file name 

                    if (ra["ro_name"].ToString().Trim().Length < 5)
                    {
                        int nameLength = ra["ro_name"].ToString().Length;

                        int namePad = (5 - nameLength);

                        name = ra["ro_name"].ToString().Substring(0, nameLength).ToUpper().Trim();
                    }
                    else
                    {
                        name = ra["ro_name"].ToString().Substring(0, 5).ToUpper().Trim();
                    }

                    //Tax Delinquents need to convert from A for Add to 0 and D to 1

                    if (ra["type_action_code"].ToString().Trim() == "A")
                    {
                        typeActionCode = "0";
                    } 
 
                    if (ra["type_action_code"].ToString().Trim() == "D")
                    {
                       typeActionCode = "1";
                    }

                    //added to insure as of date is clean

                    string fasofdate = ra["as_of_date"].ToString().Trim();


                    string vrinqline = "V11" + configure.currentRequestorCode + noticeDate +
                                        ra["license_number"].ToString().PadLeft(7) + name.PadRight(5) + typeActionCode +
                                        ra["user_information"].ToString().PadRight(15);

                   

                     
                        sa.WriteLine(vrinqline);
                        ra["hold_record_written"] = DateTime.Today;
                        da.AcceptChanges();
                        da.WriteXml(configure.vrdb);
                        count = count + 1;    
                     
                   
                }

            }




            sa.Close();
            if (count >= 1)
            {
                MessageBox.Show("Total Records written=" + count + " " + vrfilename, "Tax Delinquent Vessels ");
                string copyfile = dir + @"\" + "veseltax.txt";
                File.Delete(copyfile);
                File.Copy(vrfilename, copyfile);

                MessageBox.Show("Copying file to " + copyfile, "File Copy");
                DialogResult answer =
                MessageBox.Show("Would you like to Send File Now?", "Send File",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    sendReceive send = new sendReceive();
                    send.Send('V');

                }
            }
            if (count == 0)
            {
                MessageBox.Show("No Records are eligible to be held", "Records Not Eligible");
            }

        }
    }

}