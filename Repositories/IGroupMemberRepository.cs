using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal interface IGroupMemberRepository
    {
        GroupMember GetGroupMemberById(Guid groupMemberId);
        List<GroupMember> GetGroupMembers();
        void AddGroupMember(GroupMember groupMember);
        void UpdateGroupMember(GroupMember groupMember);
        void RemoveGroupMemberById(Guid groupMemberId);
    }
}
