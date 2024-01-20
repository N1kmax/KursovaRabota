using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp27
{
    public class ApplicationViewModelQuiz
    {
        private Quiz selecteduser;

        public ObservableCollection<Quiz> Quizzes { get; set; }
        public Quiz SelectedQuiz
        {
            get
            {
                return selecteduser;
            }
            set
            {
                selecteduser = value;
                OnPropetyChnaged("SelectedQuiz");
            }
        }

        public ApplicationViewModelQuiz()
        {
            Quizzes = new ObservableCollection<Quiz>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropetyChnaged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
