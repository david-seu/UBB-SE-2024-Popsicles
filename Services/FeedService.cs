using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Services
{
    internal class FeedService : IFeedService
    {
        public List<GroupPost> FilterGroupPostsByTags(List<GroupPost> listOfGroupPosts, List<string> listOfGroupPostTags)
        {
            // Filtering the rest of the listOfGroupPosts by listOfGroupPostTags and adding them int the filtered list
            List<GroupPost> filteredPosts = listOfGroupPosts.AsParallel()
                .Where(post => post.GroupPostTags.Any(tag => listOfGroupPostTags.Contains(tag, StringComparer.OrdinalIgnoreCase)))
                .ToList();

            // Order them be the ones that are pinned to be first in the list
            return filteredPosts.OrderBy(post => !post.IsGroupPostPinnedAsImportant).ToList();
        }
    }
}
