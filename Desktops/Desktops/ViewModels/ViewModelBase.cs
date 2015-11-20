using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Desktops.ViewModels
{

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void EmitPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (object.Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                EmitPropertyChanged(property);
                return true;
            }
        }
    }

}