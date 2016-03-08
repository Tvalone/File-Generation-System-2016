using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
 

namespace File_Generation_System
{
    public class configure
    {
        public static DataSet cf;
        public static DataSet ds;
        public static string vrdb;
        public static string cfdb;
        public string path;
        public static string vesselTaxHold;
        public static string filepath;
        public static string requestorCode;
        public static string userid;
        public static string hostname;
        public static string requestorCode1;
        public static string requestorCode2;
        public static string useThisRequestorCode;
        public static string useThisRequestorCode1;
        public static string useThisRequestorCode2;
        public static string currentRequestorCode;

         

        
        public static void read_xml_files()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            vrdb = System.IO.Path.Combine(path,"vrdatabase.xml");
            
            cfdb = System.IO.Path.Combine(path,"config.xml");
            ds = new DataSet();
            ds.ReadXml(vrdb);
             
            
            
        }


        public static void getInformationFromConfig()
        {
            cf = new DataSet();
            cf.ReadXml(cfdb);


            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                requestorCode = cfr["requestor_code"].ToString();
                requestorCode1 = cfr["requestor_code1"].ToString();
                useThisRequestorCode = cfr["use_this_requestor_code"].ToString();
                requestorCode2 = cfr["requestor_code2"].ToString();
                useThisRequestorCode = cfr["use_this_requestor_code"].ToString();
                useThisRequestorCode1 = cfr["use_this_requestor_code1"].ToString();
                useThisRequestorCode2 = cfr["use_this_requestor_code2"].ToString();
                filepath = cfr["drive_files_saved_in"].ToString();
                vesselTaxHold = cfr["vr_taxholds"].ToString();
                userid = cfr["userid"].ToString().Trim();
                hostname = cfr["hostname"].ToString().Trim();
                if (useThisRequestorCode == "True")
                {
                     currentRequestorCode = requestorCode;
                }
                if (useThisRequestorCode1 == "True")
                {
                     currentRequestorCode = requestorCode1;
                }
                if (useThisRequestorCode2 == "True")
                {
                    currentRequestorCode = requestorCode2;
                }
             

                 
            }

           

        }

       

                
        
       public static bool validate_req(ListBox listbox,ToolStripMenuItem toolstripmenuitem)
        {
            cf = new DataSet();
            cf.ReadXml(cfdb);
             

            foreach (DataRow ra in cf.Tables["fgs_config"].Rows)
            {

                
                
                //if (ra["requestor_code"].ToString().TrimEnd() == "99999")
                //{

                //    listbox.Items.Add("Please Update Requestor code in configuration file");
                //    toolstripmenuitem.Enabled = false;
                //    listbox.Visible = true;
                //    return true;
                //}

                //if (ra["requestor_code"].ToString().TrimEnd() != "99999")
                //{

                     
                //    toolstripmenuitem.Enabled = true;
                //    listbox.Visible =false;
                //    return true;
                //}


                 

                if (ra["dl_inquiry"].ToString() == "False" & toolstripmenuitem.Text == "DL Transactions")
                {
                    
                    toolstripmenuitem.Visible = false;
                    return true;
                }

                if (ra["dl_inquiry"].ToString() == "True" & toolstripmenuitem.Text == "DL Transactions")
                {
                     
                    toolstripmenuitem.Visible = true;
                    return true;
                }

                if (ra["vr_inquiry"].ToString() == "False" & toolstripmenuitem.Text == "VR Transactions")
                {

                    toolstripmenuitem.Visible = false;
                    return true;
                }

                if (ra["vr_inquiry"].ToString() == "True" & toolstripmenuitem.Text == "VR Transactions")
                {

                    toolstripmenuitem.Visible = true;
                    return true;
                }


            }
            return false;
        }
    }
}
