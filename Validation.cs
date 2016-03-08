using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace File_Generation_System
{
    public static class Validation
    {


        public static bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }




        public static bool IsSet(ComboBox comboBox, string name)
        {
            if (comboBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                comboBox.Focus();
                return false;
            }
            return true;
        }




        public static bool IsMinLength(TextBox textBox, string name, decimal min)
        {
            decimal number = textBox.TextLength;
            if (number < min)
            {
                MessageBox.Show(name + " length must be " + min.ToString() + " characters long.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }




        public static bool IsSixteen(DateTimePicker dateTimePicker)
        {
            if (!(dateTimePicker.Value < DateTime.Now.AddYears(-16)))
            {
                MessageBox.Show("Driver must be at least 16 years old.", "Date Entry Error");
                dateTimePicker.Focus();
                return false;
            }
            return true;
        }




        public static bool IsValidDL(TextBox textBox)
        {
            //Regex driverLicense = new Regex(@"^[^ILOQT]\d{7}$");
            //Regular Expressions used to force valid alpha character followed by seven digits
            Regex driverLicense = new Regex(@"^[A-HJKMNPRSU-Z]\d{7}$");

            if (driverLicense.Match(textBox.Text).Success == false)
            {
                MessageBox.Show(textBox.Text + " is not a valid DL Number", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }




        public static bool IsValidReqCode(TextBox textBox)
        {
           // Regex reqCode = new Regex(@"^[\d||\w]\d{4}$");
              
            //if (reqCode.Match(textBox.Text).Success == false)
              if (textBox.Text.Length != 5)
            {
                MessageBox.Show(textBox.Text + " is not a valid Requestor Code","Entry Error");
                textBox.Focus();
                return false;

            }

            if (textBox.Text == "99999")
            {
                MessageBox.Show(textBox.Text + " You must configure with YOUR Requestor Code","Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }




        public static bool IsValidDLInqName(TextBox lastName, TextBox firstName)
        {
            if ((lastName.TextLength < 2) && (firstName.TextLength < 1))
            {
                MessageBox.Show("First character of Drivers First Name is required.", "Entry Error");
                firstName.Focus();
                return false;
            }
            return true;
        }


        public static bool IsWord(TextBox textBox)
        {
             
            Regex isWord = new Regex(@"^[A-Z]*$");

            if (isWord.Match(textBox.Text.Trim()).Success == false)
            {
                MessageBox.Show(textBox.Text + " is not valid.  Please make sure there are no special characters.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }
 
    }
}
