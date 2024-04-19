using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.MVVM;

namespace UBB_SE_2024_Popsicles.ViewModels
{
    public class GroupPostViewModel : ViewModelBase
    {
        private string _userNameGroupName;
        public string UserNameGroupName
        {
            get { return _userNameGroupName; }
            set
            {
                if (_userNameGroupName != value)
                {
                    _userNameGroupName = value;
                    OnPropertyChanged(nameof(UserNameGroupName));
                }
            }
        }

        private string _dateTime;
        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        private string _postDescription;
        public string PostDescription
        {
            get { return _postDescription; }
            set
            {
                if (_postDescription != value)
                {
                    _postDescription = value;
                    OnPropertyChanged(nameof(PostDescription));
                }
            }
        }

        private string _likes;
        public string Likes
        {
            get { return _likes; }
            set
            {
                if (_likes != value)
                {
                    _likes = value;
                    OnPropertyChanged(nameof(Likes));
                }
            }
        }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
