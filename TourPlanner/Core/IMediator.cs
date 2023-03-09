using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner.Core
{
    public enum TourEvent
    {
        UpdateGeneralVM
    }
    interface IMediator
    {
        void Notify(object sender, TourEvent ev);
    }

    class TourMediator : IMediator
    {
        private MainViewModel _mainVM;

        private GeneralViewModel _generalVM;

        public TourMediator(MainViewModel mainVM, GeneralViewModel generalVM)
        {
            _mainVM = mainVM;
            _generalVM = generalVM;
        }

        public void Notify(object sender, TourEvent ev)
        {
            if(ev == TourEvent.UpdateGeneralVM)
            {
                _generalVM.SelectedTour = _mainVM.SelectedTour;
            }
        }
    }
}
