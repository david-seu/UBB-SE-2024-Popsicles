using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Services
{
    internal interface IFeedService
    {
        public List<GroupPost> FilterGroupPostsByTags(List<GroupPost> listOfGroupPosts, List<string> listOfGroupPostTags);
    }
}
