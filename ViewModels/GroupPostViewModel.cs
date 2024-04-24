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
            get
            {
                return this.userNameGroupName;
            }
            set
            {
                if (this.userNameGroupName != value)
                {
                    this.userNameGroupName = value;
                    OnPropertyChanged(nameof(UserNameGroupName));
                }
            }
        }

        private string dateTime;
        public string DateTime
        {
            get
            {
                return this.dateTime;
            }
            set
            {
                if (this.dateTime != value)
                {
                    this.dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        private string postDescription;
        public string PostDescription
        {
            get
            {
                return this.postDescription;
            }
            set
            {
                if (this.postDescription != value)
                {
                    this.postDescription = value;
                    OnPropertyChanged(nameof(PostDescription));
                }
            }
        }

        private string likes;
        public string Likes
        {
            get
            {
                return this.likes;
            }
            set
            {
                if (this.likes != value)
                {
                    this.likes = value;

                    OnPropertyChanged(nameof(Likes));
                }
            }
        }

        private string comments;
        public string Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                if (this.comments != value)
                {
                    this.comments = value;
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
