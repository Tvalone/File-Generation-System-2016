using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;

namespace File_Generation_System
{
    public partial class frmTranslateText : Form
    {
        #region Variables
        OpenFileDialog dlg = new OpenFileDialog();
        String line;
        int currLine = 0;
        Regex unSeen = new Regex("\x00");
        Regex unSeen2 = new Regex("\x1a");
        Regex ilLegal = new Regex("\x02");
        Regex ilLegal2 = new Regex("\x12");
        //will implement when this problem rears its head for placement
        Regex ilLegal3 = new Regex("\x1c");
        //also unused until I find where this issue arises
        Regex row2 = new Regex(@"\s{65}");
        Regex row3 = new Regex(@"\s{37}");
        Regex reAmp = new Regex("&");
        Regex rtArrow = new Regex(">");
        Regex ltArrow = new Regex("<");
        //for misc. field
        Regex resLine = new Regex("RES");
        public static DateTime currDate = DateTime.Now;
        public static string dir = @"C:\Temp\Transfer\";
        public string path;
        public static string filepath;
        public DataSet cf;
        public static string goodFileName;
        int recCount = 0;
        StreamReader sr;
        public bool readerOk = false;
        public bool adobeProOk = false;
        public long s1;
        //bool workDone;
        #endregion

        public frmTranslateText()
        {
            InitializeComponent();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            cf = new DataSet();
            cf.ReadXml(configure.cfdb);      
            

            foreach (DataRow cfr in cf.Tables["fgs_config"].Rows)
            {

                 
                filepath = cfr["drive_files_saved_in"].ToString();
            }
             path = filepath + "\\" + "DL414_" + String.Format("{0:MMddyy}", currDate) + ".xml";
        }

