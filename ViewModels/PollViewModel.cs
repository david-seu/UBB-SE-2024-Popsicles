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
<<<<<<< HEAD
            get
            {
                return this.selectedPoll;
            }
            set
            {
                this.selectedPoll = value;
=======
            get => selectedPoll;
            set
            {
                selectedPoll = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                OnPropertyChanged();
            }
        }

        public string Description
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.Description;
            }
=======
            get => SelectedPoll.Description;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.Description = value;
                OnPropertyChanged();
            }
        }

        public string EndDate
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.EndTime.ToString();
            }
=======
            get => SelectedPoll.EndTime.ToString();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.EndTime = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }

        public string Option0
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.Options[0];
            }
=======
            get => SelectedPoll.Options[0];
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.Options[0] = value;
                OnPropertyChanged();
            }
        }

        public string Option1
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.Options[1];
            }
=======
            get => SelectedPoll.Options[1];
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.Options[1] = value;
                OnPropertyChanged();
            }
        }

        public string Option2
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.Options[2];
            }
=======
            get => SelectedPoll.Options[2];
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.Options[2] = value;
                OnPropertyChanged();
            }
        }

        public string Option3
        {
<<<<<<< HEAD
            get
            {
                return SelectedPoll.Options[3];
            }
=======
            get => SelectedPoll.Options[3];
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedPoll.Options[3] = value;
                OnPropertyChanged();
            }
        }
    }
}
