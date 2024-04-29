using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class JoinRequestsRepository : IJoinRequestRepository
    {
        private SqlConnection databaseConnection;
        private List<JoinRequest> listOfJoinRequests = new List<JoinRequest>();

        public JoinRequestsRepository(SqlConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string selectAllJoinRequestsQuery = @"
SELECT r.RequestId, r.UserId, r.GroupId, m.UserName 
FROM ListOfJoinRequests r
JOIN ListOfGroupMembers m ON r.UserId = m.UserId";
            SqlCommand selectAllJoinRequestsCommand = new SqlCommand(selectAllJoinRequestsQuery, this.databaseConnection);

            this.databaseConnection.Open();

            using (SqlDataReader selectAllJoinRequestsReader = selectAllJoinRequestsCommand.ExecuteReader())
            {
                while (selectAllJoinRequestsReader.Read())
                {
                    JoinRequest joinRequest = new JoinRequest(
                        joinRequestId: selectAllJoinRequestsReader.GetGuid(0),
                        groupMemberId: selectAllJoinRequestsReader.GetGuid(1),
                        groupMemberName: selectAllJoinRequestsReader.GetString(3),
                        groupId: selectAllJoinRequestsReader.GetGuid(2));
                    this.listOfJoinRequests.Add(joinRequest);
                }
            }
            this.databaseConnection.Close();
        }

        public JoinRequest GetJoinRequestById(Guid joinRequestId)
        {
            JoinRequest joinRequest = this.listOfJoinRequests.FirstOrDefault(r => r.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            return joinRequest;
        }

        public List<JoinRequest> GetAllJoinRequests()
        {
            return this.listOfJoinRequests;
        }

        public void AddJoinRequest(JoinRequest joinRequest)
        {
            string inserJoinReqestQuery = @"INSERT INTO ListOfJoinRequests (RequestId, UserId, GroupId) 
                        VALUES (@RequestId, @UserId, @GroupId)";
            SqlCommand insertJoinRequestCommand = new SqlCommand(inserJoinReqestQuery, this.databaseConnection);
            insertJoinRequestCommand.Parameters.AddWithValue("@RequestId", joinRequest.JoinRequestId);
            insertJoinRequestCommand.Parameters.AddWithValue("@UserId", joinRequest.GroupMemberId);
            insertJoinRequestCommand.Parameters.AddWithValue("@GroupId", joinRequest.GroupId);

            this.databaseConnection.Open();
            insertJoinRequestCommand.ExecuteNonQuery();
            this.databaseConnection.Close();

            this.listOfJoinRequests.Add(joinRequest);
        }

        public void UpdateJoinRequest(JoinRequest joinRequest)
        {
            JoinRequest existingJoinRequest = this.listOfJoinRequests.FirstOrDefault(r => r.JoinRequestId == joinRequest.JoinRequestId);
            if (existingJoinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            this.listOfJoinRequests.Remove(existingJoinRequest);
            this.listOfJoinRequests.Add(joinRequest);
            string updateJoinRequestQuery = @"UPDATE ListOfJoinRequests 
                        SET UserId = @UserId, GroupId = @GroupId 
                        WHERE RequestId = @RequestId";

            SqlCommand updateJoinRequestCommand = new SqlCommand(updateJoinRequestQuery, this.databaseConnection);
            updateJoinRequestCommand.Parameters.AddWithValue("@RequestId", joinRequest.JoinRequestId);
            updateJoinRequestCommand.Parameters.AddWithValue("@UserId", joinRequest.GroupMemberId);
            updateJoinRequestCommand.Parameters.AddWithValue("@GroupId", joinRequest.GroupId);

            this.databaseConnection.Open();
            int affectedRows = updateJoinRequestCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No joinRequest was updated; the joinRequest may not exist.");
            }
        }

        public void RemoveJoinRequestById(Guid joinRequestId)
        {
            JoinRequest joinRequest = this.listOfJoinRequests.FirstOrDefault(r => r.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            this.listOfJoinRequests.Remove(joinRequest);

            string deleteJoinRequestQuery = "DELETE FROM ListOfJoinRequests WHERE RequestId = @RequestId";
            SqlCommand deleteJoinRequestCommand = new SqlCommand(deleteJoinRequestQuery, this.databaseConnection);
            deleteJoinRequestCommand.Parameters.AddWithValue("@RequestId", joinRequestId);

            this.databaseConnection.Open();
            int affectedRows = deleteJoinRequestCommand.ExecuteNonQuery();
            this.databaseConnection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No joinRequest was deleted; the joinRequest may not exist.");
            }
        }
    }
}
