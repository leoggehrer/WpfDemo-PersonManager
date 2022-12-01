using System;
using System.Windows.Input;

namespace PersonManager.WpfApp.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private ICommand? saveCommand = null;
        private ICommand? closeCommand = null;
        private Logic.Repositories.PersonalRepository Repository { get; } = new();

        private Logic.Models.Person model = new();
        public Logic.Models.Person Model 
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Firstname));
                OnPropertyChanged(nameof(Lastname));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public Action? CloseWindow { get; set; }
        public ICommand SaveCommand 
        { 
            get
            {
                return RelayCommand.CreateCommand(ref saveCommand, (p) => Save());
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref closeCommand, (p) => Close());
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

        public void Save()
        {
            if (Model.Id == 0)
            {
                Repository.Add(Model);
            }
            else
            {
                Repository.Update(Model);
            }
            Repository.Save();
            Close();
        }
        public void Close()
        {
            CloseWindow?.Invoke();
        }
    }
}
