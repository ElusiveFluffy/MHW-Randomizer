using System.Windows;
using System.Windows.Controls;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for Armour.xaml
    /// </summary>
    public partial class Armour : UserControl
    {
        public Armour()
        {
            InitializeComponent();
        }

        private void ShuffleRecipes_Unchecked(object sender, RoutedEventArgs e)
        {
            IronCB.IsChecked = false;
        }
    }
}
