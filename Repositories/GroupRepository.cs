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
        private SqlConnection connection;
        private List<Group> groups = new List<Group>();
<<<<<<< HEAD

        public GroupRepository(SqlConnection connection)
        {
            this.connection = connection;
=======

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<Group> Groups
        {
            get { return groups; }
        }

        public GroupRepository(SqlConnection connection)
        {
            connection = connection;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectGroupsQuery = @"
             SELECT GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt
             FROM Groups";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
            try
            {
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
=======
            SqlCommand selectGroupsCommand = new SqlCommand(selectGroupsQuery, connection);
            try
            {
                connection.Open();
                using (SqlDataReader reader = selectGroupsCommand.ExecuteReader())
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                {
                    while (reader.Read())
                    {
                        // Read each field into a variable
                        Guid groupId = reader.GetGuid(0);
                        Guid ownerId = reader.GetGuid(1);
                        string? name = reader.IsDBNull(2) ? null : reader.GetString(2);
                        string? description = reader.IsDBNull(3) ? null : reader.GetString(3);
                        string? icon = reader.IsDBNull(4) ? null : reader.GetString(4);
                        string? banner = reader.IsDBNull(5) ? null : reader.GetString(5);
                        int maxPostsPerHour = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        string? groupCode = reader.IsDBNull(7) ? null : reader.GetString(7);
                        bool isPublic = reader.GetBoolean(8);
                        bool canPostByDefault = reader.GetBoolean(9);
                        DateTime createdAt = reader.GetDateTime(10);

                        // Create and add the Group object to the list
                        Group group = new Group(
                            id: groupId,
                            ownerId: ownerId,
                            name: name,
                            description: description,
                            icon: icon,
                            banner: banner,
                            maxPostsPerHourPerUser: maxPostsPerHour,
                            isPublic: isPublic,
                            canMakePostsByDefault: canPostByDefault,
                            groupCode: groupCode);
<<<<<<< HEAD
                        this.groups.Add(group);
=======
                        groups.Add(group);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
                    }
                }
            }
            finally
            {
<<<<<<< HEAD
                this.connection.Close();
=======
                connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            }
        }

        public Group GetGroupById(Guid groupId)
        {
<<<<<<< HEAD
            Group group = this.groups.First(g => g.Id == groupId);
=======
            Group group = groups.First(g => g.Id == groupId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group;
        }

        public List<Group> GetGroups()
        {
<<<<<<< HEAD
            return this.groups;
=======
            return groups;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void AddGroup(Group group)
        {
<<<<<<< HEAD
            this.groups.Add(group);
            string query = "INSERT INTO Groups (GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt ) VALUES (@GroupId, @OwnerId, @Name, @Description, @Icon, @Banner, @MaxPostsPerHour, @GroupCode, @IsPublic, @CanPostByDefault, @CreatedAt)";

            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@GroupId", group.Id);
            command.Parameters.AddWithValue("@OwnerId", group.OwnerId);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Icon", group.Icon);
            command.Parameters.AddWithValue("@Banner", group.Banner);
            command.Parameters.AddWithValue("@MaxPostsPerHour", group.MaxPostsPerHourPerUser);
            command.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            command.Parameters.AddWithValue("@IsPublic", group.IsPublic);
            command.Parameters.AddWithValue("@CanPostByDefault", group.CanMakePostsByDefault);
            command.Parameters.AddWithValue("@CreatedAt", group.CreatedAt);
            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
=======
            groups.Add(group);
            string insertGroupQuery = "INSERT INTO Groups (GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt ) VALUES (@GroupId, @OwnerId, @Name, @Description, @Icon, @Banner, @MaxPostsPerHour, @GroupCode, @IsPublic, @CanPostByDefault, @CreatedAt)";

            SqlCommand insertGroupCommand = new SqlCommand(insertGroupQuery, connection);
            insertGroupCommand.Parameters.AddWithValue("@GroupId", group.Id);
            insertGroupCommand.Parameters.AddWithValue("@OwnerId", group.OwnerId);
            insertGroupCommand.Parameters.AddWithValue("@Name", group.Name);
            insertGroupCommand.Parameters.AddWithValue("@Description", group.Description);
            insertGroupCommand.Parameters.AddWithValue("@Icon", group.Icon);
            insertGroupCommand.Parameters.AddWithValue("@Banner", group.Banner);
            insertGroupCommand.Parameters.AddWithValue("@MaxPostsPerHour", group.MaxPostsPerHourPerUser);
            insertGroupCommand.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            insertGroupCommand.Parameters.AddWithValue("@IsPublic", group.IsPublic);
            insertGroupCommand.Parameters.AddWithValue("@CanPostByDefault", group.CanMakePostsByDefault);
            insertGroupCommand.Parameters.AddWithValue("@CreatedAt", group.CreatedAt);
            connection.Open();
            insertGroupCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void UpdateGroup(Group group)
        {
<<<<<<< HEAD
            Group oldGroup = this.groups.First(g => g.Id == group.Id);
=======
            Group oldGroup = groups.First(g => g.Id == group.Id);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (oldGroup == null)
            {
                throw new Exception("Group not found");
            }
<<<<<<< HEAD
            this.groups.Remove(oldGroup);
            this.groups.Add(group);
=======
            groups.Remove(oldGroup);
            groups.Add(group);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string updateGroupQuery = @"
            UPDATE Groups
            SET 
                OwnerId = @OwnerId,
                Name = @Name,
                Description = @Description,
                Icon = @Icon,
                Banner = @Banner,
                MaxPostsPerHour = @MaxPostsPerHour,
                GroupCode = @GroupCode,
                IsPublic = @IsPublic,
                CanPostByDefault = @CanPostByDefault,
                CreatedAt = @CreatedAt
             WHERE GroupId = @GroupId";
<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);

            command.Parameters.AddWithValue("@GroupId", group.Id);
            command.Parameters.AddWithValue("@OwnerId", group.OwnerId);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Icon", group.Icon);
            command.Parameters.AddWithValue("@Banner", group.Banner);
            command.Parameters.AddWithValue("@MaxPostsPerHour", group.MaxPostsPerHourPerUser);
            command.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            command.Parameters.AddWithValue("@IsPublic", group.IsPublic);
            command.Parameters.AddWithValue("@CanPostByDefault", group.CanMakePostsByDefault);
            command.Parameters.AddWithValue("@CreatedAt", group.CreatedAt);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
=======
            SqlCommand updateGroupCommand = new SqlCommand(updateGroupQuery, connection);

            updateGroupCommand.Parameters.AddWithValue("@GroupId", group.Id);
            updateGroupCommand.Parameters.AddWithValue("@OwnerId", group.OwnerId);
            updateGroupCommand.Parameters.AddWithValue("@Name", group.Name);
            updateGroupCommand.Parameters.AddWithValue("@Description", group.Description);
            updateGroupCommand.Parameters.AddWithValue("@Icon", group.Icon);
            updateGroupCommand.Parameters.AddWithValue("@Banner", group.Banner);
            updateGroupCommand.Parameters.AddWithValue("@MaxPostsPerHour", group.MaxPostsPerHourPerUser);
            updateGroupCommand.Parameters.AddWithValue("@GroupCode", group.GroupCode);
            updateGroupCommand.Parameters.AddWithValue("@IsPublic", group.IsPublic);
            updateGroupCommand.Parameters.AddWithValue("@CanPostByDefault", group.CanMakePostsByDefault);
            updateGroupCommand.Parameters.AddWithValue("@CreatedAt", group.CreatedAt);

            connection.Open();
            updateGroupCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void RemoveGroupById(Guid groupId)
        {
<<<<<<< HEAD
            Group group = this.groups.First(g => g.Id == groupId);
=======
            Group group = groups.First(g => g.Id == groupId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (group == null)
            {
                throw new Exception("Group not found!");
            }
<<<<<<< HEAD
            this.groups.Remove(group);
=======
            groups.Remove(group);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

            string deleteGroupQuery = "DELETE FROM Groups WHERE GroupId = @GroupId";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@GroupId", groupId);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
=======
            SqlCommand deleteGroupCommand = new SqlCommand(deleteGroupQuery, connection);
            deleteGroupCommand.Parameters.AddWithValue("@GroupId", groupId);

            connection.Open();
            deleteGroupCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }
    }
}
