namespace UBB_SE_2024_Popsicles.Models
{
    public class Group
    {
        private static string BANNER_PATH = "../Resources/GroupBanners/";
        private static string ICON_PATH = "../Resources/GroupIcons/";

        public Guid Id { get; }
        public Guid OwnerId { get; }

        private string _name;
        public string Name { get => _name; set => _name = value; }
        public string Description { get; set; }
        private string _icon;
        public string Icon
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(_icon); }
            set { _icon = ICON_PATH + value + ".jpg"; }
        }
        public string IconPath { get { return _icon; } }
        private string _banner;
        public string Banner
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(_banner); }
            set { _banner = BANNER_PATH + value + ".jpg"; }
        }
        public string BannerPath { get { return _banner; } }
        public int MaxPostsPerHourPerUser { get; set; }
        public bool IsPublic { get; set; }
        public bool CanMakePostsByDefault { get; set; }
        public string GroupCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GroupMembership> Memberships { get; set; }
        public List<Request> Requests { get; set; }
        public List<Poll> Polls { get; set; }
        public List<GroupPost> Posts { get; set; }
        private int _memberCount = 0;
        public int MemberCount { get { return _memberCount; } }
        private int _postCount = 0;
        public int PostCount { get { return _postCount; } }
        private int _requestCount = 0;
        public int RequestCount { get { return _requestCount; } }

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
            GroupMembership groupMembership = Memberships.First(m => m.GroupMemberId == groupMemberId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            return groupMembership;
        }

        public void AddMember(GroupMembership groupMembership)
        {
            Memberships.Add(groupMembership);
            _memberCount++;
        }

        public void RemoveMember(Guid groupMembershipId)
        {
            GroupMembership groupMembership = Memberships.First(m => m.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Membership not found");
            }
            Memberships.Remove(groupMembership);
            _memberCount--;
        }

        public Request GetRequest(Guid requestId)
        {
            Request request = Requests.First(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public void AddRequest(Request request)
        {
            Requests.Add(request);
            _requestCount++;
        }

        public void RemoveRequest(Guid requestId)
        {
            Request request = Requests.First(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            Requests.Remove(request);
            _requestCount--;
        }

        public GroupPost GetPost(Guid postId)
        {
            GroupPost post = Posts.First(p => p.Id == postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            return post;
        }

        public void AddPost(GroupPost post)
        {
            Posts.Add(post);
            _postCount++;
        }

        public void RemovePost(Guid postId)
        {
            GroupPost groupPost = Posts.First(p => p.Id == postId);
            if (groupPost == null)
            {
                throw new Exception("Post not found");
            }
            Posts.Remove(groupPost);
            _postCount--;
        }

        public Poll GetPoll(Guid pollId)
        {
            Poll poll = Polls.First(p => p.Id == pollId);
            if (poll == null)
            {
                throw new Exception("Poll not found");
            }
            return poll;
        }

        public void AddPoll(Poll poll)
        {
            Polls.Add(poll);
            _postCount++;
        }

        public void RemovePoll(Guid pollId)
        {
            Poll poll = Polls.First(p => p.Id == pollId);
            if (poll == null)
            {
                throw new Exception("Poll not found");
            }
            Polls.Remove(poll);
            _postCount--;
        }
    }
}
