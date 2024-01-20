using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp27
{
    public class Client
    {
        public User User { get; set; }
        UDP udp { get; set; }
        public ObservableCollection<Quiz> quizzes { get; set; }
        public Client() { }
    }
}
