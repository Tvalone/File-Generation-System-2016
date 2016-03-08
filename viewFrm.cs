using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace File_Generation_System
{
    public partial class modifyFrm : Form
     
    {
        public bool errorflag;
        public DataSet ds;
        public int match;
        
        // pcl holds parking action codes ACD
        List<string> pcl = new List<string>();

        //dcl holds disposition codes 0,1,2,4,8

        List<string>  dcl = new List<string>();

        basicDateValidations bs = new basicDateValidations();
        
            

        public modifyFrm()
        {
            InitializeComponent();

            ds = new DataSet();

            ds.ReadXml(configure.vrdb);

                      
        }

        

        

        private void viewFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vrdataDataSet.configuration_data' table. You can move, or remove it, as needed.
           // this.configuration_dataTableAdapter.Fill(this.vrdataDataSet.configuration_data);
            

            trackBar1.Enabled = false;
            
           listBox1.Visible = false;
          
           try
           {
                

               //This binds the license text box to the inquiry_data table

               vehicleLicenseNumberRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.license_number");
               makeOfCarRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.make");
               roFirstNameRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_name");
               citationNumberRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.user_information");
              
               asOfDateMskTxtBx.DataBindings.Add("Text", ds, "inquiry_data.as_of_date");
               dueDateTimePicker.DataBindings.Add("Text", ds, "inquiry_data.due_date");
               noticeDateMskTxtBx.DataBindings.Add("Text", ds, "inquiry_data.notice_date");
               parkingHoldsActionCodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.type_action_code");
               fileCodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.file_code");
               dispostion_CodeTxtBx.DataBindings.Add("Text", ds, "inquiry_data.dispostion_code");
               vinRichTxtBx.DataBindings.Add("Text", ds, "inquiry_data.vin");
               penaltyDueRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.penalty_amount");
               errorMessageLbl.DataBindings.Add("Text", ds, "inquiry_data.error_message");
               docketNumberTxtBx.DataBindings.Add("Text", ds, "inquiry_data.docket_number");
               locationRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.location");
               modelYearTxtBx.DataBindings.Add("Text", ds, "inquiry_data.year_model");
               colorRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.car_color");
               secViol1RchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.viol1_section");
               secViol2RchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.viol2_section");
               secVio3RchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.viol3_section");
               dmvRejectionCodeRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.rejection_code");
               dmvRejectionHoldMessageRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.rejection_message");

               paidDateMskTxtBx.DataBindings.Add("Text", ds, "inquiry_data.date_paid");
               roAddressTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_name_or_address");
               additionalRoNameOrAddressTxtBx.DataBindings.Add("Text", ds, "inquiry_data.additional_ro_name_or_address");
               cityRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_city");
               zipRchTxtBx.DataBindings.Add("Text", ds, "inquiry_data.ro_zip");

               holdWrittenToDMVDateTxtBx.DataBindings.Add("Text", ds, "inquiry_data.hold_record_written");
               
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
               listBox1.Items.Add("There are no records in the database");
               MessageBox.Show("" + ex);
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
             
         

         

        private void exitViewRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.BindingContext[ds, "inquiry_data"].Position = trackBar1.Value;
        }

        private void writeRecords_Click(object sender, EventArgs e)
        {
            pcl.Add("A");
            pcl.Add("C");
            pcl.Add("D");

            dcl.Add("0");
            dcl.Add("1");
            dcl.Add("2");
            dcl.Add("4");
            dcl.Add("8");

            errorflag = false;

            listBox1.Visible = false;
            listBox1.Items.Clear();
             

            if (parkingHoldsActionCodeTxtBx.Text.ToString().Trim().Length != 0 && roFirstNameRchTxtBx.Text.Length == 0)
            {
                listBox1.Visible = true;
                listBox1.Items.Add("No Address - cannot place hold");
                errorflag = true;

            }

            if (penaltyDueRchTxtBx.Text.ToString().Trim().Length == 0 && roFirstNameRchTxtBx.Text.Length > 0)
            {
                listBox1.Visible = true;
                listBox1.Items.Add("Parking Holds must have a penalty amount");
                errorflag = true;

            }

            if (penaltyDueRchTxtBx.Text.ToString().Trim().Length != 3 && roFirstNameRchTxtBx.Text.Length > 0)
            {
                 
                listBox1.Visible = true;
                listBox1.Items.Add("Parking Holds must have a penalty amount ");
                errorflag = true;

            }

            if (fileCodeTxtBx.Text.Trim().Length == 0)
            {

                listBox1.Visible = true;
                listBox1.Items.Add("Parking Holds must have a file code ");
                errorflag = true;
            }

            if (paidDateMskTxtBx.Text.Trim().Length > 0)
            {

                listBox1.Visible = true;
                listBox1.Items.Add("Citation Paid - Cannot place registration ");
                errorflag = true;
            }
            if (roFirstNameRchTxtBx.Text.Length > 0)
            {

                // If idx contains 0,1,2 then we have a valid Parking Hold Code
                // If dcx is less than zero then do something 

                int idx = pcl.IndexOf(parkingHoldsActionCodeTxtBx.Text.ToString());

                int dcx = dcl.IndexOf(dispostion_CodeTxtBx.Text.ToString());
                 

                if (idx == 0 && dispostion_CodeTxtBx.Text != "0")   
          
                {
                    errorflag = true;
                    listBox1.Visible = true;
                    listBox1.Items.Add("Add must have a dispostion code of 0");

                }

                if (idx == 1 && dispostion_CodeTxtBx.Text != "0")
                {
                    errorflag = true;
                    listBox1.Visible = true;
                    listBox1.Items.Add("Changes must have a dispostion code of 0");

                }

                if (idx == 2 && dcx < 0 )
                {
                    errorflag = true;
                    listBox1.Visible = true;
                    listBox1.Items.Add("Deletes must have a dispostion code of 1,2,4,8");
                    dispostion_CodeTxtBx.Focus();
                    

                }

            


                if (fileCodeTxtBx.Text == "A" & paidDateMskTxtBx.Text.Length == 10 & holdWrittenToDMVDateTxtBx.Text.Length > 0)
         {
             errorflag = true;
             listBox1.Visible = true;
             listBox1.Items.Add("Cites paid in office with a DMV hold must have ");
             listBox1.Items.Add("Parking Action Code changed to D and ");
             listBox1.Items.Add("Disposition code set to 1,2,3,4 or 8 and Hold writen to DMV cleared");

             dispostion_CodeTxtBx.Focus();
                    
              

             
              
         }

                       
                           

            }
            if (errorflag == false)
            {
             
                
                listBox1.Visible = false;
                ds.AcceptChanges();
                ds.WriteXml(configure.vrdb);
            }
            
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void deleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                var recCount = this.ds.Tables["inquiry_data"].Rows.Count;
                int current = this.BindingContext[ds, "inquiry_data"].Position;
                ds.EnforceConstraints = false;
                if (recCount > 1)
                {
                    this.ds.Tables["inquiry_data"].Rows[current].Delete();
                    //Add yes no message box and roll back changes
                    // if no
                    ds.AcceptChanges();

                    ds.WriteXml(configure.vrdb);
                }

                if (recCount == 1)
                {
                    throw new Exception("You cannot delete the last record in the DB.");
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
                listBox1.Items.Add(ex.Message.ToString());
            }

            
       
        }

        private void asOfDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void vinlbl_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vehicleLicenseNumberRchTxtBx_TextChanged(object sender, EventArgs e)
        {
            vehicleLicenseNumberRchTxtBx.Text = vehicleLicenseNumberRchTxtBx.Text.ToUpper(); 
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string search = vehicleLicenseNumberRchTxtBx.Text;

            foreach (DataRow row in ds.Tables["inquiry_data"].Rows)
            {
                string rowValue = row["license_number"] as String;
                

                if (rowValue.StartsWith(search))
                {
                    MessageBox.Show("lic" + rowValue);
                    MessageBox.Show("Hit" + search);
                }  

            }


        }

        private void citationInformationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void paidDateMskTxtBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void paidDateMskTxtBx_Leave(object sender, EventArgs e)
        {

            

         listBox1.Items.Clear();
         listBox1.Visible = false;
         if (paidDateMskTxtBx.Text.Trim().Length > 4   )
         {
             listBox1.Items.Add(bs.DateValidations(sender));
             if (listBox1.Items.Count > 0)
             {
                 listBox1.Visible = true;
             }

         }

        }

        private void findCiteBtn_Click(object sender, EventArgs e)
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