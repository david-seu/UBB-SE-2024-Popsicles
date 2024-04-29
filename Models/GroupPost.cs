using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupPost : Post
    {
        public Guid GroupId { get; }
        public List<string> GroupPostTags { get; set; }
        public string SpecificToRole { get; set; }
        public bool IsGroupPostPinnedAsImportant { get; set; }
        public DateTime PostageDateTime { get; set; }

        public GroupPost(Guid postId, Guid postOwnerId, string postDescription, string postImage, Guid groupId) : base(postId, postOwnerId, postDescription, postImage)
        {
            GroupId = groupId;
            PostageDateTime = DateTime.Now;

            GroupPostTags = new List<string>();
            SpecificToRole = string.Empty;
        }

        public void AddPostTag(string postTag)
        {
            GroupPostTags.Add(postTag);
        }

        public void RemovePostTag(string postTag)
        {
            GroupPostTags.Remove(postTag);
        }
    }
}
