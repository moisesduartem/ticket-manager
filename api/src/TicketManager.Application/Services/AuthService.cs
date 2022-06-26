using AutoMapper;
using Microsoft.Extensions.Logging;
using OperationResult;
using TicketManager.Application.Exceptions;
using TicketManager.Application.Utilities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Domain.Services;
using TicketManager.Shared.DTOs.Auth;

namespace TicketManager.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenService _tokenService;
        private readonly ILogger<AuthService> _logger;
        private readonly IBcrypt _bcrypt;
        private readonly IMapper _mapper;

        public AuthService(ILogger<AuthService> logger, IUserRepository userRepository, IAuthTokenService tokenService, IBcrypt bcrypt, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _bcrypt = bcrypt;
            _mapper = mapper;
        }

        public async Task<Result<SignInViewModel>> SignInAsync(SignInRequest request)
        {

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Email"] = request.Email,
            }))
            {
                _logger.LogInformation("Retrieving the user by its email");
                var user = await _userRepository.GetByEmailAsync(request.Email);

                _logger.LogInformation("Validating user");
                if (user == null || !HasValidPassword(user, request.Password))
                {
                    return Result.Error<SignInViewModel>(new BadRequestException("Invalid email and/or password."));
                }

                _logger.LogInformation("Generating user auth token");
                string token = _tokenService.GenerateFor(user);

                _logger.LogInformation("Mapping the result");
                var userDto = _mapper.Map<AuthUserViewModel>(user);

                return Result.Success(new SignInViewModel { Token = token, User = userDto });
            }
        }

        private bool HasValidPassword(User user, string password)
        {
            string expected = password + user.Salt;
            return _bcrypt.Verify(expected, user.Hash);
        }
    }
}
