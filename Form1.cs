using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace File_Generation_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void transactionMenusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void exitFGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configureFGSForUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new config_fgs();
            form.ShowDialog(); // show form modally


        }
    }
}