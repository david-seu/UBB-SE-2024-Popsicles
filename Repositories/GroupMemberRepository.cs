using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMemberRepository : IGroupMemberRepository
    {
        private SqlConnection connection;
        private List<GroupMember> groupMembers = new List<GroupMember>();
        public SqlConnection Connection
        {
            get
            {
               return connection;
            }
        }

        public List<GroupMember> GroupMembers
        {
            get
            {
                return groupMembers;
            }
        }
        public GroupMemberRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = "SELECT GroupMemberId, UserName, Password, Description, Email, Phone FROM GroupMembers";

            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    GroupMember groupMember = new GroupMember(
                        id: reader.GetGuid(0),
                        username: reader.GetString(1),
                        password: reader.GetString(2),
                        description: reader.IsDBNull(3) ? null : reader.GetString(3),
                        email: reader.GetString(4),
                        phone: reader.GetString(5));

                    this.groupMembers.Add(groupMember);
                }
            }
            this.connection.Close();
        }

        public GroupMember GetGroupMemberById(Guid groupMemberId)
        {
            GroupMember groupMember = this.groupMembers.First(gm => gm.Id == groupMemberId);

            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            return groupMember;
        }

        public List<GroupMember> GetGroupMembers()
        {
            return this.groupMembers;
        }

        public void AddGroupMember(GroupMember groupMember)
        {
            string insertGroupMemberQuery = @"INSERT INTO GroupMembers (GroupMemberId, UserName, Password, Description, Email, Phone) 
                        VALUES (@Id, @UserName, @Password, @Description, @Email, @Phone)";

            SqlCommand insertGroupMemberCommand = new SqlCommand(insertGroupMemberQuery, this.connection);
            insertGroupMemberCommand.Parameters.AddWithValue("@Id", groupMember.Id);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.Username);
            insertGroupMemberCommand.Parameters.AddWithValue("@Password", groupMember.Password);
            insertGroupMemberCommand.Parameters.AddWithValue("@Description", groupMember.Description);
            insertGroupMemberCommand.Parameters.AddWithValue("@Email", groupMember.Email);
            insertGroupMemberCommand.Parameters.AddWithValue("@Phone", groupMember.Phone);

            this.connection.Open();
            insertGroupMemberCommand.ExecuteNonQuery();
            this.connection.Close();

            this.groupMembers.Add(groupMember);
        }

        public void UpdateGroupMember(GroupMember groupMember)
        {
            GroupMember oldGroupMember = this.groupMembers.First(gm => gm.Id == groupMember.Id);
            if (oldGroupMember == null)
            {
                throw new Exception("Group member not found");
            }

            this.groupMembers.Remove(oldGroupMember);
            this.groupMembers.Add(groupMember);

            string updateGroupMemberQuery = @"UPDATE GroupMembers 
                        SET UserName = @UserName, Password = @Password, Description = @Description, 
                            Email = @Email, Phone = @Phone 
                        WHERE GroupMemberId = @Id";

            SqlCommand updateGroupMemberCommand = new SqlCommand(updateGroupMemberQuery, this.connection);

            updateGroupMemberCommand.Parameters.AddWithValue("@Id", groupMember.Id);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.Username);
            updateGroupMemberCommand.Parameters.AddWithValue("@Password", groupMember.Password);
            updateGroupMemberCommand.Parameters.AddWithValue("@Description", groupMember.Description);
            updateGroupMemberCommand.Parameters.AddWithValue("@Email", groupMember.Email);
            updateGroupMemberCommand.Parameters.AddWithValue("@Phone", groupMember.Phone);
            this.connection.Open();
            int affectedRows = updateGroupMemberCommand.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was updated; the member may not exist.");
            }
        }
        public void RemoveGroupMemberById(Guid groupMemberId)
        {
            GroupMember groupMember = this.groupMembers.First(gm => gm.Id == groupMemberId);

            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            this.groupMembers.Remove(groupMember);
            string deleteGroupMemberQuery = "DELETE FROM GroupMembers WHERE GroupMemberId = @Id";
            SqlCommand deleteGroupMemberCommand = new SqlCommand(deleteGroupMemberQuery, this.connection);
            deleteGroupMemberCommand.Parameters.AddWithValue("@Id", groupMemberId);
            this.connection.Open();
            int affectedRows = deleteGroupMemberCommand.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was deleted; the member may not exist.");
            }
        }
    }
}
