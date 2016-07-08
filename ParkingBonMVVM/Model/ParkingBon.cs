using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBonMVVM.Model
{
    class ParkingBon
    {
        public DateTime Datum { get; set; }
        public DateTime AankomstTijd { get; set; }
        public int Bedrag { get; set; }
        public DateTime VertrekTijd { get; set; }

    }
}
