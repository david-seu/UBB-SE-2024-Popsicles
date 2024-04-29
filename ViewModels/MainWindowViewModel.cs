using System.Collections.ObjectModel;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.MVVM;

namespace UBB_SE_2024_Popsicles.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Group> CollectionOfActiveGroups { get; set; }

        public MainWindowViewModel()
        {
            // string databaseConnection = "your databaseConnection string goes here";
            // SqlConnection sqlConnection = new SqlConnection(databaseConnection);
            // GroupMemberRepository groupMemberRepository = new GroupMemberRepository(sqlConnection);
            // GroupRepository groupRepository = new GroupRepository(sqlConnection);
            // GroupMembershipRepository groupMembershipRepository = new GroupMembershipRepository(sqlConnection);
            // JoinRequestsRepository joinRequestsRepository = new JoinRequestsRepository(sqlConnection);
            Guid idOfCurrentMockUser = new Guid("44d5aa9a-b0f4-4e36-a21e-bdc33b97b5a5");
            GroupMember mockGroupMember = new GroupMember(idOfCurrentMockUser, "Dorian", "admin", "dorian@ubb.ro", "0725702312", "No paper, no pencil but I am still drawing attention.");
            CurrentActiveUser = mockGroupMember;

            // TODO: Replace this with a call to the repository
            CollectionOfActiveGroups = new ObservableCollection<Group>
            {
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 1", "GroupDescription 1", "basket-boys", "animals", 10, true, true, "5481f1"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 2", "GroupDescription 2", "cute-girls", "lights", 20, false, false, "5481f2"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 3", "GroupDescription 3", "tech-research", "moon", 30, true, true, "5481f3"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 4", "GroupDescription 4", "tennis-club", "nature", 40, false, false, "5481f4"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 5", "GroupDescription 5", "robotics-group", "woman", 50, true, true, "5481f5"),
            };

            CurrentlySelectedGroup = CollectionOfActiveGroups[0];
        }

        private GroupMember currentActiveUser;

        public GroupMember CurrentActiveUser
        {
            get
            {
                return this.currentActiveUser;
            }
            set
            {
                this.currentActiveUser = value;
                OnPropertyChanged();
            }
        }

        private Group currentlySelectedGroup;
        public Group CurrentlySelectedGroup
        {
            get
            {
                return this.currentlySelectedGroup;
            }
            set
            {
                this.currentlySelectedGroup = value;
                OnPropertyChanged();
            }
        }

        private GroupViewModel viewModelCorrespondingToTheCurrentlySelectedGroup;
        public GroupViewModel ViewModelCorrespondingToTheCurrentlySelectedGroup
        {
            get
            {
                return this.viewModelCorrespondingToTheCurrentlySelectedGroup;
            }
            set
            {
                this.viewModelCorrespondingToTheCurrentlySelectedGroup = value;
                OnPropertyChanged();
            }
        }
    }
}
