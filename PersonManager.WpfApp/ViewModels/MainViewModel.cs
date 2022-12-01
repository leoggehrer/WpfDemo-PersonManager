
using PersonManager.Logic.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PersonManager.WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand? _addCommand = null;
        private ICommand? _editCommand = null;
        private ICommand? _deleteCommand = null;

        private Person Model { get; set; } = new();

        public ICommand AddCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _addCommand, (p) => AddPerson(), (p) => true);
            }
        }
        public ICommand EditCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _editCommand, (p) => EditPerson(SelectedItem!.Id), (p) => SelectedItem != null);
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _deleteCommand, (p) => DeletePerson(SelectedItem!.Id), (p) => SelectedItem != null);
            }
        }

        private void DeletePerson(int id)
        {
            if (MessageBox.Show($"Delete item '{SelectedItem!.FullName}'?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                var repo = new Logic.Repositories.PersonalRepository();

                repo.Delete(id);
                repo.Save();
                OnPropertyChanged(nameof(Models));
            }
        }

        private void EditPerson(int id)
        {
            PersonWindow personWindow = new PersonWindow();

            if (personWindow.DataContext is PersonViewModel viewModel)
            {
                viewModel.Model = SelectedItem!;
            }
            personWindow.ShowDialog();
            OnPropertyChanged(nameof(Models));
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
                var repo = new Logic.Repositories.PersonalRepository();
                var models = repo.GetAll().Where(e => string.IsNullOrEmpty(_searchText) || e.FullName.ToLower().Contains(_searchText.ToLower()));

                return new ObservableCollection<Logic.Models.Person>(models);
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

        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(Models));
            }
        }

        private Person? selectedItem = null;
        public Person? SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

    }
}
