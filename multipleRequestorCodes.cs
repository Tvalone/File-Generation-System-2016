using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace File_Generation_System
{
    public partial class multipleRequestorCodes : Form
    {
        public DataSet ds = new DataSet();

        public multipleRequestorCodes()
        {
            InitializeComponent();
            

            ds.ReadXml(configure.cfdb);
        }

        private void multipleRequestorCodes_Load(object sender, EventArgs e)
        {
            
            
            textBox1.DataBindings.Add("Text",ds, "fgs_config.requestor_code1");
            textBox2.DataBindings.Add("Text", ds, "fgs_config.requestor_code2");
            checkBox1.DataBindings.Add("Checked", ds, "fgs_config.use_this_requestor_code1",true);
            checkBox2.DataBindings.Add("Checked", ds, "fgs_config.use_this_requestor_code2", true);
            
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            
           ds.AcceptChanges();
           var hasChanges = ds.HasChanges();
           MessageBox.Show("has changes?" + hasChanges.ToString());
           ds.WriteXml(configure.cfdb);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
             
             
                bool isGood = Validation.IsValidReqCode(((TextBox)sender));

                 
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
             
                bool isGood = Validation.IsValidReqCode(((TextBox)sender));

                 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((((CheckBox)sender).Checked))
            {
                checkBox2.Checked = false;
            }
        }
    }
}
