using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
 

namespace File_Generation_System
{
    class cleanFile
    {

         

        

        public static byte[] ReadFile(string filePath)
        {

            byte[] buffer;
            string newFile = filePath + "c";
            string backUpFile = filePath + "b";
         
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            FileStream fileStream2 = new FileStream(newFile , FileMode.Create, FileAccess.Write);
            try                                                             
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read
                
                byte byteTest;
              
                int zeroD = 0;
                int zeroA = 0;
                bool dontWriteTheBlankByte = false;

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading

                               
                
                //Main loop 

                for (int i = 0; i < buffer.Length; i++)
                {
                   
                  byteTest =  (byte)buffer.GetValue(i);
                   
                  //If byte is equal to an ASCII code that will cause XML to choke, then change it to space
                   
                  if (byteTest == 26 | byteTest == 11 | byteTest == 02 | byteTest == 12 | byteTest == 16 | byteTest == 17 | byteTest == 28 | byteTest == 23 | byteTest == 30
                      | byteTest == 08 | byteTest == 189 | byteTest == 255 | byteTest == 27 | byteTest == 00 | byteTest == 22 | byteTest == 07 | byteTest == 01
                      | byteTest == 03 | byteTest == 14 | byteTest == 18 | byteTest == 25 | byteTest == 21 | byteTest == 225)
                  {
                      byteTest = 32;
                  }

                   

                  if (byteTest == 13)
                  {
                      zeroD = zeroD + 1;
                  }
                  if (byteTest == 10)
                    {
                        zeroA = zeroA + 1;
                    }

                    if (byteTest != 13 & byteTest != 10)
                    {
                        zeroD = 0;
                        zeroA = 0;
                        dontWriteTheBlankByte = false;
                    }

                    if (zeroD >= 2)
                    {
                        dontWriteTheBlankByte = true;
                    }

                    //if broken pipe, read again, if 0d then don't write out byte

                    if (byteTest == 166)
                    {
                        //bump up i counter by one
                        int j = i;
                        j = i + 1;

                        byteTest = (byte)buffer.GetValue(j);
                        //Added the 20 test 9/23/10 City of Santa Monica had issues.
                        if (byteTest == 13 & byteTest == 20)
                        {
                            dontWriteTheBlankByte = true;
                        }
                    }


                    if (dontWriteTheBlankByte == false)
                    {

                        fileStream2.WriteByte(byteTest);
                    }
                   
                }

                

               


            }
            finally
            {
                
                fileStream.Close();
                fileStream2.Close();
                File.Replace(newFile, filePath, backUpFile);
                File.Delete(backUpFile);
                
            }
            return buffer;
        }

        public void deleteEmptyLines(String filePath)

        {
            var tempFileName = Path.GetTempFileName();
            try
            {
            using (var streamReader = new StreamReader(filePath)) 
            using (var streamWriter = new StreamWriter(tempFileName))  
            { 
              string line;
              while ((line = streamReader.ReadLine()) != null)
              {
                  if (!string.IsNullOrEmpty(line))
                      streamWriter.WriteLine(line);
              }
            }
            File.Copy(tempFileName, filePath, true);
            }
            finally
            {
                   
            }


        }

    }
}
