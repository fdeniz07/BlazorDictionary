using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            User dbUser = null;

            if (request.UserId != Guid.Empty)
                dbUser = await _userRepository.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                dbUser = await _userRepository.GetSingleAsync(i => i.UserName == request.UserName);

            //TODO if both are empty, throw new exception  //Hem kullanici adi hem de id eksik ise ona göre bir validasyon olusturulabilir


            return _mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}