        #region User Interface
        private void button1_Click(object sender, EventArgs e)
        {
            // Filter for text Files only
            dlg.InitialDirectory = filepath;
            dlg.Filter = "Text Files|*.txt";
            dlg.ShowDialog();
            txtFileName.Text = dlg.FileName;

            //Lots of customers use FGS to get the files but then try to 
            //use the dl conversion to open the files this will address

            if (txtFileName.Text.Trim().Length != 0)
            {
                //Get the last \
                
                int LastIndex = txtFileName.Text.LastIndexOf(@"\") ;

                if (txtFileName.Text.Substring(LastIndex + 1, 7) == "EPN_OUT" || txtFileName.Text.Substring(LastIndex + 1, 8) == "EPN_LIST")  
                {
                    //open in note pad - .txt default
                    Process.Start(txtFileName.Text);
                    //close DL Form window
                    this.Close();
                     
                }
            }
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            recCount = 0;
            if (txtFileName.Text == "")
            {
                MessageBox.Show("You must select a report file to view.", "Entry Error");
                txtFileName.Focus();
            }
            else
            {
                try
                {
                    //Here lies the call to the cleanFile class
                    
                     cleanFile.ReadFile(txtFileName.Text);
                     //stupid but delete needs an instance, wherase cleanfile,readfile is 
                     //static. 
                     var dclean = new cleanFile();
                     dclean.deleteEmptyLines(txtFileName.Text);
                    

                    
                    StreamWriter recordOut = new StreamWriter(
                        new FileStream(path, FileMode.Create, FileAccess.Write));
                    recordOut.WriteLine("<?xml version=\"1.0\"?>" + "\r\n" + "<dl_records>");
                    recordOut.Close();

                    //Added for progress bar
                    FileInfo f = new FileInfo(txtFileName.Text.Trim());
                    s1 = f.Length;
                
                    sr = new StreamReader(new BufferedStream(new FileStream(txtFileName.Text, FileMode.Open)));
                    line = sr.ReadLine();
                   
                             

                    if (line.Substring(0,1) != "A")
                    {
                        throw new Exception("Invalid file format. Please assure source file is valid.");
                    }
                    while (line != null)
                    {
                        currLine++;
                        if (line.Substring(0, 1) == "A")
                        {
                            
                            parseA();
                        }
                        else if (line.Substring(0, 1) == "B")
                        {
                            parseB();
                        }
                        else if (line.Substring(0, 1) == "C")
                        {
                            parseC();
                        }
                        else if (line != null && line.Substring(0, 1) == "D")
                        {
                            parseD();
                        }
                         
                    }
                    sr.Close();
                    MessageBox.Show("Please wait while your file is created.\n\nThis may take several minutes depending\non the size of your file.",
                        "Processing File");

                    //This is a call to the thread that updates the progress bar

                    progressBar1.Visible = true;
                    progressLbl.Visible = true;
                    progressLbl.Text = "Starting DL 414 Crystal Reports conversion";
                    spawn_progressBar();
        
                    

                    //new regular expression to locate '.txt' extension and replace with '.pdf' extension
                    Regex TxtToPdfName = new Regex(".TXT");
                    Regex pdfName = new Regex(".xml");

                    //code to automatically export to pdf file from Crystal Report
                    dl_records.ReadXml(path);
                    crDL4141.SetDataSource(dl_records);
                    //string replf = pdfName.Replace(txtFileName.Text, ".pdf"); 
                    //string pfilename = path + pdfName.Replace(txtFileName.Text, ".pdf");
                    //MessageBox.Show("" + replf);
              
                    //string chop = pdfName.Replace((txtFileName.Text.Substring(2, txtFileName.Text.Length - 2 )), ".pdf");
                    //string chop = pdfName.Replace(path, ".pdf");
                    //path = pdfName.Replace(path, ".pdf");
                    string goodFileName = Path.GetFileName(txtFileName.Text);
                    goodFileName = goodFileName.ToUpper();
                    goodFileName = TxtToPdfName.Replace(goodFileName, ".pdf");
                     
                    File.Delete(path);
                     
                    //string savepath = path + chop;
                    //crDL4141.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,  path + chop + pdfName.Replace(txtFileName.Text, ".pdf"));
                    //crDL4141.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,  path);
                    //crDL4141.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"C:\Temp\Transfer\DL414_" + String.Format("{0:MMddyy}", currDate)+ ".pdf");
                    crDL4141.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath + @"\" + goodFileName);
                    crDL4141.Dispose();
                    if (MessageBox.Show("Would you like to view report now?", "View Report", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // string savepath = path + txtFileName.Text.Substring(2, txtFileName.Text.Length);
                         //MessageBox.Show("" + savepath);

                        //This needs to be more strong, machine could have a subkey but not have the
                        //adobe we are looking for
                        //5.28.15 Adobe came out with DC which I guess its will not convert the version number. 
                        //version number for Xi is 11.0.11 but DC is 15.007.20033 which does not convert using
                        //toInt32 function. 

                        RegistryKey adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Adobe");
                         
                        if (adobe != null)
                        {
                            RegistryKey acroRead = adobe.OpenSubKey("Acrobat Reader");

                            if (acroRead != null) 
                            {
                            //{
                            //    string[] acroReadVersions = acroRead.GetSubKeyNames();

                            //    foreach (string versionNumber in acroReadVersions)
                            //    {
                            //        string cleanVersionNumber = versionNumber.Replace(".", "");

                            //        int woza = Convert.ToInt32(cleanVersionNumber);
                                     
                            //        if (woza > 69)
                            //        {
                                         readerOk = true;
                            //        }
                            //        else
                            //        {
                            //            readerOk = false;
                            //        }
                            //    }
                            }
                            RegistryKey acroPro = adobe.OpenSubKey("Adobe Acrobat");
                            
                            if (acroPro != null)
                            {

                                string[] acroProVersions = acroPro.GetSubKeyNames();

                                foreach (string versionNumber in acroProVersions)
                                {
                                    string cleanVersionNumber = versionNumber.Replace(".", "");

                                    int woza = Convert.ToInt32(cleanVersionNumber);
                                   
                                    if (woza > 69)
                                    {
                                        adobeProOk = true;
                                    }
                                    else
                                    {
                                        adobeProOk = false;
                                    }
                                }
                            }
                        }

                        if (adobeProOk)
                        {
                            Process.Start("acrobat", filepath + @"\" + goodFileName);
                        }
                        else if (readerOk)
                       {
                           //6.3.13 put in because C# cannot find Reader Xi
                           
                           Process.Start(filepath + @"\" + goodFileName);
                       }
                       else
                       {
                          MessageBox.Show(
                              "You require Acrobat Standard or Acrobat Reader to view your file", 
                              "Acrobat Required");
                       }


                         
                    }
                    //MessageBox.Show("Your file has been saved as " + pdfName.Replace(txtFileName.Text, ".pdf"), "File Created");
                    MessageBox.Show("Your file has been saved as " + filepath + @"\" + goodFileName ,"File Created");
                    
                
                }   
                catch (FileNotFoundException)
                {
                    MessageBox.Show(
                        "File not found.  Please enter valid filename.", "File Entry Error");
                }
                catch (DataException)
                {
                    MessageBox.Show(
                        "Data not valid.  Please assure source file is valid.", "Data Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                    MessageBox.Show(ex.StackTrace, line);
                    MessageBox.Show(line);
                    MessageBox.Show(line.Trim().Length.ToString()); 
                }

                File.Delete(txtFileName.Text);

                this.Close();


            }
            
        }
        #endregion

        public void spawn_progressBar()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(progressBar));
            
        }


         
        private void progressBar(Object Status) 
        {
        //Timer code here is takes about 2 seconds per kilobyte, unsure of the step value or how to slow it down

        this.progressBar1.Step = 1;
                 
        //this currently works for a 60k file 
         
        //Just multi the file size and it appears to work well on my machine, for small 60kb reports MB files not so good
 
        this.progressBar1.Maximum = (int)s1 * 10 + 1;
       
          

       progressLbl.Text = "Processing File ";
         

        for(int i=progressBar1.Minimum; i<=progressBar1.Maximum; i++)

           {
       
                     progressBar1.PerformStep();
                    
                     
           }

           progressLbl.Text = "Processing Complete";
    }


        #region Build XML Records
        private void parseA()
        {
            if (line.Substring(0, 1) == "A")
            {
                StreamWriter recordAOut = new StreamWriter(
                    new FileStream(path, FileMode.Append, FileAccess.Write));
                //write row 1 of record

                string miscText = Regex.Replace(line.Substring(30, 21), "&", "&amp;");
               miscText = Regex.Replace(miscText, ">", "&gt;");
               miscText = Regex.Replace(miscText, "<", "&lt;");

                recordAOut.WriteLine("\t" + "<dl_record>" + "\r\n"
                        + "\t\t" + "<dlnumber>" + line.Substring(3, 8) + "</dlnumber>" + "\r\n"
                        + "\t\t" + "<fo_bates>" + unSeen.Replace((line.Substring(12, 7))," ") + "</fo_bates>" + "\r\n"
                        + "\t\t" + "<type_app>" + line.Substring(20, 8) + "</type_app>" + "\r\n"
                        + "\t\t" + "<misc_info>" + miscText + "</misc_info>" + "\r\n"
                        //+ "\t\t" + "<misc_info>" + ltArrow.Replace(rtArrow.Replace((line.Substring(30, 21)),"&gt;"),"&lt;") + "</misc_info>" + "\r\n"
                        + "\t\t" + "<req_code>" + line.Substring(51, 5) + "</req_code>" + "\r\n"
                        + "\t\t" + "<rec_date>" + line.Substring(57, 6) + "</rec_date>" + "\r\n"
                        + "\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>"); 
                                                        
                currLine++;


                line = sr.ReadLine();

                //In the A record loop just check for AKA not sure that the substr will always be perfect. 
                
                if (line.Contains("AKA:"))  
                {
                     recordAOut.WriteLine("\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(66)), "&amp;") + "</name_or_addr>");
                     currLine++;
                     line = sr.ReadLine();
                }  

                

                if (line.Substring(0, 1) == "B" || line.Substring(0, 1) == "C")
                {
                    //if record continuation, then finish section A; otherwise move to row 3
                    recordAOut.Close();
                    return;
                }
                else
                {
                    //if not continuation, write row 3
                    if (line.Length < 65)
                    {
                        line = unSeen2.Replace(line, " ");
                        recordAOut.WriteLine("\t\t" + "<dmv_info_line>" + ilLegal.Replace(unSeen.Replace((line.Substring(37)), " ")," ") + "</dmv_info_line>");
                    }
                    else
                    {
                        line = unSeen2.Replace(line, " ");
                        recordAOut.WriteLine("\t\t" + "<dmv_info_line>" + ilLegal.Replace(unSeen.Replace((line.Substring(37, 21)), " ")," ") + "</dmv_info_line>" + "\r\n"
                            + "\t\t" + "<name_or_addr>" + " " + line.Substring(64) + "</name_or_addr>");
                    }
                    currLine++;
                    line = sr.ReadLine();
                    
                }
                if (line.Substring(0,1) == "B" || line.Substring(0,1) == "C")
                {
                    //check for row 4; if not exist, go back
                    recordAOut.Close();
                    return;
                }
                else
                {
                    //otherwise, write row 4
                    if (line.Length < 11)
                    {
                        recordAOut.WriteLine("\t\t" + "<birthdate>" + line.Substring(3, 6) + "</birthdate>");
                    }
                    else if (line.Length > 10 && line.Length < 55)
                    {
                        recordAOut.WriteLine("\t\t" + "<birthdate>" + line.Substring(3, 6) + "</birthdate>" + "\r\n"
                                + "\t\t" + "<sex>" + line.Substring(11, 1) + "</sex>" + "\r\n"
                                + "\t\t" + "<height>" + line.Substring(14, 3) + "</height>" + "\r\n"
                                + "\t\t" + "<weight>" + line.Substring(18, 3) + "</weight>" + "\r\n"
                                + "\t\t" + "<eyes>" + line.Substring(22, 5) + "</eyes>" + "\r\n"
                                + "\t\t" + "<hair>" + line.Substring(28) + "</hair>");
                    }
                    //check to see if row 4 has additional data
                    else if (line.Length > 55 && line.Length < 65)
                    {
                        recordAOut.WriteLine("\t\t" + "<birthdate>" + line.Substring(3, 6) + "</birthdate>" + "\r\n"
                                + "\t\t" + "<sex>" + line.Substring(11, 1) + "</sex>" + "\r\n"
                                + "\t\t" + "<height>" + line.Substring(14, 3) + "</height>" + "\r\n"
                                + "\t\t" + "<weight>" + line.Substring(18, 3) + "</weight>" + "\r\n"
                                + "\t\t" + "<eyes>" + line.Substring(22, 5) + "</eyes>" + "\r\n"
                                + "\t\t" + "<hair>" + line.Substring(28, 5) + "</hair>" + "\r\n" 
                                + "\t\t" + "<vol_req>" + reAmp.Replace((line.Substring(49, 7)), "&amp;") + "</vol_req>");
                    }
                    else if (line.Length > 65)
                    {
                        recordAOut.WriteLine("\t\t" + "<birthdate>" + line.Substring(3, 6) + "</birthdate>" + "\r\n"
                                + "\t\t" + "<sex>" + line.Substring(11, 1) + "</sex>" + "\r\n"
                                + "\t\t" + "<height>" + line.Substring(14, 3) + "</height>" + "\r\n"
                                + "\t\t" + "<weight>" + line.Substring(18, 3) + "</weight>" + "\r\n"
                                + "\t\t" + "<eyes>" + line.Substring(22, 5) + "</eyes>" + "\r\n"
                                + "\t\t" + "<hair>" + line.Substring(28, 5) + "</hair>" + "\r\n"
                                + "\t\t" + "<vol_req>" + reAmp.Replace((line.Substring(49, 7)), "&amp;") + "</vol_req>" + "\r\n"
                                + "\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                    }
                }
                currLine++;
                line = sr.ReadLine();

                 //6.2.15 captures lines with state abrev and a date, not sure what it means. 
                
                //Alternate pattern record found after bdate and before RES: 12.7.15

                if (line.Length == 107)
                {
                     
                        recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(66).Trim() + "</name_or_addr>");
                        currLine++;
                        line = sr.ReadLine(); 
                    
                }
                //Second line in the above issue 5.29.15 
                if (line.Contains("RES:"))
                {
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(66) + "</name_or_addr>");
                    currLine++;
                    line = sr.ReadLine();
                }

                //This line will contain a C and the OTHER string so write out both
                //Need to find Other position to allow for no error substringing
                if (line.Contains("OTHER:") && line.Contains("C")) 
                {
                     
                    int pos = line.IndexOf("OTHER:");
                    int pos_lic_type = line.IndexOf("C");
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(pos) + "</name_or_addr>");
                    recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace(line.Substring(pos_lic_type,4), "&amp;") + "</lic_class>");
                   // currLine++;
                   // line = sr.ReadLine();
                } 

                //Trying to address a A parse line with the licence types and an address  12/14/15
                 
                if (line.Contains("C") && line.Length == 91)
                {

                    
                    int pos_lic_type = line.IndexOf("C");
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(9) + "</name_or_addr>");
                    if (pos_lic_type == 5)
                    {
                        recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace(line.Substring(pos_lic_type, 4), "&amp;") + "</lic_class>");
                    }
                    // currLine++;
                    // line = sr.ReadLine();
                } 
                                
                //check if next section or row 5
                if (line.Substring(0, 1) == "C")
                {
                    recordAOut.Close();
                    return;
                }

                //Some records have alot of extra address lines this is an attempt to read write before getting to the license

                //if (line.Length > 85 && line.Length < 108)
                //{
                //    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(107) + "</name_or_addr>");
                //}
                //currLine++;
                //line = sr.ReadLine();
                //if (line.Length > 85 && line.Length < 108)
                //{
                //    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(86) + "</name_or_addr>");
                //}
                //currLine++;
                //line = sr.ReadLine();

                //check for length of row 5 and write out accordingly 4/30/14 put trim in here to avoid
                //substring error j9/26/14 Added line length = 66 to prevent substring error. 
                //12/01/14 commented line out, was causing error with C&M1, while this code does not appear to
                //be active, but will test.
                //if (line.Trim().Length > 10 && line.Substring(66, 3) == "RES")
                ////if (line.Trim().Length == 66 && line.Substring(66, 3) == "RES")

                //{
                //    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(64) + "</name_or_addr>");
                //    currLine++;
                //    line = sr.ReadLine();

                   
                //}

                //check for length of row X and write out accordingly - 4/24/14 code fix? 
                //if (line.Length > 10 && line.Substring(64, 6) == "OTHER")
                //{
                //    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line  + "</name_or_addr>");
                //    currLine++;
                //    line = sr.ReadLine();


                //}
                //write row 6 11/25/14 had file with C&M1 - increased the line length to < 12 and line.Substring(4) from 3. 
                //Not sure about above , but found that we were missing the license class code so if the line len is 11
                //and the last line is eq C (probably need to iterate all) then write out. 
                if (line.Length > 3 && line.Length <= 17)
                {
                    var Aout_work = line.Substring(2);
                    Aout_work = reAmp.Replace((line), "&amp;");
                    Aout_work  = Aout_work.Trim();
                    recordAOut.WriteLine("\t\t" + "<lic_class>" + Aout_work + "</lic_class>");
                                     
                   if (line.Length == 11)
                       if (line.Substring(10, 1) == "C")
                       {
                           recordAOut.WriteLine("\t\t" + "<lic_class>" + line.Substring(10, 1) + "</lic_class>");
                       }

                   if (line.Length == 6)
                       if (line.Substring(5, 1) == "B")
                       {
                           recordAOut.WriteLine("\t\t" + "<lic_class>" + line.Substring(5, 1) + "</lic_class>");
                       }
                       else
                    //int pos = line.IndexOf('&');
                    //var test = "<lic_class>" + reAmp.Replace((line.Substring(4).Trim()), "&amp;") + "</lic_class>";
                  recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace((line), "&amp;") + "</lic_class>");
                 // recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace((line), "&amp;") + "</lic_class>");
                                                                  
                }
                //Added the B and C check as this fallthru logic does not address Unable to ID records 11/25/14 commented out
               //if (line.Length == 92)  
               //  {
               //      var test = "<lic_class>" + reAmp.Replace((line.Substring(7).Trim()), "&amp;") + "</lic_class>";
               //      recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace((line.Substring(7,4)), "&amp;") + "</lic_class>" + "\r\n"
               //          + "\t\t" + "<name_or_addr>" + line.Substring(68) + "</name_or_addr>");
               //  } 
               //12.4.15 commented out else due to vagrant data above
               //else 
                //if (line.Contains("C") && line.Length == 11)
                //{
                //    recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace(line,"&amp".Trim()) + "</lic_class>");
                //}

                //if (line.Contains("C") && line.Length == 6)
                //{
                //    recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace(line, "&amp").Trim() + "</lic_class>");
                //}
                 
                currLine++;
                line = sr.ReadLine();
                //Made the line live, it had been commented out, appears an error record will have just an A and C record and get to here. 
                if (line.Substring(0, 1) == "C")
                {
                    recordAOut.Close();
                    return;
                }
                

                //write row 7
                if (line.Length < 65)
                {
                    recordAOut.WriteLine("\t\t" + "<lic_class_name>" + line.Substring(3, 5) + "</lic_class_name>" + "\r\n"
                            + "\t\t" + "<lic_issue>" + line.Substring(9, 6) + "</lic_issue>" + "\r\n"
                            + "\t\t" + "<lic_expire>" + line.Substring(16, 6) + "</lic_expire>" + "\r\n"
                            + "\t\t" + "<ext>" + line.Substring(23, 3) + "</ext>" + "\r\n"
                            + "\t\t" + "<restrict>" + line.Substring(26, 11) + "</restrict>" + "\r\n"
                            + "\t\t" + "<dup_lic_iss>" + line.Substring(38, 13) + "</dup_lic_iss>" + "\r\n"
                            + "\t\t" + "<lic_held>" + line.Substring(52, 4) + "</lic_held>" + "\r\n"
                            + "\t\t" + "<seq_no>" + line.Substring(57, 4) + "</seq_no>");
                }
                else
                {
                    recordAOut.WriteLine("\t\t" + "<lic_class_name>" + line.Substring(3, 5) + "</lic_class_name>" + "\r\n"
                            + "\t\t" + "<lic_issue>" + line.Substring(9, 6) + "</lic_issue>" + "\r\n"
                            + "\t\t" + "<lic_expire>" + line.Substring(16, 6) + "</lic_expire>" + "\r\n"
                            + "\t\t" + "<ext>" + line.Substring(23, 3) + "</ext>" + "\r\n"
                            + "\t\t" + "<restrict>" + line.Substring(26, 11) + "</restrict>" + "\r\n"
                            + "\t\t" + "<dup_lic_iss>" + line.Substring(38, 13) + "</dup_lic_iss>" + "\r\n"
                            + "\t\t" + "<lic_held>" + line.Substring(52, 4) + "</lic_held>" + "\r\n"
                            + "\t\t" + "<seq_no>" + line.Substring(57, 4) + "</seq_no>" + "\r\n"
                            + "\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                }
                currLine++;
                line = sr.ReadLine();

                
                //write row 8 if it exists
                if (line.Length == 0)
                {
                    currLine++;
                    line = sr.ReadLine();
                    
                }
                //4/23/14 assumtion of last line, one account have several more lines

                if (line.Substring(0,1) != "B" && line.Substring(0,1) != "C")
                {
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                    currLine++;
                    line = sr.ReadLine();
                }
                 

                //check for length of row X and write out accordingly - 4/24/14 code fix? 
               // if (line.Length > 10 && line.Substring(64, 5) == "OTHER")
               // {
               //     recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Trim() + "</name_or_addr>");
               //     currLine++;
               //     line = sr.ReadLine();


                //}

                //if (line.Substring(0, 1) != "B" && line.Substring(0, 1) != "C")
                //{
                //    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + line.Substring(64) + "</name_or_addr>");
                //    currLine++;
                //    line = sr.ReadLine();

                //}
                 

                recordAOut.Close();
            }
            return;
        }

        private void parseB()
        {
            StreamWriter recordBOut = new StreamWriter(
                    new FileStream(path, FileMode.Append, FileAccess.Write));
            //MessageBox.Show("Begin parsing section B");
            recordBOut.WriteLine("\t\t" + "<abstract>" + reAmp.Replace((line.Substring(2)),"&amp;") + "</abstract>");
            currLine++;
            line = sr.ReadLine();
            
            while (line.Substring(0,1) == " ")
            {
                recordBOut.WriteLine("\t\t" + "<abstract>" + reAmp.Replace((line.Substring(2)), "&amp;") + "</abstract>");
                currLine++;
                line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    line = sr.ReadLine();
                }
            }
            recordBOut.Close();
            return;
        }

        private void parseC()
        {
            String absString;
            String reqString;
            StreamWriter recordCOut = new StreamWriter(
                    new FileStream(path, FileMode.Append, FileAccess.Write));
            //MessageBox.Show("Begin parsing section C")
            absString = "\t\t" + "<comment>" + reAmp.Replace((line.Substring(2, 61)), "&amp;") + "</comment>" + "\r\n";
            reqString = "\t\t" + "<req_name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</req_name_or_addr>" + "\r\n";
            currLine++;
            while ((line = sr.ReadLine()) != null && line.Substring(0, 1) == " ")
            {
                if (line.Length > 65)
                {
                    absString += "\t\t" + "<comment>" + reAmp.Replace((line.Substring(2, 61)), "&amp;") + "</comment>" + "\r\n";
                    reqString += "\t\t" + "<req_name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</req_name_or_addr>" + "\r\n";
                }
                else
                {
                    absString += "\t\t" + "<comment>" + reAmp.Replace((line.Substring(2)), "&amp;") + "</comment>" + "\r\n";
                }
                currLine++;
            }
            recordCOut.Write(absString);
            recordCOut.Write(reqString);
            if (line != null && line.Substring(0, 1) == "D")
            {
                recordCOut.Close();
                return;
            }
            else if (line != null && line.Substring(0, 1) == "A")
            {
                recordCOut.WriteLine("\t" + "</dl_record>");
                recordCOut.Close();
                ++recCount;
                //MessageBox.Show("End of records.\nPage Count=" + recCount);
                return;
            }
            else
            {
                recordCOut.WriteLine("\t" + "</dl_record>" + "\r\n" + "</dl_records>");
                recordCOut.Close();
                ++recCount;
                MessageBox.Show("End of records.\nPage Count=" + recCount, "End of Records");
                return;
            }
        }

        private void parseD()
        {
            StreamWriter recordDOut = new StreamWriter(
                    new FileStream(path, FileMode.Append, FileAccess.Write));            
            //MessageBox.Show("Begin parsing section D");
            recordDOut.WriteLine("\t\t" + "<action>" + reAmp.Replace((line.Substring(2)), "&amp;") + "</action>");
            currLine++;
            
            while ((line = sr.ReadLine()) != null && line.Substring(0, 1) == " ")
            {
                recordDOut.WriteLine("\t\t" + "<action>" + reAmp.Replace((line.Substring(2)), "&amp;") + "</action>");
                currLine++;   
                }
            if (line != null)
            {
                recordDOut.WriteLine("\t" + "</dl_record>");
                recordDOut.Close();
                ++recCount;
                //MessageBox.Show("End of record.\nRecord Count=" + recCount);
                return;
            }
            else
            {
                recordDOut.WriteLine("\t" + "</dl_record>" + "\r\n" + "</dl_records>");
                recordDOut.Close();
                ++recCount;
                MessageBox.Show("End of records.\nPage Count=" + recCount, "End of Records");
                return;
            }
        }


        #endregion

        private void frmTranslateText_Load(object sender, EventArgs e)
        {
           // dlFileWatcher.Path = filepath;
           // dlFileWatcher.Filter = "DLINQ_OUT*.TXT";
         
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

             
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void dlFileWatcher_Created(object sender, FileSystemEventArgs e)
        {




           
            
            
        }

        private void dlFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("changed");
        }

        private void dlFileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("deleted");
        }

        private void dlFileWatcher_Renamed(object sender, RenamedEventArgs e)
        {

            //MessageBox.Show("renamed");
        }

         


        
    }
}