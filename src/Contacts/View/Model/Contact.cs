using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace View.Model
{
    /// <summary>
    /// Класс, представляющий контакт с именем, номером телефона и электронной почтой.
    /// Реализует интерфейс <see cref="INotifyPropertyChanged"/> для уведомления об изменениях свойств.
    /// </summary>
    public class Contact : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Приватное поле имени
        /// </summary>
        private string _name;

        /// <summary>
        /// Приватное поле номера телефона
        /// </summary>
        private string _phoneNumber;

        /// <summary>
        /// Приватное поле эл.почты
        /// </summary>
        private string _email;

        private const int MaxLengthName = 100;

        private const int MaxLengthNumber = 100;

        private const int MaxLengthEmail = 100;

        /// <summary>
        /// Словарь ошибок
        /// </summary>
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// Регулярное выражение для проверки корректности номера телефона.
        /// </summary>
        public static readonly Regex PhoneNumberReg =
            new Regex(@"^\+?(\d{1,3})?[-. (]*(\d{1,4})[-. )]*(\d{1,4})[-. ]*(\d{1,9})$");

        /// <summary>
        /// Регулярное выражение для проверки корректности электронной почты.
        /// </summary>
        public static readonly Regex EmailReg = new Regex(@"^[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+$");

        /// <summary>
        /// Регулярное выражение для маски ввода номера телефона.
        /// </summary>
        public static readonly Regex NumberMask = new Regex(@"^[0-9+() -]*$");

        /// <summary>
        /// Событие, которое происходит при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Получает или задает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    ValidateProperty(nameof(Name), value);
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Получает или задает номер телефона контакта.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    ValidateProperty(nameof(PhoneNumber), value);
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        /// <summary>
        /// Получает или задает электронную почту контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    ValidateProperty(nameof(Email), value);
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        /// <summary>
        /// Получает все ошибки валидации, объединенные в одну строку.
        /// </summary>
        public string Error => string.Join("\n", _errors.Values);

        /// <summary>
        /// Получает сообщение об ошибке для указанного свойства.
        /// </summary>
        /// <param name="columnName">Имя свойства для проверки.</param>
        /// <returns>Сообщение об ошибке или null, если ошибок нет.</returns>
        public string this[string columnName] => _errors.TryGetValue(columnName, out var error) ? error : null;

        /// <summary>
        /// Уведомляет об изменении значения свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidateProperty(string propertyName, string value)
        {
            string error = null;

            switch (propertyName)
            {
                case nameof(Name):
                    if (string.IsNullOrWhiteSpace(value))
                        error = "Имя не может быть пустым.";
                    else if (value.Length > MaxLengthName)
                        error = "Имя не должно превышать 100 символов.";
                    break;

                case nameof(PhoneNumber):
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        error = "Номер телефона не может быть пустым.";
                    }
                    else
                    {
                        if (!NumberMask.IsMatch(value))
                            error = "Номер телефона содержит недопустимые символы.";
                        else if (value.Length > MaxLengthNumber)
                            error = "Номер телефона не должен превышать 100 символов.";
                        else if (!PhoneNumberReg.IsMatch(value))
                            error = "Номер телефона имеет неверный формат. Пример: 8-(966)-666-0066 или +7-(966)-666-0066";
                    }
                    break;

                case nameof(Email):
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        error = "Email не может быть пустым.";
                    }
                    else
                    {
                        if (value.Length > MaxLengthEmail)
                            error = "Email не должен превышать 100 символов.";
                        else if (!EmailReg.IsMatch(value))
                            error = "Email имеет неверный формат. Пример: example@domain.ru";
                    }
                    break;
            }

            if (error != null)
                _errors[propertyName] = error;
            else
                _errors.Remove(propertyName);
        }

        /// <summary>
        /// Экземпляр класса Contact.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="phoneNumber">Номер телефона.</param>
        /// <param name="email">Электронная почта.</param>
        public Contact(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// Конструктор класса Contact.
        /// </summary>
        public Contact()
        {

        }
             
        /// <summary>
        /// Создает новый объект <see cref="Contact"/>, который является копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект <see cref="Contact"/>, который является копией текущего экземпляра.</returns>
        public Contact Clone()
        {
            return (Contact)MemberwiseClone();
        }

        public Contact(Contact contact)
        {
            Name = contact.Name;
            PhoneNumber = contact.PhoneNumber;
            Email = contact.Email;
        }
    }
}