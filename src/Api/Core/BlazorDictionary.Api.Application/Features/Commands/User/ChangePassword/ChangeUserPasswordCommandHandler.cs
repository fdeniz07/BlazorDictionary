using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Events.User;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentException(nameof(request.UserId));


            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);


            if (dbUser is null)
                throw new DatabaseValidationsException("User not found");


            var encryptedPassword = PasswordEncryptor.Encrypt(request.OldPassword);
            if (dbUser.Password != encryptedPassword)
                throw new DatabaseValidationsException("Old password wrong!");


            dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);

            await _userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
