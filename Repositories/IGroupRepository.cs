using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal interface IGroupRepository
    {
        Group GetGroupById(Guid groupId);
        List<Group> GetGroups();
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroupById(Guid groupId);
    }
}
