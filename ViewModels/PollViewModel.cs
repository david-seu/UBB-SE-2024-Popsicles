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
        public PollViewModel(Poll selectedPoll)
        {
            SelectedPoll = selectedPoll;
        }

        private Poll selectedPoll;
        public Poll SelectedPoll
        {
            get => selectedPoll;
            set
            {
                selectedPoll = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => SelectedPoll.Description;
            set
            {
                SelectedPoll.Description = value;
                OnPropertyChanged();
            }
        }

        public string EndDate
        {
            get => SelectedPoll.EndTime.ToString();
            set
            {
                SelectedPoll.EndTime = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }

        public string Option0
        {
            get => SelectedPoll.Options[0];
            set
            {
                SelectedPoll.Options[0] = value;
                OnPropertyChanged();
            }
        }

        public string Option1
        {
            get => SelectedPoll.Options[1];
            set
            {
                SelectedPoll.Options[1] = value;
                OnPropertyChanged();
            }
        }

        public string Option2
        {
            get => SelectedPoll.Options[2];
            set
            {
                SelectedPoll.Options[2] = value;
                OnPropertyChanged();
            }
        }

        public string Option3
        {
            get => SelectedPoll.Options[3];
            set
            {
                SelectedPoll.Options[3] = value;
                OnPropertyChanged();
            }
        }
    }
}
