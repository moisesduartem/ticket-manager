using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.DTOs.Auth;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Requests;
using TicketManager.Api.Core.Services.Authentication;
using TicketManager.Api.Core.Utilities;

namespace TicketManager.Api.Core.Handlers
{
    public class SignInRequestHandler : IRequestHandler<SignInRequest, Result<SignInDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationTokenGenerator _tokenService;
        private readonly ILogger<SignInRequestHandler> _logger;
        private readonly IBcrypt _bcrypt;
        private readonly IMapper _mapper;

        public SignInRequestHandler(ILogger<SignInRequestHandler> logger, IUserRepository userRepository, IAuthenticationTokenGenerator tokenService, IBcrypt bcrypt, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _bcrypt = bcrypt;
            _mapper = mapper;
        }

        public async Task<Result<SignInDTO>> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Email"] = request.Email,
            }))
            {
                _logger.LogInformation("Searching user with Email={UserEmail}", request.Email);
                var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

                _logger.LogInformation("Validating user with Email={UserEmail}", request.Email);
                if (user == null || !HasValidPassword(user, request.Password))
                {
                    return Result.Fail("Invalid email and/or password.");
                }

                _logger.LogInformation("Generating user auth token for user with Id={UserId}", user.Id);
                string token = _tokenService.GenerateFor(user);

                _logger.LogInformation("Mapping result for user with Id={UserId}", user.Id);
                var userDto = _mapper.Map<AuthUserDTO>(user);

                return Result.Ok(new SignInDTO { Token = token, User = userDto });
            }
        }

        private bool HasValidPassword(User user, string password)
        {
            string expected = password + user.Salt;
            return _bcrypt.Verify(expected, user.Hash);
        }
    }
}
