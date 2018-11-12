using Inspark.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Inspark.Viewmodels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // BaseViewModel, of which all other viewmodels will inherit from.
        public event PropertyChangedEventHandler PropertyChanged;

        public ApiServices _api = new ApiServices();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
