using MVVMFramework.Interfaces;
using System.Collections.ObjectModel;
using MVVMFramework.Models;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace MVVMFramework.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        IDataStore dataStore;

        public ObservableCollection<Person> People { get; set; }
        public LoginViewModel(IDataStore dataStore)
        {
            this.dataStore = dataStore;
            People = new ObservableCollection<Person>();
        }

        RelayCommand getPeopleCommand;
        public RelayCommand GetPeopleCommand
        {
            get { 
                return getPeopleCommand ?? 
                (getPeopleCommand = new RelayCommand(async ()=>
                    {
                            IsBusy = true;

                            await Task.Delay(5000);
                            //spin thread for 5 seconds
                            var people = await dataStore.GetPeopleAsync();
                            foreach(var p in people)
                                People.Add(p);

                            RaisePropertyChanged(()=>People);

                            IsBusy = false;
    
                    }));
            }
        }



        string username = string.Empty;
        public const string UsernamePropertyName = "Username";
        public string Username
        {
            get { return username; }
            set 
            { 
                if (Set(() => Username, ref username, value))
                {
                    RaisePropertyChanged(() => ComboDisplay);
                } 
            }
        }

        string password = string.Empty;
        public const string PasswordPropertyName = "Password";
        public string Password
        {
            get { return password; }
            set 
            { 
                if (Set(() => Password, ref password, value))
                {
                    RaisePropertyChanged(() => ComboDisplay);
                } 
            }
        }

        public const string ComboDisplayPropertyName = "ComboDisplay";
        public string ComboDisplay
        {
            get { return $"UN: {Username} Pass: {Password}"; }
        }
        
    }
}

