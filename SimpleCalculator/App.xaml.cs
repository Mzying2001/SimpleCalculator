using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using static SimpleCalculator.Properties.Resources;

namespace SimpleCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!System.IO.Directory.Exists(DATA_PATH))
                System.IO.Directory.CreateDirectory(DATA_PATH);

            base.OnStartup(e);
        }
    }
}
