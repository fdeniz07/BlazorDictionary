using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:DictionaryConstants.FavExchangeName,
                exchangeType:DictionaryConstants.DefaultExchangeType,
                queueName:DictionaryConstants.DeleteEntryFavQueueName,
                obj: new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
