using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class RequestsRepository : IRequestRepository
    {
        private SqlConnection connection;
        private List<Request> requests = new List<Request>();

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<Request> Requests
        {
            get { return requests; }
        }

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
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Request request = new Request(
                        id: reader.GetGuid(0),
                        groupMemberId: reader.GetGuid(1),
                        groupMemberName: reader.GetString(3),
                        groupId: reader.GetGuid(2));
                    requests.Add(request);
                }
            }
            connection.Close();
        }

        public Request GetRequestById(Guid requestId)
        {
            Request request = requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public List<Request> GetAllRequests()
        {
            return requests;
        }

        public void AddRequest(Request request)
        {
            string insertRequestQuery = @"INSERT INTO Requests (RequestId, GroupMemberId, GroupId) 
                        VALUES (@RequestId, @GroupMemberId, @GroupId)";

            SqlCommand insertRequestCommand = new SqlCommand(insertRequestQuery, connection);
            insertRequestCommand.Parameters.AddWithValue("@RequestId", request.Id);
            insertRequestCommand.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            insertRequestCommand.Parameters.AddWithValue("@GroupId", request.GroupId);

            connection.Open();
            insertRequestCommand.ExecuteNonQuery();
            connection.Close();

            requests.Add(request);
        }

        public void UpdateRequest(Request request)
        {
            Request existingRequest = requests.FirstOrDefault(r => r.Id == request.Id);
            if (existingRequest == null)
            {
                throw new Exception("Request not found");
            }
            requests.Remove(existingRequest);
            requests.Add(request);

            string updateRequestQuery = @"UPDATE Requests 
                        SET GroupMemberId = @GroupMemberId, GroupId = @GroupId 
                        WHERE RequestId = @RequestId";

            SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, connection);
            updateRequestCommand.Parameters.AddWithValue("@RequestId", request.Id);
            updateRequestCommand.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            updateRequestCommand.Parameters.AddWithValue("@GroupId", request.GroupId);

            connection.Open();
            int affectedRows = updateRequestCommand.ExecuteNonQuery();
            connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was updated; the request may not exist.");
            }
        }

        public void RemoveRequestById(Guid requestId)
        {
            Request request = requests.FirstOrDefault(r => r.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            requests.Remove(request);

            string deleteRequestQuery = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand deleteRequestCommand = new SqlCommand(deleteRequestQuery, connection);
            deleteRequestCommand.Parameters.AddWithValue("@RequestId", requestId);

            connection.Open();
            int affectedRows = deleteRequestCommand.ExecuteNonQuery();
            connection.Close();
            if (affectedRows == 0)
            {
                throw new Exception("No request was deleted; the request may not exist.");
            }
        }
    }
}
