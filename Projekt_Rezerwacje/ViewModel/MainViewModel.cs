using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rezerwacje.ViewModel
{
    using Model;
    class MainViewModel
    {
        private Model model = new Model();
        public ClientsTab ClientsTabVM { get; set; }
        public ReservationsTab ReservationsTabVM { get; set; }

        public MainViewModel()
        {
            ClientsTabVM = new ClientsTab(model);
            ReservationsTabVM = new ReservationsTab(model);
        }

    }
}
