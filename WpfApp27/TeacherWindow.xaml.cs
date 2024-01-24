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
        public TeacherWindow(Client client, User user, ObservableCollection<Quiz> quizzes)
        {
            InitializeComponent();
            User = user;
            viewModel = new ApplicationViewModelQuiz();
            viewModel1 = new ApplicationViewModelQuiz();
            viewModel.Quizzes = quizzes;
            var v = from quiz in viewModel.Quizzes
                    where quiz.Teacher.Login == user.Login
                    select quiz;
            viewModel1.Quizzes = new ObservableCollection<Quiz>(v.ToList());
            DataContext = viewModel1;
            this.client = client;
        }

        private void CreationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreationNameQuizTextBox.Text.Replace(" ", "") == "" || CreationStudentPasswordTextBox.Text.Replace(" ","") == "" || CreationNameQuizTextBox.Text.Replace(" ","").Length > 30 || CreationNameQuizTextBox.Text.Replace(" ","").Length <3 && CreationStudentPasswordTextBox.Text.Replace(" ","").Length < 7 && CreationStudentPasswordTextBox.Text.Replace(" ", "").Length > 17) return;
            viewModel.Quizzes.Add(new Quiz { Name = CreationNameQuizTextBox.Text, Teacher = User, Questions = new List<string>() { }, Answers = new List<List<string>>() { }, Right_answer = new List<List<string>>() { } });
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
            viewModel1.Quizzes.Remove(viewModel.Quizzes[Quiz.SelectedIndex]);
            
        }

        private void EditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if(Quiz.SelectedIndex == -1) return;
            Create_EditQuiz create_EditQuiz = new Create_EditQuiz(client, viewModel.Quizzes, Quiz.SelectedIndex);
            create_EditQuiz.Show();
            this.Close();
        }
    }
}
