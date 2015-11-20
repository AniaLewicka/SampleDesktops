using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Desktops.Models;

namespace Desktops.ViewModels
{

    public class DesktopListViewModel
    {
        public ObservableCollection<DesktopViewModel> _desktopList;

        public ObservableCollection<DesktopViewModel> DesktopList
        {
            get { return _desktopList; }
        }

        public DesktopListViewModel()
        {
            var desktopList = DesktopModel.GetDesktopModels();
            _desktopList = new ObservableCollection<DesktopViewModel>();
            desktopList.ForEach(desktop => _desktopList.Add(new DesktopViewModel(desktop)));
        }
    }
}
