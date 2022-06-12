using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.Infrastructure;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:DictionaryConstants.VoteExchangeName,
                exchangeType:DictionaryConstants.DefaultExchangeType,
                queueName:DictionaryConstants.DeleteEntryCommentVoteQueueName,
                obj:new DeleteEntryCommentVoteEvent()
                {
                    EntryCommendId = request.EntryCommentId,
                    CreatedBy = request.UserId
                });
            return await Task.FromResult(true);
        }
    }
}
