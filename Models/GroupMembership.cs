using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupMembership
    {
        public Guid Id { get; set; }
        public Guid GroupMemberId { get; }
        public string GroupMemberName { get; set; }
        public Guid GroupId { get; }
        public string Role { get; set; }
        public DateTime JoinDate { get; }
        public bool IsBanned { get; set; }
        public bool IsTimedOut { get; set; }
        public bool ByPassPostSettings { get; set; }

        public GroupMembership(Guid id, Guid groupMemberId, string groupMemberName, Guid groupId, string role, DateTime join, bool isBanned, bool isTimedOut, bool byPassPostSettings)
        {
            Id = id;
            Role = role;
            JoinDate = join;
            IsBanned = isBanned;
            IsTimedOut = isTimedOut;
            ByPassPostSettings = byPassPostSettings;
            GroupId = groupId;
            GroupMemberId = groupMemberId;
            GroupMemberName = groupMemberName;
        }
    }
}
