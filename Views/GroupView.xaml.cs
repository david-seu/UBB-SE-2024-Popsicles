using System.Windows;
using System.Windows.Controls;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.ViewModels;

namespace UBB_SE_2024_Popsicles.Views
{
    public partial class GroupView : UserControl
    {
        public GroupView()
        {
            InitializeComponent();
        }

        private void GroupSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            GroupSettings.Visibility = Visibility.Visible;
            GroupFeed.Visibility = Visibility.Collapsed;
        }

        private void GroupPostsButton_Click(object sender, RoutedEventArgs e)
        {
            GroupFeed.Visibility = Visibility.Visible;
            PollsListBox.Visibility = Visibility.Collapsed;
            PostsListBox.Visibility = Visibility.Visible;
            GroupSettings.Visibility = Visibility.Collapsed;
        }

        private void CreatePollButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement this
        }

        private void GroupPollsButton_Click(object sender, RoutedEventArgs e)
        {
            GroupFeed.Visibility = Visibility.Visible;
            PollsListBox.Visibility = Visibility.Visible;
            PostsListBox.Visibility = Visibility.Collapsed;
            GroupSettings.Visibility = Visibility.Collapsed;
        }
    }
}
