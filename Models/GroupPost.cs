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
        public List<string> Tags { get; set; }
        public string SpecificToRole { get; set; }
        public bool IsPinned { get; set; }
        public DateTime DateTime { get; set; }

        public GroupPost(Guid id, Guid ownerId, string description, string image, Guid groupId) : base(id, ownerId, description, image)
        {
            GroupId = groupId;
            DateTime = DateTime.Now;

            Tags = new List<string>();
            SpecificToRole = "";
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            Tags.Remove(tag);
        }
    }
}
