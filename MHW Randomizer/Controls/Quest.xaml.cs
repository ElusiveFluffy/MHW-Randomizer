using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MHW_Randomizer
{
    /// <summary>
    /// Interaction logic for Quest.xaml
    /// </summary>
    public partial class Quest : UserControl
    {
        public Quest()
        {
            InitializeComponent();
        }

        private void MapOption_Checked(object sender, RoutedEventArgs e)
        {
            if (Rsobj != null)
                Rsobj.IsChecked = true;
        }

        private void MapOption_Unchecked(object sender, RoutedEventArgs e)
        {
            Rsobj.IsChecked = false;
        }

        private void OnlyIBMonsters_Checked(object sender, RoutedEventArgs e)
        {
            if (NonIBMonsters != null)
                NonIBMonsters.IsChecked = false;
            MonstersFoundInIB.IsChecked = false;
        }

        private void NonIBMonsters_Checked(object sender, RoutedEventArgs e)
        {
            MonstersFoundInIB.IsChecked = false;
            OnlyIBMonsters.IsChecked = false;
        }

        private void MonstersFoundInIB_Checked(object sender, RoutedEventArgs e)
        {
            if (NonIBMonsters != null)
            {
                OnlyIBMonsters.IsChecked = false;
                NonIBMonsters.IsChecked = false;
            }
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

        private void TBMin_TextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TBMax == null)
                return;
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Truncate(5);
            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text.StartsWith("0"))
            {
                tb.Text = "1";
                tb.SelectionStart = tb.Text.Length;
            }

            if (int.Parse(tb.Text) > int.Parse(TBMax.Text))
            {
                tb.Text = TBMax.Text;
                tb.SelectionStart = tb.Text.Length;
            }

            if (int.Parse(tb.Text) > 2000)
            {
                tb.Text = "2000";
                tb.SelectionStart = 4;
            }
        }

        private void TBMax_TextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Truncate(5);
            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text.StartsWith("0"))
            {
                tb.Text = "1";
                tb.SelectionStart = tb.Text.Length;
            }

            if (int.Parse(TBMin.Text) > int.Parse(tb.Text))
            {
                TBMin.Text = tb.Text;
                tb.SelectionStart = tb.Text.Length;
            }

            if (int.Parse(tb.Text) > 2000)
            {
                tb.Text = "2000";
                tb.SelectionStart = 4;
            }
        }

        private void EnterKeyRemoveFocus(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                // Kill logical focus
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tb), null);
                // Kill keyboard focus
                Keyboard.ClearFocus();
            }
        }

        private void MonstersFoundInIB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IoC.Settings.IceborneOnlyMonsters && !IoC.Settings.IncludeHighRankOnly)
                MonstersFoundInIB.IsChecked = true;
        }

        private void OnlyIBMonsters_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IoC.Settings.MonstersFoundInIB && !IoC.Settings.IncludeHighRankOnly)
                OnlyIBMonsters.IsChecked = true;
        }

        private void NonIBMonsters_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IoC.Settings.IceborneOnlyMonsters && !IoC.Settings.MonstersFoundInIB)
                NonIBMonsters.IsChecked = true;
        }

        private void TBSupply_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Truncate(5);

            if (int.Parse(tb.Text) > 2000)
            {
                tb.Text = "2000";
                tb.SelectionStart = 4;
            }
        }
    }
}
