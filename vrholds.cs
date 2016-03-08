using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
 

namespace File_Generation_System
{
    public enum parkingActionCode
    {
        A,
        C,
        D,

    }

     
    public partial class vrholds : Form
    {
        public DataSet ds;
        public bool errorflag;
        List<string> rcl = new List<string>();
        public int match;
        public BindingSource mybind;
        public vrholds()
        
        {
            InitializeComponent();

            ds = new DataSet();
            ds.ReadXml(configure.vrdb);

             
             
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitParkingHoldsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vrholds_Load(object sender, EventArgs e)
        {
            trackBar1.Enabled = false;
            currentRequestorCodelbl.Text = configure.currentRequestorCode;
           //this.violationDatePicker.CustomFormat = "MMddyy";
           //this.noticeDatePicker.CustomFormat = "MMddyy";
           listBox1.Visible = false;
           rcl.Add("C");
           rcl.Add("D");
           rcl.Add("R");
           rcl.Add("T");

          
           try
           {
                

               //This binds the license text box to the inquiry_data table
               vehicleLicenseNumberRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.license_number");
               makeOfCarRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.make");
               roFirstNameRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_name");
               citationNumberRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.user_information");
               asOfDateTxtBx.DataBindings.Add("Text", ds, "inquiry_data.as_of_date",true);
               
               parkingHoldsActionCode.DataBindings.Add("Text", ds, "inquiry_data.type_action_code");
               fileCodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.file_code");
                
               dispLstBx.DataBindings.Add("Text", ds, "inquiry_data.dispostion_code");
               vinTxtBx.DataBindings.Add("Text", ds, "inquiry_data.vin");
               penaltyAmountRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.penalty_amount");
               errorMessageLbl.DataBindings.Add("Text", ds, "inquiry_data.error_message");
               docketNumberTxtBx.DataBindings.Add("Text", ds, "inquiry_data.docket_number");
               rejectionMessageRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.rejection_message");
               recordConditionCodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.record_condition_code");
               removalCodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.removal_code");
               removalMessageTxtBx.DataBindings.Add("Text", ds, "inquiry_data.removal_message");
               datePaidTxtBx.DataBindings.Add("Text", ds, "inquiry_data.date_paid");
               roNameOrAddressRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_name_or_address");
               roCityRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_city");
               roZipRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_zip");
               holdWrittenToDMV.DataBindings.Add("Text", ds, "inquiry_data.hold_record_written");

               trackBar1.Minimum = 0;
               trackBar1.Maximum = this.BindingContext[ds, "inquiry_data"].Count - 1;
                
                
               trackBar1.Enabled = true;
               
               if (ds.Tables.Count == 0)
                   throw new Exception();


           }

           catch (ArgumentException ex)
           {
               errorflag = true;
               listBox1.Visible = true;
               listBox1.Items.Add("There are no records in the database" + ex);
               
           }

           catch (Exception ex)
           {
               errorflag = true;
               listBox1.Visible = true;
               listBox1.Items.Add("An unknown error occurred loading the vrholds form" + ex);
                    
           }
           finally
           {

           }
            
           
             
        }

         
          

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
            this.BindingContext[ds, "inquiry_data"].Position = trackBar1.Value;
            listBox1.Items.Clear();
            listBox1.Visible = false;
            

        }
      
         

        private void button1_Click(object sender, EventArgs e)
        {
            

             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button2 is actually the add button
            writeMessageLbl.Text = "";
            listBox1.Items.Clear();
            listBox1.Visible = false;
            errorflag = false;
 

              

            if (roFirstNameRchTxtBx.Text.Length <= 0)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Record has no address info - cannot hold reg");
                
            }

            if (fileCodeTxtBx.Text.Length <= 0)
            {

                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Record has no file code - cannot hold reg");
            }


            if (datePaidTxtBx.Text.Trim().Length > 0)
            {

                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Record has been paid - cannot hold reg");
            }


            //The record condition Code combes back from the Vr Inquiry 
            if (recordConditionCodeTxtBx.Text.ToString().Trim() == "22")
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Cannot place hold - Notice of Release of Liability Reported");

            }

