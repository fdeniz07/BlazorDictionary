using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorDictionary.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (dbUser == null)
                throw new DatabaseValidationsException("User not found!");


            var pass = PasswordEncryptor.Encrypt(request.Password); // Disaridan gelen sifreyi, kriptoluyoruz
            if (dbUser.Password != pass)
                throw new DatabaseValidationsException("Password is wrong!"); //Kriptoladigimiz sifre ile Db'deki kriptolu sifreyi karsilastiriyoruz

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationsException("Email address isn't confirmed yet!");

            var result = _mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.EmailAddress),
                new Claim(ClaimTypes.Name, dbUser.UserName),
                new Claim(ClaimTypes.GivenName, dbUser.FirstName),
                new Claim(ClaimTypes.Surname, dbUser.LastName)
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
