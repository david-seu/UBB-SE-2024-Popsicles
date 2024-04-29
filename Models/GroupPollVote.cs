using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupPollVote
    {
        public Guid VoteId { get; }
        public Guid GroupMemberId { get; }
        public Guid PollId { get; }
        public List<string> SelectedOptionsFromPoll { get; }

        public GroupPollVote(Guid voteId, Guid groupMemberId, Guid pollId)
        {
            VoteId = voteId;
            GroupMemberId = groupMemberId;
            PollId = pollId;

            SelectedOptionsFromPoll = new List<string>();
        }

        public void AddSelectedOptionFromPoll(string selectedOptionFromPoll)
        {
            SelectedOptionsFromPoll.Add(selectedOptionFromPoll);
        }

        public void RemoveSelectedOptionFromPoll(string selectedOptionFromPoll)
        {
            SelectedOptionsFromPoll.Remove(selectedOptionFromPoll);
        }
    }
}