            //The datePaid comes from the monthly file 
            if (datePaidTxtBx.Text.ToString().Trim().Length > 0)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Cannot place hold - Hold reported paid by DMV");

            }

            //The removal code coes from the monthly parking file
            int idx = rcl.IndexOf(removalCodeTxtBx.Text.ToString().Trim());

            if (idx != -1)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Cannot place hold - DMV removed hold - check removal message");
            }

            if (rejectionMessageRchTxtBx.Text.Length > 0)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Citation Record Rejected - Correction needed");
            }
             

            if (parkingHoldsActionCode.Text.Trim().Length == 0)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Parking Hold Code must be entered");
              
                 
            }

            if (parkingHoldsActionCode.Text == "A")
            {
                dispLstBx.SelectedIndex = 0;
             
                

            }

            if (parkingHoldsActionCode.Text == "C" && dispLstBx.SelectedItem.ToString() != "0")
            {
                dispLstBx.SelectedIndex = 0;

            }

            //Use index of any if code isnt A C or D then hd will equal -1
             int hd = (parkingHoldsActionCode.Text.IndexOfAny("ACD-".ToCharArray()));
            
            if (hd == -1)

            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("Hold Action Code Must Be A C or D");
            }




            //dispLstBx.SelectedIndex = 0;

            if (hd == 0)
            {
                if (parkingHoldsActionCode.Text == "D" &&  dispLstBx.SelectedIndex == 0)
                     
                            {
                                errorflag = true;
                                listBox1.Visible = true;
                                listBox1.Items.Add("Delete must have a dispostion code of 1,2,4,8");
                            }

            }

                if (penaltyAmountRchTxtBx.Text.Trim().Length != 3)
                {
                    errorflag = true;
                    listBox1.Visible = true;
                     
                    listBox1.Items.Add("Hold Tranaction must have a penalty amount - three bytes long");
                }

                if (penaltyAmountRchTxtBx.Text.Trim().Length == 3 && penaltyAmountRchTxtBx.Text.Trim() == "000")
                {
                    errorflag = true;
                    listBox1.Visible = true;

                    listBox1.Items.Add("Hold Tranaction penalty amount cannot be zero");
                }

                if (fileCodeTxtBx.Text.Trim().Length == 0)
                {
                    errorflag = true;
                    listBox1.Visible = true;
                    listBox1.Items.Add("File code must be present on any hold transaction");
                }

                if (parkingHoldsActionCode.Text == "D" && holdWrittenToDMV.Text.Trim().Length > 0)
                {
                    errorflag = true;
                    listBox1.Visible = true;
                    listBox1.Items.Add("You must clear the Holds Written to Date for Deletes");
                } 


            if (errorflag == false)
            {
               
                listBox1.Items.Clear();

                 
                ds.AcceptChanges();
                writeMessageLbl.Text = "Hold written";
                ds.WriteXml(configure.vrdb);
            }
             
        }

         

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int current = this.BindingContext[ds, "inquiry_data"].Position;

                ds.EnforceConstraints = false;

                this.ds.Tables["inquiry_data"].Rows[current].Delete();
                                
                //Add yes no message box and roll back changes
                // if no
                ds.AcceptChanges();

                ds.WriteXml(configure.vrdb);
            }
            catch (IndexOutOfRangeException ex)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("There are no records in the Database" + ex);
            }

            catch (Exception ex)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("An unknown error occured in vrholds.cs " + ex);
            }

            
        }

        private void roFirstNameRchTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void parkingHoldsActionCodelstBx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vrholds_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
              
                
                 

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void violationDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void violationDateRchTxtBx_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void vehicleLicenseNumberRchTxtBx_Leave(object sender, EventArgs e)
        {
            vehicleLicenseNumberRchTxtBx.Text = vehicleLicenseNumberRchTxtBx.Text.ToUpper();
        }

        private void vehicleLicenseNumberRchTxtBx_MouseClick(object sender, MouseEventArgs e)
        {
             
        }

        private void vehicleLicenseNumberRchTxtBx_Enter(object sender, EventArgs e)
            {
             

        }

        private void dispositionCodeTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void dispositonCodeLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void parkingcodeActionHold_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void violationDatePicker_ValueChanged_1(object sender, EventArgs e)
        {
             
        }

        private void recordConditionCodeTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void findBtn_Click(object sender, EventArgs e)
        {


            try
            {
                int findCount = 0;

                foreach (DataRow rw in ds.Tables["inquiry_data"].Rows)
                {

                    findCount++;
                    match = string.Compare(rw["user_information"].ToString().Trim(), findCiteTxtBx.Text.Trim());

                    if (match == 0)
                    {
                        this.BindingContext[ds, "inquiry_data"].Position = findCount - 1;
                        break;


                    }
                     

                }


            }
            catch (IndexOutOfRangeException ex)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("There are no records in the Database" + ex);
            }

            catch (Exception ex)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("An unknown error occured in vrholds.cs " + ex);
            }

            finally
            {
                if (match != 0)
                {
                    MessageBox.Show("No citations match search criteria", "Citation Not Found");
                }
            }
        } 
    }
   
     

}