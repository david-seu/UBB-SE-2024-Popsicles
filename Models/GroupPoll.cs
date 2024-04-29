using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Popsicles.Models
{
    public class GroupPoll
    {
        public Guid PollId { get; }
        public Guid PoolOwnerId { get; }
        public Guid GroupId { get; }
        private List<string> PollTags { get; set; }
        public string SpecificToRole { get; set; }
        public bool IsGroupPollPinnedAsImportant { get; set; }
        public string GroupPollDescription { get; set; }
        public List<string> GroupPollOptions { get; set; }
        public bool AreGroupPollResultsVisibleToGroup { get; set; }
        public bool AreMultipleChoicesAllowedForPoll { get; set; }
        public bool AreGroupPollAnswersAnonymous { get; set; }
        public bool IsGroupPollActive { get; set; }
        public DateTime GroupPollEndTime { get; set; }
        public List<GroupPollVote> ListOfGroupPollVotes { get; set; }

        public GroupPoll(Guid pollId, Guid poolOwnerId, string groupPollDescription, Guid groupId)
        {
            GroupId = groupId;
            PollId = pollId;
            PoolOwnerId = poolOwnerId;
            GroupPollDescription = groupPollDescription;

            AreGroupPollResultsVisibleToGroup = true;
            AreMultipleChoicesAllowedForPoll = true;
            AreGroupPollAnswersAnonymous = false;
            IsGroupPollActive = true;

            PollTags = new List<string>();
            GroupPollOptions = new List<string>();
            ListOfGroupPollVotes = new List<GroupPollVote>();
            SpecificToRole = string.Empty;
        }

        public void AddGroupPollOption(string groupPollOption)
        {
            GroupPollOptions.Add(groupPollOption);
        }

        public void RemoveGroupPollOption(string groupPollOption)
        {
            GroupPollOptions.Remove(groupPollOption);
        }

        public void AddPollTag(string pollTag)
        {
            PollTags.Add(pollTag);
        }

        public void RemovePollTag(string pollTag)
        {
            PollTags.Remove(pollTag);
        }

        public GroupPollVote GetVote(Guid pollVoteId)
        {
            GroupPollVote groupPollVote = ListOfGroupPollVotes.First(pollVote => pollVote.VoteId == pollVoteId);
            if (groupPollVote == null)
            {
                throw new Exception("GroupPollVote not found");
            }
            return groupPollVote;
        }

        public void AddPollVote(GroupPollVote groupPollVote)
        {
            ListOfGroupPollVotes.Add(groupPollVote);
        }

        public void RemovePollVote(Guid pollVoteId)
        {
            GroupPollVote groupPollVote = ListOfGroupPollVotes.First(pollVote => pollVote.VoteId == pollVoteId);
            if (groupPollVote == null)
            {
                throw new Exception("GroupPollVote not found");
            }
            ListOfGroupPollVotes.Remove(groupPollVote);
        }
    }
}
