using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class User
    {
        public Guid UserId
        {
            get;
        }

        public string UserName
        {
            get;
        }

        public string UserPassword
        {
            get;
        }

        public string UserEmailAdress
        {
            get;
        }

        public string UserPhoneNumber
        {
            get;
        }

        public string UserDescription
        {
            get; set;
        }

        public List<JoinRequest> ActiveJoinRequests
        {
            get; set;
        }

        public User(Guid userId, string userName, string userPassword, string userEmailAdress, string userPhoneNumber, string userDescription)
        {
            UserId = userId;
            UserName = userName;
            UserPassword = userPassword;
            UserEmailAdress = userEmailAdress;
            UserPhoneNumber = userPhoneNumber;
            UserDescription = userDescription;

            ActiveJoinRequests = new List<JoinRequest>();
        }

        public JoinRequest GetActiveJoinRequest(Guid joinRequestId)
        {
            JoinRequest joinRequest = ActiveJoinRequests.First(joinRequest => joinRequest.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            return joinRequest;
        }

        public void AddActiveJoinRequest(JoinRequest joinRequest)
        {
            ActiveJoinRequests.Add(joinRequest);
        }

        public void RemoveActiveJoinRequest(Guid joinRequestId)
        {
            JoinRequest joinRequest = ActiveJoinRequests.First(joinRequest => joinRequest.JoinRequestId == joinRequestId);
            if (joinRequest == null)
            {
                throw new Exception("JoinRequest not found");
            }
            ActiveJoinRequests.Remove(joinRequest);
        }

        public bool Login(string username, string password)
        {
            if (username == UserName && password == UserPassword)
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
