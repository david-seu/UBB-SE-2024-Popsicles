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
        private static int defaultMaxPostsPerHourPerUser = 5;
        private static bool defaultIsPublic = false;
        private static bool defaultCanMakePosts = false;
        private static string defaultGroupRole = "user";

        // private static bool defaultPostIsPinned = false;
        // private static string defaultPostDescription = "This is a new post";
        // Add the three repos: GroupRepository, GroupMemberRepository, and GroupMembershipRepository
        private IGroupRepository groupRepository;
        private IGroupMemberRepository groupMemberRepository;
        private IGroupMembershipRepository groupMembershipRepository;
        private IRequestRepository requestsRepository;

        internal GroupService(IGroupRepository groupRepository, IGroupMemberRepository groupMemberRepository, IGroupMembershipRepository groupMembershipRepository, IRequestRepository requestsRepository)
        {
            this.groupRepository = groupRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.groupMembershipRepository = groupMembershipRepository;
            this.requestsRepository = requestsRepository;
        }

        public void CreateGroup(Guid ownerId)
        {
            Guid groupId = Guid.NewGuid();
            // Generate a random group code by slicing a random GUID into a 6-character string
            // This results in a 1 in 2^36 chance of a collision (it should be fine)
            string uniqueGroupCode = Guid.NewGuid().ToString().Substring(0, 6);
            Group newGroup = new Group(groupId, ownerId, defaultGroupName, defaultGroupDescription, defaultGroupIcon, defaultGroupBanner, defaultMaxPostsPerHourPerUser, defaultIsPublic, defaultCanMakePosts, uniqueGroupCode);

            // Add the new group to the GroupRepository
            groupRepository.AddGroup(newGroup);
            AddMemberToGroup(ownerId, groupId, "admin");
        }

        public void UpdateGroup(Guid groupId, string newGroupName, string newGroupDescription, string newGroupIcon, string newGroupBanner,
            int maxPostsPerHourPerUser, bool isTheGroupPublic, bool allowanceOfPostageByDefault)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            Group newGroup = new Group(groupId, group.OwnerId, newGroupName, newGroupDescription, newGroupIcon, newGroupBanner, maxPostsPerHourPerUser, isTheGroupPublic, allowanceOfPostageByDefault, group.GroupCode);

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
            bool isBanned = false;
            bool isTimedOut = false;
            bool byPassPostSettings = false;
            string groupMemberName = groupMemberRepository.GetGroupMemberById(groupMemberId).Username;
            GroupMembership newMembership = new GroupMembership(groupMembershipId, groupMemberId, groupMemberName, groupId, userRole, joinDate, isBanned, isTimedOut, byPassPostSettings);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);
            group.AddMember(newMembership);

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

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            group.RemoveMember(groupMembership.Id);
            groupMember.RemoveGroupMembership(groupMembership.Id);

            // Delete the GroupMembership from the GroupMembershipRepository
            groupMembershipRepository.RemoveGroupMembershipById(groupMembership.Id);
        }

        public void BanMemberFromGroup(Guid bannedGroupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(bannedGroupMemberId);

            groupMembership.IsBanned = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void UnbanMemberFromGroup(Guid unbannedGroupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(unbannedGroupMemberId);

            groupMembership.IsBanned = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void TimeoutMemberFromGroup(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsTimedOut = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void EndTimeoutOfMemberFromGroup(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsTimedOut = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void ChangeMemberRoleInTheGroup(Guid groupMemberId, Guid groupId, string newGroupRole)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.Role = newGroupRole;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void AllowMemberToBypassPostageAllowance(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.ByPassPostSettings = true;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void DisallowMemberToBypassPostageAllowance(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.ByPassPostSettings = false;

            // Update the GroupMembership in the GroupMembershipRepository
            groupMembershipRepository.UpdateGroupMembership(groupMembership);
        }

        public void AddNewRequestToJoinGroup(Guid groupMemberId, Guid groupId)
        {
            Guid requestId = Guid.NewGuid();
            string groupMemberName = groupMemberRepository.GetGroupMemberById(groupMemberId).Username;
            Request newRequest = new Request(requestId, groupMemberId, groupMemberName, groupId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);

            group.AddRequest(newRequest);
            groupMember.AddOutgoingRequest(newRequest);

            // Add the new Request to the RequestRepository
            requestsRepository.AddRequest(newRequest);
        }

        public void AcceptRequestToJoinGroup(Guid requestId)
        {
            // Get the Request from the RequestRepository
            Request request = requestsRepository.GetRequestById(requestId);
            Group group = groupRepository.GetGroupById(request.GroupId);

            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(request.GroupMemberId);

            group.RemoveRequest(request.Id);
            groupMember.RemoveOutgoingRequest(request.Id);

            AddMemberToGroup(request.GroupMemberId, request.GroupId, defaultGroupRole);

            // Delete the Request from the RequestRepository
            requestsRepository.RemoveRequestById(request.Id);
        }

        public void RejectRequestToJoinGroup(Guid requestId)
        {
            // Get the Request from the RequestRepository
            Request request = requestsRepository.GetRequestById(requestId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(request.GroupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(request.GroupMemberId);

            group.RemoveRequest(request.Id);
            groupMember.RemoveOutgoingRequest(request.Id);

            // Delete the Request from the RequestRepository
            requestsRepository.RemoveRequestById(request.Id);
        }

        public void CreateNewPostOnGroupChat(Guid groupId, Guid groupMemberId, string postContent, string postImage)
        {
            Guid postId = Guid.NewGuid();
            DateTime postDate = DateTime.Now;
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            if (groupMembership.ByPassPostSettings || group.CanMakePostsByDefault)
            {
                GroupPost newPost = new GroupPost(postId, groupMemberId, postContent, postImage, groupId);
                group.Posts.Add(newPost);
            }
            else
            {
                int postCount = 0;
                foreach (GroupPost post in group.Posts)
                {
                    if (post.OwnerId == groupMemberId && post.DateTime.Date == postDate.Date)
                    {
                        postCount++;
                    }
                }

                if (postCount < group.MaxPostsPerHourPerUser)
                {
                    GroupPost newPost = new GroupPost(postId, groupMemberId, postContent, postImage, groupId);

                    group.Posts.Add(newPost);
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

            return group.Posts;
        }

        public List<GroupMember> GetGroupMembers(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            // Get the Members from the Group
            List<GroupMember> members = new List<GroupMember>();
            foreach (GroupMembership membership in group.Memberships)
            {
                GroupMember member = groupMemberRepository.GetGroupMemberById(membership.GroupMemberId);
                members.Add(member);
            }

            return members;
        }

        public GroupMember GetMemberFromGroup(Guid groupId, Guid groupMemberId)
        {
            Group group = groupRepository.GetGroupById(groupId);
            foreach (GroupMembership membership in group.Memberships)
            {
                if (membership.GroupMemberId == groupMemberId)
                {
                    return groupMemberRepository.GetGroupMemberById(groupMemberId);
                }
            }

            throw new Exception("Group member not found");
        }

        public List<Request> GetRequestsToJoin(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.Requests;
        }

        public List<Group> GetAllGroups(Guid groupMemberId)
        {
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = groupMemberRepository.GetGroupMemberById(groupMemberId);

            // Get the Groups from the GroupMember
            List<Group> groups = new List<Group>();
            foreach (GroupMembership membership in groupMember.Memberships)
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

        public List<Poll> GetGroupPolls(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.Polls;
        }

        public Poll GetSpecificGroupPoll(Guid groupId, Guid pollId)
        {
            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            return group.GetPoll(pollId);
        }

        public void CreateNewPoll(Guid groupId, Guid groupMemberId, string pollDescription)
        {
            Guid pollId = Guid.NewGuid();
            Poll newPoll = new Poll(pollId, groupMemberId, pollDescription, groupId);

            // Get the Group from the GroupRepository
            Group group = groupRepository.GetGroupById(groupId);

            group.Polls.Add(newPoll);
        }

        public void AddNewOptionToAPoll(Guid pollId, Guid groupId, string newPollOption)
        {
            Group group = groupRepository.GetGroupById(groupId);
            Poll poll = group.GetPoll(pollId);
            if (poll != null)
            {
                   poll.AddOption(newPollOption);
            }
            else
            {
                throw new Exception("Poll not found");
            }
        }
    }
}