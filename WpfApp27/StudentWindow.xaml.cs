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
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        ApplicationViewModelQuiz viewModel;
        User user;
        Client client;
        public StudentWindow(Client client, User user, ApplicationViewModelQuiz quizzes)
        {
            InitializeComponent();
            this.client = client;
            this.user = user;
            viewModel = quizzes; 
            DataContext = viewModel;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (quizzes.IsSelected) 
            {
                Quizzes.SelectedIndex = -1;
                Quiz.SelectedIndex = -1;
            }
            if (Results.IsSelected)
            {
                Quizzes.SelectedIndex = -1;
                Quiz.SelectedIndex = -1;
            }
        }

        private void QuizzesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Quiz.SelectedIndex==-1 || quizzes.IsSelected) 
            {
                Quiz.SelectedIndex = -1;
                return; 
            }
            bool check = false;
            if (!viewModel.Quizzes[Quiz.SelectedIndex].StudentsResults.ContainsKey(user.Login))
            {
                check = true;
            }
            else if (viewModel.Quizzes[Quiz.SelectedIndex].StudentsResults[user.Login][2]!=-1)
            {
                check = false;
                MessageBox.Show("You've already exhausted your number of attempts.");
            }
            else
            {
                check= true;
            }
            if (check)
            {
                quizWindow quizWindow = new quizWindow(client, viewModel, Quiz.SelectedIndex, user);
                quizWindow.Show();
                this.Close();
            }
            Quiz.SelectedIndex = -1;
        }
        private void ResultsChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = -1 ,y = -1,z = -1;
            if (Quizzes.SelectedIndex==-1 || Results.IsSelected)
            {
                Quizzes.SelectedIndex = -1;
                return;
            }
            if (!viewModel.Quizzes[Quizzes.SelectedIndex].StudentsResults.ContainsKey(user.Login))
            {
                MessageBox.Show($"You have never tried this quiz");
            }
            else 
            {
                for (int i = 0; i<3; i++)
                    switch (i)
                    {
                        case 0:
                            x = viewModel.Quizzes[Quizzes.SelectedIndex].StudentsResults[user.Login][0];
                            break;
                        case 1:
                            y = viewModel.Quizzes[Quizzes.SelectedIndex].StudentsResults[user.Login][1];
                            break;
                        case 2:
                            z = viewModel.Quizzes[Quizzes.SelectedIndex].StudentsResults[user.Login][2];
                            break;
                    }
                if (y == -1)
                {
                    MessageBox.Show($"Your results:\nFirst attempt: {x}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}");
                }
                else if (z==-1)
                {
                    MessageBox.Show($"Your results:\nFirst attempt: {x}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}\nSecond attempt: {y}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}");
                }
                else
                {
                    MessageBox.Show($"Your results:\nFirst attempt: {x}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}\nSecond attempt: {y}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}\nThird attempt: {z}/{viewModel.Quizzes[Quizzes.SelectedIndex].Answers.Count}\n");
                }
            }
            Quizzes.SelectedIndex = -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.SetQuizzes(viewModel.Quizzes);
            client.SendMessage("3");
        }
    }
}
