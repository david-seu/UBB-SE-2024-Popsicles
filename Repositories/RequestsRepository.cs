using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class RequestsRepository : IRequestRepository
    {
        private SqlConnection connection;
        private List<Request> requests = new List<Request>();

        public RequestsRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = @"
SELECT r.RequestId, r.GroupMemberId, r.GroupId, m.UserName 
FROM Requests r
JOIN GroupMembers m ON r.GroupMemberId = m.GroupMemberId";
            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Request request = new Request(
                        id: reader.GetGuid(0),
                        groupMemberId: reader.GetGuid(1),
                        groupMemberName: reader.GetString(3),
                        groupId: reader.GetGuid(2));
                    this.requests.Add(request);
                }
            }
            this.connection.Close();
        }

        public Request GetRequestById(Guid requestId)
        {
            Request request = this.requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public List<Request> GetAllRequests()
        {
            return this.requests;
        }

        public void AddRequest(Request request)
        {
            string insertRequestQuery = @"INSERT INTO Requests (RequestId, GroupMemberId, GroupId) 
                        VALUES (@RequestId, @GroupMemberId, @GroupId)";
            SqlCommand command = new SqlCommand(insertRequestQuery, this.connection);
            command.Parameters.AddWithValue("@RequestId", request.Id);
            command.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            command.Parameters.AddWithValue("@GroupId", request.GroupId);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();

            this.requests.Add(request);
        }

        public void UpdateRequest(Request request)
        {
            Request existingRequest = this.requests.FirstOrDefault(r => r.Id == request.Id);
            if (existingRequest == null)
            {
                throw new Exception("Request not found");
            }
            this.requests.Remove(existingRequest);
            this.requests.Add(request);
            string query = @"UPDATE Requests 
                        SET GroupMemberId = @GroupMemberId, GroupId = @GroupId 
                        WHERE RequestId = @RequestId";

            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@RequestId", request.Id);
            command.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            command.Parameters.AddWithValue("@GroupId", request.GroupId);

            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was updated; the request may not exist.");
            }
        }

        public void RemoveRequestById(Guid requestId)
        {
            Request request = this.requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            this.requests.Remove(request);

            string query = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@RequestId", requestId);

            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was deleted; the request may not exist.");
            }
        }
    }
}
