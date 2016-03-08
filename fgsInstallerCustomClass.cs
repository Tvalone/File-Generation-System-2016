using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Runtime.InteropServices;


namespace File_Generation_System
{
    [RunInstaller(true)]
    public partial class fgsInstallerCustomClass : Installer
    {
        public fgsInstallerCustomClass()
        {
            InitializeComponent();
        }
    }
}
