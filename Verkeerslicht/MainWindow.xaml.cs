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

namespace Verkeerslicht
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            OranjeLicht.Opacity = 0;
            GroenLicht.Opacity = 1;
            ButtonGo.IsEnabled = false;
            ButtonOpgelet.IsEnabled = true;
        }

        private void ButtonOpgelet_Click(object sender, RoutedEventArgs e)
        {
            if (GroenLicht.Opacity==1)
            {
                GroenLicht.Opacity = 0;
                OranjeLicht.Opacity = 1;
                ButtonOpgelet.IsEnabled = false;
                ButtonStop.IsEnabled = true;

            }
            else
            {
                RoodLicht.Opacity = 0;
                OranjeLicht.Opacity = 1;
                ButtonOpgelet.IsEnabled = false;
                ButtonGo.IsEnabled = true;
            }
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            OranjeLicht.Opacity = 0;
            RoodLicht.Opacity = 1;
            ButtonStop.IsEnabled = false;
            ButtonOpgelet.IsEnabled = true;
        }
    }
}
