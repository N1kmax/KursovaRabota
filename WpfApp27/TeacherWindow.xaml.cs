using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace WpfApp27
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        ApplicationViewModelQuiz viewModel;
        ApplicationViewModelQuiz viewModel1;
        User User;
        Client client;
        public TeacherWindow(Client client, User user, ApplicationViewModelQuiz quizzes)
        {
            InitializeComponent();
            User = user;
            viewModel = quizzes;
            var v = from quiz in viewModel.Quizzes
                    where quiz.Teacher.Login == user.Login
                    select quiz;
            viewModel1 = new ApplicationViewModelQuiz(new ObservableCollection<Quiz>(v.ToList()));
            DataContext = viewModel1;
            this.client = client;
        }

        private void CreationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreationNameQuizTextBox.Text.Replace(" ", "") == "" || CreationNameQuizTextBox.Text.Replace(" ","").Length > 30 || CreationNameQuizTextBox.Text.Replace(" ","").Length <3) return;
            viewModel.Quizzes.Add(new Quiz { Name = CreationNameQuizTextBox.Text, Teacher = User, Questions = new List<string>() { "New question" }, Answers = new List<List<string>>() { new List<string>() { "Correct answer" } }, StudentsResults = new Dictionary<string, int[]>(){ }, Right_answer = new List<List<string>>() { new List<string>() { "Correct answer" } } });
            Create_EditQuiz create_EditQuiz = new Create_EditQuiz(client,viewModel.Quizzes, viewModel.Quizzes.Count-1);
            create_EditQuiz.Show();
            this.Close();
        }

        private void ResultsChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Quiz.SelectedIndex == -1) return;
            viewModel.Quizzes.Remove(viewModel1.Quizzes[Quiz.SelectedIndex]);
            viewModel1.Quizzes.Remove(viewModel1.Quizzes[Quiz.SelectedIndex]);
        }

        private void EditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if(Quiz.SelectedIndex == -1) return;
            for (int i = 0; i < viewModel.Quizzes.Count; i++)
                if (viewModel.Quizzes[i] == viewModel1.Quizzes[Quiz.SelectedIndex])
                {
                    Create_EditQuiz create_EditQuiz = new Create_EditQuiz(client, viewModel.Quizzes, i);
                    create_EditQuiz.Show();
                }
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.SetQuizzes(viewModel.Quizzes);
            client.SendMessage("3");
        }
    }
}
