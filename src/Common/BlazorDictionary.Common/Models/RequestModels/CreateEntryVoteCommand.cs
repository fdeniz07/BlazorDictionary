using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels
{
    public class CreateEntryVoteCommand:IRequest<bool>
    {
        public Guid EntryId { get; set; }

        public VoteType VoteType { get; set; }

        public Guid CreatedBy { get; set; }

        public CreateEntryVoteCommand(Guid entryId, VoteType voteType, Guid createdBy)
        {
            EntryId = entryId;
            VoteType = voteType;
            CreatedBy = createdBy;
        }
    }
}
