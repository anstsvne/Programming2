using System;
using System.Windows.Input;
using System.IO;
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

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string filePath = "contact_save.txt";
            try
            {
                    File.WriteAllText(filePath, $"{_viewModel.Name}\n{_viewModel.PhoneNumber}\n{_viewModel.Email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
