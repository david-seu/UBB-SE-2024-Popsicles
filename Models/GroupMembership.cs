using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupMembership
    {
        public Guid GroupMembershipId { get; set; }
        public Guid GroupMemberId { get; }
        public string GroupMemberName { get; set; }
        public Guid GroupId { get; }
        public string GroupMemberRole { get; set; }
        public DateTime JoinDate { get; }
        public bool IsBannedFromGroup { get; set; }
        public bool IsTimedOutFromGroup { get; set; }
        public bool BypassPostageRestriction { get; set; }

        public GroupMembership(Guid groupMembershipId, Guid groupMemberId, string groupMemberName, Guid groupId, string groupMemberRole, DateTime joinDate, bool isBannedFromGroup, bool isTimedOutFromGroup, bool bypassPostageRestriction)
        {
            GroupMembershipId = groupMembershipId;
            GroupMemberRole = groupMemberRole;
            JoinDate = joinDate;
            IsBannedFromGroup = isBannedFromGroup;
            IsTimedOutFromGroup = isTimedOutFromGroup;
            BypassPostageRestriction = bypassPostageRestriction;
            GroupId = groupId;
            GroupMemberId = groupMemberId;
            GroupMemberName = groupMemberName;
        }
    }
}
