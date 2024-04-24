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
        public PollViewModel(Poll pollThatIsEncapsulatedByThisInstanceOnAViewModel)
        {
            PollThatIsEncapsulatedByThisInstanceOnViewModel = pollThatIsEncapsulatedByThisInstanceOnAViewModel;
        }

        private Poll pollThatIsEncapsulatedByThisInstanceOnViewModel;
        public Poll PollThatIsEncapsulatedByThisInstanceOnViewModel
        {
            get
            {
                return this.pollThatIsEncapsulatedByThisInstanceOnViewModel;
            }
            set
            {
                this.pollThatIsEncapsulatedByThisInstanceOnViewModel = value;
                OnPropertyChanged();
            }
        }

        public string DescriptionOfThePoll
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.Description;
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.Description = value;
                OnPropertyChanged();
            }
        }

        public string DueDateOfThePollInStringFormat
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.EndTime.ToString();
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.EndTime = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }

        public string FirstPossibleOptionOfThePoll
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[0];
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[0] = value;
                OnPropertyChanged();
            }
        }

        public string SecondPossibleOptionOfThePoll
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[1];
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[1] = value;
                OnPropertyChanged();
            }
        }

        public string ThirdPossibleOptionOfThePoll
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[2];
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[2] = value;
                OnPropertyChanged();
            }
        }

        public string FourthPossibleOptionOfThePoll
        {
            get
            {
                return PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[3];
            }
            set
            {
                PollThatIsEncapsulatedByThisInstanceOnViewModel.Options[3] = value;
                OnPropertyChanged();
            }
        }
    }
}
