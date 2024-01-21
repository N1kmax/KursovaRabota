﻿using Microsoft.SqlServer.Server;
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
        ObservableCollection<User> users = new ObservableCollection<User>() { new User {Login = "qwe", Mail= "qwe", Password="qwe", UserType="Student" }, new User { Login = "Mr. Math", Mail= "qwe", Password="qwe", UserType="Teacher" } };
        Client client;
        
        int x;

        public MainWindow()
        {
            InitializeComponent();
            client = new Client();
            
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
                        if (users[x].UserType == "Student")
                        {
                            StudentWindow student = new StudentWindow(users[x], new ObservableCollection<Quiz>() { new Quiz { Name = "FirstQuiz", Questions = new List<string>() { "5+5", "4+8" }, Answers = new List<List<string>>() { new List<string>() { "4", "23", "10" }, new List<string>() { "10", "12" } }, Right_answer = new List<List<string>>() { new List<string>() { "10" }, new List<string>() { "12" } }, StudentPassword = "Math", Teacher = new User("Mr. Math", "qwe@gmail.com", "qweqwe", "Teacher"), StudentsResults = new Dictionary<User, int[]>() { } } });
                            student.Show();
                        }
                        if (users[x].UserType == "Teacher") 
                        {
                            TeacherWindow teacherWindow = new TeacherWindow(users[x], new ObservableCollection<Quiz>() { new Quiz { Name = "FirstQuiz", Questions = new List<string>() { "5+5", "4+8" }, Answers = new List<List<string>>() { new List<string>() { "4", "23", "10" }, new List<string>() { "10", "12" } }, Right_answer = new List<List<string>>() { new List<string>() { "10" }, new List<string>() { "12" } }, StudentPassword = "Math", Teacher = new User("Mr. Math", "qwe@gmail.com", "qweqwe", "Teacher"), StudentsResults = new Dictionary<User, int[]>() { } } });
                            teacherWindow.Show();
                        }
                        this.Close();
                    }
                    else
                        MessageBox.Show("User name or password isn't right");
            }
            else
                MessageBox.Show("You should fill all filds");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (SignUpLogin.Text.Replace(" ", "")!="" && Password.Password.Replace(" ", "")!="" && SignUpLogin.Text.Replace(" ", "").Length>=3 && SignUpLogin.Text.Replace(" ", "").Length <= 80 && Password.Password.Replace(" ","").Length >=7 && Password.Password.Replace(" ", "").Length <=17 && Password.Password==ConfirmPassword.Password && CheckEmail(Mail.Text) && (Teacher.IsChecked==true || Student.IsChecked==true))
            {
                try
                {
                    if(Teacher.IsChecked == true)
                        users.Add(new User {Login = SignUpLogin.Text, Password = Password.Password, Mail = Mail.Text, UserType =  "Teacher"});
                    if (Student.IsChecked == true)
                        users.Add(new User { Login = SignUpLogin.Text, Password = Password.Password, Mail = Mail.Text, UserType =  "Student"});
                    StudentWindow student = new StudentWindow(users[users.Count-1], new ObservableCollection<Quiz>() { new Quiz { Name = "FirstQuiz", Questions = new List<string>() { "5+5", "4+8" }, Answers = new List<List<string>>() { new List<string>() { "4", "23", "10" }, new List<string>() { "10", "12" } }, Right_answer = new List<List<string>>() { new List<string>() { "10" }, new List<string>() { "12" } }, StudentPassword = "Math", Teacher = new User("Mr. Math", "qwe@gmail.com", "qweqwe", "Teacher"), StudentsResults = new Dictionary<User, int[]>() { } } });
                    student.Show();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("You should fill all filds");
        }
        private bool CheckEmail(string email) 
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
