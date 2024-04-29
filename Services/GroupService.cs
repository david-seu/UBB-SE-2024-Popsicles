using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.Repositories;

namespace UBB_SE_2024_Popsicles.Services
{
    internal class GroupService : IGroupService
    {
        private static string defaultGroupName = "New Group";
        private static string defaultGroupDescription = "This is a new group";
        private static string defaultGroupIcon = "default";
        private static string defaultGroupBanner = "default";
        private static int defaultMaximumNumberOfPostsPerHourPerUser = 5;
        private static bool defaultIsGroupPublic = false;
        private static bool defaultAllowanceOfPostage = false;
        private static string defaultGroupRole = "user";

        // private static bool defaultPostIsPinned = false;
        // private static string defaultPostDescription = "This is a new post";
        // Add the three repos: GroupRepository, GroupMemberRepository, and GroupMembershipRepository
        private IGroupRepository groupRepository;
        private IGroupMemberRepository groupMemberRepository;
        private IGroupMembershipRepository groupMembershipRepository;
        private IJoinRequestRepository joinRequestsRepository;

        internal GroupService(IGroupRepository groupRepository, IGroupMemberRepository groupMemberRepository, IGroupMembershipRepository groupMembershipRepository, IJoinRequestRepository joinRequestsRepository)
        {
            this.groupRepository = groupRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.groupMembershipRepository = groupMembershipRepository;
            this.joinRequestsRepository = joinRequestsRepository;
        }

        public void CreateGroup(Guid groupOwnerId)
        {
            Guid groupId = Guid.NewGuid();
            // Generate a random group code by slicing a random GUID into a 6-character string
            // This results in a 1 in 2^36 chance of a collision (it should be fine)
            string uniqueGroupCode = Guid.NewGuid().ToString().Substring(0, 6);
            Group newGroup = new Group(groupId, groupOwnerId, defaultGroupName, defaultGroupDescription, defaultGroupIcon, defaultGroupBanner, defaultMaximumNumberOfPostsPerHourPerUser, defaultIsGroupPublic, defaultAllowanceOfPostage, uniqueGroupCode);

            // Add the new group to the GroupRepository
            groupRepository.AddGroup(newGroup);
            AddMemberToGroup(groupOwnerId, groupId, "admin");
        }

        public void UpdateGroup(Guid groupId, string newGroupName, string newGroupDescription, string newGroupIcon, string newGroupBanner,
            int maximumNumberOfPostsPerHourPerUser, bool isGroupPublic, bool allowanceOfPostageByDefault)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            Group newGroup = new Group(groupId, group.GroupOwnerId, newGroupName, newGroupDescription, newGroupIcon, newGroupBanner, maximumNumberOfPostsPerHourPerUser, isGroupPublic, allowanceOfPostageByDefault, group.GroupCode);

            // Update the group in the GroupRepository
            groupRepository.UpdateGroup(newGroup);
        }

        public void DeleteGroup(Guid groupId)
        {
            // Delete the group from the GroupRepository
            groupRepository.RemoveGroupById(groupId);
        }

