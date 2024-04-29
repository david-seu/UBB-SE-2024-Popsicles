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
        public ObservableCollection<GroupPoll> CollectionOfPolls
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

        public ObservableCollection<JoinRequest> RequestsToJoinTheGroup
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

            RequestsToJoinTheGroup = new ObservableCollection<JoinRequest>()
            {
                new JoinRequest(Guid.NewGuid(), Guid.NewGuid(), "Vasile", Guid.NewGuid()),
                new JoinRequest(Guid.NewGuid(), Guid.NewGuid(), "Andrei", Guid.NewGuid()),
                new JoinRequest(Guid.NewGuid(), Guid.NewGuid(), "Maria", Guid.NewGuid()),
                new JoinRequest(Guid.NewGuid(), Guid.NewGuid(), "Gabriel", Guid.NewGuid())
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

            List<GroupPoll> polls = new List<GroupPoll>
            {
                new GroupPoll(Guid.NewGuid(), Guid.NewGuid(), "Mergeti la Untold?", Guid.NewGuid()),
                new GroupPoll(Guid.NewGuid(), Guid.NewGuid(), "Il votati pe Boc?", Guid.NewGuid()),
                new GroupPoll(Guid.NewGuid(), Guid.NewGuid(), "V-ati facut la ISS?", Guid.NewGuid()),
                new GroupPoll(Guid.NewGuid(), Guid.NewGuid(), "Ati semnat pentru Sosoaca?", Guid.NewGuid()),
                new GroupPoll(Guid.NewGuid(), Guid.NewGuid(), "Iti place Aqua Carpatica?", Guid.NewGuid()),
            };
            foreach (GroupPoll poll in polls)
            {
                poll.AddGroupPollOption("Da");
                poll.AddGroupPollOption("Nu");
                poll.AddGroupPollOption("Poate");
                poll.AddGroupPollOption("Nu vreau sa raspund");
            }
            CollectionOfPolls = new ObservableCollection<GroupPoll>(polls);

            List<PollViewModel> pollViewModels = new List<PollViewModel>();
            foreach (GroupPoll poll in CollectionOfPolls)
            {
                pollViewModels.Add(new PollViewModel(poll));
            }
            CollectionOfViewModelsForEachIndividualPoll = new ObservableCollection<PollViewModel>(pollViewModels);
        }

        private GroupPoll currentlySelectedGroupPoll;
        public GroupPoll CurrentlySelectedGroupPoll
        {
            get
            {
                return this.currentlySelectedGroupPoll;
            }
            set
            {
                this.currentlySelectedGroupPoll = value;
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
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupName;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupName = value;
                // TODO: notify somehow the main window view model that GroupName has changed
                OnPropertyChanged();
            }
        }

        public string DirectoryPathToTheGroupsBannerImageFile
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupBannerPath;
            }
        }
        // Group Settings Tab
        public string NameOfTheGroupsOwner
        {
            // TODO: Fetch owner name from the repository
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupOwnerId.ToString();
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
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.DateOfGroupCreation.ToString();
            }
        }

        public string MemberCounterInStringFormat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupMemberCount.ToString();
            }
        }

        public string PostCounterInStringFormat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupPostCount.ToString();
            }
        }

        public string RequestCounterInStringFormat
        {
            get
            {
                // ma everva ca afisa 0. DACA codul ar merge, ai folosi ca mai sus
                return RequestsToJoinTheGroup.Count.ToString();
                // return GroupThatIsEncapsulatedByThisInstanceOnViewModel.JoinRequestCount.ToString();
            }
        }

        public string IsTheGroupPublicToOutsiders
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.IsGroupPublic == true ? "Public" : "Private";
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.IsGroupPublic = value == "Public";
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
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupDescription;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupDescription = value;
                OnPropertyChanged();
            }
        }

        public string MaximumAmountOfPostsAllowed
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.MaximumNumberOfPostsPerHourPerUser.ToString();
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.MaximumNumberOfPostsPerHourPerUser = int.Parse(value);
                OnPropertyChanged();
            }
        }

        public string AllowanceOfPostageOnTheGroupChat
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.AllowanceOfPostage == true ? "Yes" : "No";
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.AllowanceOfPostage = value == "Yes";
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
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupIcon;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupIcon = value;
                // TODO: notify somehow the main window view model that GroupIconPath has changed
                // OnPropertyChanged("GroupIconPath");
                OnPropertyChanged();
            }
        }

        public string NameOfTheGroupsBanner
        {
            get
            {
                return GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupBanner;
            }
            set
            {
                GroupThatIsEncapsulatedByThisInstanceOnViewModel.GroupBanner = value;
                OnPropertyChanged();
                OnPropertyChanged("GroupBannerPath");
            }
        }
        // ListOfJoinRequests
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
