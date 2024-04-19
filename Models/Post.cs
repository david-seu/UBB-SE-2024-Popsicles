using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class Post
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public string Description { get; }
        public string Image { get; }

        public Post(Guid id, Guid ownerId, string description, string image)
        {
            Id = id;
            OwnerId = ownerId;
            Description = description;
            Image = image;
        }
    }
}
