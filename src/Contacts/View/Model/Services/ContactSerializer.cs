using System;
using System.IO;
using Newtonsoft.Json;
using View.Model;

namespace View.Model.Services
{
    /// <summary>
    /// Статический класс для сериализации и десериализации контактов в JSON-файл.
    /// </summary>
    public static class ContactSerializer
    {
        /// <summary>
        /// Поле, содержащее путь к файлу JSON, где хранятся контакты.
        /// </summary>
        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "contacts.json");

        /// <summary>
        /// Сохраняет контакт в JSON-файл.
        /// </summary>
        /// <param name="contact">Объект Contact, который необходимо сохранить.</param>
        public static void SaveContact(Contact contact)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contact, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает контакт из JSON-файла.
        /// </summary>
        /// <returns>Объект Contact, если файл существует и успешно загружен; иначе новый пустой объект Contact.</returns>
        public static Contact LoadContact()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<Contact>(json) ?? new Contact();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            }
            return new Contact();
        }
    }
}
