using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using View.Model;
using System.Windows.Input;

namespace View.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private Contact _contact;

        public MainVM()
        {
            _contact = new Contact();
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
        }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public string Name
        {
            get => _contact.Name;
            set { _contact.Name = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _contact.PhoneNumber;
            set { _contact.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _contact.Email;
            set { _contact.Email = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}