using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        
        /// <summary>
        /// Main method for the application
        /// </summary>
        [STAThread]
        public static void Main()
        {

            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}

