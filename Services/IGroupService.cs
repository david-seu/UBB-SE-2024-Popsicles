using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Services
{
    internal interface IGroupService
    {
        public void CreateGroup(Guid groupOwnerId);
        public void UpdateGroup(Guid groupId, string newGroupName, string newGroupDescription, string newGroupIcon, string newGroupBanner,
            int maximumNumberOfPostsPerHourPerUser, bool isGroupPublic, bool allowanceOfPostageByDefault);
        public void DeleteGroup(Guid groupId);
        public void AddMemberToGroup(Guid groupMemberId, Guid groupId, string userRole);
        public void RemoveMemberFromGroup(Guid groupMemberId, Guid groupId);
        public void BanMemberFromGroup(Guid bannedGroupMemberId, Guid groupId);
        public void UnbanMemberFromGroup(Guid unbannedGroupMemberId, Guid groupId);
        public void TimeoutMemberFromGroup(Guid groupMemberId, Guid groupId);
        public void EndTimeoutOfMemberFromGroup(Guid groupMemberId, Guid groupId);
        public void ChangeMemberRoleInTheGroup(Guid groupMemberId, Guid groupId, string newGroupRole);
        public void AllowMemberToBypassPostageRestriction(Guid groupMemberId, Guid groupId);
        public void DisallowMemberToBypassPostageRestriction(Guid groupMemberId, Guid groupId);
        public void AddNewRequestToJoinGroup(Guid groupMemberId, Guid groupId);
        public void AcceptRequestToJoinGroup(Guid joinRequestId);
        public void RejectRequestToJoinGroup(Guid joinRequestId);
        public void CreateNewPostOnGroupChat(Guid groupId, Guid groupMemberId, string postContent, string postImage);
        public List<GroupPost> GetGroupPosts(Guid groupId);
        public List<GroupMember> GetGroupMembers(Guid groupId);
        public GroupMember GetMemberFromGroup(Guid groupId, Guid groupMemberId);
        public List<JoinRequest> GetRequestsToJoin(Guid groupId);
        public List<Group> GetAllGroups(Guid groupMemberId);
        public Group GetGroup(Guid groupId);
        public List<GroupPoll> GetGroupPolls(Guid groupId);
        public GroupPoll GetSpecificGroupPoll(Guid groupId, Guid pollId);
        public void CreateNewPoll(Guid groupId, Guid groupMemberId, string pollDescription);
        public void AddNewOptionToAPoll(Guid pollId, Guid groupId, string newPollOption);
    }
}
