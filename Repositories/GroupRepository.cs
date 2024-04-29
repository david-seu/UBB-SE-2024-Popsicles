using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class GroupRepository : IGroupRepository
    {
        private SqlConnection databaseConnection;
        private List<Group> listOfGroups = new List<Group>();

        public GroupRepository(SqlConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectAllGroupsQuery = @"
             SELECT GroupId, GroupOwnerId, GroupName, GroupDescription, GroupIcon, GroupBanner, MaxPostsPerHour, GroupCode, IsGroupPublic, CanPostByDefault, DateOfGroupCreation
             FROM Groups";
            SqlCommand selectAllGroupsCommand = new SqlCommand(selectAllGroupsQuery, this.databaseConnection);
            try
            {
                this.databaseConnection.Open();
                using (SqlDataReader selectAllGroupReader = selectAllGroupsCommand.ExecuteReader())
                {
                    while (selectAllGroupReader.Read())
                    {
                        // Read each field into a variable
                        Guid groupId = selectAllGroupReader.GetGuid(0);
                        Guid ownerId = selectAllGroupReader.GetGuid(1);
                        string? groupName = selectAllGroupReader.IsDBNull(2) ? null : selectAllGroupReader.GetString(2);
                        string? groupDescription = selectAllGroupReader.IsDBNull(3) ? null : selectAllGroupReader.GetString(3);
                        string? groupIcon = selectAllGroupReader.IsDBNull(4) ? null : selectAllGroupReader.GetString(4);
                        string? groupBanner = selectAllGroupReader.IsDBNull(5) ? null : selectAllGroupReader.GetString(5);
                        int maximumNumberOfPostsPerHourPerUser = selectAllGroupReader.IsDBNull(6) ? 0 : selectAllGroupReader.GetInt32(6);
                        string? groupCode = selectAllGroupReader.IsDBNull(7) ? null : selectAllGroupReader.GetString(7);
                        bool isGroupPublic = selectAllGroupReader.GetBoolean(8);
                        bool allowanceOfPostage = selectAllGroupReader.GetBoolean(9);
                        DateTime createdAt = selectAllGroupReader.GetDateTime(10);

                        // Create and add the Group object to the list
                        Group group = new Group(
                            groupId: groupId,
                            groupOwnerId: ownerId,
                            groupName: groupName,
                            groupDescription: groupDescription,
                            groupIcon: groupIcon,
                            groupBanner: groupBanner,
                            maximumNumberOfPostsPerHourPerUser: maximumNumberOfPostsPerHourPerUser,
                            isGroupPublic: isGroupPublic,
                            allowanceOfPostage: allowanceOfPostage,
                            groupCode: groupCode);
                        this.listOfGroups.Add(group);
                    }
                }
            }
            finally
            {
                this.databaseConnection.Close();
            }
        }

        public Group GetGroupById(Guid groupId)
        {
            Group group = this.listOfGroups.First(g => g.GroupId == groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group;
        }

        public List<Group> GetGroups()
        {
            return this.listOfGroups;
        }

        public void AddGroup(Group group)
        {
            this.listOfGroups.Add(group);
            string insertGroupQuery = "INSERT INTO Groups (GroupId, GroupOwnerId, GroupName, GroupDescription, GroupIcon, GroupBanner, MaxPostsPerHour, GroupCode, IsGroupPublic, CanPostByDefault, DateOfGroupCreation ) VALUES (@GroupId, @GroupOwnerId, @GroupName, @GroupDescription, @GroupIcon, @GroupBanner, @MaxPostsPerHour, @GroupCode, @IsGroupPublic, @CanPostByDefault, @DateOfGroupCreation)";

            SqlCommand insertGroupCommand = new SqlCommand(insertGroupQuery, this.databaseConnection);
            insertGroupCommand.Parameters.AddWithValue("@GroupId", group.GroupId);
            insertGroupCommand.Parameters.AddWithValue("@GroupOwnerId", group.GroupOwnerId);
            insertGroupCommand.Parameters.AddWithValue("@GroupName", group.GroupName);
            insertGroupCommand.Parameters.AddWithValue("@GroupDescription", group.GroupDescription);
            insertGroupCommand.Parameters.AddWithValue("@GroupIcon", group.GroupIcon);
            insertGroupCommand.Parameters.AddWithValue("@GroupBanner", group.GroupBanner);
            insertGroupCommand.Parameters.AddWithValue("@MaxPostsPerHour", group.MaximumNumberOfPostsPerHourPerUser);
            insertGroupCommand.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            insertGroupCommand.Parameters.AddWithValue("@IsGroupPublic", group.IsGroupPublic);
            insertGroupCommand.Parameters.AddWithValue("@CanPostByDefault", group.AllowanceOfPostage);
            insertGroupCommand.Parameters.AddWithValue("@DateOfGroupCreation", group.DateOfGroupCreation);
            this.databaseConnection.Open();
            insertGroupCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
        }

        public void UpdateGroup(Group group)
        {
            Group oldGroup = this.listOfGroups.First(g => g.GroupId == group.GroupId);
            if (oldGroup == null)
            {
                throw new Exception("Group not found");
            }
            this.listOfGroups.Remove(oldGroup);
            this.listOfGroups.Add(group);
            string updateGroupQuery = @"
            UPDATE Groups
            SET 
                GroupOwnerId = @GroupOwnerId,
                GroupName = @GroupName,
                GroupDescription = @GroupDescription,
                GroupIcon = @GroupIcon,
                GroupBanner = @GroupBanner,
                MaxPostsPerHour = @MaxPostsPerHour,
                GroupCode = @GroupCode,
                IsGroupPublic = @IsGroupPublic,
                CanPostByDefault = @CanPostByDefault,
                DateOfGroupCreation = @DateOfGroupCreation
             WHERE GroupId = @GroupId";
            SqlCommand updateGroupCommand = new SqlCommand(updateGroupQuery, this.databaseConnection);

            updateGroupCommand.Parameters.AddWithValue("@GroupId", group.GroupId);
            updateGroupCommand.Parameters.AddWithValue("@GroupOwnerId", group.GroupOwnerId);
            updateGroupCommand.Parameters.AddWithValue("@GroupName", group.GroupName);
            updateGroupCommand.Parameters.AddWithValue("@GroupDescription", group.GroupDescription);
            updateGroupCommand.Parameters.AddWithValue("@GroupIcon", group.GroupIcon);
            updateGroupCommand.Parameters.AddWithValue("@GroupBanner", group.GroupBanner);
            updateGroupCommand.Parameters.AddWithValue("@MaxPostsPerHour", group.MaximumNumberOfPostsPerHourPerUser);
            updateGroupCommand.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            updateGroupCommand.Parameters.AddWithValue("@IsGroupPublic", group.IsGroupPublic);
            updateGroupCommand.Parameters.AddWithValue("@CanPostByDefault", group.AllowanceOfPostage);
            updateGroupCommand.Parameters.AddWithValue("@DateOfGroupCreation", group.DateOfGroupCreation);

            this.databaseConnection.Open();
            updateGroupCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
        }

        public void RemoveGroupById(Guid groupId)
        {
            Group group = this.listOfGroups.First(g => g.GroupId == groupId);
            if (group == null)
            {
                throw new Exception("Group not found!");
            }

            this.listOfGroups.Remove(group);

            string deleteGroupQuery = "DELETE FROM Groups WHERE GroupId = @GroupId";
            SqlCommand deleteGroupCommand = new SqlCommand(deleteGroupQuery, this.databaseConnection);
            deleteGroupCommand.Parameters.AddWithValue("@GroupId", groupId);

            this.databaseConnection.Open();
            deleteGroupCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
        }
    }
}
