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
        ApplicationViewModelQuiz viewModel;
        int currentindexQuiz, currentindexQuestion;
        public AnswersEditWindow(ObservableCollection<Quiz> quizzes, int currentindexQuiz, int currentindexQuestion)
        {
            InitializeComponent();
            viewModel = new ApplicationViewModelQuiz(quizzes);
            this.currentindexQuiz = currentindexQuiz;
            this.currentindexQuestion = currentindexQuestion;
            answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            rightanswerlist.ItemsSource  =viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            if (viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Contains(viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex])) 
            {
                if (viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Count == 1)
                {
                    MessageBox.Show("Question need to have at least one correct answer");
                    return;
                }
                viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Remove(viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex]);
                rightanswerlist.ItemsSource = null;
                rightanswerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
            }
            viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Remove(viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex]);
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (rightanswerlist.SelectedIndex == -1) return;
            if (viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Count == 1)
            {
                MessageBox.Show("Question need to have at least one correct answer");
                return;
            }
            viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Remove(viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][rightanswerlist.SelectedIndex]);
            answerlist.ItemsSource = null;
            answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Remove(viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][rightanswerlist.SelectedIndex]);
            rightanswerlist.ItemsSource = null;
            rightanswerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            rightanswerlist.SelectedIndex = -1;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            answerlist.SelectedIndex = -1;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (answerlist.SelectedIndex == -1) return;
            if (!viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Contains(AnswerBox.Text.Replace(" ","")) && AnswerBox.Text.Replace(" ", "") != "" ) 
            {
                if (viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Contains(viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex]))
                    for (int i = 0; i < viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Count; i++)
                    {
                        if (viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][i] == viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex])
                        {
                            rightanswerlist.ItemsSource = null;
                            rightanswerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
                            viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][i] = AnswerBox.Text;
                            break;
                        }
                    }
                viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][answerlist.SelectedIndex] = AnswerBox.Text;
                answerlist.SelectedIndex = -1;
                answerlist.ItemsSource = null;
                answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            }
        }

        private void answerlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (!viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Contains(RightAnswerBox.Text.Replace(" ", "")) && RightAnswerBox.Text.Replace(" ", "") != "" && !viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Contains(AnswerBox.Text.Replace(" ", "")))
            {
                viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Add(RightAnswerBox.Text);
                viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Add(RightAnswerBox.Text);
                rightanswerlist.ItemsSource = null;
                rightanswerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
                answerlist.ItemsSource = null;
                answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            }
            else
                MessageBox.Show("The textBox is empty or this answer already exsists");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (rightanswerlist.SelectedIndex == -1) return;
            if (!viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion].Contains(RightAnswerBox.Text.Replace(" ", "")) && RightAnswerBox.Text.Replace(" ", "") != "")
            {
                for (int i = 0; i < viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Count; i++) 
                {
                    if (viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][i] == viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][rightanswerlist.SelectedIndex]) 
                    {
                        viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion][i] = RightAnswerBox.Text;
                        break;
                    }
                }
                viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion][rightanswerlist.SelectedIndex] = RightAnswerBox.Text;
                rightanswerlist.SelectedIndex = -1;
                rightanswerlist.ItemsSource = null;
                answerlist.ItemsSource = null;
                answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
                rightanswerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Right_answer[currentindexQuestion];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Contains(AnswerBox.Text.Replace(" ","")) && AnswerBox.Text.Replace(" ", "") != "")
            {
                viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion].Add(AnswerBox.Text);
                answerlist.ItemsSource = null;
                answerlist.ItemsSource = viewModel.Quizzes[currentindexQuiz].Answers[currentindexQuestion];
            }
            else
                MessageBox.Show("The textBox is empty or this answer already exsists");
        }
    }
}
