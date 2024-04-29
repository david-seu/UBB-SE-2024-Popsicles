namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupMember : User
    {
        public List<GroupMembership> GroupMemberships { get; set; }

        public GroupMember(Guid userId, string userName, string userPassword, string userEmailAdress, string userPhoneNumber, string userDescription) : base(userId, userName, userPassword, userEmailAdress, userPhoneNumber, userDescription)
        {
            GroupMemberships = new List<GroupMembership>();
        }

        public GroupMembership GetMembership(Guid groupId)
        {
            GroupMembership groupMembership = GroupMemberships.First(groupMembership => groupMembership.GroupId == groupId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            GroupMemberships.Add(groupMembership);
        }

        public void RemoveGroupMembership(Guid groupMembershipId)
        {
            GroupMembership groupMembership = GroupMemberships.First(groupMembership => groupMembership.GroupMembershipId == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            GroupMemberships.Remove(groupMembership);
        }
    }
}
