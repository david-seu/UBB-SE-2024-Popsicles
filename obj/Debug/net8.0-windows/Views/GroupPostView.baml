   M S B A M L   `   `   `  ����P  LUBB-SE-2024-Popsicles, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:UBB_SE_2024_Popsicles.ViewsUBB_SE_2024_Popsicles.Views  5      S.clr-namespace:UBB_SE_2024_Popsicles.ViewModels UBB_SE_2024_Popsicles.ViewModels  5      /    )UBB_SE_2024_Popsicles.Views.GroupPostView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:UBB_SE_2024_Popsicles.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      E
viewmodels.clr-namespace:UBB_SE_2024_Popsicles.ViewModels     5      .��5      �   � RowDefinitions  5   
   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $	��10* �6   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��10* �6#   .+�5      �� $��0��5      $��5q�5       �� CornerRadius$ 50,50,50,50��5       ��5!      �� 5"      -   5        UserIcon�� �� ImageSource$2 ,../resources/images/user-profile-picture.jpg��5$       j� Stretch$
 Fill=�5%      5'      � 5*      $��1��6     5+      �� 5,      $��* �6$   �� 5-      $��* �6$   .+�50      � $��0��6   .+�51      �� -   5         ��Name$ UserNameGroupNameLabel��52      $
��Left=�53      $��Center=�54      $��
Dorian Pop��55      56      � 58      $��1��6   .+�59      �� -   5        $ DateTimeLabel��5:      $
��Left=�5;      $��Center=�5<      $��3 minutes ago��5=      5>      5?      5@      � 5D   
   $��1��6   .+�5E      � $
��Left=�5F      $��Center=�5G       � Orientation$ 
Horizontal=�5H      .+�5I      �� $��1q�6   $��#king��6#   �� 5J      $��1q�6   $��#summer��6#   �� 5K      $��1q�6   $
��#hot��6#   �� 5L      $��1q�6   $��#glowup��6#   5N      5O      �� 5S   
   -   5         ��Name$ ContentPost��5T      $��2��5U      $2��,../resources/images/user-profile-picture.jpg��5V      $
��Fill=�5W      � 5[   
   $��3��6   .+�5\      �� -   5        $ PostDescriptionLabel��5]      $
��Left=�5^      $��	Sunkissed��5_      5`      � 5d   
   $��4��6    5e      �� 5f      $
��Auto �6#   �� 5g      $
��Auto �6#   .+�5j      � $��0��6   .+�5k      �� -   5        $ 
LikesLabel��5k      $��31 likes��6,   5l      � 5m      $��1��6   .+�5n      �� -   5        $ CommnetsLabel��5n      $��
5 comments��6/   5o      5p      5r      5s      