namespace UBB_SE_2024_Popsicles.Models
{
    public class Group
    {
        private static string groupBannerPath = "../Resources/GroupBanners/";
        private static string groupIconPath = "../Resources/GroupIcons/";

        public Guid GroupId { get; }
        public Guid GroupOwnerId { get; }

        private string groupName;
        public string GroupName { get => groupName; set => groupName = value; }
        public string GroupDescription { get; set; }

        private string groupIcon;
        public string GroupIcon
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(groupIcon); }
            set { groupIcon = groupIconPath + value + ".jpg"; }
        }

        public string GroupIconPath
        {
            get { return groupIcon; }
        }
        private string groupBanner;
        public string GroupBanner
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(groupBanner);
            }
            set
            {
                groupBanner = groupBannerPath + value + ".jpg";
            }
        }

        public string GroupBannerPath
        {
            get { return groupBanner; }
        }

        public int MaximumNumberOfPostsPerHourPerUser
        {
            get; set;
        }

        public bool IsGroupPublic
        {
            get; set;
        }

        public bool AllowanceOfPostage
        {
            get; set;
        }

        public string GroupCode
        {
            get; set;
        }

        public DateTime DateOfGroupCreation
        {
            get; set;
        }

        public List<GroupMembership> ListOfGroupMemberships
        {
            get; set;
        }

        public List<JoinRequest> ListOfJoinRequests
        {
            get; set;
        }

        public List<GroupPoll> ListOfGroupPolls
        {
            get; set;
        }

        public List<GroupPost> ListOfGroupPosts
        {
            get; set;
        }
        private int groupMemberCount = 0;

        public int GroupMemberCount
        {
            get { return groupMemberCount; }
        }
        private int groupPostCount = 0;

        public int GroupPostCount
        {
            get { return groupPostCount; }
        }
        private int joinRequestCount = 0;

        public int JoinRequestCount
        {
            get { return joinRequestCount; }
        }

        public Group(Guid groupId, Guid groupOwnerId, string groupName, string groupDescription, string groupIcon, string groupBanner, int maximumNumberOfPostsPerHourPerUser, bool isGroupPublic, bool allowanceOfPostage, string groupCode)
        {
            GroupId = groupId;
            GroupOwnerId = groupOwnerId;
            GroupName = groupName;
            GroupDescription = groupDescription;
            GroupIcon = groupIcon;
            GroupBanner = groupBanner;
            MaximumNumberOfPostsPerHourPerUser = maximumNumberOfPostsPerHourPerUser;
            IsGroupPublic = isGroupPublic;
            AllowanceOfPostage = allowanceOfPostage;
            GroupCode = groupCode;
            DateOfGroupCreation = DateTime.Now;

            ListOfGroupMemberships = new List<GroupMembership>();
            ListOfJoinRequests = new List<JoinRequest>();
            ListOfGroupPolls = new List<GroupPoll>();
            ListOfGroupPosts = new List<GroupPost>();
        }

        public GroupMembership GetMembershipFromGroupMemberId(Guid groupMemberId)
        {
            GroupMembership? groupMembership = ListOfGroupMemberships.FirstOrDefault(groupMembership => groupMembership.GroupMemberId == groupMemberId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            return groupMembership;
        }

        public void AddMember(GroupMembership groupMembership)
        {
            ListOfGroupMemberships.Add(groupMembership);
            groupMemberCount++;
        }

        public void RemoveMember(Guid groupMembershipId)
        {
            GroupMembership groupMembership = ListOfGroupMemberships.First(groupMembership => groupMembership.GroupMembershipId == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            ListOfGroupMemberships.Remove(groupMembership);
            groupMemberCount--;
        }

        public JoinRequest GetJoinRequest(Guid joinRequestId)
        {
            JoinRequest joinRequest = ListOfJoinRequests.First(joinRequest => joinRequest.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            return joinRequest;
        }

        public void AddJoinRequest(JoinRequest joinRequest)
        {
            ListOfJoinRequests.Add(joinRequest);
            joinRequestCount++;
        }

        public void RemoveJoinRequest(Guid joinRequestId)
        {
            JoinRequest joinRequest = ListOfJoinRequests.First(joinRequest => joinRequest.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("Join JoinRequest not found");
            }
            ListOfJoinRequests.Remove(joinRequest);
            joinRequestCount--;
        }

        public GroupPost GetGroupPost(Guid groupPostId)
        {
            GroupPost groupPost = ListOfGroupPosts.First(groupPost => groupPost.PostId == groupPostId);
            if (groupPost == null)
            {
                throw new Exception("Post not found");
            }
            return groupPost;
        }

        public void AddGroupPost(GroupPost groupPost)
        {
            ListOfGroupPosts.Add(groupPost);
            groupPostCount++;
        }

        public void RemoveGroupPost(Guid groupPostId)
        {
            GroupPost groupPost = ListOfGroupPosts.First(groupPost => groupPost.PostId == groupPostId);
            if (groupPost == null)
            {
                throw new Exception("Post not found");
            }
            ListOfGroupPosts.Remove(groupPost);
            groupPostCount--;
        }

        public GroupPoll GetGroupPoll(Guid groupPollId)
        {
            GroupPoll groupPoll = ListOfGroupPolls.First(groupPoll => groupPoll.PollId == groupPollId);
            if (groupPoll == null)
            {
                throw new Exception("GroupPoll not found");
            }
            return groupPoll;
        }

        public void AddGroupPoll(GroupPoll groupPoll)
        {
            ListOfGroupPolls.Add(groupPoll);
            groupPostCount++;
        }

        public void RemoveGroupPoll(Guid groupPollId)
        {
            GroupPoll groupPoll = ListOfGroupPolls.First(poll => poll.PollId == groupPollId);
            if (groupPoll == null)
            {
                throw new Exception("GroupPoll not found");
            }
            ListOfGroupPolls.Remove(groupPoll);
            groupPostCount--;
        }
    }
}
