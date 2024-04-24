using System.Data.SqlClient;
using System.Text.RegularExpressions;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal class RequestsRepository : IRequestRepository
    {
        private SqlConnection connection;
        private List<Request> requests = new List<Request>();
<<<<<<< HEAD
=======

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public List<Request> Requests
        {
            get { return requests; }
        }
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0

        public RequestsRepository(SqlConnection connection)
        {
            this.connection = connection;
            LoadDataFromSql();
        }

        private void LoadDataFromSql()
        {
            string query = @"
<<<<<<< HEAD
SELECT r.RequestId, r.GroupMemberId, r.GroupId, m.UserName 
FROM Requests r
JOIN GroupMembers m ON r.GroupMemberId = m.GroupMemberId";
            SqlCommand command = new SqlCommand(query, this.connection);

            this.connection.Open();
=======
                SELECT r.RequestId, r.GroupMemberId, r.GroupId, m.UserName 
                FROM Requests r
                JOIN GroupMembers m ON r.GroupMemberId = m.GroupMemberId";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Request request = new Request(
                        id: reader.GetGuid(0),
                        groupMemberId: reader.GetGuid(1),
                        groupMemberName: reader.GetString(3),
                        groupId: reader.GetGuid(2));
<<<<<<< HEAD
                    this.requests.Add(request);
                }
            }
            this.connection.Close();
=======
                    requests.Add(request);
                }
            }
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public Request GetRequestById(Guid requestId)
        {
<<<<<<< HEAD
            Request request = this.requests.FirstOrDefault(r => r.Id == requestId);
=======
            Request request = requests.FirstOrDefault(r => r.Id == requestId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public List<Request> GetAllRequests()
        {
<<<<<<< HEAD
            return this.requests;
=======
            return requests;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void AddRequest(Request request)
        {
            string insertRequestQuery = @"INSERT INTO Requests (RequestId, GroupMemberId, GroupId) 
                        VALUES (@RequestId, @GroupMemberId, @GroupId)";

<<<<<<< HEAD
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@RequestId", request.Id);
            command.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            command.Parameters.AddWithValue("@GroupId", request.GroupId);

            this.connection.Open();
            command.ExecuteNonQuery();
            this.connection.Close();

            this.requests.Add(request);
=======
            SqlCommand insertRequestCommand = new SqlCommand(insertRequestQuery, connection);
            insertRequestCommand.Parameters.AddWithValue("@RequestId", request.Id);
            insertRequestCommand.Parameters.AddWithValue("@GroupMemberId", request.GroupMemberId);
            insertRequestCommand.Parameters.AddWithValue("@GroupId", request.GroupId);

            connection.Open();
            insertRequestCommand.ExecuteNonQuery();
            connection.Close();

            requests.Add(request);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void UpdateRequest(Request request)
        {
<<<<<<< HEAD
            Request existingRequest = this.requests.FirstOrDefault(r => r.Id == request.Id);
=======
            Request existingRequest = requests.FirstOrDefault(r => r.Id == request.Id);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (existingRequest == null)
            {
                throw new Exception("Request not found");
            }
<<<<<<< HEAD
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
=======
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
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (affectedRows == 0)
            {
                throw new Exception("No request was updated; the request may not exist.");
            }
        }

        public void RemoveRequestById(Guid requestId)
        {
<<<<<<< HEAD
            Request request = this.requests.FirstOrDefault(r => r.Id == requestId);
=======
            Request request = requests.FirstOrDefault(r => r.Id == requestId);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (request == null)
            {
                throw new Exception("Request not found");
            }
<<<<<<< HEAD
            this.requests.Remove(request);

            string query = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@RequestId", requestId);

            this.connection.Open();
            int affectedRows = command.ExecuteNonQuery();
            this.connection.Close();
=======
            requests.Remove(request);

            string deleteRequestQuery = "DELETE FROM Requests WHERE RequestId = @RequestId";
            SqlCommand deleteRequestCommand = new SqlCommand(deleteRequestQuery, connection);
            deleteRequestCommand.Parameters.AddWithValue("@RequestId", requestId);

            connection.Open();
            int affectedRows = deleteRequestCommand.ExecuteNonQuery();
            connection.Close();
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
            if (affectedRows == 0)
            {
                throw new Exception("No request was deleted; the request may not exist.");
            }
        }
    }
}
