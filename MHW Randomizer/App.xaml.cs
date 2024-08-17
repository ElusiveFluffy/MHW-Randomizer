using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            ViewModels.Settings = new();
            //Load the settings if the file exists
            if (!File.Exists("Settings.json"))
            {
                ViewModels.Settings.LoadSettingsString();
            }
            else
            {
                //Load old settings method
                using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "Settings.json"))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        DefaultValueHandling = DefaultValueHandling.Populate
                    };
                    ViewModels.Settings = (RandomizerSettings?)serializer.Deserialize(file, typeof(RandomizerSettings));
                }
                //Delete the old settings
                File.Delete("Settings.json");
            }
            ViewModels.Randomizer = new();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
