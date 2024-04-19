using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class Vote
    {
        public Guid Id { get; }
        public Guid GroupMemberId { get; }
        public Guid PollId { get; }
        public List<string> Choices { get; }

        public Vote(Guid id, Guid groupMemberId, Guid pollId)
        {
            Id = id;
            GroupMemberId = groupMemberId;
            PollId = pollId;

            Choices = new List<string>();
        }

        public void AddChoice(string choice)
        {
            Choices.Add(choice);
        }

        public void RemoveChoice(string choice)
        {
            Choices.Remove(choice);
        }
    }
}
