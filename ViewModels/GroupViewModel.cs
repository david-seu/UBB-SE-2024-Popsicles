using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.MVVM;

namespace UBB_SE_2024_Popsicles.ViewModels
{
    public class GroupViewModel : ViewModelBase
    {
        public ObservableCollection<Poll> Polls
        {
            get; set;
        }

        public ObservableCollection<PollViewModel> PollViewModels
        {
            get; set;
        }

        public ObservableCollection<GroupMember> GroupMembers
        {
            get; set;
        }

        public ObservableCollection<Request> Requests
        {
            get; set;
        }

        public ObservableCollection<GroupPost> Posts
        {
            get; set;
        }

        public GroupViewModel(Group selectedGroup)
        {
            SelectedGroup = selectedGroup;

            // TODO: Fetch posts and members from the repository
            GroupMembers = new ObservableCollection<GroupMember>
            {
                new GroupMember(Guid.NewGuid(), "Denis", "admin", "denis@ubb.ro", "0749999345", "I am stupid."),
                new GroupMember(Guid.NewGuid(), "Andreea", "admin", "denis@ubb.ro", "0749999345", "I am stupid."),
                new GroupMember(Guid.NewGuid(), "Dorian Pop", "admin", "denis@ubb.ro", "0749999345", "I am stupid."),
                new GroupMember(Guid.NewGuid(), "Razvan", "admin", "denis@ubb.ro", "0749999345", "I am stupid."),
                new GroupMember(Guid.NewGuid(), "Cristi", "admin", "denis@ubb.ro", "0749999345", "I am stupid."),
                new GroupMember(Guid.NewGuid(), "Cristos", "admin", "denis@ubb.ro", "0749999345", "I am stupid.")
            };

            Requests = new ObservableCollection<Request>()
            {
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Vasile", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Andrei", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Maria", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Gabriel", Guid.NewGuid())
            };

            Posts = new ObservableCollection<GroupPost>
            {
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid()),
                new GroupPost(Guid.NewGuid(), Guid.NewGuid(), "This is a description", "This is an image", Guid.NewGuid())
            };

            List<Poll> polls = new List<Poll>
            {
                new Poll(Guid.NewGuid(), Guid.NewGuid(), "Mergeti la Untold?", Guid.NewGuid()),
                new Poll(Guid.NewGuid(), Guid.NewGuid(), "Il votati pe Boc?", Guid.NewGuid()),
                new Poll(Guid.NewGuid(), Guid.NewGuid(), "V-ati facut la ISS?", Guid.NewGuid()),
                new Poll(Guid.NewGuid(), Guid.NewGuid(), "Ati semnat pentru Sosoaca?", Guid.NewGuid()),
                new Poll(Guid.NewGuid(), Guid.NewGuid(), "Iti place Aqua Carpatica?", Guid.NewGuid()),
            };
            foreach (Poll poll in polls)
            {
                poll.AddOption("Da");
                poll.AddOption("Nu");
                poll.AddOption("Poate");
                poll.AddOption("Nu vreau sa raspund");
            }
            Polls = new ObservableCollection<Poll>(polls);

            List<PollViewModel> pollViewModels = new List<PollViewModel>();
            foreach (Poll poll in Polls)
            {
                pollViewModels.Add(new PollViewModel(poll));
            }
            PollViewModels = new ObservableCollection<PollViewModel>(pollViewModels);
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

        private PollViewModel selectedPollViewModel;
        public PollViewModel SelectedPollViewModel
        {
<<<<<<< HEAD
            get
            {
                return this.selectedPollViewModel;
            }
            set
            {
                this.selectedPollViewModel = value;
=======
            get => selectedPollViewModel;
            set
            {
                selectedPollViewModel = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                OnPropertyChanged();
            }
        }

        private Group selectedGroup;
        public Group SelectedGroup
        {
<<<<<<< HEAD
            get
            {
                return this.selectedGroup;
            }
            set
            {
                this.selectedGroup = value;
=======
            get => selectedGroup;
            set
            {
                selectedGroup = value;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                OnPropertyChanged();
            }
        }

        public string Name
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.Name;
            }
=======
            get => SelectedGroup.Name;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.Name = value;
                // TODO: notify somehow the main window view model that Name has changed
                OnPropertyChanged();
            }
        }

        public string BannerPath
        {
            get
            {
                return SelectedGroup.BannerPath;
            }
        }

<<<<<<< HEAD
        /// Group Settings Tab
=======
        // Group Settings Tab
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        public string Owner
        {
            // TODO: Fetch owner name from the repository
            get
            {
                return SelectedGroup.OwnerId.ToString();
            }
        }

        public string GroupCode
        {
            get
            {
                return SelectedGroup.GroupCode;
            }
        }

        public string CreatedAt
        {
            get
            {
                return SelectedGroup.CreatedAt.ToString();
            }
        }

        public string MemberCount
        {
            get
            {
                return SelectedGroup.MemberCount.ToString();
            }
        }

        public string PostCount
        {
            get
            {
                return SelectedGroup.PostCount.ToString();
            }
        }

        public string RequestCount
        {
            get
            {
                return SelectedGroup.RequestCount.ToString();
            }
        }

        public string IsPublic
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.IsPublic == true ? "Public" : "Private";
            }
=======
            get => SelectedGroup.IsPublic == true ? "Public" : "Private";
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.IsPublic = value == "Public";
                OnPropertyChanged();
            }
        }

        public RelayCommand ChangePrivacyCommand => new RelayCommand(execute => ChangePrivacy());

        private void ChangePrivacy()
        {
            IsPublic = IsPublic == "Public" ? "Private" : "Public";
        }

        public string Description
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.Description;
            }
=======
            get => SelectedGroup.Description;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.Description = value;
                OnPropertyChanged();
            }
        }

        public string MaxPosts
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.MaxPostsPerHourPerUser.ToString();
            }
=======
            get => SelectedGroup.MaxPostsPerHourPerUser.ToString();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.MaxPostsPerHourPerUser = int.Parse(value);
                OnPropertyChanged();
            }
        }

        public string CanMakePosts
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.CanMakePostsByDefault == true ? "Yes" : "No";
            }
=======
            get => SelectedGroup.CanMakePostsByDefault == true ? "Yes" : "No";
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.CanMakePostsByDefault = value == "Yes";
                OnPropertyChanged();
            }
        }

        public RelayCommand ChangeCanMakePostsCommand => new RelayCommand(execute => ChangeCanMakePosts());

        private void ChangeCanMakePosts()
        {
            CanMakePosts = CanMakePosts == "Yes" ? "No" : "Yes";
        }

        public string Icon
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.Icon;
            }
=======
            get => SelectedGroup.Icon;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.Icon = value;
                // TODO: notify somehow the main window view model that IconPath has changed
                // OnPropertyChanged("IconPath");
                OnPropertyChanged();
            }
        }

        public string Banner
        {
<<<<<<< HEAD
            get
            {
                return SelectedGroup.Banner;
            }
=======
            get => SelectedGroup.Banner;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            set
            {
                SelectedGroup.Banner = value;
                OnPropertyChanged();
                OnPropertyChanged("BannerPath");
            }
        }

<<<<<<< HEAD
        /// Requests
=======
        // Requests
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        public RelayCommand AcceptRequestCommand => new RelayCommand(execute => AcceptRequest());

        private void AcceptRequest()
        {
        }

        public RelayCommand RejectRequestCommand => new RelayCommand(execute => RejectRequest());

        private void RejectRequest()
        {
        }
    }
}
