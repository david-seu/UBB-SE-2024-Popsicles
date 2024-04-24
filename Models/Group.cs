namespace UBB_SE_2024_Popsicles.Models
{
    public class Group
    {
        private static string bannerPath = "../Resources/GroupBanners/";
        private static string iconPath = "../Resources/GroupIcons/";

        public Guid Id { get; }
        public Guid OwnerId { get; }

        private string name;
        public string Name { get => name; set => name = value; }
        public string Description { get; set; }
        private string icon;
        public string Icon
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(icon); }
            set { icon = iconPath + value + ".jpg"; }
        }

        public string IconPath
        {
            get { return icon; }
        }
        private string banner;
        public string Banner
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(banner);
            }
            set
            {
                banner = bannerPath + value + ".jpg";
            }
        }

        public string BannerPath
        {
            get { return banner; }
        }

        public int MaxPostsPerHourPerUser
        {
            get; set;
        }

        public bool IsPublic
        {
            get; set;
        }

        public bool CanMakePostsByDefault
        {
            get; set;
        }

        public string GroupCode
        {
            get; set;
        }

        public DateTime CreatedAt
        {
            get; set;
        }

        public List<GroupMembership> Memberships
        {
            get; set;
        }

        public List<Request> Requests
        {
            get; set;
        }

        public List<Poll> Polls
        {
            get; set;
        }

        public List<GroupPost> Posts
        {
            get; set;
        }
        private int memberCount = 0;

        public int MemberCount
        {
            get { return memberCount; }
        }
        private int postCount = 0;

        public int PostCount
        {
            get { return postCount; }
        }
        private int requestCount = 0;

        public int RequestCount
        {
            get { return requestCount; }
        }

        public Group(Guid id, Guid ownerId, string name, string description, string icon, string banner, int maxPostsPerHourPerUser, bool isPublic, bool canMakePostsByDefault, string groupCode)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            Icon = icon;
            Banner = banner;
            MaxPostsPerHourPerUser = maxPostsPerHourPerUser;
            IsPublic = isPublic;
            CanMakePostsByDefault = canMakePostsByDefault;
            GroupCode = groupCode;
            CreatedAt = DateTime.Now;

            Memberships = new List<GroupMembership>();
            Requests = new List<Request>();
            Polls = new List<Poll>();
            Posts = new List<GroupPost>();
        }

        public GroupMembership GetMembership(Guid groupMemberId)
        {
            GroupMembership groupMembership = Memberships.First(groupMembership => groupMembership.GroupMemberId == groupMemberId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            return groupMembership;
        }

        public void AddMember(GroupMembership groupMembership)
        {
            Memberships.Add(groupMembership);
            memberCount++;
        }

        public void RemoveMember(Guid groupMembershipId)
        {
            GroupMembership groupMembership = Memberships.First(groupMembership => groupMembership.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            Memberships.Remove(groupMembership);
            memberCount--;
        }

        public Request GetRequest(Guid requestId)
        {
            Request request = Requests.First(request => request.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public void AddRequest(Request request)
        {
            Requests.Add(request);
            requestCount++;
        }

        public void RemoveRequest(Guid requestId)
        {
            Request request = Requests.First(request => request.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            Requests.Remove(request);
            requestCount--;
        }

        public GroupPost GetPost(Guid postId)
        {
            GroupPost post = Posts.First(post => post.Id == postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            return post;
        }

        public void AddPost(GroupPost post)
        {
            Posts.Add(post);
            postCount++;
        }

        public void RemovePost(Guid postId)
        {
            GroupPost groupPost = Posts.First(groupPost => groupPost.Id == postId);
            if (groupPost == null)
            {
                throw new Exception("Post not found");
            }
            Posts.Remove(groupPost);
            postCount--;
        }

        public Poll GetPoll(Guid pollId)
        {
            Poll poll = Polls.First(poll => poll.Id == pollId);
            if (poll == null)
            {
                throw new Exception("Poll not found");
            }
            return poll;
        }

        public void AddPoll(Poll poll)
        {
            Polls.Add(poll);
            postCount++;
        }

        public void RemovePoll(Guid pollId)
        {
            Poll poll = Polls.First(poll => poll.Id == pollId);
            if (poll == null)
            {
                throw new Exception("Poll not found");
            }
            Polls.Remove(poll);
            postCount--;
        }
    }
}
