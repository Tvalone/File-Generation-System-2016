namespace File_Generation_System
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace File_Generation_System
    {
        class ClearListControl
        {
            public static void clearForm(System.Windows.Forms.ListControl parent)
            {
                foreach (System.Windows.Forms.ListControl ctrControl in parent.Controls)
                {
                    //Loop through all controls 
                    
                    if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ListControl)))
                    {
                        //Enable all listboxes
                        ((System.Windows.Forms.ListBox)ctrControl).Enabled = true;
                    }
                    if (ctrControl.Controls.Count > 0)
                    {
                        //Call itself to get all other controls in other containers 
                        clearForm(ctrControl);
                    }
                }
            }


        }
    }

}
