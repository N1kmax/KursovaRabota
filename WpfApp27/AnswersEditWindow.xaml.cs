using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AnswersEditWindow.xaml
    /// </summary>
    public partial class AnswersEditWindow : Window
    {
        ApplicationViewModelQuiz viewModel = new ApplicationViewModelQuiz() { };
        int currentindexQuiz, currentindexQuestion;
        public AnswersEditWindow(ObservableCollection<Quiz> quizzes, int currentindexQuiz, int currentindexQuestion)
        {
            InitializeComponent();
            viewModel.Quizzes = quizzes;
            this.currentindexQuiz = currentindexQuiz;
            this.currentindexQuestion = currentindexQuestion;
            answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            rightanswerlist.ItemsSource  =viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Remove(viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex]);
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
