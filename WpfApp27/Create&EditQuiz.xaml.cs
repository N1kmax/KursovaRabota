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
            viewModel.Quizzes[currentindex].Questions.Add("New question");
            viewModel.Quizzes[currentindex].Answers.Add(new List<string>(){"Correct answer" });
            viewModel.Quizzes[currentindex].Right_answer.Add(new List<string>() { "Correct answer" });
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindex].Questions;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            if (viewModel.Quizzes[currentindex].Questions.Count==1)
            {
                MessageBox.Show("Every quiz must have at least one question");
                return;
            }
            viewModel.Quizzes[currentindex].Questions.Remove(viewModel.Quizzes[currentindex].Questions[answerlist.SelectedIndex]);
            viewModel.Quizzes[currentindex].Answers.Remove(viewModel.Quizzes[currentindex].Answers[answerlist.SelectedIndex]);
            viewModel.Quizzes[currentindex].Right_answer.Remove(viewModel.Quizzes[currentindex].Right_answer[answerlist.SelectedIndex]);
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
            QuestionBox.Text = answerlist.SelectedItem.ToString();
            AnswersEditWindow answersEdit = new AnswersEditWindow(viewModel.Quizzes, currentindex, answerlist.SelectedIndex);
            answersEdit.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            TeacherWindow teacherWindow = new TeacherWindow(client, viewModel.Quizzes[currentindex].Teacher, viewModel);
            teacherWindow.Show(); 
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1 || QuestionBox.Text.Replace(" ","")=="") return;
            viewModel.Quizzes[currentindex].Questions[answerlist.SelectedIndex] = QuestionBox.Text;
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindex].Questions;
        }
    }
}
