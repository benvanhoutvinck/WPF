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
using System.ComponentModel;
using System.Media;

namespace Telefoon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Persoon> personen = new List<Persoon>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            personen.Add(new Persoon("anne", "0478568915", "Vrienden", 
                new BitmapImage(new Uri(@"images\anne.jpg", UriKind.Relative))));
            personen.Add(new Persoon("bob", "0478528937", "Vrienden",
                new BitmapImage(new Uri(@"images\bob.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega1", "0486451278", "Werk",
                new BitmapImage(new Uri(@"images\collega1.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega2", "0471548720", "Werk",
                new BitmapImage(new Uri(@"images\collega2.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega3", "0486685062", "Werk",
                new BitmapImage(new Uri(@"images\collega3.jpg", UriKind.Relative))));
            personen.Add(new Persoon("ed", "0471578621", "Vrienden",
               new BitmapImage(new Uri(@"images\ed.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Grote zus", "0478245812", "Familie",
               new BitmapImage(new Uri(@"images\grotezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Kleine zus", "0486517854", "Familie",
              new BitmapImage(new Uri(@"images\kleinezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Tante non", "0486987213", "Familie",
              new BitmapImage(new Uri(@"images\tantenon.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Telefoon 2", "0471584596", "Vrienden",
              new BitmapImage(new Uri(@"images\telefoon2.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Vader", "0486841525", "Familie",
              new BitmapImage(new Uri(@"images\vader.jpg", UriKind.Relative))));

            ComboBoxGroepen.Items.Add("Iedereen");
            ComboBoxGroepen.Items.Add("Familie");
            ComboBoxGroepen.Items.Add("Vrienden");
            ComboBoxGroepen.Items.Add("Werk");
            ComboBoxGroepen.SelectedIndex = 0;

            foreach(Persoon pers in personen)
            {
                ListBoxPersonen.Items.Add(pers);
            }

        }

        private void ComboBoxGroepen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxPersonen.Items.Clear();
            foreach(Persoon pers in personen)
            {
                if (pers.Groep == ComboBoxGroepen.SelectedItem.ToString() || ComboBoxGroepen.SelectedIndex == 0)
                {
                    ListBoxPersonen.Items.Add(pers);
                }
            }
            ListBoxPersonen.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
        }

        private void ButtonKies_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPersonen.SelectedItem == null)
            {
                MessageBox.Show("Je moet eerst iemand selecteren");
            }
            else
            {
                string message = "Wil je ";
                message += ((Persoon) ListBoxPersonen.SelectedItem).Naam;
                message += " bellen\nop het nummer: ";
                message += ((Persoon)ListBoxPersonen.SelectedItem).Telefoonnr;

                if (MessageBox.Show(message, "titel", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    SoundPlayer speler = new SoundPlayer(@"C:\\Users\\ben.vanhoutvinck\\Documents\\Visual Studio 2013\\Projects\\WpfCursusSolution\\Telefoon\\PHONE.wav");
                    speler.Play();
                }
            }
        }
    }
}
