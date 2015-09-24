using System;
using MVVMFramework.Interfaces;
using MVVMFramework.Services;
using System.Collections.ObjectModel;
using MVVMFramework.Models;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MVVMFramework.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        IDataStore dataStore;

        public ObservableCollection<Person> People { get; set; }
        public LoginViewModel()
        {
            this.dataStore = new OfflineDataStore();
            People = new ObservableCollection<Person>();
        }

        RelayCommand getPeopleCommand;
        public ICommand GetPeopleCommand
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
                SetProperty(ref username, value, onChanged: ()=>
                {
                    OnPropertyChanged(ComboDisplayPropertyName);
                }); 
            }
        }

        string password = string.Empty;
        public const string PasswordPropertyName = "Password";
        public string Password
        {
            get { return password; }
            set 
            { 
                SetProperty(ref password, value, onChanged: ()=>
                {
                    OnPropertyChanged(ComboDisplayPropertyName);
                }); 
            }
        }

        public const string ComboDisplayPropertyName = "ComboDisplay";
        public string ComboDisplay
        {
            get { return $"UN: {Username} Pass: {Password}"; }
        }
        
    }
}

