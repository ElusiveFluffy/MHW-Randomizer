using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window window;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                //serialize objects directly into file stream
                serializer.Serialize(file, IoC.Settings);
            }
            Application.Current.Shutdown();
        }
    }
}
