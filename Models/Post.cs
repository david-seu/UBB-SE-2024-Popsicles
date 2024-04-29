using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class Post
    {
        public Guid PostId { get; }
        public Guid PostOwnerId { get; }
        public string PostDescription { get; }
        public string PostImage { get; }

        public Post(Guid postId, Guid postOwnerId, string postDescription, string postImage)
        {
            PostId = postId;
            PostOwnerId = postOwnerId;
            PostDescription = postDescription;
            PostImage = postImage;
        }
    }
}
