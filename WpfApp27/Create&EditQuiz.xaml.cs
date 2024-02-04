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
        ApplicationViewModelQuiz viewModel;
        int currentindex;
        Client client;
        public Create_EditQuiz(Client client,ObservableCollection<Quiz> quizzes, int currentindex)
        {
            InitializeComponent();
            viewModel = new ApplicationViewModelQuiz(quizzes);
            this.currentindex = currentindex;
            this.client = client;
            answerlist.ItemsSource = viewModel.Quizzes[currentindex].Questions;
            studentlist.ItemsSource = viewModel.Quizzes[currentindex].StudentsResults.Keys;
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

        private void studentlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = -1, y = -1, z = -1;
            if (studentlist.SelectedIndex==-1)
            {
                studentlist.SelectedIndex = -1;
                return;
            }
            for (int i = 0; i<3; i++)
                switch (i)
                {
                    case 0:
                        x = viewModel.Quizzes[currentindex].StudentsResults[studentlist.SelectedItem.ToString()][0];
                        break;
                    case 1:
                        y = viewModel.Quizzes[currentindex].StudentsResults[studentlist.SelectedItem.ToString()][1];
                        break;
                    case 2:
                        z = viewModel.Quizzes[currentindex].StudentsResults[studentlist.SelectedItem.ToString()][2];
                        break;
                }
            if (y == -1)
            {
                MessageBox.Show($"Student's result:\nFirst attempt: {x}/{viewModel.Quizzes[currentindex].Answers.Count}");
            }
            else if (z==-1)
            {
                MessageBox.Show($"Student's results:\nFirst attempt: {x}/{viewModel.Quizzes[currentindex].Answers.Count}\nSecond attempt: {y}/{viewModel.Quizzes[currentindex].Answers.Count}");
            }
            else
            {
                MessageBox.Show($"Student's results:\nFirst attempt: {x}/{viewModel.Quizzes[currentindex].Answers.Count}\nSecond attempt: {y}/{viewModel.Quizzes[currentindex].Answers.Count}\nThird attempt: {z}/{viewModel.Quizzes[currentindex].Answers.Count}\n");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            studentlist.SelectedIndex =  -1;
        }
    }
}
