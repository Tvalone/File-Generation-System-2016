using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;

 

namespace File_Generation_System
{
    class basicDateValidations
    {

        public int holdValidations(Object sender)
        {
            int ssc;

            if (((String)sender).Length != 0)
            {
                String testForValid = (((String)sender).ToString());
                ssc = testForValid.Substring(0, 1).IndexOfAny("ACD".ToCharArray());

                return ssc;
            }


            return 0;
        }

        public string DateValidations(Object sender)
        {

       

            System.DateTime currentDate = System.DateTime.Now;

            //MaskedTextBox TextMaskFormat must be Include literals 

            if (((MaskedTextBox)sender).Text.Length != 10 & ((MaskedTextBox)sender).Name != "paidDateMskTxtBx")
            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
               return "Date must be fully entered";
            }

            //So a full date mm/dd/yyyy was entered

            if (((MaskedTextBox)sender).Text.Length == 10)
            {

                string mm = (((MaskedTextBox)sender).Text.Substring(0, 2));
                string dd = (((MaskedTextBox)sender).Text.Substring(3, 2));
                string year = (((MaskedTextBox)sender).Text.Substring(6, 4));

                int monthc = Convert.ToInt32(mm);
                int dayc = Convert.ToInt32(dd);
                int yearc = Convert.ToInt32(year);

                bool leapyear = System.DateTime.IsLeapYear(yearc);

                //Converts the Date from the masked text box to a date object

                try
                {
                    DateTime dt = Convert.ToDateTime(((MaskedTextBox)sender).Text.ToString());


                    //compares the current date to the converted date from the amended text box

                    int greaterThanCurrentDate = currentDate.CompareTo(dt);



                    if (greaterThanCurrentDate == -1)
                    {
                        ((MaskedTextBox)sender).Focus();
                        ((MaskedTextBox)sender).BackColor = Color.Red;

                        return "Date cannot be a date in the future";

                    }
                 



                if ((monthc < 01) || (monthc > 12))
                {
                    ((MaskedTextBox)sender).Focus();
                    ((MaskedTextBox)sender).BackColor = Color.Red;

                    return  "Month must be 01- 12";


                }



                //calculates the days in the month for the year and month from masked text box

                int daysinmonth = System.DateTime.DaysInMonth(yearc, monthc);



                if ((dayc < 01) || (dayc > daysinmonth))
                {
                    ((MaskedTextBox)sender).Focus();
                    ((MaskedTextBox)sender).BackColor = Color.Red;
                   return "Day must be between 01 and" + daysinmonth;
                }
                else
                {
                    ((MaskedTextBox)sender).BackColor = Color.White;
                    return "";
                }



                if (greaterThanCurrentDate == -1)
                {
                    ((MaskedTextBox)sender).Focus();
                    ((MaskedTextBox)sender).BackColor = Color.Red;

                     return "Date cannot be a date in the future";

                }

                //Leaving the conviction date box one assumes the violation date has already been filled

                if (((MaskedTextBox)sender).Name == "convictionDateMskTxtBx")
                {

                    DateTime ct = Convert.ToDateTime(((MaskedTextBox)sender).Text.ToString());
                    //DateTime vt = Convert.ToDateTime(violationDateMskTxtBx.Text.ToString());

                    //int compareConvitDateToVioDate = ct.CompareTo(vt);

                    return "";
                }


                return "";
            }
            catch (System.FormatException fx)

            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
                string error = fx.ToString();
                return "Date was not entered correctly please fix";
            }

            catch (Exception ex)

            {
                ((MaskedTextBox)sender).Focus();
                ((MaskedTextBox)sender).BackColor = Color.Red;
                return "Something wrong with the date" + ex.ToString();
            }
            }
            return "";
        }
    }
}
