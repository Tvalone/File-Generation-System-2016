using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace File_Generation_System
{
    public partial class exceptionFrm : Form
    {
        public DataSet ds;
        public BindingSource mybind;
        public bool errorflag;
        public int ssc;


        public exceptionFrm()
        {
            InitializeComponent();
        }

        private void exceptionFrm_Load(object sender, EventArgs e)
        {


            try
            {

                mybind = new BindingSource();

                configure.read_xml_files();

                ds = new DataSet();

                ds.ReadXml(configure.vrdb);

                mybind.DataSource = ds;

                mybind.DataMember = "inquiry_data";

                SetupColumns(ds);

                dataGridView1.DataSource = ds;

                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = mybind;


                if (ds.Tables.Count == 0)
                    throw new Exception();


            }

            catch (ArgumentException ex)
            {
                errorflag = true;
                listBox1.Visible = true;
                listBox1.Items.Add("There are no records in the database");

            }



        }
        private void SetupColumns(DataSet ds)
        {

            DataGridViewTextBoxColumn roName = new DataGridViewTextBoxColumn();
            roName.DataPropertyName = "ro_name";
            roName.HeaderText = "Registered Owner Name";
            roName.ValueType = typeof(string);
            dataGridView1.Columns.Add(roName);

            DataGridViewTextBoxColumn roNameOrAddress = new DataGridViewTextBoxColumn();
            roNameOrAddress.DataPropertyName = "ro_name_or_address";
            roNameOrAddress.HeaderText = "Registered Owner Name/or Address";
            roNameOrAddress.ValueType = typeof(string);
            dataGridView1.Columns.Add(roNameOrAddress);

            DataGridViewTextBoxColumn licenseNumber = new DataGridViewTextBoxColumn();
            licenseNumber.DataPropertyName = "license_number";
            licenseNumber.HeaderText = "License Number";
            licenseNumber.ValueType = typeof(string);
            dataGridView1.Columns.Add(licenseNumber);

            DataGridViewTextBoxColumn regExpireDate = new DataGridViewTextBoxColumn();
            regExpireDate.DataPropertyName = "reg_expire_date";
            regExpireDate.HeaderText = "Registration Expiration Date";
            regExpireDate.ValueType = typeof(string);
            dataGridView1.Columns.Add(regExpireDate);

            DataGridViewTextBoxColumn make = new DataGridViewTextBoxColumn();
            make.DataPropertyName = "make";
            make.HeaderText = "Make of Car";
            make.ValueType = typeof(string);
            dataGridView1.Columns.Add(make);

            DataGridViewTextBoxColumn vin = new DataGridViewTextBoxColumn();
            vin.DataPropertyName = "vin";
            vin.HeaderText = "VIN";
            vin.ValueType = typeof(string);
            dataGridView1.Columns.Add(vin);

            DataGridViewTextBoxColumn roCity = new DataGridViewTextBoxColumn();
            roCity.DataPropertyName = "ro_city";
            roCity.HeaderText = "City";
            roCity.ValueType = typeof(string);
            dataGridView1.Columns.Add(roCity);

            DataGridViewTextBoxColumn roZip = new DataGridViewTextBoxColumn();
            roZip.DataPropertyName = "ro_zip";
            roZip.HeaderText = "ZIP";
            roZip.ValueType = typeof(string);
            dataGridView1.Columns.Add(roZip);

            DataGridViewTextBoxColumn roCountyCode = new DataGridViewTextBoxColumn();
            roCountyCode.DataPropertyName = "ro_county_code";
            roCountyCode.HeaderText = "County Code";
            roCountyCode.ValueType = typeof(string);
            dataGridView1.Columns.Add(roCountyCode);

            DataGridViewTextBoxColumn asOfDate = new DataGridViewTextBoxColumn();
            asOfDate.DataPropertyName = "as_of_date";
            asOfDate.HeaderText = "As of Date";
            asOfDate.ValueType = typeof(string);
            dataGridView1.Columns.Add(asOfDate);

            DataGridViewTextBoxColumn fileCode = new DataGridViewTextBoxColumn();
            fileCode.DataPropertyName = "file_code";
            fileCode.HeaderText = "File Code";
            fileCode.ValueType = typeof(string);
            dataGridView1.Columns.Add(fileCode);

            DataGridViewTextBoxColumn parkCode = new DataGridViewTextBoxColumn();
            parkCode.DataPropertyName = "type_action_code";
            parkCode.HeaderText = "Parking Hold Code";
            parkCode.ValueType = typeof(string);
            parkCode.MaxInputLength = 1;

            dataGridView1.Columns.Add(parkCode);

            DataGridViewTextBoxColumn yearModel = new DataGridViewTextBoxColumn();
            yearModel.DataPropertyName = "year_model";
            yearModel.HeaderText = "Model Year";
            yearModel.ValueType = typeof(string);
            dataGridView1.Columns.Add(yearModel);

            DataGridViewTextBoxColumn citeNumber = new DataGridViewTextBoxColumn();
            citeNumber.DataPropertyName = "user_information";
            citeNumber.HeaderText = "Citation Number";
            citeNumber.ValueType = typeof(string);
            dataGridView1.Columns.Add(citeNumber);

            DataGridViewTextBoxColumn paperIssueDate = new DataGridViewTextBoxColumn();
            paperIssueDate.DataPropertyName = "paper_issue_date";
            paperIssueDate.HeaderText = "Date last document was issued";
            paperIssueDate.ValueType = typeof(string);
            dataGridView1.Columns.Add(paperIssueDate);

            DataGridViewTextBoxColumn recordConditionCode = new DataGridViewTextBoxColumn();
            recordConditionCode.DataPropertyName = "record_condition_code";
            recordConditionCode.HeaderText = "Record Condtion code";
            recordConditionCode.ValueType = typeof(string);
            dataGridView1.Columns.Add(recordConditionCode);

            DataGridViewTextBoxColumn recordCondtionCodeDate = new DataGridViewTextBoxColumn();
            recordCondtionCodeDate.DataPropertyName = "record_condition_code_date";
            recordCondtionCodeDate.HeaderText = "Date Record Condition Code was Entered";
            recordCondtionCodeDate.ValueType = typeof(string);
            dataGridView1.Columns.Add(recordCondtionCodeDate);

            DataGridViewTextBoxColumn recordRemarks = new DataGridViewTextBoxColumn();
            recordRemarks.DataPropertyName = "record_remarks";
            recordRemarks.HeaderText = "Record Remarks";
            recordRemarks.ValueType = typeof(string);
            dataGridView1.Columns.Add(recordRemarks);

            DataGridViewTextBoxColumn errorCode = new DataGridViewTextBoxColumn();
            errorCode.DataPropertyName = "error_code";
            errorCode.HeaderText = "Error Code";
            errorCode.ValueType = typeof(string);
            dataGridView1.Columns.Add(errorCode);

            DataGridViewTextBoxColumn errorMessage = new DataGridViewTextBoxColumn();
            errorMessage.DataPropertyName = "error_message";
            errorMessage.HeaderText = "Error Message";
            errorMessage.ValueType = typeof(string);
            dataGridView1.Columns.Add(errorMessage);

            DataGridViewTextBoxColumn errorDate = new DataGridViewTextBoxColumn();
            errorDate.DataPropertyName = "error_date";
            errorDate.HeaderText = "Error Date";
            errorDate.ValueType = typeof(string);
            errorDate.ReadOnly = true;
            dataGridView1.Columns.Add(errorDate);

            DataGridViewTextBoxColumn datePaid = new DataGridViewTextBoxColumn();
            datePaid.DataPropertyName = "date_paid";
            datePaid.HeaderText = "Date Paid";
            datePaid.ValueType = typeof(string);
            dataGridView1.Columns.Add(datePaid);

            DataGridViewTextBoxColumn removalCode = new DataGridViewTextBoxColumn();
            removalCode.DataPropertyName = "removal_code";
            removalCode.HeaderText = "DMV Removal Code";
            removalCode.ValueType = typeof(string);
            dataGridView1.Columns.Add(removalCode);

            DataGridViewTextBoxColumn removalDate = new DataGridViewTextBoxColumn();
            removalDate.DataPropertyName = "removed_date";
            removalDate.HeaderText = "Date DMV removed hold";
            removalDate.ValueType = typeof(string);
            dataGridView1.Columns.Add(removalDate);

            DataGridViewTextBoxColumn removalMessage = new DataGridViewTextBoxColumn();
            removalMessage.DataPropertyName = "removal_message";
            removalMessage.HeaderText = "Removal Message";
            removalMessage.ValueType = typeof(string);
            dataGridView1.Columns.Add(removalMessage);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text.Substring(0, 1) == "B")
            {
                textBox2.Visible = true;
            }


            if (textBox1.Text.Trim().Length == 0)
            {
                listBox1.Visible = true;

                listBox1.Items.Add("Seach criteria empty");

            }
            else
                listBox1.Items.Clear();
            listBox1.Visible = false;






        }

        private void exitFilterViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Substring(0, 1) == "0") mybind.Filter = "";
                if (comboBox1.Text.Substring(0, 1) == "C") mybind.Filter = "rejection_code = 'C'";

                if (comboBox1.Text.Substring(0, 1) == "R") mybind.Filter = "record_condition_code  > '0'";

                if (comboBox1.Text.Substring(0, 1) == "V") mybind.Filter = "vin like '%" +
                                                        textBox1.Text.Trim() + "%'";
                if (comboBox1.Text.Substring(0, 1) == "L") mybind.Filter = "license_number like '%" +
                                                        textBox1.Text.Trim() + "%'";
                if (comboBox1.Text.Substring(0, 1) == "M") mybind.Filter = "make like '%" +
                                                        textBox1.Text + "%'";

                if (comboBox1.Text.Substring(0, 1) == "E") mybind.Filter = "error_code > '0'";

                if (comboBox1.Text.Substring(0, 1) == "P") mybind.Filter = "user_information like '%" +
                                                        textBox1.Text.Trim() + "%'";
                if (comboBox1.Text.Substring(0, 1) == "S") mybind.Filter = "removal_code  > '0'";


                if (comboBox1.Text.Substring(0, 1) == "B") mybind.Filter = "date_paid >" + "'" + textBox1.Text.Trim() + "'" + "AND " + "date_paid < " + "'" + textBox2.Text.Trim() + "'";





            }

            catch (ArgumentOutOfRangeException e1)
            {
                listBox1.Visible = true;
                listBox1.Items.Add("You must select a critera to select records");
                comboBox1.BackColor = Color.Red;
                comboBox1.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error beep beep " + ex);

            }



        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            basicDateValidations bs = new basicDateValidations();


            // column 11 is the parking holds action code

            if (e.ColumnIndex == 11)
            {



                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString().Length == 1)
                {
                    ssc = bs.holdValidations(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());



                    if (ssc == -1)
                    {
                        listBox1.Visible = true;
                        listBox1.Items.Add("Parking Action code must be A,D,C");
                        //this forces focus on cell 
                        dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                        e.Cancel = true;



                    }
                }



            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exceptionFrm_Resize(object sender, EventArgs e)
        {
             dataGridView1.Width =  exceptionFrm.ActiveForm.Size.Width - 100 ;
             dataGridView1.Height = exceptionFrm.ActiveForm.Size.Height - 200; 

        }
    }
     
}