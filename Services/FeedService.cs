using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Services
{
    class FeedService
    {
        public List<GroupPost> FilterPostsByTags(List<GroupPost> posts, List<string> tags)
        {
            // Filtering the rest of the posts by tags and adding them int the filtered list
            List<GroupPost> filteredPosts = posts.AsParallel()
                .Where(post => post.Tags.Any(tag => tags.Contains(tag, StringComparer.OrdinalIgnoreCase)))
                .ToList();

            // Order them be the ones that are pinned to be first in the list
            return filteredPosts.OrderBy(post => !post.IsPinned).ToList();
        }
    }
}
