using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMembershipRepository
    {
        private SqlConnection connection;
        private List<GroupMembership> groupMemberships = new List<GroupMembership>();

        public GroupMembershipRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = @"
 SELECT gm.GroupMembershipId, gm.GroupId, gm.GroupMemberId, gm.Role, gm.JoinDate, gm.IsBanned, gm.IsTimedOut, gm.ByPassPostSettings, m.UserName
 FROM GroupMemberships gm
 JOIN GroupMembers m ON gm.GroupMemberId = m.GroupMemberId";

            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
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

                    this.groupMemberships.Add(groupMembership);
                }
            }
            this.connection.Close();
        }

        public GroupMembership GetGroupMembership(Guid groupMembershipId)
        {
            GroupMembership groupMembership = this.groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public List<GroupMembership> GetGroupMemberships()
        {
            return this.groupMemberships;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            string query = @"
            INSERT INTO GroupMemberships (GroupMembershipId, GroupId, GroupMemberId, Role, JoinDate, IsBanned, IsTimedOut, ByPassPostSettings)
            VALUES (@Id, @GroupId, @GroupMemberId, @Role, @JoinDate, @IsBanned, @IsTimedOut, @ByPassPostSettings)";

            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Id", groupMembership.Id);
            command.Parameters.AddWithValue("@GroupId", groupMembership.GroupId);
            command.Parameters.AddWithValue("@GroupMemberId", groupMembership.GroupMemberId);
            command.Parameters.AddWithValue("@Role", groupMembership.Role);
            command.Parameters.AddWithValue("@JoinDate", groupMembership.JoinDate);
            command.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            command.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            command.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();

            this.groupMemberships.Add(groupMembership);
        }

        public void Update(GroupMembership groupMembership)
        {
            GroupMembership oldGroupMembership = this.groupMemberships.First(gm => gm.Id == groupMembership.Id);
            if (oldGroupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            this.groupMemberships.Remove(oldGroupMembership);
            this.groupMemberships.Add(groupMembership);

            string query = @"
            UPDATE GroupMemberships
            SET Role = @Role, IsBanned = @IsBanned, IsTimedOut = @IsTimedOut, ByPassPostSettings = @ByPassPostSettings
            WHERE GroupMembershipId = @Id";

            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Id", groupMembership.Id);
            command.Parameters.AddWithValue("@Role", groupMembership.Role);
            command.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            command.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            command.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);
            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
        }

        public void RemoveGroupMembership(Guid groupMembershipId)
        {
            GroupMembership groupMembership = this.groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            this.groupMemberships.Remove(groupMembership);

            string query = "DELETE FROM GroupMemberships WHERE GroupMembershipId = @Id";

            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Id", groupMembershipId);
            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group membership was deleted; it may not exist.");
            }
        }
    }
}
