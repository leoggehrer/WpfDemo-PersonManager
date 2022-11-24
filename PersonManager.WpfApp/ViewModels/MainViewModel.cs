
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonManager.WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand? addCommand = null;

        private Logic.Repositories.PersonalRepository Repository { get; } = new();
        private Logic.Models.Person Model { get; set; } = new();

        public ICommand AddCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref addCommand, (p) => AddPerson(), (p) => true);
            }
        }

        public void AddPerson()
        {
            PersonWindow personWindow = new PersonWindow();

            personWindow.ShowDialog();
            OnPropertyChanged(nameof(Models));
        }
        public ObservableCollection<Logic.Models.Person> Models 
        { 
            get
            {
                var result = new ObservableCollection<Logic.Models.Person>(Repository.GetAll());

                return result;
            }
        }
        public string Firstname 
        { 
            get => Model.Firstname; 
            set
            {
                Model.Firstname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string Lastname
        {
            get => Model.Lastname;
            set
            {
                Model.Lastname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string FullName
        {
            get => Model.FullName;
        }
    }
}
