using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ParkingBonMVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM.ViewModel
{
    class ParkingBonVM : ViewModelBase
    {
        private ParkingBon parkingBon;

       
        public ParkingBonVM(ParkingBon parkingBon)
        {
            this.parkingBon = parkingBon;
            NieuweBon();
        }

        public DateTime Datum { 
            get { return parkingBon.Datum; }
            set { parkingBon.Datum = value;
            RaisePropertyChanged("Datum");
            }
        }

        public DateTime AankomstTijd
        {
            get { return parkingBon.AankomstTijd; }
            set { parkingBon.AankomstTijd = value;
            RaisePropertyChanged("AankomstTijd");
            }
        }

        public DateTime VertrekTijd
        {
            get { return parkingBon.VertrekTijd; }
            set { parkingBon.VertrekTijd = value;
            RaisePropertyChanged("VertrekTijd");
            }
        }
       
        public int Bedrag
        {
            get { return parkingBon.Bedrag;  }
            set { parkingBon.Bedrag = value;
            RaisePropertyChanged("Bedrag");
            }
        }

        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweBon); }
        }

        public RelayCommand OpenenCommand
        {
            get { return new RelayCommand(OpenenBestand); }
        }

        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(OpslaanBestand);  }
        }

        private void OpslaanBestand()
        {
            string fileName = Datum.Day + "-" + Datum.Month + "-" + Datum.Year;
            fileName += "om";
            fileName += AankomstTijd.Hour + "-" + AankomstTijd.Minute + "-" + AankomstTijd.Second;

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
                        bestand.WriteLine(Datum.ToShortDateString());
                        bestand.WriteLine(AankomstTijd.ToLongTimeString());
                        bestand.WriteLine(Bedrag);
                        bestand.WriteLine(VertrekTijd.ToLongTimeString());

                    }
                }
               // statusMessage.Content = dlg.FileName;

            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }
        private void OpenenBestand()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".box";
                dlg.Filter = "Textbox documents |*.box";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        //Inhoud = bestand.ReadLine();
                        //Vet = Convert.ToBoolean(bestand.ReadLine());
                        //Schuin = Convert.ToBoolean(bestand.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        public RelayCommand<CancelEventArgs> ClosingCommand
        {
            get { return new RelayCommand<CancelEventArgs>(OnWindowClosing); }
        }

        public RelayCommand MeerCommand
        {
            get { return new RelayCommand(Meer);  }
        }

        public RelayCommand MinderCommand
        {
            get { return new RelayCommand(Minder); }
        }

        private void Meer()
        {
            if (VertrekTijd.Hour < 22)
                Bedrag++;
            VertrekTijd = AankomstTijd.AddHours(0.5 * Bedrag);
        }

        private void Minder()
        {
            if (Bedrag > 0)
            Bedrag -= 1;
        }

        private void NieuweBon()
        {
            Datum = DateTime.Now;
            AankomstTijd = DateTime.Now;
            VertrekTijd = DateTime.Now;
            Bedrag = 0;
        }

        public void OnWindowClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u het programma sluiten ?",
            MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No)
                e.Cancel = true;
        }

    }
}
