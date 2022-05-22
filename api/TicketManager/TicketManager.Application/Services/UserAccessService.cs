using OperationResult;
using TicketManager.Application.Exceptions;
using TicketManager.Domain.Repositories;
using TicketManager.Domain.Services;
using TicketManager.Shared.DTOs.UserAccess;

namespace TicketManager.Application.Services
{
    public class UserAccessService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenService _tokenService;

        public UserAccessService(IUserRepository userRepository, IAuthTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<SignInViewModel>> SignInAsync(SignInCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user == null)
            {
                return Result.Error<SignInViewModel>(new NotFoundException("User not found"));
            }

            string token = _tokenService.GenerateFor(user);

            return Result.Success(new SignInViewModel { Token = token });
        }
    }
}
