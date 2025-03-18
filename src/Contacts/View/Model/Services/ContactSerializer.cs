using System;
using System.IO;
using Newtonsoft.Json;

namespace View.Model.Services
{
    public static class ContactSerializer
    {
        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Contacts","contacts.json");

        public static void Save(Contact contact)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

                var json = JsonConvert.SerializeObject(contact, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        public static Contact Load()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    if (string.IsNullOrWhiteSpace(json))
                        return new Contact();

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
