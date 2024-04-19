using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class Poll
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid GroupId { get; }
        private List<string> Tags { get; set; }
        public string SpecificToRole { get; set; }
        public bool IsPinned { get; set; }
        public string Description { get; set; }
        public List<string> Options { get; set; }
        public bool ResultsVisible { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsLive { get; set; }
        public DateTime EndTime { get; set; }
        public List<Vote> Votes { get; set; }

        public Poll(Guid id, Guid ownerId, string description, Guid groupId)
        {
            GroupId = groupId;
            Id = id;
            OwnerId = ownerId;
            Description = description;

            ResultsVisible = true;
            IsMultipleChoice = true;
            IsAnonymous = false;
            IsLive = true;

            Tags = new List<string>();
            Options = new List<string>();
            Votes = new List<Vote>();
            SpecificToRole = "";
        }

        public void AddOption(string option)
        {
            Options.Add(option);
        }

        public void RemoveOption(string option)
        {
            Options.Remove(option);
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            Tags.Remove(tag);
        }

        public Vote GetVote(Guid voteId)
        {
            Vote vote = Votes.First(v => v.Id == voteId);
            if (vote == null)
            {
                throw new Exception("Vote not found");
            }
            return vote;
        }

        public void AddVote(Vote vote)
        {
            Votes.Add(vote);
        }

        public void RemoveVote(Guid voteId)
        {
            Vote vote = Votes.First(v => v.Id == voteId);
            if (vote == null)
            {
                throw new Exception("Vote not found");
            }
            Votes.Remove(vote);
        }
    }
}
