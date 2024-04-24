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
        private string userNameGroupName;
        public string UserNameGroupName
        {
            get => userNameGroupName;
            set
            {
                if (userNameGroupName != value)
                {
                    userNameGroupName = value;
                    OnPropertyChanged(nameof(UserNameGroupName));
                }
            }
        }

        private string dateTime;
        public string DateTime
        {
            get => dateTime;
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        private string postDescription;
        public string PostDescription
        {
            get => postDescription;
            set
            {
                if (postDescription != value)
                {
                    postDescription = value;
                    OnPropertyChanged(nameof(PostDescription));
                }
            }
        }

        private string likes;
        public string Likes
        {
            get => likes;
            set
            {
                if (likes != value)
                {
                    likes = value;
                    OnPropertyChanged(nameof(Likes));
                }
            }
        }

        private string comments;
        public string Comments
        {
            get => comments;
            set
            {
                if (comments != value)
                {
                    comments = value;
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
