using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal interface IGroupMembershipRepository
    {
        GroupMembership GetGroupMembershipById(Guid groupMembershipId);
        List<GroupMembership> GetGroupMemberships();
        void AddGroupMembership(GroupMembership groupMembership);
        void UpdateGroupMembership(GroupMembership groupMembership);
        void RemoveGroupMembershipById(Guid groupMembershipId);

    }
}
