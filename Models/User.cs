using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class User
    {
        public Guid Id
        {
            get;
        }

        public string Username
        {
            get;
        }

        public string Password
        {
            get;
        }

        public string Email
        {
            get;
        }

        public string Phone
        {
            get;
        }

        public string Description
        {
            get; set;
        }

        public List<Request> OutgoingRequests
        {
            get; set;
        }

        public User(Guid id, string username, string password, string email, string phone, string description)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Description = description;

            OutgoingRequests = new List<Request>();
        }

        public Request GetOutgoingRequest(Guid requestId)
        {
            Request request = OutgoingRequests.First(request => request.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            return request;
        }

        public void AddOutgoingRequest(Request request)
        {
            OutgoingRequests.Add(request);
        }

        public void RemoveOutgoingRequest(Guid requestId)
        {
            Request request = OutgoingRequests.First(request => request.Id == requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            OutgoingRequests.Remove(request);
        }

        public bool Login(string username, string password)
        {
            if (username == Username && password == Password)
            {
                return true;
            }
            return false;
        }

        public void Logout()
        {
            return;
        }
    }
}
