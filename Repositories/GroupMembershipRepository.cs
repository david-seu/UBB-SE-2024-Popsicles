using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMembershipRepository : IGroupMembershipRepository
    {
        private SqlConnection connection;
        private List<GroupMembership> groupMemberships = new List<GroupMembership>();
<<<<<<< HEAD
=======

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<GroupMembership> GroupMemberships
        {
            get { return groupMemberships; }
        }
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

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

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
=======
            SqlCommand selectGeoupMembershipCommand = new SqlCommand(selectGroupMembershipQuery, connection);

            connection.Open();
            using (SqlDataReader reader = selectGeoupMembershipCommand.ExecuteReader())
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
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
<<<<<<< HEAD
=======

>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
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

<<<<<<< HEAD
                    this.groupMemberships.Add(groupMembership);
                }
            }
            this.connection.Close();
=======
                    groupMemberships.Add(groupMembership);
                }
            }
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public GroupMembership GetGroupMembershipById(Guid groupMembershipId)
        {
<<<<<<< HEAD
            GroupMembership groupMembership = this.groupMemberships.First(gm => gm.Id == groupMembershipId);
=======
            GroupMembership groupMembership = groupMemberships.First(gm => gm.Id == groupMembershipId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public List<GroupMembership> GetGroupMemberships()
        {
<<<<<<< HEAD
            return this.groupMemberships;
=======
            return groupMemberships;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            string insertGroupMembershipQuery = @"
            INSERT INTO GroupMemberships (GroupMembershipId, GroupId, GroupMemberId, Role, JoinDate, IsBanned, IsTimedOut, ByPassPostSettings)
            VALUES (@Id, @GroupId, @GroupMemberId, @Role, @JoinDate, @IsBanned, @IsTimedOut, @ByPassPostSettings)";

<<<<<<< HEAD
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
=======
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
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void UpdateGroupMembership(GroupMembership groupMembership)
        {
<<<<<<< HEAD
            GroupMembership oldGroupMembership = this.groupMemberships.First(gm => gm.Id == groupMembership.Id);
=======
            GroupMembership oldGroupMembership = groupMemberships.First(gm => gm.Id == groupMembership.Id);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (oldGroupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
<<<<<<< HEAD
            this.groupMemberships.Remove(oldGroupMembership);
            this.groupMemberships.Add(groupMembership);
=======
            groupMemberships.Remove(oldGroupMembership);
            groupMemberships.Add(groupMembership);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string updateGroupMembershipQuery = @"
            UPDATE GroupMemberships
            SET Role = @Role, IsBanned = @IsBanned, IsTimedOut = @IsTimedOut, ByPassPostSettings = @ByPassPostSettings
            WHERE GroupMembershipId = @Id";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Id", groupMembership.Id);
            command.Parameters.AddWithValue("@Role", groupMembership.Role);
            command.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            command.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            command.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);
            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
=======
            SqlCommand updateGroupMembershipCommand = new SqlCommand(updateGroupMembershipQuery, connection);

            updateGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembership.Id);
            updateGroupMembershipCommand.Parameters.AddWithValue("@Role", groupMembership.Role);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            updateGroupMembershipCommand.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            connection.Open();
            updateGroupMembershipCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void RemoveGroupMembershipById(Guid groupMembershipId)
        {
<<<<<<< HEAD
            GroupMembership groupMembership = this.groupMemberships.First(gm => gm.Id == groupMembershipId);
=======
            GroupMembership groupMembership = groupMemberships.First(gm => gm.Id == groupMembershipId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
<<<<<<< HEAD
            this.groupMemberships.Remove(groupMembership);
=======
            groupMemberships.Remove(groupMembership);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string deleteGroupMembershipQuery = "DELETE FROM GroupMemberships WHERE GroupMembershipId = @Id";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@Id", groupMembershipId);
            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
=======
            SqlCommand deleteGroupMembershipCommand = new SqlCommand(deleteGroupMembershipQuery, connection);
            deleteGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembershipId);
            connection.Open();
            int affectedRows = deleteGroupMembershipCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (affectedRows == 0)
            {
                throw new Exception("No group membership was deleted; it may not exist.");
            }
        }
    }
}
