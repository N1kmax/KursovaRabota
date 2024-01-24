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
    /// Логика взаимодействия для quizWindow.xaml
    /// </summary>
    public partial class quizWindow : Window
    {
        private int currentQuestionIndex;
        private int CorrectAnswersCount = 0;
        Quiz quiz;
        User user;
        public quizWindow(Quiz quiz, User user)
        {
            InitializeComponent();
            this.quiz = quiz;
            this.user = user;
            LoadQuestion();
        }


        private void LoadQuestion()
        {
            if (currentQuestionIndex < quiz.Questions.Count)
            {
                string currentQuestion = quiz.Questions[currentQuestionIndex];
                questionTextBlock.Text = currentQuestion;
                answersItemsControl.ItemsSource = quiz.Answers[currentQuestionIndex];
            }
            else
            {
                if (!quiz.StudentsResults.ContainsKey(user))
                {
                    quiz.StudentsResults.Add(user, new int[3]);
                    for (int i = 0; i < 3; i++)
                        quiz.StudentsResults[user][i] = -1;
                    quiz.StudentsResults[user][0] = CorrectAnswersCount;
                }
                else
                {
                    if (quiz.StudentsResults[user][1]==-1)
                        quiz.StudentsResults[user][1] = CorrectAnswersCount;
                    else
                        quiz.StudentsResults[user][2] = CorrectAnswersCount;
                }
                MessageBox.Show($"Correct answers: {CorrectAnswersCount}/{quiz.Questions.Count}");
                StudentWindow student = new StudentWindow(user, new ObservableCollection<Quiz>() {quiz });
                student.Show();
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
            if (selectedAnswers.Count==quiz.Right_answer[currentQuestionIndex].Count)
            {
                check =true;
                for (int j = 0; j<quiz.Right_answer[currentQuestionIndex].Count; j++)
                {
                    if (selectedAnswers.OrderBy(n => n).ToList()[j]!=quiz.Right_answer[currentQuestionIndex].OrderBy(n => n).ToList()[j])
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

            // Переход к следующему вопросу
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
    }


}

