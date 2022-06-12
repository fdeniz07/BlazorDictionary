using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommand:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }

        public Guid UserId { get; set; }

        public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
