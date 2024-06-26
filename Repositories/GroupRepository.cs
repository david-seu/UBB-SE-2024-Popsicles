﻿using System;
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

        public GroupRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectGroupsQuery = @"
             SELECT GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt
             FROM Groups";
            SqlCommand command = new SqlCommand(selectGroupsQuery, this.connection);
            try
            {
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
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
                        this.groups.Add(group);
                    }
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public Group GetGroupById(Guid groupId)
        {
            Group group = this.groups.First(g => g.Id == groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group;
        }

        public List<Group> GetGroups()
        {
            return this.groups;
        }

        public void AddGroup(Group group)
        {
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
        }

        public void UpdateGroup(Group group)
        {
            Group oldGroup = this.groups.First(g => g.Id == group.Id);
            if (oldGroup == null)
            {
                throw new Exception("Group not found");
            }
            this.groups.Remove(oldGroup);
            this.groups.Add(group);
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
            SqlCommand command = new SqlCommand(updateGroupQuery, this.connection);

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
        }

        public void RemoveGroupById(Guid groupId)
        {
            Group group = this.groups.First(g => g.Id == groupId);
            if (group == null)
            {
                throw new Exception("Group not found!");
            }

            this.groups.Remove(group);

            string deleteGroupQuery = "DELETE FROM Groups WHERE GroupId = @GroupId";
            SqlCommand command = new SqlCommand(deleteGroupQuery, this.connection);
            command.Parameters.AddWithValue("@GroupId", groupId);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();
        }
    }
}
