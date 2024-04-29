using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMembershipRepository : IGroupMembershipRepository
    {
        private SqlConnection databaseConnection;
        private List<GroupMembership> listOfGroupMemberships = new List<GroupMembership>();
        public SqlConnection DatabaseConnection
        {
            get { return databaseConnection; }
        }

        public List<GroupMembership> ListOfGroupMemberships
        {
            get
            {
                return listOfGroupMemberships;
            }
        }
        public GroupMembershipRepository(SqlConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectAllGroupMembershipsQuery = @"
                SELECT gm.GroupMembershipId, gm.GroupId, gm.UserId, gm.GroupMemberRole, gm.JoinDate, gm.IsBannedFromGroup, gm.IsTimedOutFromGroup, gm.BypassPostageRestriction, m.UserName
                FROM ListOfGroupMemberships gm
                JOIN ListOfGroupMembers m ON gm.UserId = m.UserId";

            SqlCommand selectAllGroupMembershipsCommand = new SqlCommand(selectAllGroupMembershipsQuery, this.databaseConnection);
            this.databaseConnection.Open();
            using (SqlDataReader selectAllGroupMembershipsReader = selectAllGroupMembershipsCommand.ExecuteReader())
            {
                while (selectAllGroupMembershipsReader.Read())
                {
                    Guid groupMembershipId = selectAllGroupMembershipsReader.GetGuid(0);
                    Guid groupId = selectAllGroupMembershipsReader.GetGuid(1);
                    Guid groupMemberId = selectAllGroupMembershipsReader.GetGuid(2);
                    string groupMemberRole = selectAllGroupMembershipsReader.GetString(3);
                    DateTime joinDate = selectAllGroupMembershipsReader.GetDateTime(4);
                    bool isBannedFromGroup = selectAllGroupMembershipsReader.GetBoolean(5);
                    bool isTimedOutFromGroup = selectAllGroupMembershipsReader.GetBoolean(6);
                    bool byPaddPostageRestriction = selectAllGroupMembershipsReader.GetBoolean(7);
                    string groupMemberName = selectAllGroupMembershipsReader.GetString(8);
                    GroupMembership groupMembership = new GroupMembership(
                        groupMembershipId: groupMembershipId,
                        groupMemberId: groupMemberId,
                        groupMemberName: groupMemberName,
                        groupId: groupId,
                        groupMemberRole: groupMemberRole,
                        joinDate: joinDate,
                        isBannedFromGroup: isBannedFromGroup,
                        isTimedOutFromGroup: isTimedOutFromGroup,
                        bypassPostageRestriction: byPaddPostageRestriction);
                    this.listOfGroupMemberships.Add(groupMembership);
                }
            }
            this.databaseConnection.Close();
        }

        public GroupMembership GetGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = this.listOfGroupMemberships.First(gm => gm.GroupMembershipId == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public List<GroupMembership> GetGroupMemberships()
        {
            return this.listOfGroupMemberships;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            string insertGroupMembershipQuery = @"
            INSERT INTO ListOfGroupMemberships (GroupMembershipId, GroupId, UserId, GroupMemberRole, JoinDate, IsBannedFromGroup, IsTimedOutFromGroup, BypassPostageRestriction)
            VALUES (@UserId, @GroupId, @UserId, @GroupMemberRole, @JoinDate, @IsBannedFromGroup, @IsTimedOutFromGroup, @BypassPostageRestriction)";
            SqlCommand insertGroupMembershipCommand = new SqlCommand(insertGroupMembershipQuery, this.databaseConnection);
            insertGroupMembershipCommand.Parameters.AddWithValue("@UserId", groupMembership.GroupMembershipId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupId", groupMembership.GroupId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@UserId", groupMembership.GroupMemberId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupMemberRole", groupMembership.GroupMemberRole);
            insertGroupMembershipCommand.Parameters.AddWithValue("@JoinDate", groupMembership.JoinDate);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsBannedFromGroup", groupMembership.IsBannedFromGroup);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOutFromGroup", groupMembership.IsTimedOutFromGroup);
            insertGroupMembershipCommand.Parameters.AddWithValue("@BypassPostageRestriction", groupMembership.BypassPostageRestriction);

            this.databaseConnection.Open();
            insertGroupMembershipCommand.ExecuteNonQuery();
            this.databaseConnection.Close();

            this.listOfGroupMemberships.Add(groupMembership);
        }

        public void UpdateGroupMembership(GroupMembership groupMembership)
        {
            GroupMembership oldGroupMembership = this.listOfGroupMemberships.First(gm => gm.GroupMembershipId == groupMembership.GroupMembershipId);
            if (oldGroupMembership == null)
            {
                throw new Exception("Group membership not found");
            }

            this.listOfGroupMemberships.Remove(oldGroupMembership);
            this.listOfGroupMemberships.Add(groupMembership);

            string updateGroupMembershipQuery = @"
            UPDATE ListOfGroupMemberships
            SET GroupMemberRole = @GroupMemberRole, IsBannedFromGroup = @IsBannedFromGroup, IsTimedOutFromGroup = @IsTimedOutFromGroup, BypassPostageRestriction = @BypassPostageRestriction
            WHERE GroupMembershipId = @UserId";
            SqlCommand updateGroupMembershipCommand = new SqlCommand(updateGroupMembershipQuery, this.databaseConnection);
            updateGroupMembershipCommand.Parameters.AddWithValue("@UserId", groupMembership.GroupMembershipId);
            updateGroupMembershipCommand.Parameters.AddWithValue("@GroupMemberRole", groupMembership.GroupMemberRole);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsBannedFromGroup", groupMembership.IsBannedFromGroup);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOutFromGroup", groupMembership.IsTimedOutFromGroup);
            updateGroupMembershipCommand.Parameters.AddWithValue("@BypassPostageRestriction", groupMembership.BypassPostageRestriction);
            this.databaseConnection.Open();
            updateGroupMembershipCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
        }

        public void RemoveGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = this.listOfGroupMemberships.First(gm => gm.GroupMembershipId == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }

            this.listOfGroupMemberships.Remove(groupMembership);
            string deleteGroupMembershipQuery = "DELETE FROM ListOfGroupMemberships WHERE GroupMembershipId = @UserId";
            SqlCommand deleteGroupMembershipCommand = new SqlCommand(deleteGroupMembershipQuery, this.databaseConnection);
            deleteGroupMembershipCommand.Parameters.AddWithValue("@UserId", groupMembershipId);
            this.databaseConnection.Open();
            int affectedRows = deleteGroupMembershipCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group membership was deleted; it may not exist.");
            }
        }
    }
}
