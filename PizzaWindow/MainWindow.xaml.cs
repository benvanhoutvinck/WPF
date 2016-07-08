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

namespace PizzaWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string size;
        public MainWindow()
        {
            InitializeComponent();
            radioLarge.IsChecked = true;
            setMessage();
        }

        private void Size_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton knop = (RadioButton)sender;
            size = knop.Content.ToString();
            setMessage();
        }

        private void setMessage()
        {
            string message = "U heeft ";
            message += hoeveelheidLabel.Content.ToString();
            message += " " + size;
            message += " pizza('s)";

            LabelTekst.Content = message;

        }

        private void RepeatButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(hoeveelheidLabel.Content) < 10)
            {
                hoeveelheidLabel.Content = Convert.ToInt16(hoeveelheidLabel.Content) + 1;
                setMessage();
            }
        }

        private void RepeatButtonMin_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(hoeveelheidLabel.Content) > 1)
            {
                hoeveelheidLabel.Content = Convert.ToInt16(hoeveelheidLabel.Content) - 1;
                setMessage();
            }
        }
    }
}
