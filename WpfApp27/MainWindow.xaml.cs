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
        UDP udp = new UDP("5000");
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => udp.ReceiveUsers());
            users = udp.GetUsers();
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
            
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        private bool CheckEmail(string email) 
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
