using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfApp27
{
    public class ApplicationViewModelQuiz
    {
        private string _searchText;
        private Quiz selecteduser;
        public ObservableCollection<Quiz> Quizzes { get; set; }
        private ICollectionView _collectionView;
        public ICommand SearchCommand { get; }
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

        public ApplicationViewModelQuiz(ObservableCollection<Quiz> quizzes )
        {
            Quizzes = quizzes;
            SearchCommand = new RelayCommand(() => _collectionView.Refresh());
            _collectionView = CollectionViewSource.GetDefaultView(Quizzes);
            _collectionView.Filter = FilterItems;
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    SearchCommand.Execute(_searchText);
                }
            }
        }

        private bool FilterItems(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return true;

            if (obj is Quiz item)
            {
                return item.Name.ToLower().Contains(SearchText.ToLower());
            }

            return false;
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
