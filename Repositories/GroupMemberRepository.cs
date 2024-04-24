using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMemberRepository
    {
        private SqlConnection connection;
        private List<GroupMember> groupMembers = new List<GroupMember>();

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

        public GroupMember GetGroupMember(Guid groupMemberId)
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
            string query = @"INSERT INTO GroupMembers (GroupMemberId, UserName, Password, Description, Email, Phone) 
                        VALUES (@Id, @UserName, @Password, @Description, @Email, @Phone)";

            SqlCommand command = new SqlCommand(query, this.connection);

            command.Parameters.AddWithValue("@Id", groupMember.Id);
            command.Parameters.AddWithValue("@UserName", groupMember.Username);
            command.Parameters.AddWithValue("@Password", groupMember.Password);
            command.Parameters.AddWithValue("@Description", groupMember.Description);
            command.Parameters.AddWithValue("@Email", groupMember.Email);
            command.Parameters.AddWithValue("@Phone", groupMember.Phone);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();

            this.groupMembers.Add(groupMember);
        }

        public void Update(GroupMember groupMember)
        {
            GroupMember oldGroupMember = this.groupMembers.First(gm => gm.Id == groupMember.Id);
            if (oldGroupMember == null)
            {
                throw new Exception("Group member not found");
            }
            this.groupMembers.Remove(oldGroupMember);
            this.groupMembers.Add(groupMember);

            string query = @"UPDATE GroupMembers 
                        SET UserName = @UserName, Password = @Password, Description = @Description, 
                            Email = @Email, Phone = @Phone 
                        WHERE GroupMemberId = @Id";

            SqlCommand command = new SqlCommand(query, this.connection);

            command.Parameters.AddWithValue("@Id", groupMember.Id);
            command.Parameters.AddWithValue("@UserName", groupMember.Username);
            command.Parameters.AddWithValue("@Password", groupMember.Password);
            command.Parameters.AddWithValue("@Description", groupMember.Description);
            command.Parameters.AddWithValue("@Email", groupMember.Email);
            command.Parameters.AddWithValue("@Phone", groupMember.Phone);

            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was updated; the member may not exist.");
            }
        }
        public void RemoveGroupMember(Guid groupMemberId)
        {
            GroupMember groupMember = this.groupMembers.First(gm => gm.Id == groupMemberId);
            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            this.groupMembers.Remove(groupMember);

            string query = "DELETE FROM GroupMembers WHERE GroupMemberId = @Id";

            SqlCommand command = new SqlCommand(query, this.connection);

            command.Parameters.AddWithValue("@Id", groupMemberId);

            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was deleted; the member may not exist.");
            }
        }
    }
}
