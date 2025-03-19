using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace View.Model
{
    /// <summary>
    /// Класс, представляющий контакт с именем, номером телефона и электронной почтой.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Получает или задает имя контакта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает номер телефона контакта.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Получает или задает электронную почту контакта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Получает или задает дополнительный номер телефона контакта.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Конструктор по умолчанию. Инициализирует свойства пустыми строками.
        /// </summary>
        public Contact()
        {
            Name = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
        }

        /// <summary>
        /// Параметризованный конструктор. Инициализирует свойства переданными значениями.
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        /// <param name="phoneNumber">Номер телефона контакта.</param>
        /// <param name="email">Электронная почта контакта.</param>
        public Contact(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}