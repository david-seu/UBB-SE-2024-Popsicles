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
        private SqlConnection _connection;
        private List<Group> _groups = new List<Group>();

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        public List<Group> Groups
        {
            get { return _groups; }
        }

        public GroupRepository(SqlConnection connection)
        {
            _connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectGroupsQuery = @"
             SELECT GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt
             FROM Groups";

            SqlCommand selectGroupsCommand = new SqlCommand(selectGroupsQuery, _connection);
            try
            {
                _connection.Open();
                using (SqlDataReader reader = selectGroupsCommand.ExecuteReader())
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
                        Group group = new Group
                        (
                            id: groupId,
                            ownerId: ownerId,
                            name: name,
                            description: description,
                            icon: icon,
                            banner: banner,
                            maxPostsPerHourPerUser: maxPostsPerHour,
                            isPublic: isPublic,
                            canMakePostsByDefault: canPostByDefault,
                            groupCode: groupCode

                        );
                        _groups.Add(group);
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public Group GetGroupById(Guid groupId)
        {
            Group group = _groups.First(g => g.Id == groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group;
        }

        public List<Group> GetGroups()
        {
            return _groups;
        }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
            string insertGroupQuery = "INSERT INTO Groups (GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt ) VALUES (@GroupId, @OwnerId, @Name, @Description, @Icon, @Banner, @MaxPostsPerHour, @GroupCode, @IsPublic, @CanPostByDefault, @CreatedAt)";

            SqlCommand insertGroupCommand = new SqlCommand(insertGroupQuery, _connection);
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
            _connection.Open();
            insertGroupCommand.ExecuteNonQuery();
            _connection.Close();
        }

        public void UpdateGroup(Group group)
        {
            Group oldGroup = _groups.First(g => g.Id == group.Id);
            if (oldGroup == null)
            {
                throw new Exception("Group not found");
            }
            _groups.Remove(oldGroup);
            _groups.Add(group);

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
            SqlCommand updateGroupCommand = new SqlCommand(updateGroupQuery, _connection);

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

            _connection.Open();
            updateGroupCommand.ExecuteNonQuery();
            _connection.Close();

        }

        public void RemoveGroupById(Guid groupId)
        {
            Group group = _groups.First(g => g.Id == groupId);
            if (group == null)
            {
                throw new Exception("Group not found!");
            }
            _groups.Remove(group);

            string deleteGroupQuery = "DELETE FROM Groups WHERE GroupId = @GroupId";

            SqlCommand deleteGroupCommand = new SqlCommand(deleteGroupQuery, _connection);
            deleteGroupCommand.Parameters.AddWithValue("@GroupId", groupId);

            _connection.Open();
            deleteGroupCommand.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
