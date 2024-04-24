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
<<<<<<< HEAD
            get
            {
                return this.userNameGroupName;
            }
            set
            {
                if (this.userNameGroupName != value)
                {
                    this.userNameGroupName = value;
=======
            get => userNameGroupName;
            set
            {
                if (userNameGroupName != value)
                {
                    userNameGroupName = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                    OnPropertyChanged(nameof(UserNameGroupName));
                }
            }
        }

        private string dateTime;
        public string DateTime
        {
<<<<<<< HEAD
            get
            {
                return this.dateTime;
            }
            set
            {
                if (this.dateTime != value)
                {
                    this.dateTime = value;
=======
            get => dateTime;
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        private string postDescription;
        public string PostDescription
        {
<<<<<<< HEAD
            get
            {
                return this.postDescription;
            }
            set
            {
                if (this.postDescription != value)
                {
                    this.postDescription = value;
=======
            get => postDescription;
            set
            {
                if (postDescription != value)
                {
                    postDescription = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                    OnPropertyChanged(nameof(PostDescription));
                }
            }
        }

        private string likes;
        public string Likes
        {
<<<<<<< HEAD
            get
            {
                return this.likes;
            }
            set
            {
                if (this.likes != value)
                {
                    this.likes = value;
=======
            get => likes;
            set
            {
                if (likes != value)
                {
                    likes = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                    OnPropertyChanged(nameof(Likes));
                }
            }
        }

        private string comments;
        public string Comments
        {
<<<<<<< HEAD
            get
            {
                return this.comments;
            }
            set
            {
                if (this.comments != value)
                {
                    this.comments = value;
=======
            get => comments;
            set
            {
                if (comments != value)
                {
                    comments = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
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
