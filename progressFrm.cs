using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace File_Generation_System
{
    public partial class progressFrm : Form
    {

        public DataSet ds;
        public BindingSource mybind;
        public bool errorflag;
        public int ssc;
        StreamReader sr;
        String line;
        string rc;
        public ReportViewer reportViewer2;
        
        //Schema
        public processingRpt processingRpt = new processingRpt();
        //Crystal report
        //public processingReport processingReport = new processingReport();
        
        public progressFrm()
        {
            InitializeComponent();
        }

        private void progressFrm_Load(object sender, EventArgs e)
        {
             
             reportViewer2.Height =   ActiveForm.Size.Height;
             reportViewer2.Width =   ActiveForm.Size.Width;

            try
            {

                StreamWriter recordOut = new StreamWriter(
                    new FileStream("report.xml", FileMode.Create, FileAccess.Write));
                recordOut.WriteLine("<?xml version=\"1.0\" standalone=\"yes\" ?>");

                recordOut.WriteLine("<Report>");

                sr = new StreamReader(new BufferedStream(new FileStream(process.process_file, FileMode.Open)));
                line = sr.ReadLine();



                while (line != null)
                {

                    if (line.Substring(0, 1) == "1")
                    {
                        recordOut.WriteLine("<processingReport>");
                        recordOut.WriteLine("<record_status>Record Rejected</record_status>");
                        recordOut.WriteLine("<citation>" + line.Substring(6, 15).Trim() + "</citation>");
                        recordOut.WriteLine("<license_plate>" + line.Substring(22, 7).Trim() + "</license_plate>");
                        recordOut.WriteLine("<ro_name>" + line.Substring(29, 30).Trim() +  "</ro_name>");
                        recordOut.WriteLine("<make>" + line.Substring(114, 12).Trim() + "</make>");
                        recordOut.WriteLine("<violation_date>" + line.Substring(126, 2).Trim() + "/" + line.Substring(128, 2).Trim() + "/" + line.Substring(130, 2).Trim() + "</violation_date>");
                        recordOut.WriteLine("<penalty>" + "$" + line.Substring(132, 3).Trim() + "</penalty>");
                        string rejectCode = line.Substring(136, 1);
                        rc = lookupRejectionMessage(rejectCode);
                        recordOut.WriteLine("<reject_code>" + rc.Trim() + "</reject_code>");
                        recordOut.WriteLine("</processingReport>");

                    }

                    if (line.Substring(0, 1) == "2")
                    {
                        recordOut.WriteLine("<processingReport>");
                        recordOut.WriteLine("<record_status>Collected</record_status>");
                        recordOut.WriteLine("<citation>" + line.Substring(6, 15).Trim() + "</citation>");
                        recordOut.WriteLine("<license_plate>" + line.Substring(21, 7).Trim() + "</license_plate>");
                        recordOut.WriteLine("<ro_name>" + line.Substring(28, 5).Trim() + "</ro_name>");
                        recordOut.WriteLine("<make>" + "</make>");
                        recordOut.WriteLine("<date_paid>" + line.Substring(122, 2).Trim() + "/" + line.Substring(124, 2).Trim() + "/" + line.Substring(126, 2).Trim() + "</date_paid>");
                        recordOut.WriteLine("<violation_date>" + line.Substring(113, 2).Trim() +  "/" + line.Substring(115, 2).Trim() + "/" + line.Substring(117, 2).Trim() + "</violation_date>");
                        recordOut.WriteLine("<penalty>" + "$" + line.Substring(119, 3).TrimStart('0') + "</penalty>");
                        recordOut.WriteLine("<reject_code>" + "</reject_code>");
                        recordOut.WriteLine("</processingReport>");

                    }

                    if (line.Substring(0, 1) == "3")
                    {
                        recordOut.WriteLine("<processingReport>");
                        recordOut.WriteLine("<record_status>Removed</record_status>");
                        recordOut.WriteLine("<citation>" + line.Substring(6, 15).Trim() + "</citation>");
                        recordOut.WriteLine("<license_plate>" + line.Substring(21, 7).Trim() + "</license_plate>");
                        recordOut.WriteLine("<ro_name>" + line.Substring(28, 30).Trim() + "</ro_name>");
                        recordOut.WriteLine("<ro_address>" + line.Substring(58, 22) + "</ro_address>");
                        recordOut.WriteLine("<ro_city_state>" + line.Substring(88, 13) + "</ro_city_state>");
                        recordOut.WriteLine("<make>" + line.Substring(113, 12).Trim() + "</make>");
                        recordOut.WriteLine("<violation_date>" + line.Substring(125, 2).Trim() + "/" + line.Substring(127, 2).Trim() + "/" + line.Substring(129, 2).Trim() + "</violation_date>");
                        recordOut.WriteLine("<penalty>" + "$" + line.Substring(131, 3).TrimStart('0') + "</penalty>");
                        recordOut.WriteLine("<date_removed>" + line.Substring(134, 2).Trim() + "/" + line.Substring(136, 2).Trim() + "/" + line.Substring(138, 2).Trim() +  "</date_removed>");
                        string rcm = lookupRemovalCode(line.Substring(140, 1).Trim());
                        recordOut.WriteLine("<removal_code>" + rcm + "</removal_code>");
                        recordOut.WriteLine("</processingReport>");

                    }

                    if (line.Substring(0, 1) == "4")
                    {
                        recordOut.WriteLine("<processingReport>");
                        recordOut.WriteLine("<record_status>Added</record_status>");
                        recordOut.WriteLine("<citation>" + line.Substring(6, 15).Trim() + "</citation>");
                        recordOut.WriteLine("<license_plate>" + line.Substring(21, 7).Trim() + "</license_plate>");
                        recordOut.WriteLine("<ro_name>" + line.Substring(28, 30).Trim() + "</ro_name>");
                        recordOut.WriteLine("<ro_city_state>" + line.Substring(58, 30) + "</ro_city_state>");
                        recordOut.WriteLine("<make>" + line.Substring(113, 12).Trim() + "</make>");
                        recordOut.WriteLine("<violation_date>" + line.Substring(125, 2).Trim() + "/" + line.Substring(127, 2).Trim() + "/" + line.Substring(129, 6).Trim() + "</violation_date>");
                        recordOut.WriteLine("<penalty>" + "$" + line.Substring(131, 3).TrimStart('0') + "</penalty>");
                        recordOut.WriteLine("</processingReport>");
                    }


                    line = sr.ReadLine();

                }

                recordOut.WriteLine("</Report>");
                recordOut.Close();
                 
  


                FileInfo f = new FileInfo("report.xml");

                string whereFile = f.FullName;

                //Schema processing report schema


                 

                  ds = new DataSet();
                 //processingRpt ps = new processingRpt();
                //processingRpt1.ReadXml(whereFile);
                //ps.ReadXml(whereFile);

                ds.ReadXml(whereFile);
               
                 
                

               processingReport_DataTableBindingSource.DataSource = ds;
                
                         

               processingReport_DataTableBindingSource.DataMember = "processingReport";


               

                

               

                
                
            }

            catch (ArgumentException ex)
            {
                errorflag = true;
                var nothing = ex;

            }




            
              this.reportViewer2.Height =  ActiveForm.Size.Height;
             this.reportViewer2.Width =  ActiveForm.Size.Width;
            this.reportViewer2.ZoomPercent = 75;
            //this.reportViewer2.RefreshReport();
        }

        private string lookupRejectionMessage(string rejectCode)
        {

            switch (rejectCode)
            {

                case "1":
                    return "No Citations on File";
                case "2":
                    return "File Code Invalid or Incompatible With License";
                case "3":
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

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             

        }

        private void recordsProcessedDataGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void vrmonthlyprocess1_InitReport(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void vrmonthly_InitReport(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void CrystalReport11_InitReport(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_2(object sender, EventArgs e)
        {

        }

        private void processing1_InitReport(object sender, EventArgs e)
        {

        }

        private void processingReport1_InitReport(object sender, EventArgs e)
        {

        }

        private void processingReport2_InitReport(object sender, EventArgs e)
        {

        }

        private void processingReport1_InitReport_1(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_3(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void processingReportBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void progressFrm_Resize(object sender, EventArgs e)
        {

            reportViewer2.Height = ActiveForm.Size.Height ;
            reportViewer2.Width = ActiveForm.Size.Width  - 5;
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {
              reportViewer2.Height = ActiveForm.Size.Height ;
              reportViewer2.Width = ActiveForm.Size.Width - 5 ;
        }

        private void processingReport_DataTableBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void progressFrm_Load_1(object sender, EventArgs e)
        {



            

        }

        private void crystalReportViewer1_Load_4(object sender, EventArgs e)
        {

        }
    }
}