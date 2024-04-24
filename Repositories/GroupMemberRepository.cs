using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMemberRepository : IGroupMemberRepository
    {
        private SqlConnection connection;
        private List<GroupMember> groupMembers = new List<GroupMember>();
<<<<<<< HEAD
=======

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<GroupMember> GroupMembers
        {
            get { return groupMembers; }
        }
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

        public GroupMemberRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = "SELECT GroupMemberId, UserName, Password, Description, Email, Phone FROM GroupMembers";
<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();
=======
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
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
<<<<<<< HEAD
                    this.groupMembers.Add(groupMember);
                }
            }
            this.connection.Close();
=======
                    groupMembers.Add(groupMember);
                }
            }
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public GroupMember GetGroupMemberById(Guid groupMemberId)
        {
<<<<<<< HEAD
            GroupMember groupMember = this.groupMembers.First(gm => gm.Id == groupMemberId);
=======
            GroupMember groupMember = groupMembers.First(gm => gm.Id == groupMemberId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            return groupMember;
        }

        public List<GroupMember> GetGroupMembers()
        {
<<<<<<< HEAD
            return this.groupMembers;
=======
            return groupMembers;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void AddGroupMember(GroupMember groupMember)
        {
            string insertGroupMemberQuery = @"INSERT INTO GroupMembers (GroupMemberId, UserName, Password, Description, Email, Phone) 
                        VALUES (@Id, @UserName, @Password, @Description, @Email, @Phone)";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
=======
            SqlCommand insertGroupMemberCommand = new SqlCommand(insertGroupMemberQuery, connection);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            insertGroupMemberCommand.Parameters.AddWithValue("@Id", groupMember.Id);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.Username);
            insertGroupMemberCommand.Parameters.AddWithValue("@Password", groupMember.Password);
            insertGroupMemberCommand.Parameters.AddWithValue("@Description", groupMember.Description);
            insertGroupMemberCommand.Parameters.AddWithValue("@Email", groupMember.Email);
            insertGroupMemberCommand.Parameters.AddWithValue("@Phone", groupMember.Phone);

<<<<<<< HEAD
            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();

            this.groupMembers.Add(groupMember);
=======
            connection.Open();
            insertGroupMemberCommand.ExecuteNonQuery();
            connection.Close();

            groupMembers.Add(groupMember);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void UpdateGroupMember(GroupMember groupMember)
        {
<<<<<<< HEAD
            GroupMember oldGroupMember = this.groupMembers.First(gm => gm.Id == groupMember.Id);
=======
            GroupMember oldGroupMember = groupMembers.First(gm => gm.Id == groupMember.Id);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (oldGroupMember == null)
            {
                throw new Exception("Group member not found");
            }
<<<<<<< HEAD
            this.groupMembers.Remove(oldGroupMember);
            this.groupMembers.Add(groupMember);
=======
            groupMembers.Remove(oldGroupMember);
            groupMembers.Add(groupMember);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string updateGroupMemberQuery = @"UPDATE GroupMembers 
                        SET UserName = @UserName, Password = @Password, Description = @Description, 
                            Email = @Email, Phone = @Phone 
                        WHERE GroupMemberId = @Id";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
=======
            SqlCommand updateGroupMemberCommand = new SqlCommand(updateGroupMemberQuery, connection);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            updateGroupMemberCommand.Parameters.AddWithValue("@Id", groupMember.Id);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.Username);
            updateGroupMemberCommand.Parameters.AddWithValue("@Password", groupMember.Password);
            updateGroupMemberCommand.Parameters.AddWithValue("@Description", groupMember.Description);
            updateGroupMemberCommand.Parameters.AddWithValue("@Email", groupMember.Email);
            updateGroupMemberCommand.Parameters.AddWithValue("@Phone", groupMember.Phone);

<<<<<<< HEAD
            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
=======
            connection.Open();
            int affectedRows = updateGroupMemberCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (affectedRows == 0)
            {
                throw new Exception("No group member was updated; the member may not exist.");
            }
        }
        public void RemoveGroupMemberById(Guid groupMemberId)
        {
<<<<<<< HEAD
            GroupMember groupMember = this.groupMembers.First(gm => gm.Id == groupMemberId);
=======
            GroupMember groupMember = groupMembers.First(gm => gm.Id == groupMemberId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
<<<<<<< HEAD
            this.groupMembers.Remove(groupMember);
=======
            groupMembers.Remove(groupMember);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string deleteGroupMemberQuery = "DELETE FROM GroupMembers WHERE GroupMemberId = @Id";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
=======
            SqlCommand deleteGroupMemberCommand = new SqlCommand(deleteGroupMemberQuery, connection);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            deleteGroupMemberCommand.Parameters.AddWithValue("@Id", groupMemberId);

<<<<<<< HEAD
            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
=======
            connection.Open();
            int affectedRows = deleteGroupMemberCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (affectedRows == 0)
            {
                throw new Exception("No group member was deleted; the member may not exist.");
            }
        }
    }
}
