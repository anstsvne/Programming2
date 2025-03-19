using System;
using System.IO;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    public class LoadCommand : ICommand
    {
        /// <summary>
        /// Приватное поле класса MainVM с информацией контакта.
        /// </summary>
        private readonly MainVM _viewModel;

        /// <summary>
        /// Команда загрузки.
        /// </summary>
        /// <param name="viewModel"></param>
        public LoadCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Contact contact = ContactSerializer.LoadContact();
            _viewModel.Name = contact.Name;
            _viewModel.PhoneNumber = contact.PhoneNumber;
            _viewModel.Email = contact.Email;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

    }
}