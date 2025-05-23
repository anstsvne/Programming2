using System;
using System.Windows.Input;

/// <summary>
/// Универсальная реализация интерфейса <see cref="ICommand"/>, 
/// позволяющая связывать команды с логикой выполнения и условиями доступности.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    /// <summary>
    /// Событие, вызываемое при изменении условий выполнения команды.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RelayCommand"/>.
    /// </summary>
    /// <param name="execute">Метод, выполняемый при активации команды.</param>
    /// <param name="canExecute">Функция, определяющая, может ли команда быть выполнена (необязательно).</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="execute"/> равен <c>null</c>.</exception>
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Определяет, может ли команда быть выполнена в текущем состоянии.
    /// </summary>
    /// <param name="parameter">Параметр команды (не используется).</param>
    /// <returns><c>true</c>, если команда может быть выполнена; иначе <c>false</c>.</returns>
    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    /// <summary>
    /// Выполняет команду.
    /// </summary>
    /// <param name="parameter">Параметр команды (не используется).</param>
    public void Execute(object parameter) => _execute();

    /// <summary>
    /// Принудительно вызывает событие <see cref="CanExecuteChanged"/>, 
    /// указывающее, что результат <see cref="CanExecute"/> мог измениться.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}