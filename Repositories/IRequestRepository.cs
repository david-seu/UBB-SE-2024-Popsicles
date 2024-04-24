using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Popsicles.Models;

namespace UBB_SE_2024_Popsicles.Repositories
{
    internal interface IRequestRepository
    {
        Request GetRequestById(Guid requestId);
        List<Request> GetAllRequests();
        void AddRequest(Request request);
        void UpdateRequest(Request request);
        void RemoveRequestById(Guid requestId);
    }
}
