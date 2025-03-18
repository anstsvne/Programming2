using System;
using System.Windows.Input;
using View.Model.Services;
using View.Model;

namespace View.ViewModel
{
    public class SaveCommand : ICommand
    {
        private readonly MainVM _viewModel;

        public SaveCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            ContactSerializer.Save(new Contact
            {
                Name = _viewModel.Name,
                PhoneNumber = _viewModel.PhoneNumber,
                Email = _viewModel.Email
            });
        }

        public event EventHandler CanExecuteChanged;
    }
}
