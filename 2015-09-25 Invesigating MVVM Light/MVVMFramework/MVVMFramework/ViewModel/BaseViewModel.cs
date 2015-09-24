using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace MVVMFramework.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        bool busy;
        public const string IsBusyPropertyName = "IsBusy";
        public bool IsBusy
        {
            get { return busy; }
            set 
            {
                SetProperty(ref busy, value);
            }
        }


        protected void SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null) 
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value)) 
                return;

            backingStore = value;

            if (onChanged != null) 
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