        public void AddMemberToGroup(Guid groupMemberId, Guid groupId, string userRole = "user")
        {
            Guid groupMembershipId = Guid.NewGuid();
            DateTime joinDate = DateTime.Now;
            bool isBannedFromGroup = false;
            bool isTimedOutFromGroup = false;
            bool bypassPostageRestrictionPostSettings = false;
            string groupMemberName = groupMemberRepository.GetGroupMemberById(groupMemberId).UserName;
            GroupMembership newMembership = new GroupMembership(groupMembershipId, groupMemberId, groupMemberName, groupId, userRole, joinDate, isBannedFromGroup, isTimedOutFromGroup, bypassPostageRestrictionPostSettings = false);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);
            group.AddMembership(newMembership);

            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);
            groupMember.AddGroupMembership(newMembership);

            // Add the new GroupMembership to the GroupMembershipRepository
            groupMembershipRepository.AddGroupMembership(newMembership);
        }

        public void RemoveMemberFromGroup(Guid groupMemberId, Guid groupId)
        {
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            group.RemoveMember(groupMembership.GroupMembershipId);
            groupMember.RemoveGroupMembership(groupMembership.GroupMembershipId);

            // Delete the GroupMembership from the GroupMembershipRepository
            groupMembershipRepository.RemoveGroupMembershipById(groupMembership.GroupMembershipId);
        }

        public void BanMemberFromGroup(Guid bannedGroupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(bannedGroupMemberId);

            groupMembership.IsBannedFromGroup = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void UnbanMemberFromGroup(Guid unbannedGroupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(unbannedGroupMemberId);

            groupMembership.IsBannedFromGroup = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void TimeoutMemberFromGroup(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            groupMembership.IsTimedOutFromGroup = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void EndTimeoutOfMemberFromGroup(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            groupMembership.IsTimedOutFromGroup = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void ChangeMemberRoleInTheGroup(Guid groupMemberId, Guid groupId, string newGroupRole)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            groupMembership.GroupMemberRole = newGroupRole;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void AllowMemberToBypassPostageRestriction(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            groupMembership.BypassPostageRestriction = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void DisallowMemberToBypassPostageRestriction(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            groupMembership.BypassPostageRestriction = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void AddNewRequestToJoinGroup(Guid groupMemberId, Guid groupId)
        {
            Guid joinRequestId = Guid.NewGuid();
            string groupMemberName = groupMemberRepository.GetGroupMemberById(groupMemberId).UserName;
            JoinRequest newJoinRequest = new JoinRequest(joinRequestId, groupMemberId, groupMemberName, groupId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);

            group.AddJoinRequest(newJoinRequest);
            groupMember.AddActiveJoinRequest(newJoinRequest);

            // Add the new JoinRequest to the RequestRepository
            joinRequestsRepository.AddJoinRequest(newJoinRequest);
        }

        public void AcceptRequestToJoinGroup(Guid joinRequestId)
        {
            // Get the JoinRequest from the RequestRepository
            JoinRequest joinRequest = joinRequestsRepository.GetJoinRequestById(joinRequestId);
            Group group = groupRepository.GetGroupById(joinRequest.GroupId);

            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(joinRequest.GroupMemberId);

            group.RemoveJoinRequest(joinRequest.JoinRequestId);
            groupMember.RemoveActiveJoinRequest(joinRequest.JoinRequestId);

            AddMemberToGroup(joinRequest.GroupMemberId, joinRequest.GroupId, defaultGroupRole);

            // Delete the JoinRequest from the RequestRepository
            joinRequestsRepository.RemoveJoinRequestById(joinRequest.JoinRequestId);
        }

        public void RejectRequestToJoinGroup(Guid joinRequestId)
        {
            // Get the JoinRequest from the RequestRepository
            JoinRequest joinRequest = joinRequestsRepository.GetJoinRequestById(joinRequestId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(joinRequest.GroupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(joinRequest.GroupMemberId);

            group.RemoveJoinRequest(joinRequest.JoinRequestId);
            groupMember.RemoveActiveJoinRequest(joinRequest.JoinRequestId);

            // Delete the JoinRequest from the RequestRepository
            joinRequestsRepository.RemoveJoinRequestById(joinRequest.JoinRequestId);
        }

        public void CreateNewPostOnGroupChat(Guid groupId, Guid groupMemberId, string postContent, string postImage)
        {
            Guid postId = Guid.NewGuid();
            DateTime postDate = DateTime.Now;
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembershipFromGroupMemberId(groupMemberId);

            if (groupMembership.BypassPostageRestriction || group.AllowanceOfPostage)
            {
                GroupPost newPost = new GroupPost(postId, groupMemberId, postContent, postImage, groupId);
                group.ListOfGroupPosts.Add(newPost);
            }
            else
            {
                int postCount = 0;
                foreach (GroupPost post in group.ListOfGroupPosts)
                {
                    if (post.PostOwnerId == groupMemberId && post.PostageDateTime.Date == postDate.Date)
                    {
                        postCount++;
                    }
                }

                if (postCount < group.MaximumNumberOfPostsPerHourPerUser)
                {
                    GroupPost newPost = new GroupPost(postId, groupMemberId, postContent, postImage, groupId);

                    group.ListOfGroupPosts.Add(newPost);
                }
                else
                {
                    throw new Exception("Post limit exceeded");
                }
            }
        }

        public List<GroupPost> GetGroupPosts(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.ListOfGroupPosts;
        }

        public List<GroupMember> GetGroupMembers(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            // Get the Members from the Group
            List<GroupMember> listOfGroupMembers = new List<GroupMember>();
            foreach (GroupMembership groupMembership in group.ListOfGroupMemberships)
            {
                GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMembership.GroupMemberId);
                listOfGroupMembers.Add(groupMember);
            }

            return listOfGroupMembers;
        }

        public GroupMember GetMemberFromGroup(Guid groupId, Guid groupMemberId)
        {
            Group group = groupRepository.GetGroupById(groupId);
            foreach (GroupMembership membership in group.ListOfGroupMemberships)
            {
                if (membership.GroupMemberId == groupMemberId)
                {
                    return groupMemberRepository.GetGroupMemberById(groupMemberId);
                }
            }
            throw new Exception("Group member not found");
        }

        public List<JoinRequest> GetRequestsToJoin(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.ListOfJoinRequests;
        }

        public List<Group> GetAllGroups(Guid groupMemberId)
        {
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);

            // Get the Groups from the GroupMember
            List<Group> groups = new List<Group>();
            foreach (GroupMembership membership in groupMember.GroupMemberships)
            {
                Group group = groupRepository.GetGroupById(membership.GroupId);
                groups.Add(group);
            }

            return groups;
        }

        public Group GetGroup(Guid groupId)
        {
            return groupRepository.GetGroupById(groupId);
        }

        public List<GroupPoll> GetGroupPolls(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.ListOfGroupPolls;
        }

        public GroupPoll GetSpecificGroupPoll(Guid groupId, Guid pollId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.GetGroupPoll(pollId);
        }

        public void CreateNewPoll(Guid groupId, Guid groupMemberId, string pollDescription)
        {
            Guid pollId = Guid.NewGuid();
            GroupPoll newGroupPoll = new GroupPoll(pollId, groupMemberId, pollDescription, groupId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            group.ListOfGroupPolls.Add(newGroupPoll);
        }

        public void AddNewOptionToAPoll(Guid pollId, Guid groupId, string newPollOption)
        {
            Group group = groupRepository.GetGroupById(groupId);
            GroupPoll groupPoll = group.GetGroupPoll(pollId);
            if (groupPoll != null)
            {
                   groupPoll.AddGroupPollOption(newPollOption);
            }
            else
            {
                throw new Exception("GroupPoll not found");
            }
        }
    }
}