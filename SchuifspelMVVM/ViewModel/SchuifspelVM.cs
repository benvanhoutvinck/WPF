using GalaSoft.MvvmLight;
using SchuifspelMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuifspelMVVM.ViewModel
{
    public class SchuifspelVM : ViewModelBase
    {
        private Schuifspel schuifspel;

        public SchuifspelVM(Schuifspel schuifspel)
        {
            this.schuifspel = schuifspel;
        }
    }

  
}
