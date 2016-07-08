using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        private double breedte = 640;
        private double hoogte = 320;
        private double vertPositie;

        public static RoutedCommand mijnRouteCtrlN = new RoutedCommand();
        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
        }
        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new System.Windows.Size(breedte, hoogte);

            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);

            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = breedte;
            page.Height = hoogte;
            vertPositie = 96;

            Image afbeelding = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("C:\\Users\\ben.vanhoutvinck\\Documents\\Visual Studio 2013\\Projects\\WpfCursusSolution\\ParkingBon\\images\\parkingbon.jpg");
            b.EndInit();
            afbeelding.Source = b;
            afbeelding.Margin = new Thickness(96, 96, 0, 0);
            page.Children.Add(afbeelding);
            page.Children.Add(Regel("datum : " + DatumBon.SelectedDate.Value.ToLongDateString()));
            page.Children.Add(Regel("starttijd : " + AankomstLabelTijd.Content));
            page.Children.Add(Regel("eindtijd : " + VertrekLabelTijd.Content));
            page.Children.Add(Regel("bedrag betaald : " + TeBetalenLabel.Content));

            return document;
        }
        private TextBlock Regel(string tekst)
        {
            TextBlock deRegel = new TextBlock();
            deRegel.Text = tekst;
            deRegel.Margin = new Thickness(300, vertPositie, 96, 96);
            vertPositie += 30;
            return deRegel;
        }
        private void ctrlNExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //Vet_Aan_Uit();
            Nieuw();
        }

        private void Nieuw()
        {
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            var picker = DatumBon as DatePicker;
            DateTime? date = picker.SelectedDate;

            DateTime startTime = Convert.ToDateTime(AankomstLabelTijd.Content);
            DateTime endTime = Convert.ToDateTime(VertrekLabelTijd.Content);

            string fileName = date.Value.Day + "-" + date.Value.Month + "-" + date.Value.Year;
            fileName += "om";
            fileName += startTime.Hour + "-" + startTime.Minute + "-" + startTime.Second;

            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = fileName;
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parking files |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(date.Value.ToShortDateString());
                        bestand.WriteLine(startTime.ToLongTimeString());
                        bestand.WriteLine(TeBetalenLabel.Content);
                        bestand.WriteLine(endTime.ToLongTimeString());

                    }
                }
                statusMessage.Content = dlg.FileName;

            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parking files |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        string datum = bestand.ReadLine();
                        DateTime? dt = DateTime.Parse(datum);
                        DatumBon.SelectedDate = dt;

                        string start = bestand.ReadLine();
                        DateTime startTime = DateTime.Parse(start);
                        AankomstLabelTijd.Content = startTime.ToLongTimeString();

                        TeBetalenLabel.Content = bestand.ReadLine();

                        string einde = bestand.ReadLine();
                        DateTime endTime = DateTime.Parse(einde);
                        VertrekLabelTijd.Content = endTime.ToLongTimeString();
                    }
                }
                statusMessage.Content = dlg.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
            {
                afdrukken.PrintDocument(StelAfdrukSamen().DocumentPaginator, "tekstbox");
            }
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
