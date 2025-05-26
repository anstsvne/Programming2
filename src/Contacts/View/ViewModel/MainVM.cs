using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using View.Model;
using View.Model.Services;

namespace ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly ContactSerializer _serializer;
        private bool _isInitialized = false;

        public ObservableCollection<Contact> Contacts { get; set; }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                if (_isInitialized)
                {
                    EditCommand.RaiseCanExecuteChanged();
                    RemoveCommand.RaiseCanExecuteChanged();
                }

                if (value != null)
                {
                    ClonedContact = value.Clone();
                    IsContactReadOnly = true;
                    OnPropertyChanged(nameof(IsApplyVisible));
                }
            }
        }

        private Contact _clonedContact;
        public Contact ClonedContact
        {
            get => _clonedContact;
            set
            {
                _clonedContact = value;
                OnPropertyChanged();
                if (_isInitialized)
                    ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isContactReadOnly = true;
        public bool IsContactReadOnly
        {
            get => _isContactReadOnly;
            set
            {
                _isContactReadOnly = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsApplyVisible));
                if (_isInitialized)
                {
                    ApplyCommand.RaiseCanExecuteChanged();
                    AddCommand.RaiseCanExecuteChanged();
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsApplyVisible => !IsContactReadOnly;

        public RelayCommand ApplyCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand RemoveCommand { get; }

        private Contact _newContactDraft;

        public MainVM()
        {
            _serializer = new ContactSerializer();
            Contacts = _serializer.LoadContacts();

            ApplyCommand = new RelayCommand(ApplyChanges, CanApplyChanges);
            AddCommand = new RelayCommand(AddContact, () => IsContactReadOnly);
            EditCommand = new RelayCommand(EditContact, () => SelectedContact != null && IsContactReadOnly);
            RemoveCommand = new RelayCommand(RemoveContact, () => SelectedContact != null && IsContactReadOnly);

            _isInitialized = true;
        }

        private void AddContact()
        {
            _newContactDraft = new Contact
            {
                Name = "New Contact",
                PhoneNumber = "",
                Email = ""
            };
            SelectedContact = _newContactDraft;
            EditContact();
        }

        private void EditContact()
        {
            if (SelectedContact != null)
            {
                ClonedContact = SelectedContact.Clone();
                IsContactReadOnly = false;
            }
        }

        private void RemoveContact()
        {
            if (SelectedContact != null)
            {
                Contacts.Remove(SelectedContact);
                SelectedContact = null;
                ClonedContact = null;
                IsContactReadOnly = true;
                _serializer.SaveContacts(Contacts);
            }
        }

        private void ApplyChanges()
        {
            if (ClonedContact != null)
            {
                if (_newContactDraft != null)
                {
                    _newContactDraft.Name = ClonedContact.Name;
                    _newContactDraft.PhoneNumber = ClonedContact.PhoneNumber;
                    _newContactDraft.Email = ClonedContact.Email;
                    Contacts.Add(_newContactDraft);
                    SelectedContact = _newContactDraft;
                    _newContactDraft = null;
                }
                else if (SelectedContact != null)
                {
                    SelectedContact.Name = ClonedContact.Name;
                    SelectedContact.PhoneNumber = ClonedContact.PhoneNumber;
                    SelectedContact.Email = ClonedContact.Email;
                }

                _serializer.SaveContacts(Contacts);
                IsContactReadOnly = true;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        private bool CanApplyChanges()
        {
            return ClonedContact != null && !IsContactReadOnly;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
