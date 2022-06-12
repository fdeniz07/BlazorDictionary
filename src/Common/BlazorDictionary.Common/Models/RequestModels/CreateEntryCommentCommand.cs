using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels
{
    public class CreateEntryCommentCommand:IRequest<Guid>
    {
        public Guid? EntryId { get; set; }
        
        public string Context { get; set; }

        public Guid? CreatedById { get; set; }

        public CreateEntryCommentCommand()
        {
            
        }

        public CreateEntryCommentCommand(Guid entryId, string context, Guid createdById)
        {
            EntryId = entryId;
            Context = context;
            CreatedById = createdById;
        }
    }
}
