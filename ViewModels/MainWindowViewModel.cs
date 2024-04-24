using System.Collections.ObjectModel;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.MVVM;

namespace UBB_SE_2024_Popsicles.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Group> Groups { get; set; }

        public MainWindowViewModel()
        {
            // string connection = "your connection string goes here";
            // SqlConnection sqlConnection = new SqlConnection(connection);
            // GroupMemberRepository groupMemberRepository = new GroupMemberRepository(sqlConnection);
            // GroupRepository groupRepository = new GroupRepository(sqlConnection);
            // GroupMembershipRepository groupMembershipRepository = new GroupMembershipRepository(sqlConnection);
            // RequestsRepository requestsRepository = new RequestsRepository(sqlConnection);
            Guid id = new Guid("44d5aa9a-b0f4-4e36-a21e-bdc33b97b5a5");
            GroupMember groupMember = new GroupMember(id, "Dorian", "admin", "dorian@ubb.ro", "0725702312", "No paper, no pencil but I am still drawing attention.");
            User = groupMember;

            // TODO: Replace this with a call to the repository
            Groups = new ObservableCollection<Group>
            {
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 1", "Description 1", "basket-boys", "animals", 10, true, true, "5481f1"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 2", "Description 2", "cute-girls", "lights", 20, false, false, "5481f2"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 3", "Description 3", "tech-research", "moon", 30, true, true, "5481f3"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 4", "Description 4", "tennis-club", "nature", 40, false, false, "5481f4"),
                new Group(Guid.NewGuid(), Guid.NewGuid(), "Group 5", "Description 5", "robotics-group", "woman", 50, true, true, "5481f5"),
            };

            SelectedGroup = Groups[0];
        }

        private GroupMember user;

        public GroupMember User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                OnPropertyChanged();
            }
        }

        private GroupViewModel selectedGroupViewModel;
        public GroupViewModel SelectedGroupViewModel
        {
            get => selectedGroupViewModel;
            set
            {
                selectedGroupViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
