﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Services
{
    public interface IFeedService
    {
        public List<GroupPost> FilterPostsByTags(List<GroupPost> posts, List<string> tags);
    }
}
