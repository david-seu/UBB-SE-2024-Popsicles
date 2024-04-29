using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.MVVM;

namespace UBB_SE_2024_Popsicles.ViewModels
{
    public class PollViewModel : ViewModelBase
    {
        public PollViewModel(GroupPoll groupPollThatIsEncapsulatedByThisInstanceOnAViewModel)
        {
            GroupPollThatIsEncapsulatedByThisInstanceOnViewModel = groupPollThatIsEncapsulatedByThisInstanceOnAViewModel;
        }

        private GroupPoll groupPollThatIsEncapsulatedByThisInstanceOnViewModel;
        public GroupPoll GroupPollThatIsEncapsulatedByThisInstanceOnViewModel
        {
            get
            {
                return this.groupPollThatIsEncapsulatedByThisInstanceOnViewModel;
            }
            set
            {
                this.groupPollThatIsEncapsulatedByThisInstanceOnViewModel = value;
                OnPropertyChanged();
            }
        }

        public string DescriptionOfThePoll
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollDescription;
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollDescription = value;
                OnPropertyChanged();
            }
        }

        public string DueDateOfThePollInStringFormat
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollEndTime.ToString();
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollEndTime = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }

        public string FirstPossibleOptionOfThePoll
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[0];
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[0] = value;
                OnPropertyChanged();
            }
        }

        public string SecondPossibleOptionOfThePoll
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[1];
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[1] = value;
                OnPropertyChanged();
            }
        }

        public string ThirdPossibleOptionOfThePoll
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[2];
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[2] = value;
                OnPropertyChanged();
            }
        }

        public string FourthPossibleOptionOfThePoll
        {
            get
            {
                return GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[3];
            }
            set
            {
                GroupPollThatIsEncapsulatedByThisInstanceOnViewModel.GroupPollOptions[3] = value;
                OnPropertyChanged();
            }
        }
    }
}
