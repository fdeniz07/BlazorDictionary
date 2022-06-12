using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.CreateEntryFavQueueName,
                obj: new CreateEntryFavEvent()
                {
                    EntryId = request.EntryId.Value,
                    CreatedBy = request.UserId.Value
                });

            return await Task.FromResult(true);
        }
    }
}
