using System;
using System.IO;
using System.Windows.Input;
using View.Model;

namespace View.ViewModel
{
    public class LoadCommand : ICommand
    {
        private readonly MainVM _viewModel;

        public LoadCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            string filePath = "contact_save.txt";
            if (File.Exists(filePath))
            {
                try
                {
                    string[] data = File.ReadAllLines(filePath);
                    if (data.Length >= 3)
                    {
                        _viewModel.Name = data[0];
                        _viewModel.PhoneNumber = data[1];
                        _viewModel.Email = data[2];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

    }
}