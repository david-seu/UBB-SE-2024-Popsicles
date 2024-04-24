using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.Repositories;

namespace UBB_SE_2024_Popsicles.Services
{
    public class GroupService
    {
        private static string DEFAULT_GROUP_NAME = "New Group";
        private static string DEFAULT_GROUP_DESCRIPTION = "This is a new group";
        private static string DEFAULT_GROUP_ICON = "default";
        private static string DEFAULT_GROUP_BANNER = "default";
        private static int DEFAULT_MAX_POSTS_PER_HOUR_PER_USER = 5;
        private static bool DEFAULT_IS_PUBLIC = false;
        private static bool DEFAULT_CAN_MAKE_POSTS_BY_DEFAULT = false;
        private static string DEFAULT_GROUP_ROLE = "user";

        private static bool DEFAULT_POST_IS_PINNED = false;
        private static string DEFAULT_POST_DESCRIPTION = "This is a new post";


        // Add the three repos: GroupRepository, GroupMemberRepository, and GroupMembershipRepository
        private GroupRepository GroupRepository { get; }
        private GroupMemberRepository GroupMemberRepository { get; }
        private GroupMembershipRepository GroupMembershipRepository { get; }
        private RequestsRepository RequestsRepository { get; }

        GroupService(GroupRepository groupRepository, GroupMemberRepository groupMemberRepository, GroupMembershipRepository groupMembershipRepository, RequestsRepository requestsRepository)
        {
            GroupRepository = groupRepository;
            GroupMemberRepository = groupMemberRepository;
            GroupMembershipRepository = groupMembershipRepository;
            RequestsRepository = requestsRepository;
        }

        public void CreateGroup(Guid ownerId)
        {
            Guid groupId = Guid.NewGuid();
            // Generate a random group code by slicing a random GUID into a 6-character string
            // This results in a 1 in 2^36 chance of a collision (it should be fine)
            string groupCode = Guid.NewGuid().ToString().Substring(0, 6);
            Group newGroup = new Group(groupId, ownerId, DEFAULT_GROUP_NAME, DEFAULT_GROUP_DESCRIPTION, DEFAULT_GROUP_ICON, DEFAULT_GROUP_BANNER, DEFAULT_MAX_POSTS_PER_HOUR_PER_USER, DEFAULT_IS_PUBLIC, DEFAULT_CAN_MAKE_POSTS_BY_DEFAULT, groupCode);

            // Add the new group to the GroupRepository
            GroupRepository.AddGroup(newGroup);


            AddMember(ownerId, groupId, "admin");
        }

        public void UpdateGroup(Guid groupId, string name, string description, string icon, string banner, int maxPostsPerHourPerUser, bool isPublic, bool canMakePostsByDefault)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            Group newGroup = new Group(groupId, group.OwnerId, name, description, icon, banner, maxPostsPerHourPerUser, isPublic, canMakePostsByDefault, group.GroupCode);

            // Update the group in the GroupRepository
            GroupRepository.Update(newGroup);
        }

        public void DeleteGroup(Guid groupId)
        {
            // Delete the group from the GroupRepository
            GroupRepository.RemoveGroup(groupId);
        }

        public void AddMember(Guid groupMemberId, Guid groupId, string role = "user")
        {
            Guid groupMembershipId = Guid.NewGuid();
            DateTime joinDate = DateTime.Now;
            bool isBanned = false;
            bool isTimedOut = false;
            bool byPassPostSettings = false;
            string groupMemberName = GroupMemberRepository.GetGroupMember(groupMemberId).Username;
            GroupMembership newMembership = new GroupMembership(groupMembershipId, groupMemberId, groupMemberName, groupId, role, joinDate, isBanned, isTimedOut, byPassPostSettings);

            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);
            group.AddMember(newMembership);

            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = GroupMemberRepository.GetGroupMember(groupMemberId);
            groupMember.AddGroupMembership(newMembership);

