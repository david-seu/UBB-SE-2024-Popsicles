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
        private string groupSpecificUserName;
        public string GroupSpecificUserName
        {
            get
            {
                return this.groupSpecificUserName;
            }
            set
            {
                if (this.groupSpecificUserName != value)
                {
                    this.groupSpecificUserName = value;
                    OnPropertyChanged(nameof(GroupSpecificUserName));
                }
            }
        }

        private string dateAndTimeOfPosting;
        public string DateAndTimeOfPosting
        {
            get
            {
                return this.dateAndTimeOfPosting;
            }
            set
            {
                if (this.dateAndTimeOfPosting != value)
                {
                    this.dateAndTimeOfPosting = value;
                    OnPropertyChanged(nameof(DateAndTimeOfPosting));
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

        private string likeCounterInStringFormat;
        public string LikeCounterInStringFormat
        {
            get
            {
                return this.likeCounterInStringFormat;
            }
            set
            {
                if (this.likeCounterInStringFormat != value)
                {
                    this.likeCounterInStringFormat = value;

                    OnPropertyChanged(nameof(LikeCounterInStringFormat));
                }
            }
        }

        private string commentsLeftOnThePost;
        public string CommentsLeftOnThePost
        {
            get
            {
                return this.commentsLeftOnThePost;
            }
            set
            {
                if (this.commentsLeftOnThePost != value)
                {
                    this.commentsLeftOnThePost = value;
                    OnPropertyChanged(nameof(CommentsLeftOnThePost));
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
