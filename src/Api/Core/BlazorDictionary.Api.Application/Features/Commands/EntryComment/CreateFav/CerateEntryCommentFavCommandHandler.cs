using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.Infrastructure;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CerateEntryCommentFavCommandHandler : IRequestHandler<CerateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CerateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavExchangeName,exchangeType:DictionaryConstants.DefaultExchangeType,queueName:DictionaryConstants.CreateEntryCommentFavQueueName,obj:new CreateEntryCommentFavEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId
            });

            return await Task.FromResult(true);
        }
    }
}
