using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Telefoon
{
    public class Persoon
    {
        public string Naam { get; set; }
        public string Telefoonnr { get; set; }
        public string Groep { get; set; }
        public BitmapImage Afbeelding { get; set; }

        public Persoon(string naam, string telefoonnr, string groep, BitmapImage image)
        {
            this.Naam = naam;
            this.Telefoonnr = telefoonnr;
            this.Groep = groep;
            this.Afbeelding = image;
        }
    }
}
