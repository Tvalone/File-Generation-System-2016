using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace File_Generation_System
{
    class dlConversion
    {
        String line;
        StreamReader sr;
        int currLine = 0;
        String path;
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
        int recCount = 0;
        //DL414 dl = new crDL414();


        


        public void dl414Conversion(String path,String fullPath)
        {

            try
            {

                StreamWriter recordOut = new StreamWriter(
                                new FileStream(path, FileMode.Create, FileAccess.Write));
                recordOut.WriteLine("<?xml version=\"1.0\"?>" + "\r\n" + "<dl_records>");
                recordOut.Close();

                sr = new StreamReader(new BufferedStream(new FileStream(fullPath, FileMode.Open)));
                line = sr.ReadLine();

                if (line.Substring(0, 1) != "A")
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

                //new regular expression to locate '.txt' extension and replace with '.pdf' extension
                Regex TxtToPdfName = new Regex(".TXT");
                Regex pdfName = new Regex(".xml");

                //code to automatically export to pdf file from Crystal Report
                dl_records dlr = new dl_records();
                dlr.ReadXml(path);
                crDL414 crl = new crDL414();

                crl.SetDataSource(dlr);

                string goodFileName = fullPath.ToUpper();

                goodFileName = TxtToPdfName.Replace(goodFileName, ".pdf");
                File.Delete(path);
                 
                crl.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, goodFileName);
                crl.Dispose();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error=" + Ex);
            }

            
        }



        

             private void parseA()
        {
            if (line.Substring(0, 1) == "A")
            {
                StreamWriter recordAOut = new StreamWriter(
                    new FileStream(this.Path, FileMode.Append, FileAccess.Write));
                //write row 1 of record
                recordAOut.WriteLine("\t" + "<dl_record>" + "\r\n"
                        + "\t\t" + "<dlnumber>" + line.Substring(3, 8) + "</dlnumber>" + "\r\n"
                        + "\t\t" + "<fo_bates>" + unSeen.Replace((line.Substring(12, 7))," ") + "</fo_bates>" + "\r\n"
                        + "\t\t" + "<type_app>" + line.Substring(20, 8) + "</type_app>" + "\r\n"
                        + "\t\t" + "<misc_info>" + ltArrow.Replace(rtArrow.Replace((line.Substring(30, 21)),"&gt;"),"&lt;") + "</misc_info>" + "\r\n"
                        + "\t\t" + "<req_code>" + line.Substring(51, 5) + "</req_code>" + "\r\n"
                        + "\t\t" + "<rec_date>" + line.Substring(57, 6) + "</rec_date>" + "\r\n"
                        + "\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                currLine++;


                line = sr.ReadLine();
                
                
                if (line.Length > 59 && row2.Match(line.Substring(1, 65)).Success)
                {
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                    //write row 2 of record if it exist
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
                            + "\t\t" + "<name_or_addr>" + " " + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
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

                                
                //check if next section or row 5
                if (line.Substring(0, 1) == "C")
                {
                    recordAOut.Close();
                    return;
                }
                //check for length of row 5 and write out accordingly
                if (line.Length > 10 && line.Substring(66, 3) == "RES")
                {
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                    currLine++;
                    line = sr.ReadLine();

                   
                }
                //write row 6
                if (line.Length > 3 && line.Length < 10)
                {
                    recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace((line.Substring(3)), "&amp;") + "</lic_class>");
                }
                else if (line.Length > 10)
                {
                    recordAOut.WriteLine("\t\t" + "<lic_class>" + reAmp.Replace((line.Substring(3, 5)), "&amp;") + "</lic_class>" + "\r\n"
                        + "\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(63)), "&amp;") + "</name_or_addr>");
                }
                    
                else
                {
                    recordAOut.WriteLine("\t\t" + "<lic_class>   </lic_class>");
                }
                 
                currLine++;
                line = sr.ReadLine();

                
                

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
                if (line.Substring(0,1) != "B" && line.Substring(0,1) != "C")
                {
                    recordAOut.WriteLine("\t\t" + "<name_or_addr>" + reAmp.Replace((line.Substring(64)), "&amp;") + "</name_or_addr>");
                    currLine++;
                    line = sr.ReadLine();
                    
                }

                recordAOut.Close();
            }
            return;
        }

        private void parseB()
        {
            StreamWriter recordBOut = new StreamWriter(
                    new FileStream(this.path, FileMode.Append, FileAccess.Write));
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
                    new FileStream(this.path, FileMode.Append, FileAccess.Write));
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
                    new FileStream(this.path, FileMode.Append, FileAccess.Write));            
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

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

   
    } //end of class

 }//end of namespace 


    
 
 
 
 
