using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMembershipRepository : IGroupMembershipRepository
    {
        private SqlConnection connection;
        private List<GroupMembership> groupMemberships = new List<GroupMembership>();

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<GroupMembership> GroupMemberships
        {
            get { return groupMemberships; }
        }

        public GroupMembershipRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectGroupMembershipQuery = @"
                SELECT gm.GroupMembershipId, gm.GroupId, gm.GroupMemberId, gm.Role, gm.JoinDate, gm.IsBanned, gm.IsTimedOut, gm.ByPassPostSettings, m.UserName
                FROM GroupMemberships gm
                JOIN GroupMembers m ON gm.GroupMemberId = m.GroupMemberId";

            SqlCommand selectGeoupMembershipCommand = new SqlCommand(selectGroupMembershipQuery, connection);

            connection.Open();
            using (SqlDataReader reader = selectGeoupMembershipCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Guid id = reader.GetGuid(0);
                    Guid groupId = reader.GetGuid(1);
                    Guid groupMemberId = reader.GetGuid(2);
                    string role = reader.GetString(3);
                    DateTime joinDate = reader.GetDateTime(4);
                    bool isBanned = reader.GetBoolean(5);
                    bool isTimedOut = reader.GetBoolean(6);
                    bool byPassPostSettings = reader.GetBoolean(7);
                    string groupMemberName = reader.GetString(8);

                    GroupMembership groupMembership = new GroupMembership(
                        id: id,
                        groupMemberId: groupMemberId,
                        groupMemberName: groupMemberName,
                        groupId: groupId,
                        role: role,
                        join: joinDate,
                        isBanned: isBanned,
                        isTimedOut: isTimedOut,
                        byPassPostSettings: byPassPostSettings);

                    groupMemberships.Add(groupMembership);
                }
            }
            connection.Close();
        }

        public GroupMembership GetGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public List<GroupMembership> GetGroupMemberships()
        {
            return groupMemberships;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            string insertGroupMembershipQuery = @"
            INSERT INTO GroupMemberships (GroupMembershipId, GroupId, GroupMemberId, Role, JoinDate, IsBanned, IsTimedOut, ByPassPostSettings)
            VALUES (@Id, @GroupId, @GroupMemberId, @Role, @JoinDate, @IsBanned, @IsTimedOut, @ByPassPostSettings)";

            SqlCommand insertGroupMembershipCommand = new SqlCommand(insertGroupMembershipQuery, connection);

            insertGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembership.Id);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupId", groupMembership.GroupId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupMemberId", groupMembership.GroupMemberId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@Role", groupMembership.Role);
            insertGroupMembershipCommand.Parameters.AddWithValue("@JoinDate", groupMembership.JoinDate);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            insertGroupMembershipCommand.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            connection.Open();
            insertGroupMembershipCommand.ExecuteNonQuery();
            connection.Close();

            groupMemberships.Add(groupMembership);
        }

        public void UpdateGroupMembership(GroupMembership groupMembership)
        {
            GroupMembership oldGroupMembership = groupMemberships.First(gm => gm.Id == groupMembership.Id);
            if (oldGroupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            groupMemberships.Remove(oldGroupMembership);
            groupMemberships.Add(groupMembership);

            string updateGroupMembershipQuery = @"
            UPDATE GroupMemberships
            SET Role = @Role, IsBanned = @IsBanned, IsTimedOut = @IsTimedOut, ByPassPostSettings = @ByPassPostSettings
            WHERE GroupMembershipId = @Id";

            SqlCommand updateGroupMembershipCommand = new SqlCommand(updateGroupMembershipQuery, connection);

            updateGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembership.Id);
            updateGroupMembershipCommand.Parameters.AddWithValue("@Role", groupMembership.Role);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            updateGroupMembershipCommand.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            connection.Open();
            updateGroupMembershipCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            groupMemberships.Remove(groupMembership);

            string deleteGroupMembershipQuery = "DELETE FROM GroupMemberships WHERE GroupMembershipId = @Id";

            SqlCommand deleteGroupMembershipCommand = new SqlCommand(deleteGroupMembershipQuery, connection);
            deleteGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembershipId);
            connection.Open();
            int affectedRows = deleteGroupMembershipCommand.ExecuteNonQuery();
            connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group membership was deleted; it may not exist.");
            }
        }
    }
}
