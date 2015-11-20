using System;
using Desktops.Models;
using System.Windows.Input;
using System.Diagnostics;

namespace Desktops.ViewModels
{

    public class DesktopViewModel : ViewModelBase
    {
        private DesktopModel _desktop;
        private ICommand _addToValueCommand;
        private int _addValue;

        public string DesktopName
        {
            get { return _desktop.Name; }
        }

        public int DesktopValue
        {
            get { return _desktop.Value; }
            set { _desktop.Value = value; EmitPropertyChanged(); }
        }

        public string DesktopComments
        {
            get { return _desktop.Comments; }
            set { _desktop.Comments = value; }
        }

        public int AddValue
        {
            get { return _addValue; }
            set { SetProperty(ref _addValue, value); }
        }

        public ICommand AddToValue
        {
            get { return _addToValueCommand; }
        }

        public DesktopViewModel(DesktopModel desktop)
        {
            _desktop = desktop;
            _addValue = 0;
            _addToValueCommand = new ActionCommand((o) => { DesktopValue += AddValue; });
        }
    }

}
