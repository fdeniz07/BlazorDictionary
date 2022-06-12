using MediatR;

namespace BlazorDictionary.Common.Events.EntryComment
{
    public class DeleteEntryCommentFavEvent:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
