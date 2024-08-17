﻿using Newtonsoft.Json;
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
        public static Window? window;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Save before closing
            ViewModels.Settings.SaveSettingString();
            Application.Current.Shutdown();
        }
    }
}
