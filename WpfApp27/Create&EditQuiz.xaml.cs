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
using System.Windows.Shapes;

namespace WpfApp27
{
    /// <summary>
    /// Логика взаимодействия для Create_EditQuiz.xaml
    /// </summary>
    public partial class Create_EditQuiz : Window
    {
        ApplicationViewModelQuiz viewModel;
        public Create_EditQuiz(Quiz quiz)
        {
            InitializeComponent();
            DataContext  = quiz;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void booklist_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