            // Add the new GroupMembership to the GroupMembershipRepository
            GroupMembershipRepository.AddGroupMembership(newMembership);
        }

        public void RemoveMember(Guid groupMemberId, Guid groupId)
        {
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = GroupMemberRepository.GetGroupMember(groupMemberId);
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            group.RemoveMember(groupMembership.Id);
            groupMember.RemoveGroupMembership(groupMembership.Id);

            // Delete the GroupMembership from the GroupMembershipRepository
            GroupMembershipRepository.RemoveGroupMembership(groupMembership.Id);
        }

        public void BanMember(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsBanned = true;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void UnbanMember(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsBanned = false;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void TimeoutMember(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsTimedOut = true;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void UntimeoutMember(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.IsTimedOut = false;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void ChangeRole(Guid groupMemberId, Guid groupId, string newRole)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.Role = newRole;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void ByPassPostSettings(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.ByPassPostSettings = true;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void RemoveByPassPostSettings(Guid groupMemberId, Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            groupMembership.ByPassPostSettings = false;

            // Update the GroupMembership in the GroupMembershipRepository
            GroupMembershipRepository.Update(groupMembership);
        }

        public void AddRequest(Guid groupMemberId, Guid groupId)
        {
            Guid requestId = Guid.NewGuid();
            string groupMemberName = GroupMemberRepository.GetGroupMember(groupMemberId).Username;
            Request newRequest = new Request(requestId, groupMemberId, groupMemberName, groupId);

            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = GroupMemberRepository.GetGroupMember(groupMemberId);

            group.AddRequest(newRequest);
            groupMember.AddOutgoingRequest(newRequest);

            // Add the new Request to the RequestRepository
            RequestsRepository.AddRequest(newRequest);
        }

        public void AcceptRequest(Guid requestId)
        {
            // Get the Request from the RequestRepository
            Request request = RequestsRepository.GetRequest(requestId);
            Group group = GroupRepository.GetGroup(request.GroupId);

            GroupMember groupMember = GroupMemberRepository.GetGroupMember(request.GroupMemberId);

            group.RemoveRequest(request.Id);
            groupMember.RemoveOutgoingRequest(request.Id);

            AddMember(request.GroupMemberId, request.GroupId, DEFAULT_GROUP_ROLE);

            // Delete the Request from the RequestRepository
            RequestsRepository.RemoveRequest(request.Id);
        }

        public void RejectRequest(Guid requestId)
        {
            // Get the Request from the RequestRepository
            Request request = RequestsRepository.GetRequest(requestId);

            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(request.GroupId);
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = GroupMemberRepository.GetGroupMember(request.GroupMemberId);

            group.RemoveRequest(request.Id);
            groupMember.RemoveOutgoingRequest(request.Id);

            // Delete the Request from the RequestRepository
            RequestsRepository.RemoveRequest(request.Id);
        }

        public void CreateGroupPost(Guid groupId, Guid groupMemberId, string content, string image)
        {
            Guid postId = Guid.NewGuid();
            DateTime postDate = DateTime.Now;
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            GroupMembership groupMembership = group.GetMembership(groupMemberId);

            if (groupMembership.ByPassPostSettings || group.CanMakePostsByDefault)
            { 
                GroupPost newPost = new GroupPost(postId, groupMemberId, content, image, groupId);

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
                    GroupPost newPost = new GroupPost(postId, groupMemberId, content, image, groupId);

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
            Group group = GroupRepository.GetGroup(groupId);

            return group.Posts;
        }

        public List<GroupMember> GetGroupMembers(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            // Get the Members from the Group
            List<GroupMember> members = new List<GroupMember>();
            foreach (GroupMembership membership in group.Memberships)
            {
                GroupMember member = GroupMemberRepository.GetGroupMember(membership.GroupMemberId);
                members.Add(member);
            }

            return members;
        }

        public GroupMember GetGroupMember(Guid groupId, Guid groupMemberId)
        {
            Group group = GroupRepository.GetGroup(groupId);
            foreach (GroupMembership membership in group.Memberships)
            {
                if (membership.GroupMemberId == groupMemberId)
                {
                    return GroupMemberRepository.GetGroupMember(groupMemberId);
                }
            }

            throw new Exception("Group member not found");

        }

        public List<Request> GetRequests(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            return group.Requests;
        }

        public List<Group> GetGroups(Guid groupMemberId)
        {
            // Get the GroupMember from the GroupMemberRepository
            GroupMember groupMember = GroupMemberRepository.GetGroupMember(groupMemberId);

            // Get the Groups from the GroupMember
            List<Group> groups = new List<Group>();
            foreach (GroupMembership membership in groupMember.Memberships)
            {
                Group group = GroupRepository.GetGroup(membership.GroupId);
                groups.Add(group);
            }

            return groups;
        }

        public Group GetGroup(Guid groupId)
        {
            return GroupRepository.GetGroup(groupId);
        }

        public List<Poll> GetPolls(Guid groupId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            return group.Polls;
        }

        public Poll GetPoll(Guid groupId, Guid pollId)
        {
            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            return group.GetPoll(pollId);
        }

        public void CreatePoll(Guid groupId, Guid groupMemberId, string description, string image)
        {
            Guid pollId = Guid.NewGuid();
            Poll newPoll = new Poll(pollId, groupMemberId, description, groupId);

            // Get the Group from the GroupRepository
            Group group = GroupRepository.GetGroup(groupId);

            group.Polls.Add(newPoll);
        }

        public void AddPollOption(Guid pollId, Guid groupId, string option)
        {
            Group group = GroupRepository.GetGroup(groupId);
            Poll poll = group.GetPoll(pollId);
            if (poll != null)
            {
                   poll.AddOption(option);
            }
            else
            {
                throw new Exception("Poll not found");
            }
        }
    }
}