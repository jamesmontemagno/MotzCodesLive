using GalaSoft.MvvmLight;

namespace MVVMFramework.ViewModel
{
    public class BaseViewModel : ViewModelBase
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
                this.Set(() => IsBusy, ref busy, value);
            }
        }
    }
}

