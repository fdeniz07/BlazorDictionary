using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CerateEntryCommentFavCommand : IRequest<bool>
    {
    
        public Guid EntryCommentId { get; set; }

        public Guid UserId { get; set; }  
        
        
        public CerateEntryCommentFavCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
