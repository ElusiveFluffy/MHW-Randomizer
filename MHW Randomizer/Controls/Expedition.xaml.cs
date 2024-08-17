using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for Expedition.xaml
    /// </summary>
    public partial class Expedition : UserControl
    {
        public Expedition()
        {
            InitializeComponent();
        }

        private void RandomExpeditions_Checked(object sender, RoutedEventArgs e)
        {
            ViewModels.Settings.ExpeditionRandomSobj = true;
        }

        private void RandomExpeditions_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModels.Settings.ExpeditionRandomSobj = false;
        }

        private void RandomIBExpeditions_Checked(object sender, RoutedEventArgs e)
        {
            ViewModels.Settings.ExpeditionRandomIBSobj = true;
        }

        private void RandomIBExpeditions_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModels.Settings.ExpeditionRandomIBSobj = false;
        }
    }
}
