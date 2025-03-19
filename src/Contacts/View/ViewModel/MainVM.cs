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
    /// <summary>
    /// Главная модель управления контактом (ViewModel).
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Приватное поле контакта.
        /// </summary>
        private Contact _contact;

        /// <summary>
        /// Конструктор класса MainVM.
        /// Инициализирует объект Contact с пустыми значениями.
        /// </summary>
        public MainVM()
        {
            _contact = new Contact();
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
        }

        /// <summary>
        /// Свойство для команды сохранения.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Свойство для команды загрузки.
        /// </summary>
        public ICommand LoadCommand { get; }

        /// <summary>
        /// Свойство для получения и установки имени контакта.
        /// При изменении значения вызывает уведомление об изменении свойства.
        /// </summary>
        public string Name
        {
            get => _contact.Name;
            set { _contact.Name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Свойство для получения и установки номера телефона контакта.
        /// При изменении значения вызывает уведомление об изменении свойства.
        /// </summary>
        public string PhoneNumber
        {
            get => _contact.PhoneNumber;
            set { _contact.PhoneNumber = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Свойство для получения и установки электронной почты контакта.
        /// При изменении значения вызывает уведомление об изменении свойства.
        /// </summary>
        public string Email
        {
            get => _contact.Email;
            set { _contact.Email = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Событие, уведомляющее об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, вызывающий событие PropertyChanged при изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства. Если не указано, используется имя вызывающего свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}