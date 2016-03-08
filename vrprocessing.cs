using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace File_Generation_System
{
    public partial class vrprocessing : Form
    {
        long s1;

        public vrprocessing()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vrprocessing_Load(object sender, EventArgs e)
        {
            progressLbl.Text = "Reading VR Inquiry File";

            MessageBox.Show("" + process.process_file.Length);
            FileInfo f = new FileInfo(process.process_file);
            s1 = f.Length;

            spawn_progressBar();
            
        }

        public void spawn_progressBar()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(progressBar));

        }

        private void progressBar(Object Status)
        {
            //Timer code here is takes about 2 seconds per kilobyte, unsure of the step value or how to slow it down

            this.progressBar1.Step = 1;

            //this currently works for a 60k file 

            //Just multi the file size and it appers to work well on my machine, for small 60kb reports MB files not so good

            this.progressBar1.Maximum = (int)s1 * 10 + 1;



            progressLbl.Text = "Processing File ";


            for (int i = progressBar1.Minimum; i <= progressBar1.Maximum; i++)
            {

                progressBar1.PerformStep();


            }

            progressLbl.Text = "Processing Complete";
        }
    }
}