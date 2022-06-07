using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            if (dbUser == null)
                throw new DatabaseValidationsException("User not found");

            _mapper.Map(request, dbUser);

            var row = await _userRepository.UpdateAsync(dbUser);

            //Check if email changed

            return dbUser.Id;
        }
    }
}
