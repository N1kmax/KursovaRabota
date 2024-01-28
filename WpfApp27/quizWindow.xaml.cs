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
using System.Windows.Shapes;

namespace WpfApp27
{
    /// <summary>
    /// Логика взаимодействия для quizWindow.xaml
    /// </summary>
    public partial class quizWindow : Window
    {
        private int currentQuestionIndex;
        private int CorrectAnswersCount = 0;
        int CurrentIndex;
        ApplicationViewModelQuiz quizzes;
        User user;
        Client client;
        public quizWindow(Client client, ApplicationViewModelQuiz quizzes, int CurrentIndex, User user)
        {
            InitializeComponent();
            this.quizzes = quizzes;
            this.user = user;
            this.CurrentIndex = CurrentIndex;
            this.client = client;
            LoadQuestion();
        }


        private void LoadQuestion()
        {
            if (currentQuestionIndex < quizzes.Quizzes[CurrentIndex].Questions.Count)
            {
                string currentQuestion = quizzes.Quizzes[CurrentIndex].Questions[currentQuestionIndex];
                questionTextBlock.Text = currentQuestion;
                answersItemsControl.ItemsSource = quizzes.Quizzes[CurrentIndex].Answers[currentQuestionIndex];
            }
            else
            {
                
                this.Close();
            }

        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            bool check = false; 
            ObservableCollection<string> selectedAnswers = new ObservableCollection<string>();

            foreach (var item in answersItemsControl.Items)
            {
                var container = answersItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    var checkBox = FindChild<CheckBox>(container);

                    if (checkBox != null && checkBox.Content is string answer && checkBox.IsChecked == true)
                    {
                        selectedAnswers.Add(answer);
                    }
                }
            }
            if (selectedAnswers.Count==quizzes.Quizzes[CurrentIndex].Right_answer[currentQuestionIndex].Count)
            {
                check =true;
                for (int j = 0; j<quizzes.Quizzes[CurrentIndex].Right_answer[currentQuestionIndex].Count; j++)
                {
                    if (selectedAnswers.OrderBy(n => n).ToList()[j]!=quizzes.Quizzes[CurrentIndex].Right_answer[currentQuestionIndex].OrderBy(n => n).ToList()[j])
                    {
                        check= false;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("You matched right answer");
                    CorrectAnswersCount++;
                }
            }
            else
                MessageBox.Show("You match wrong answer");
            currentQuestionIndex++;
            LoadQuestion();
        }
        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild)
                {
                    return typedChild;
                }

                T result = FindChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!quizzes.Quizzes[CurrentIndex].StudentsResults.ContainsKey(user.Login))
            {
                quizzes.Quizzes[CurrentIndex].StudentsResults.Add( user.Login, new int[3]);
                for (int i = 0; i < 3; i++)
                    quizzes.Quizzes[CurrentIndex].StudentsResults[user.Login][i] = -1;
                quizzes.Quizzes[CurrentIndex].StudentsResults[user.Login][0] = CorrectAnswersCount;
            }
            else
            {
                if (quizzes.Quizzes[CurrentIndex].StudentsResults[user.Login][1]==-1)
                    quizzes.Quizzes[CurrentIndex].StudentsResults[user.Login][1] = CorrectAnswersCount;
                else
                    quizzes.Quizzes[CurrentIndex].StudentsResults[user.Login][2] = CorrectAnswersCount;
            }
            MessageBox.Show($"Correct answers: {CorrectAnswersCount}/{quizzes.Quizzes[CurrentIndex].Questions.Count}");
            StudentWindow student = new StudentWindow(client, user, quizzes);
            student.Show();        
        }
    }


}

