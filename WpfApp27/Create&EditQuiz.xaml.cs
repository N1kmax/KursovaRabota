using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        ApplicationViewModelQuiz viewModel = new ApplicationViewModelQuiz() { };
        int currentindex;
        Client client;
        public Create_EditQuiz(Client client,ObservableCollection<Quiz> quizzes, int currentindex)
        {
            InitializeComponent();
            viewModel.Quizzes = quizzes;
            this.currentindex = currentindex;
            this.client = client;
            answerlist.ItemsSource = viewModel.Quizzes[currentindex].Questions;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            viewModel.Quizzes[currentindex].Questions.Remove(viewModel.Quizzes[currentindex].Questions[answerlist.SelectedIndex]);
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindex].Questions;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            answerlist.SelectedIndex = -1;
        }

        private void booklist_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            AnswersEditWindow answersEdit = new AnswersEditWindow(viewModel.Quizzes, currentindex, answerlist.SelectedIndex);
            answersEdit.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            TeacherWindow teacherWindow = new TeacherWindow(client, viewModel.Quizzes[currentindex].Teacher, viewModel.Quizzes);
            teacherWindow.Show(); 
        }
    }
}
