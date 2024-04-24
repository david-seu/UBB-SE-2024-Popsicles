using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class RequestsRepository : IRequestRepository
    {
        private SqlConnection _connection;
        private List<Request> _requests = new List<Request>();

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        public List<Request> Requests
        {
            get { return _requests; }
        }

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

        public Request GetRequestById(Guid requestId)
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
            string insertRequestQuery = @"INSERT INTO Requests (RequestId, GroupMemberId, GroupId) 
                        VALUES (@RequestId, @GroupMemberId, @GroupId)";

            SqlCommand insertRequestCommand = new SqlCommand(insertRequestQuery, _connection);
            insertRequestCommand.Parameters.AddWithValue("@RequestId", request.Id);
            insertRequestCommand.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            insertRequestCommand.Parameters.AddWithValue("@GroupId", request.GroupId);

            _connection.Open();
            insertRequestCommand.ExecuteNonQuery();
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


            string updateRequestQuery = @"UPDATE Requests 
                        SET GroupMemberId = @GroupMemberId, GroupId = @GroupId 
                        WHERE RequestId = @RequestId";

            SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, _connection);
            updateRequestCommand.Parameters.AddWithValue("@RequestId", request.Id);
            updateRequestCommand.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            updateRequestCommand.Parameters.AddWithValue("@GroupId", request.GroupId);

            _connection.Open();
            int affectedRows = updateRequestCommand.ExecuteNonQuery();
            _connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was updated; the request may not exist.");
            }
        }

        public void RemoveRequestById(Guid requestId)
        {
            Request request = _requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            _requests.Remove(request);

            string deleteRequestQuery = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand deleteRequestCommand = new SqlCommand(deleteRequestQuery, _connection);
            deleteRequestCommand.Parameters.AddWithValue("@RequestId", requestId);

            _connection.Open();
            int affectedRows = deleteRequestCommand.ExecuteNonQuery();
            _connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was deleted; the request may not exist.");
            }
        }
    }
}
