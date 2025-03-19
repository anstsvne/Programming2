using System;
using System.IO;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Команда загрузки данных контакта из JSON-файла в ViewModel.
    /// Реализует интерфейс ICommand.
    /// </summary>
    public class LoadCommand : ICommand
    {
        /// <summary>
        /// Приватное поле класса MainVM, содержащее данные контакта.
        /// </summary>
        private readonly MainVM _viewModel;

        /// <summary>
        /// Создает новую команду загрузки.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel, в который будут загружены данные контакта.</param>
        public LoadCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Событие, вызываемое при изменении состояния команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Загружает данные контакта из JSON-файла и обновляет поля ViewModel.
        /// </summary>
        /// <param name="parameter">Не используется.</param>
        public void Execute(object parameter)
        {
            Contact contact = ContactSerializer.LoadContact();
            _viewModel.Name = contact.Name;
            _viewModel.PhoneNumber = contact.PhoneNumber;
            _viewModel.Email = contact.Email;
        }

        /// <summary>
        /// Определяет, может ли команда выполняться.
        /// </summary>
        /// <param name="parameter">Не используется.</param>
        /// <returns>Всегда возвращает true.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
