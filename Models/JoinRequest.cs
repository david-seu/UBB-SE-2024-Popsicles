using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class JoinRequest
    {
        public Guid JoinRequestId { get; set; }
        public Guid GroupMemberId { get; }
        public string GroupMemberName { get; set; }
        public Guid GroupId { get; }

        public JoinRequest(Guid joinRequestId, Guid groupMemberId, string groupMemberName, Guid groupId)
        {
            JoinRequestId = joinRequestId;
            GroupMemberId = groupMemberId;
            GroupMemberName = groupMemberName;
            GroupId = groupId;
            GroupMemberName = groupMemberName;
        }
    }
}
