using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.User;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (existUser != null)
                throw new DatabaseValidationException("User already exists!");

            var dbUser = _mapper.Map<Domain.Models.User>(request);

            var rows = await _userRepository.AddAsync(dbUser);

            //Email Changed/Created

            if (rows > 0)
            {
                var @event = new UserEmailChangeEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress,
                };

                QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.UserExchangeName,
                                                    exchangeType: DictionaryConstants.DefaultExchangeType,
                                                    queueName: DictionaryConstants.UserEmailChangedQueueName,
                                                    obj: @event);
            }

            return dbUser.Id;
        }
    }
}
