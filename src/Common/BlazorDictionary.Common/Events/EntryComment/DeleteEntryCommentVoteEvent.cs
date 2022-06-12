using MediatR;

namespace BlazorDictionary.Common.Events.EntryComment
{
    public class DeleteEntryCommentVoteEvent:IRequest<bool>
    {
        public Guid EntryCommendId { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
