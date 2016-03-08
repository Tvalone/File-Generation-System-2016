using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RestApiSFT;

namespace File_Generation_System
{
    
    class sendReceive
    {
        public StreamWriter sa;

        public sendReceive()
        {
            
       

            create_LOG_File();
            

            //Get data from configuration file
            configure.getInformationFromConfig();
            //used to determine if chilkat interaction is successfull
            bool success;
            //Create a FTP2 instance of Chilkat 
            //Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
            ftp = new Chilkat.Ftp2();
            
            //Unlock key for Chilkat
            success = ftp.UnlockComponent("DMVCAGFTP_rk6c67zZ4XnK");
            //need to add to configure class. 
            ftp.Hostname = configure.hostname;
            ftp.Username = configure.userid;
            //string certName = "dmv-ddt-" + configure.userid + "-x509";
            //For the DMV Server I removed the string dmv-ddt-
            string certName = configure.userid.Trim() + "-x509";
            //Changed to False to trouble shoot problem
            ftp.AuthSsl = true;
            ftp.AuthTls = true;
            ftp.Ssl = false;
            ftp.Port = 2121;
            ftp.Passive = true;
            ftp.UseEpsv = true;
            ftp.ConnectTimeout = 20000;
            ftp.SetTypeAscii();
            var version = ftp.Version;
            //Add verbose loggging for trouble shooting
            ftp.VerboseLogging = true;
            
             
            ftp.KeepSessionLog = true;

            var readOnly = true;
            //Get todays date for cert compare
            DateTime Today1 = DateTime.Today;
            Chilkat.Cert cert = new Chilkat.Cert();
            //Create CertStore, Open then obtains current machine values
            Chilkat.CertStore certStore = new Chilkat.CertStore();
            certStore.OpenCurrentUserStore(readOnly);
            

            var numCert = certStore.NumCertificates;

            //set to false before iterating, should set to true if valid
            success = false;
            for (int i = 0; i < numCert; i++)
            {
                var lcert = certStore.GetCertificate(i);

                //only match DMV certs
                if (lcert.SubjectCN.ToString() == certName) 
                {
                     //get certs exipre date
                    DateTime expireDate = lcert.ValidTo;
                    //Calc diff from curent date to exipre date
                    var Days = (expireDate - Today1).Days;
                    //If Certificates have 30's or less, start nagging 
                    if (Days <= 30 && lcert.Expired != true)
                    {

                        MessageBox.Show("Certificate will expire in " + Days + " Days " + "Thumbprint " + lcert.Sha1Thumbprint.ToString(), "Get New Cert From DMV");
                    } 

                    if (lcert.Expired)
                  
                    { MessageBox.Show("This Cert is Expired  " + lcert.Sha1Thumbprint.ToString()," FGS will attempt to find a non expired cert  ");
               
                      
                    }
                    //8.10.15 testing for version 9.5.0.55
                    if (!lcert.Expired)
                    { 
                       success = cert.LoadByCommonName(certName);
                        cert = certStore.FindCertBySubjectCN(certName);
                        var certLastError = cert.LastErrorText.ToString();
                        var certStoreLastError = certStore.LastErrorText.ToString();
                    }
                }
                 
            }
            
            if (success == true)
            {
               success =  ftp.SetSslClientCert(cert);
               write_to_LOG();
              
            }

            if (success != true)
            {
                MessageBox.Show("Certificate not installed", "Certificate Error");
                write_to_LOG();
                
                return;
            }

             


            success = ftp.Connect();

            if (success != true)
            {

                write_to_LOG();
                MessageBox.Show("Error" + ftp.LastErrorText);
               
             
                return;
            }

           
        }
        public void create_LOG_File()
        {
            DateTime currDate = DateTime.Now;

            string logfilename = configure.filepath + @"\" + String.Format("{0:MMddyy}", currDate) + "log.txt";

            FileStream logFileStream = new FileStream(logfilename, FileMode.Create, FileAccess.Write);
            
            sa = new StreamWriter(logFileStream);
        }

        public void write_to_LOG()
        {

             

             sa.Write(ftp.LastErrorText.ToString());
             sa.Write(ftp.SessionLog.ToString());
             
               
              
            
             

            //  The possible failure reasons are:
            //  0 = success
            //  Non-SSL socket fail reasons:
            //   1 = empty hostname
            //   2 = DNS lookup failed
            //   3 = DNS timeout
            //   4 = Aborted by application.
            //   5 = Internal failure.
            //   6 = Connect Timed Out
            //   7 = Connect Rejected (or failed for some other reason)
            //  SSL failure reasons:
            //   100 = Internal schannel error
            //   101 = Failed to create credentials
            //   102 = Failed to send initial message to proxy.
            //   103 = Handshake failed.
            //   104 = Failed to obtain remote certificate.
            //  300 = asynch op in progress
            //  301 = login failure.
            int failReason;
            failReason = ftp.ConnectFailReason;
            //MessageBox.Show("Fail Reason=  " + failReason.ToString());
            //put here for now.
           

        }
        public void close_LOG_file()
        {
            sa.Close();
        }

