using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users;
        ApplicationViewModelQuiz quizzes;
        Client client;
        int x;

        public MainWindow()
        {
            InitializeComponent();
            client = new Client();
            quizzes = new ApplicationViewModelQuiz();
            client.SendMessage("0");
            client.SendMessage("2");
            users = client.GetUsers();
            quizzes.Quizzes = client.GetQuizzes();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegestrtionPanel.Visibility = Visibility.Collapsed;
            LogInPanel.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegestrtionPanel.Visibility = Visibility.Collapsed;
            SignUpPanel.Visibility = Visibility.Visible;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool check;

            if (LogInLogin.Text.Replace(" ", "")!="" && LogInPassword.Password.Replace(" ", "")!="" && users.Count>0)
            {
                    check = false;
                    
                    for (int i = 0; i<users.Count; i++)
                        if (users[i].Login==LogInLogin.Text && users[i].Password==LogInPassword.Password)
                        {
                            check=true;
                            x=i;
                            break;
                        }
                    if (check)
                    {
                        openWindow(users[x]);
                    }
                    else
                        MessageBox.Show("User name or password isn't right");
            }
            else
                MessageBox.Show("You should fill all filds");
        }
        private void openWindow(User user) 
        {
            if (user.UserType == "Student")
            {
                StudentWindow student = new StudentWindow(client, user, quizzes);
                student.Show();
            }
            if (user.UserType == "Teacher")
            {
                TeacherWindow teacherWindow = new TeacherWindow(client, user, quizzes);
                teacherWindow.Show();
            }
            this.Close();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (SignUpLogin.Text.Replace(" ", "")!="" && Password.Password.Replace(" ", "")!="" && SignUpLogin.Text.Replace(" ", "").Length>=3 && SignUpLogin.Text.Replace(" ", "").Length <= 30 && Password.Password.Replace(" ","").Length >=7 && Password.Password.Replace(" ", "").Length <=17 && Password.Password==ConfirmPassword.Password && (Teacher.IsChecked==true || Student.IsChecked==true))
            {
                if (!CheckEmail(Mail.Text)) 
                {
                    MessageBox.Show("Email is wrong");
                    return;
                }
                if (!CheckLogin(SignUpLogin.Text)) 
                {
                    MessageBox.Show("Login is already exists");
                    return;
                }
                try
                {
                    MessageBox.Show(Student.IsChecked.ToString());
                    if (Student.IsChecked == true)
                        users.Add(new User { Login = SignUpLogin.Text, Mail = Mail.Text, Password = Password.Password, UserType =  "Student" });
                    if (Teacher.IsChecked == true)
                        users.Add(new User {Login = SignUpLogin.Text, Mail = Mail.Text, Password = Password.Password, UserType =  "Teacher"});
                    client.SendMessage("1");
                    openWindow(users[users.Count-1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("You should fill all filds");
        }
        private bool CheckLogin(string login) 
        {
            foreach (User user in users)
                if (user.Login == login)
                    return false;
            return true;
        }
        private bool CheckEmail(string email) 
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
