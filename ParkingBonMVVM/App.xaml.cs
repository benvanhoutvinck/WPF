using ParkingBonMVVM.Model;
using ParkingBonMVVM.View;
using ParkingBonMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ParkingBon mijnBon = new ParkingBon();
            ParkingBonVM vm = new ParkingBonVM(mijnBon);
            ParkingBonView mijnParkingBonView = new ParkingBonView();
            mijnParkingBonView.DataContext = vm;
            mijnParkingBonView.Show();
        }
    }
}
