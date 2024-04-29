using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal interface IJoinRequestRepository
    {
        JoinRequest GetJoinRequestById(Guid joinRequestId);
        List<JoinRequest> GetAllJoinRequests();
        void AddJoinRequest(JoinRequest joinRequest);
        void UpdateJoinRequest(JoinRequest joinRequest);
        void RemoveJoinRequestById(Guid joinRequestId);
    }
}
