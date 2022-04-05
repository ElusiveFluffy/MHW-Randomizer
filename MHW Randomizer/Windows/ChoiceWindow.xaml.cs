using System.Windows;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public ChoiceWindow(string message)
        {
            InitializeComponent();
            MessageTB.Text = message;
        }

        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
