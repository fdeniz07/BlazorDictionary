using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
           QueueFactory.SendMessageToExchange(exchangeName:DictionaryConstants.VoteExchangeName,
               exchangeType: DictionaryConstants.DefaultExchangeType,
               queueName:DictionaryConstants.DeleteEntryVoteQueueName,
               obj: new DeleteEntryVoteEvent()
               {
                   EntryId = request.EntryId,
                   CreatedBy = request.UserId
               });

           return await Task.FromResult(true);
        }
    }
}
