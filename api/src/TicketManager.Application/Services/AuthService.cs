using AutoMapper;
using OperationResult;
using TicketManager.Application.Exceptions;
using TicketManager.Application.Utilities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Domain.Services;
using TicketManager.Shared.DTOs.UserAccess;

namespace TicketManager.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenService _tokenService;
        private readonly IBcrypt _bcrypt;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IAuthTokenService tokenService, IBcrypt bcrypt, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _bcrypt = bcrypt;
            _mapper = mapper;
        }

        public async Task<Result<SignInViewModel>> SignInAsync(SignInCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user == null || !HasValidPassword(user, command.Password))
            {
                return Result.Error<SignInViewModel>(new BadRequestException("Invalid email and/or password."));
            }

            string token = _tokenService.GenerateFor(user);

            var userDto = _mapper.Map<AuthUserViewModel>(user);

            return Result.Success(new SignInViewModel { Token = token, User = userDto });
        }

        private bool HasValidPassword(User user, string password)
        {
            string expected = password + user.Salt;
            return _bcrypt.Verify(expected, user.Hash);
        }
    }
}
