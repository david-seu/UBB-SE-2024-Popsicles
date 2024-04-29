using System.Data.SqlClient;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupMemberRepository : IGroupMemberRepository
    {
        private SqlConnection databaseConnection;
        private List<GroupMember> listOfGroupMembers = new List<GroupMember>();
        public SqlConnection DatabaseConnection
        {
            get
            {
               return databaseConnection;
            }
        }

        public List<GroupMember> ListOfGroupMembers
        {
            get
            {
                return listOfGroupMembers;
            }
        }
        public GroupMemberRepository(SqlConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectAllGroupMemberQuery = "SELECT UserId, UserName, UserPassword, GroupDescription, UserEmailAdress, UserPhoneNumber FROM ListOfGroupMembers";

            SqlCommand selectAllGroupMembersCommand = new SqlCommand(selectAllGroupMemberQuery, this.databaseConnection);

            this.databaseConnection.Open();

            using (SqlDataReader selectAllGroupMembersReader = selectAllGroupMembersCommand.ExecuteReader())
            {
                while (selectAllGroupMembersReader.Read())
                {
                    GroupMember groupMember = new GroupMember(
                        userId: selectAllGroupMembersReader.GetGuid(0),
                        userName: selectAllGroupMembersReader.GetString(1),
                        userPassword: selectAllGroupMembersReader.GetString(2),
                        userDescription: selectAllGroupMembersReader.IsDBNull(3) ? null : selectAllGroupMembersReader.GetString(3),
                        userEmailAdress: selectAllGroupMembersReader.GetString(4),
                        userPhoneNumber: selectAllGroupMembersReader.GetString(5));

                    this.listOfGroupMembers.Add(groupMember);
                }
            }
            this.databaseConnection.Close();
        }

        public GroupMember GetGroupMemberById(Guid groupMemberId)
        {
            GroupMember groupMember = this.listOfGroupMembers.First(gm => gm.UserId == groupMemberId);

            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            return groupMember;
        }

        public List<GroupMember> GetGroupMembers()
        {
            return this.listOfGroupMembers;
        }

        public void AddGroupMember(GroupMember groupMember)
        {
            string insertGroupMemberQuery = @"INSERT INTO ListOfGroupMembers (UserId, UserName, UserPassword, GroupDescription, UserEmailAdress, UserPhoneNumber) 
                        VALUES (@UserId, @UserName, @UserPassword, @GroupDescription, @UserEmailAdress, @UserPhoneNumber)";

            SqlCommand insertGroupMemberCommand = new SqlCommand(insertGroupMemberQuery, this.databaseConnection);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserId", groupMember.UserId);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.UserName);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserPassword", groupMember.UserPassword);
            insertGroupMemberCommand.Parameters.AddWithValue("@GroupDescription", groupMember.UserDescription);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserEmailAdress", groupMember.UserEmailAdress);
            insertGroupMemberCommand.Parameters.AddWithValue("@UserPhoneNumber", groupMember.UserPhoneNumber);

            this.databaseConnection.Open();
            insertGroupMemberCommand.ExecuteNonQuery();
            this.databaseConnection.Close();

            this.listOfGroupMembers.Add(groupMember);
        }

        public void UpdateGroupMember(GroupMember groupMember)
        {
            GroupMember oldGroupMember = this.listOfGroupMembers.First(gm => gm.UserId == groupMember.UserId);
            if (oldGroupMember == null)
            {
                throw new Exception("Group member not found");
            }

            this.listOfGroupMembers.Remove(oldGroupMember);
            this.listOfGroupMembers.Add(groupMember);

            string updateGroupMemberQuery = @"UPDATE ListOfGroupMembers 
                        SET UserName = @UserName, UserPassword = @UserPassword, GroupDescription = @GroupDescription, 
                            UserEmailAdress = @UserEmailAdress, UserPhoneNumber = @UserPhoneNumber 
                        WHERE UserId = @UserId";

            SqlCommand updateGroupMemberCommand = new SqlCommand(updateGroupMemberQuery, this.databaseConnection);

            updateGroupMemberCommand.Parameters.AddWithValue("@UserId", groupMember.UserId);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserName", groupMember.UserName);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserPassword", groupMember.UserPassword);
            updateGroupMemberCommand.Parameters.AddWithValue("@GroupDescription", groupMember.UserDescription);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserEmailAdress", groupMember.UserEmailAdress);
            updateGroupMemberCommand.Parameters.AddWithValue("@UserPhoneNumber", groupMember.UserPhoneNumber);
            this.databaseConnection.Open();
            int affectedRows = updateGroupMemberCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was updated; the member may not exist.");
            }
        }
        public void RemoveGroupMemberById(Guid groupMemberId)
        {
            GroupMember groupMember = this.listOfGroupMembers.First(gm => gm.UserId == groupMemberId);

            if (groupMember == null)
            {
                throw new Exception("Group member not found");
            }
            this.listOfGroupMembers.Remove(groupMember);
            string deleteGroupMemberQuery = "DELETE FROM ListOfGroupMembers WHERE UserId = @UserId";
            SqlCommand deleteGroupMemberCommand = new SqlCommand(deleteGroupMemberQuery, this.databaseConnection);
            deleteGroupMemberCommand.Parameters.AddWithValue("@UserId", groupMemberId);
            this.databaseConnection.Open();
            int affectedRows = deleteGroupMemberCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No group member was deleted; the member may not exist.");
            }
        }
    }
}
