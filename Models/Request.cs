using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public Guid GroupMemberId { get; }
        public string GroupMemberName { get; set; }
        public Guid GroupId { get; }

        public Request(Guid id, Guid groupMemberId, string groupMemberName, Guid groupId)
        {
            Id = id;
            GroupMemberId = groupMemberId;
            GroupMemberName = groupMemberName;
            GroupId = groupId;
            GroupMemberName = groupMemberName;
        }
    }
}
