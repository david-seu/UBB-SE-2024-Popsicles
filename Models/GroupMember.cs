namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupMember : User
    {
        public List<GroupMembership> Memberships { get; set; }

        public GroupMember(Guid id, string username, string password, string email, string phone, string description) : base(id, username, password, email, phone, description)
        {
            Memberships = new List<GroupMembership>();
        }

        public GroupMembership GetMembership(Guid groupId)
        {
            GroupMembership groupMembership = Memberships.First(groupMembership => groupMembership.GroupId == groupId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            Memberships.Add(groupMembership);
        }

        public void RemoveGroupMembership(Guid groupMembershipId)
        {
            GroupMembership groupMembership = Memberships.First(groupMembership => groupMembership.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            Memberships.Remove(groupMembership);
        }
    }
}
