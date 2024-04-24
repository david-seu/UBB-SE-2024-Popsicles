using System.Windows;
using System.Windows.Controls;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.ViewModels;

namespace UBB_SE_2024_Popsicles
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GroupListListBox.SelectedIndex = 0;
        }

        private void GroupListListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedGroup = listBox.SelectedItem as Group;

            if (selectedGroup != null)
            {
                // Ensure that DataContext is MainViewModel
                if (DataContext is MainWindowViewModel mainViewModel)
                {
                    mainViewModel.CurrentlySelectedGroup = selectedGroup;
                    GroupViewModel viewModelForSelectedGroup = new GroupViewModel(selectedGroup);
                    viewModelForSelectedGroup.PropertyChanged += GroupViewModel_PropertyChanged;
                    mainViewModel.ViewModelCorrespondingToTheCurrentlySelectedGroup = viewModelForSelectedGroup;
                }
            }
        }

        private void GroupViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement CreateGroupButton_Click
        }
    }
}