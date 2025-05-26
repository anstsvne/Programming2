using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace View.Model.Services
{
    /// <summary>
    /// Класс, обеспечивающий сохранение и загрузку контактов в формате JSON.
    /// </summary>
    public class ContactSerializer
    {
        private readonly string _filePath;

        /// <summary>
        /// Настройки сериализации JSON.
        /// </summary>
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ContactSerializer"/>.
        /// Создаёт папку "Contacts" в "Мои документы" и указывает путь к файлу.
        /// </summary>
        public ContactSerializer()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactsDir = Path.Combine(documentsPath, "Contacts");

            if (!Directory.Exists(contactsDir))
                Directory.CreateDirectory(contactsDir);

            _filePath = Path.Combine(contactsDir, "contacts.json");
        }

        /// <summary>
        /// Сохраняет переданную коллекцию контактов в файл JSON.
        /// </summary>
        /// <param name="contacts">Коллекция контактов для сохранения.</param>
        public void SaveContacts(ObservableCollection<Contact> contacts)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contacts, Settings);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ошибка при сохранении контактов: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает коллекцию контактов из файла JSON.
        /// </summary>
        /// <returns>
        /// Возвращает коллекцию <see cref="ObservableCollection{Contact}"/>. 
        /// Если файл отсутствует или произошла ошибка, возвращает пустую коллекцию.
        /// </returns>
        public ObservableCollection<Contact> LoadContacts()
        {
            if (!File.Exists(_filePath))
                return new ObservableCollection<Contact>();

            try
            {
                string json = File.ReadAllText(_filePath);
                var contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json, Settings);
                return contacts ?? new ObservableCollection<Contact>();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ошибка при загрузке контактов: {ex.Message}");
                return new ObservableCollection<Contact>();
            }
        }
    }
}