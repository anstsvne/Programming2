using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Представление главного окна приложения.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует главное окно и устанавливает контекст данных.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();

        }
    }
}
