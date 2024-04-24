using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMembershipRepository : IGroupMembershipRepository
    {
        private SqlConnection _connection;
        private List<GroupMembership> _groupMemberships = new List<GroupMembership>();

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        public List<GroupMembership> GroupMemberships
        {
            get { return _groupMemberships; }
        }

        public GroupMembershipRepository(SqlConnection connection)
        {
            this._connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectGroupMembershipQuery = @"
                SELECT gm.GroupMembershipId, gm.GroupId, gm.GroupMemberId, gm.Role, gm.JoinDate, gm.IsBanned, gm.IsTimedOut, gm.ByPassPostSettings, m.UserName
                FROM GroupMemberships gm
                JOIN GroupMembers m ON gm.GroupMemberId = m.GroupMemberId";

            SqlCommand selectGeoupMembershipCommand = new SqlCommand(selectGroupMembershipQuery, _connection);

            _connection.Open();
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
                        byPassPostSettings: byPassPostSettings
                    );

                    _groupMemberships.Add(groupMembership);
                }
            }
            _connection.Close();
        }

        public GroupMembership GetGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = _groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            return groupMembership;
        }

        public List<GroupMembership> GetGroupMemberships()
        {
            return _groupMemberships;
        }

        public void AddGroupMembership(GroupMembership groupMembership)
        {
            string insertGroupMembershipQuery = @"
            INSERT INTO GroupMemberships (GroupMembershipId, GroupId, GroupMemberId, Role, JoinDate, IsBanned, IsTimedOut, ByPassPostSettings)
            VALUES (@Id, @GroupId, @GroupMemberId, @Role, @JoinDate, @IsBanned, @IsTimedOut, @ByPassPostSettings)";

            SqlCommand insertGroupMembershipCommand = new SqlCommand(insertGroupMembershipQuery, _connection);


            insertGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembership.Id);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupId", groupMembership.GroupId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@GroupMemberId", groupMembership.GroupMemberId);
            insertGroupMembershipCommand.Parameters.AddWithValue("@Role", groupMembership.Role);
            insertGroupMembershipCommand.Parameters.AddWithValue("@JoinDate", groupMembership.JoinDate);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            insertGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            insertGroupMembershipCommand.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            _connection.Open();
            insertGroupMembershipCommand.ExecuteNonQuery();
            _connection.Close();

            _groupMemberships.Add(groupMembership);


        }

        public void UpdateGroupMembership(GroupMembership groupMembership)
        {
            GroupMembership oldGroupMembership = _groupMemberships.First(gm => gm.Id == groupMembership.Id);
            if (oldGroupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            _groupMemberships.Remove(oldGroupMembership);
            _groupMemberships.Add(groupMembership);

            string updateGroupMembershipQuery = @"
            UPDATE GroupMemberships
            SET Role = @Role, IsBanned = @IsBanned, IsTimedOut = @IsTimedOut, ByPassPostSettings = @ByPassPostSettings
            WHERE GroupMembershipId = @Id";

            SqlCommand updateGroupMembershipCommand = new SqlCommand(updateGroupMembershipQuery, _connection);


            updateGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembership.Id);
            updateGroupMembershipCommand.Parameters.AddWithValue("@Role", groupMembership.Role);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsBanned", groupMembership.IsBanned);
            updateGroupMembershipCommand.Parameters.AddWithValue("@IsTimedOut", groupMembership.IsTimedOut);
            updateGroupMembershipCommand.Parameters.AddWithValue("@ByPassPostSettings", groupMembership.ByPassPostSettings);

            _connection.Open();
            updateGroupMembershipCommand.ExecuteNonQuery();
            _connection.Close();

        }

        public void RemoveGroupMembershipById(Guid groupMembershipId)
        {
            GroupMembership groupMembership = _groupMemberships.First(gm => gm.Id == groupMembershipId);
            if (groupMembership == null)
            {
                throw new Exception("Group membership not found");
            }
            _groupMemberships.Remove(groupMembership);

            string deleteGroupMembershipQuery = "DELETE FROM GroupMemberships WHERE GroupMembershipId = @Id";

            SqlCommand deleteGroupMembershipCommand = new SqlCommand(deleteGroupMembershipQuery, _connection);
            deleteGroupMembershipCommand.Parameters.AddWithValue("@Id", groupMembershipId);
            _connection.Open();
            int affectedRows = deleteGroupMembershipCommand.ExecuteNonQuery();
            _connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group membership was deleted; it may not exist.");
            }
        }
    }
}