        //Variables dont lose em
        bool success;
        Chilkat.Ftp2 ftp;

        
        public void receive()
        {

            //This is the REST API class library - Set the userId from the config
            //The GetFiles and Delete is wrapped in a try just in case certifcate is not set
            //or some other bizzare error unfolds. This hopefully avoids the whole DMVData vs dmv-ddt-router issue
            //Customers will need to have their subscription folder altered to DMVData. 

            RestApiSFT.SetUserid.Userid = configure.userid;
            RestApiSFT.SetURL.URL = "https://" + configure.hostname + ":443/api/v1.1/files/DMVData/FromDMV/";

            try
            {
                //Call Get Files if no files then message box, if files delete them. 
                var file_count = RestApiSFT.SecureTransportAPI.GetFiles();
                if (file_count == 0)
                {
                    MessageBox.Show("No Files in Account", "SFT Account");
                }

                else if (file_count > 0)
                {

                    RestApiSFT.SecureTransportAPI.DeleteFiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - Certificate Not Installed?" + ex.ToString(), "RESTAPI");
            }

            try
            {
                success = ftp.ChangeRemoteDir("/DMVData/FromDMV");

                if (success != true)
                {
                    success = ftp.ChangeRemoteDir("/dmv-ddt-router/FromDMV");
                }

                 if (success != true)
                {
                    
                    MessageBox.Show(ftp.LastErrorText + "\n");
                    write_to_LOG();
                    return;
                }

                //This gets the number of files in the current directory


                int getAllFiles = ftp.MGetFiles("*.*", configure.filepath);

                if (getAllFiles != 0)
                {
                    for (int i = 0; i < getAllFiles; i++)
                    {
                        MessageBox.Show("Downloading File " + ftp.GetFilename(i) , "File Download");
                        //MessageBox.Show("Deleting files from your SFT Account", "Deleting Files");
                        ftp.DeleteMatching("*.*");
                    }
                }

                if (getAllFiles == 0)
                {
                    MessageBox.Show("There are no files in your SFT account", "SFT Account Empty");
                }

                if (getAllFiles < 0)
                {
                    MessageBox.Show(ftp.LastErrorText);
                    return;
                }

                 
                  }
                 catch (UriFormatException f)
                  {
                       Console.WriteLine("Invalid URL" + f);
                  }
                  catch (IOException f)
                  {
                       Console.WriteLine("Could not connect to URL" + f.ToString());
                  }

            close_LOG_file();
             
            }
            public void Send(Char Flag) 
            {

                //This is the REST API class library - Set the userId from the config
                //The GetFiles and Delete is wrapped in a try just in case certifcate is not set
                //or some other bizzare error unfolds. This hopefully avoids the whole DMVData vs dmv-ddt-router issue
                //Customers will need to have their subscription folder altered to DMVData. Still need you use
                //local delete otherwise can gut FTPS code when ready. Would like to write a native C# JSO
                //so we don't have to rely on third party code. 

                RestApiSFT.SetUserid.Userid = configure.userid;
                RestApiSFT.SetURL.URL = "https://" + configure.hostname + ":443/api/v1.1/files/DMVData/SendToDMV/";
                RestApiSFT.SetDir.DIR = configure.filepath;

                try
                {
                    //Call Send Files, Should be in an array or loop 
                    string[] files = new string[]{"dlinquiry","epn","court","vrinquiry","parkhold","veseltax","dlist"};
                    for (int x = 0; x < 7; x++)
                    {
                        RestApiSFT.SetFileName.FileName = files[x] + ".txt";
                        var ok = RestApiSFT.SecureTransportAPI.PutFiles();
                        if (ok)
                        {
                            MessageBox.Show(files[x] + " Sent Succesfully. ", "File Sent");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error - Certificate Not Installed?" + ex.ToString(), "RESTAPI");
                }
                configure.getInformationFromConfig();

                //First Try to change to DMVData Folder

                success = ftp.ChangeRemoteDir("/DMVData/SendToDMV");

                //If not succesfull then try to change to dmv-ddt-router folder

                if (success != true)
                {
                    success = ftp.ChangeRemoteDir("/dmv-ddt-router/SendToDMV");
                }
                if (success != true)
                {
                    MessageBox.Show(ftp.LastErrorText);
                    write_to_LOG();
                    return;
                }
                 
                if (Flag == 'D')
                {
                    FileInfo dlinquiryFile = new FileInfo(configure.filepath + @"\" + "dlinquiry.txt");
                    FileInfo epnFile = new FileInfo(configure.filepath + @"\" + "epn.txt");
                    FileInfo courtFile = new FileInfo(configure.filepath + @"\" + "court.txt");
                    FileInfo dlistFile = new FileInfo(configure.filepath + @"\" + "dlist.txt");
                    if ((dlinquiryFile.Exists == true) && (dlinquiryFile.Length > 0))
                    {
                       success = ftp.PutFile(configure.filepath + @"\" + "dlinquiry.txt", "dlinquiry.txt");
                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("DL Inquiry File Sent Succesfully. " , "DL Inquiry File Sent");
                            write_to_LOG();
                            dlinquiryFile.Delete();
                        }

                    }
                    //Drivers License Inquiry file not there
                    if (dlinquiryFile.Exists == false)
                    {
                        //MessageBox.Show("Drivers License Inquiry File Empty", "File Not Sent");
                    }
                    if ((epnFile.Exists == true) && (epnFile.Length > 0))
                    {
                       success = ftp.PutFile(configure.filepath + @"\" + "epn.txt", "epn.txt");


                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("EPN File Sent Succesfully.   "  , "EPN File Sent");
                            write_to_LOG();
                            epnFile.Delete();
                        }
                        
                    }
                    if (epnFile.Exists == false)
                    {
                        //MessageBox.Show("EPN File Empty", "File Not Sent");
                    }
                    if ((courtFile.Exists == true) && (courtFile.Length > 0))
                    {

                        success = ftp.PutFile(configure.filepath + @"\" + "court.txt", "court.txt");


                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("Court File Sent Succesfully.   "  , "Court Abstract File Sent");

                            courtFile.Delete();
                        }

                    }
                    if (courtFile.Exists == false)
                    {
                        //MessageBox.Show("Court File Empty", "File Not Sent");
                    }
                    if ((dlistFile.Exists == true) && (dlistFile.Length > 0))
                    {

                        success = ftp.PutFile(configure.filepath + @"\" + "dlist.txt", "dlist.txt");


                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("Drivers List File Sent Succesfully.  " , "Drivers List File");

                            dlistFile.Delete();
                        }
                        //Changed to over all no files but does not work because deletes are up above comment out 
                        //for now until rewrite.
                       // if (dlistFile.Exists == false && epnFile.Exists == false && courtFile.Exists == false)
                       // {
                       //     MessageBox.Show("No files to send", "There are no files to send!");
                       // }

                    }
                    

                //Ends DL 
                }
                if (Flag == 'V')
                {

                    FileInfo vrinquiryFile = new FileInfo(configure.filepath + @"\" + "vrinquiry.txt");
                    FileInfo vrholdFile = new FileInfo(configure.filepath + @"\" + "parkhold.txt");
                    FileInfo veseltaxFile = new FileInfo(configure.filepath + @"\" + "veseltax.txt");


                    if ((vrinquiryFile.Exists == true) && (vrinquiryFile.Length > 0))
                    {
                        success = ftp.PutFile(configure.filepath + @"\" + "vrinquiry.txt", "vrinquiry.txt");
                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("VR Inquiry File Sent Succesfully. ", "VR Inquiry File Sent");

                            vrinquiryFile.Delete();
                        }

                    }
                    //Vehicle Inquiry file not there
                    if (vrinquiryFile.Exists == false)
                    {
                       // MessageBox.Show("Vehicle Inquiry File Empty", "File Not Sent");
                    } 
                    //Parking Holds
                    if ((vrholdFile.Exists == true) && (vrholdFile.Length > 0))
                    {
                        success = ftp.PutFile(configure.filepath + @"\" + "parkhold.txt", "parkhold.txt");


                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("Parking Holds File Sent Succesfully.   ", "Holds File Sent");

                            vrholdFile.Delete();
                        }

                    }
                    if (vrholdFile.Exists == false)
                    {
                       // MessageBox.Show("Parking Holds File Empty", "File Not Sent");
                    } 
                    //Vessel Tax
                    if ((veseltaxFile.Exists == true) && (veseltaxFile.Length > 0))
                    {

                        success = ftp.PutFile(configure.filepath + @"\" + "veseltax.txt", "veseltax.txt");


                        if (success != true)
                        {
                            MessageBox.Show(ftp.LastErrorText);
                            write_to_LOG();
                            return;
                        }

                        if (success == true)
                        {
                            MessageBox.Show("Vessel Tax File Sent Succesfully.   ", "Vessel Tax File Sent");

                            veseltaxFile.Delete();
                        }

                    }
                    if (veseltaxFile.Exists == false && vrinquiryFile.Exists == false && vrholdFile.Exists == false)
                    {
                        MessageBox.Show("No files to Send", "There are no Files to Send!");
                    }
                     

                    //Ends VR 
                }

                close_LOG_file();
            }
     
              
        }
    }
 

        