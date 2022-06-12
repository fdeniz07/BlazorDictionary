using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommand:IRequest<bool>
    {
        public Guid EntryId { get; set; }

        public Guid UserId { get; set; }

        public DeleteEntryVoteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
