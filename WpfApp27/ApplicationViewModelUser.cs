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
    public class ApplicationViewModelUser : INotifyPropertyChanged
    {
        private User selecteduser;

        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser
        {
            get
            {
                return selecteduser;
            }
            set
            {
                selecteduser = value;
                OnPropetyChnaged("SelectedUser");
            }
        }

        public ApplicationViewModelUser()
        {
            Users = new ObservableCollection<User>();
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
