using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreatedEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async  Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:DictionaryConstants.VoteExchangeName,
                exchangeType:DictionaryConstants.DefaultExchangeType,
                queueName:DictionaryConstants.CreateEntryCommentVoteQueueName,
                obj: new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType
                });

            return await Task.FromResult(true);
        }
    }
}
