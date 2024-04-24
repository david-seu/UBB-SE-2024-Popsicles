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
        public ObservableCollection<Poll> CollectionOfPolls
        {
            get; set;
        }

        public ObservableCollection<PollViewModel> CollectionOfViewModelsForEachIndividualPoll
        {
            get; set;
        }

        public ObservableCollection<GroupMember> GroupMembers
        {
            get; set;
        }

        public ObservableCollection<Request> RequestsToJoinTheGroup
        {
            get; set;
        }

        public ObservableCollection<GroupPost> PostsMadeInTheGroupChat
        {
            get; set;
        }

        public GroupViewModel(Group selectedGroup)
        {
            GroupThatIsEncapsulatedByThisInstanceOnViewModel = selectedGroup;

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

            RequestsToJoinTheGroup = new ObservableCollection<Request>()
            {
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Vasile", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Andrei", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Maria", Guid.NewGuid()),
                new Request(Guid.NewGuid(), Guid.NewGuid(), "Gabriel", Guid.NewGuid())
            };

            PostsMadeInTheGroupChat = new ObservableCollection<GroupPost>
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
            CollectionOfPolls = new ObservableCollection<Poll>(polls);

            List<PollViewModel> pollViewModels = new List<PollViewModel>();
            foreach (Poll poll in CollectionOfPolls)
            {
                pollViewModels.Add(new PollViewModel(poll));
            }
            CollectionOfViewModelsForEachIndividualPoll = new ObservableCollection<PollViewModel>(pollViewModels);
        }

        private Poll currentlySelectedPoll;
        public Poll CurrentlySelectedPoll
        {
            get
            {
                return this.currentlySelectedPoll;
            }
            set
            {
                this.currentlySelectedPoll = value;
                OnPropertyChanged();
            }
        }

        private PollViewModel viewModelCorrecpondingToCurrentlySelectedPoll;
        public PollViewModel ViewModelCorrecpondingToCurrentlySelectedPoll
        {
            get
            {
                return this.viewModelCorrecpondingToCurrentlySelectedPoll;
            }
            set
            {
                this.viewModelCorrecpondingToCurrentlySelectedPoll = value;
                OnPropertyChanged();
            }
        }

        // ???
        private Group groupThatIsEncapsulatedByThisInstanceOnViewModel;
        public Group GroupThatIsEncapsulatedByThisInstanceOnViewModel
        {
            get
            {
                return this.groupThatIsEncapsulatedByThisInstanceOnViewModel;
            }
            set
            {
                this.groupThatIsEncapsulatedByThisInstanceOnViewModel = value;
                OnPropertyChanged();
            }
        }

        public string GroupName
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.Name;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.Name = value;
                // TODO: notify somehow the main window view model that Name has changed
                OnPropertyChanged();
            }
        }

        public string DirectoryPathToTheGroupsBannerImageFile
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.BannerPath;
            }
        }
        // Group Settings Tab
        public string NameOfTheGroupsOwner
        {
            // TODO: Fetch owner name from the repository
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.OwnerId.ToString();
            }
        }

        // nush redenumi asta
        public string UniqueGroupCode
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupCode;
            }
        }

        public string DateOfCreationInStringFormat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.CreatedAt.ToString();
            }
        }

        public string MemberCounterInStringFormat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.MemberCount.ToString();
            }
        }

        public string PostCounterInStringFormat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.PostCount.ToString();
            }
        }

        public string RequestCounterInStringFormat
        {
            get
            {
                // ma everva ca afisa 0. DACA codul ar merge, ai folosi ca mai sus
                return RequestsToJoinTheGroup.Count.ToString();
                // return GroupThatIsEncapsulatedByThisInstanceOnViewModel.RequestCount.ToString();
            }
        }

        public string IsTheGroupPublicToOutsiders
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.IsPublic == true ? "Public" : "Private";
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.IsPublic = value == "Public";
                OnPropertyChanged();
            }
        }

        public RelayCommand ChangePrivacyPolicyCommand => new RelayCommand(execute => ChangePrivacyPolicyOfTheGroup());

        private void ChangePrivacyPolicyOfTheGroup()
        {
            IsTheGroupPublicToOutsiders = IsTheGroupPublicToOutsiders == "Public" ? "Private" : "Public";
        }

        public string DescriptionOfTheGroup
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.Description;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.Description = value;
                OnPropertyChanged();
            }
        }

        public string MaximumAmountOfPostsAllowed
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.MaxPostsPerHourPerUser.ToString();
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.MaxPostsPerHourPerUser = int.Parse(value);
                OnPropertyChanged();
            }
        }

        public string AllowanceOfPostageOnTheGroupChat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.CanMakePostsByDefault == true ? "Yes" : "No";
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.CanMakePostsByDefault = value == "Yes";
                OnPropertyChanged();
            }
        }

        public RelayCommand ChangeAllowanceOfPostageCommand => new RelayCommand(execute => ChangeAllowanceOfPostage());

        private void ChangeAllowanceOfPostage()
        {
            AllowanceOfPostageOnTheGroupChat = AllowanceOfPostageOnTheGroupChat == "Yes" ? "No" : "Yes";
        }

        /// cum plm poate fi o iconita string???
        public string NameOfTheGroupsIcon
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.Icon;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.Icon = value;
                // TODO: notify somehow the main window view model that IconPath has changed
                // OnPropertyChanged("IconPath");
                OnPropertyChanged();
            }
        }

        public string NameOfTheGroupsBanner
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.Banner;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.Banner = value;
                OnPropertyChanged();
                OnPropertyChanged("BannerPath");
            }
        }
        // Requests
        public RelayCommand AcceptJoinRequestCommand => new RelayCommand(execute => AcceptJoinRequest());

        private void AcceptJoinRequest()
        {
        }

        public RelayCommand RejectJoinRequestCommand => new RelayCommand(execute => RejectJoinRequest());

        private void RejectJoinRequest()
        {
        }
    }
}
