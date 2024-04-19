using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class RequestsRepository
    {
        private SqlConnection _connection;
        private List<Request> _requests = new List<Request>();

        public RequestsRepository(SqlConnection connection)
        {
            this._connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = @"
SELECT r.RequestId, r.GroupMemberId, r.GroupId, m.UserName 
FROM Requests r
JOIN GroupMembers m ON r.GroupMemberId = m.GroupMemberId";
            SqlCommand command = new SqlCommand(query, _connection);

            _connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Request request = new Request(
                        id: reader.GetGuid(0),
                        groupMemberId: reader.GetGuid(1),
                        groupMemberName: reader.GetString(3),
                        groupId: reader.GetGuid(2)
                    );
                    _requests.Add(request);
                }
            }
            _connection.Close();

        }

        public Request GetRequest(Guid requestId)
        {
            Request request = _requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public List<Request> GetAllRequests()
        {
            return _requests;
        }

        public void AddRequest(Request request)
        {
            string query = @"INSERT INTO Requests (RequestId, GroupMemberId, GroupId) 
                        VALUES (@RequestId, @GroupMemberId, @GroupId)";

            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@RequestId", request.Id);
            command.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            command.Parameters.AddWithValue("@GroupId", request.GroupId);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();

            _requests.Add(request);
        }

        public void UpdateRequest(Request request)
        {
            Request existingRequest = _requests.FirstOrDefault(r => r.Id == request.Id);
            if (existingRequest == null)
            {
                throw new Exception("Request not found");
            }
            _requests.Remove(existingRequest);
            _requests.Add(request);


            string query = @"UPDATE Requests 
                        SET GroupMemberId = @GroupMemberId, GroupId = @GroupId 
                        WHERE RequestId = @RequestId";

            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@RequestId", request.Id);
            command.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            command.Parameters.AddWithValue("@GroupId", request.GroupId);

            _connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            _connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was updated; the request may not exist.");
            }
        }

        public void RemoveRequest(Guid requestId)
        {
            Request request = _requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            _requests.Remove(request);

            string query = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@RequestId", requestId);

            _connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            _connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was deleted; the request may not exist.");
            }
        }
    }
}
