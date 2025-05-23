using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using View.Model;
using View.Model.Services;

namespace ViewModel
{
    /// <summary>
    /// ViewModel главного окна, управляющая списком контактов и действиями над ними.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Сервис для сериализации и десериализации контактов в/из JSON-файла.
        /// Используется для постоянного хранения данных между запусками приложения.
        /// </summary>
        private readonly ContactSerializer _serializer;

        /// <summary>
        /// Коллекция контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; }

        /// <summary>
        /// Текущий выбранный контакт из списка.
        /// При выборе происходит обновление состояния команд и отображаемых данных. 
        /// </summary>
        private Contact _selectedContact;

        /// <summary>
        /// Выбранный контакт из списка.
        /// При установке также создаёт его копию для редактирования.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();

                if (value != null)
                {
                    ClonedContact = value.Clone();
                    IsContactReadOnly = true;
                    OnPropertyChanged(nameof(IsApplyVisible));

                }
            }
        }

        /// <summary>
        /// Копия выбранного контакта, используемая для редактирования без изменения оригинала.
        /// </summary>
        private Contact _clonedContact;

        /// <summary>
        /// Клонированный контакт, используемый для редактирования, чтобы сохранить оригинал.
        /// </summary>
        public Contact ClonedContact
        {
            get => _clonedContact;
            set
            {
                _clonedContact = value;
                OnPropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Определяет, разрешено ли редактирование контакта.
        /// </summary>
        private bool _isContactReadOnly = true;

        /// <summary>
        /// Признак того, что контакт доступен только для чтения.
        /// Управляет доступностью полей ввода и видимостью кнопки применения.
        /// </summary>
        public bool IsContactReadOnly
        {
            get => _isContactReadOnly;
            set
            {
                _isContactReadOnly = value;
                OnPropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Отображает кнопку "Применить", если контакт находится в режиме редактирования.
        /// </summary>
        public bool IsApplyVisible => !IsContactReadOnly;

        /// <summary>
        /// Команда применения изменений к выбранному контакту.
        /// </summary>
        public RelayCommand ApplyCommand { get; }

        /// <summary>
        /// Команда добавления нового контакта.
        /// </summary>
        public RelayCommand AddCommand { get; }

        /// <summary>
        /// Команда перехода в режим редактирования выбранного контакта.
        /// </summary>
        public RelayCommand EditCommand { get; }

        /// <summary>
        /// Команда удаления выбранного контакта.
        /// </summary>
        public RelayCommand RemoveCommand { get; }

        /// <summary>
        /// Конструктор ViewModel. Загружает контакты и инициализирует команды.
        /// </summary>
        public MainVM()
        {
            _serializer = new ContactSerializer();
            Contacts = _serializer.LoadContacts();

            ApplyCommand = new RelayCommand(ApplyChanges, CanApplyChanges);
            AddCommand = new RelayCommand(AddContact);
            EditCommand = new RelayCommand(EditContact, () => SelectedContact != null);
            RemoveCommand = new RelayCommand(RemoveContact, () => SelectedContact != null);
        }

        /// <summary>
        /// Добавляет новый контакт и переключает в режим редактирования.
        /// </summary>
        private void AddContact()
        {
            var newContact = new Contact
            {
                Name = "New Contact",
                PhoneNumber = "",
                Email = ""
            };
            Contacts.Add(newContact);
            SelectedContact = newContact;
            EditContact();
            _serializer.SaveContacts(Contacts);
        }

        /// <summary>
        /// Переводит выбранный контакт в режим редактирования.
        /// </summary>
        private void EditContact()
        {
            if (SelectedContact != null)
            {
                ClonedContact = SelectedContact.Clone();
                IsContactReadOnly = false;
                OnPropertyChanged(nameof(IsApplyVisible));
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Удаляет выбранный контакт и сохраняет обновлённый список.
        /// </summary>
        private void RemoveContact()
        {
            if (SelectedContact != null)
            {
                Contacts.Remove(SelectedContact);
                SelectedContact = null;
                ClonedContact = null;
                IsContactReadOnly = true;
                OnPropertyChanged(nameof(IsApplyVisible));
                ApplyCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
                _serializer.SaveContacts(Contacts);
            }
        }

        /// <summary>
        /// Применяет изменения из клонированного контакта к оригинальному и сохраняет.
        /// </summary>
        private void ApplyChanges()
        {
            if (SelectedContact != null && ClonedContact != null)
            {
                SelectedContact.Name = ClonedContact.Name;
                SelectedContact.PhoneNumber = ClonedContact.PhoneNumber;
                SelectedContact.Email = ClonedContact.Email;
                _serializer.SaveContacts(Contacts);
            }

            IsContactReadOnly = true;
            OnPropertyChanged(nameof(IsApplyVisible));
            ApplyCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Определяет, доступна ли кнопка применения изменений.
        /// </summary>
        private bool CanApplyChanges()
        {
            return ClonedContact != null && !IsContactReadOnly;
        }

        /// <summary>
        /// Событие уведомления об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Генерация события об изменении свойства.
        /// </summary>
        /// <param name="name">Имя свойства (определяется автоматически).</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}