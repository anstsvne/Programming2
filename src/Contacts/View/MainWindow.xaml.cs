using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.ViewModel;

namespace View
{
    /// <summary>
    /// Главное окно приложения (MainWindow).
    /// Определяет пользовательский интерфейс и связывает его с ViewModel.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainWindow"/>.
        /// Устанавливает контекст данных для связывания с ViewModel.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
    }
}
