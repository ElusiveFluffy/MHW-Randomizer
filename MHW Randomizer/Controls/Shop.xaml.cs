using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for Shop.xaml
    /// </summary>
    public partial class Shop : UserControl
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");

            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        private void RandomShopGear_Unchecked(object sender, RoutedEventArgs e)
        {
            RandomShopType.IsChecked = false;
        }

        private void TBItems_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox? tb = sender as TextBox;
            tb.Text = tb.Text.Truncate(4);
            if (string.IsNullOrWhiteSpace(tb.Text) || int.Parse(tb.Text) <= 0)
            {
                tb.Text = "1";
                tb.SelectionStart = tb.Text.Length;
                ViewModels.Settings.AmountOfShopItems = ushort.Parse(tb.Text);
            }
            if (int.Parse(tb.Text) > 255)
            {
                tb.Text = "255";
                tb.SelectionStart = 4;
                ViewModels.Settings.AmountOfShopItems = ushort.Parse(tb.Text);
            }
        }

        private void EnterKeyRemoveFocus(object sender, KeyEventArgs e)
        {
            TextBox? tb = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                // Kill logical focus
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tb), null);
                // Kill keyboard focus
                Keyboard.ClearFocus();
            }
        }

        private void TBArmItems_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox? tb = sender as TextBox;
            tb.Text = tb.Text.Truncate(5);
            if (string.IsNullOrWhiteSpace(tb.Text) || uint.Parse(tb.Text) <= 0)
            {
                tb.Text = "1";
                tb.SelectionStart = tb.Text.Length;
                ViewModels.Settings.AmountOfGearShopItems = ushort.Parse(tb.Text);
            }
            else if (uint.Parse(tb.Text) > 4259)
            {
                tb.Text = "4259";
                tb.SelectionStart = 4;
                ViewModels.Settings.AmountOfGearShopItems = ushort.Parse(tb.Text);
            }
        }
    }
}
