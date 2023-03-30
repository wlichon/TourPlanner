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
        RefreshRouteImage
    }
    public interface IMediator
    {
        void Notify(object sender, TourEvent ev);
    }

    class TourMediator : IMediator
    {
        private RouteViewModel _routeVM;

        private GeneralViewModel _generalVM;


        public TourMediator(RouteViewModel routeVM, GeneralViewModel generalVM)
        {
            _routeVM = routeVM;
            _routeVM.SetMediator(this);
            _generalVM = generalVM;
            _generalVM.SetMediator(this);
        }

        public void Notify(object sender, TourEvent ev)
        {
            if(ev == TourEvent.RefreshRouteImage)
            {
                _routeVM.MediatorRefreshMap();
            }
        }
    }
}
