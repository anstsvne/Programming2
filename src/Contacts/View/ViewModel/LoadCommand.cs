using System;
using System.IO;
using System.Windows.Input;
using View.Model;

namespace View.ViewModel
{
    public class LoadCommand : ICommand
    {
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
                        Contact loadedContact = new Contact(data[0], data[1], data[2]);
                        Console.WriteLine($"Загруженные данные: {loadedContact.Name}, {loadedContact.PhoneNumber}, {loadedContact.Email}");
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